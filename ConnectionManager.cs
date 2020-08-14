﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using NTCOM_WPF.Properties;
using System.Net.Sockets;
using System.CodeDom;
using System.Net;

namespace NTCOM_WPF
{

    public delegate void RecvMsgEventHandler(RecvMsgEvent e);

    public class RecvMsgEvent
    {
        public string msg;
        public RecvMsgEvent( string cmsg)
        {
            msg = cmsg;
        }
    }

    public delegate void ConnectionChangedHandler(ConnectionChangedEvent e);

    public class ConnectionChangedEvent
    {
        public string desc;
        public ConnectionChangedEvent(string newDesc)
        {
            desc = newDesc;
        }
    }


    class ConnectionManager : IDisposable
    {
        private bool _isRunning = true;

        private SerialPort serial_port;
        private UdpClient udp_client;

        static bool autoconnecting = false;

        public event RecvMsgEventHandler OnRecvMsg;
        public event ConnectionChangedHandler OnConnectionChanged;

        public ConnectionManager()
        {
            serial_port = new SerialPort();
            serial_port.Parity = Parity.None;
            serial_port.DataBits = 8;
            serial_port.StopBits = StopBits.One;
            serial_port.NewLine = "\r";
            serial_port.ReadTimeout = 2000;
            serial_port.WriteTimeout = 1500;
            serial_port.DataReceived += new SerialDataReceivedEventHandler(OnSerialPortRecv);

            udp_client = new UdpClient();
            udp_client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp_client.ExclusiveAddressUse = false;
            udp_client.Client.Bind(new IPEndPoint(IPAddress.Any, 1470));

            var from = new IPEndPoint(0, 0);
            Task.Run(() =>
            {
                while (true)
                {
                    var recvBuffer = udp_client.Receive(ref from);
                    OnRecvMsg(new RecvMsgEvent(Encoding.ASCII.GetString(recvBuffer)));
                }
            });

            new Thread(TickThread).Start();
        }

        private static void OnSerialPortRecv(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Serial port recv:");
            Console.Write(indata);
        }

        public void send(string msg)
        {
            if (Convert.ToBoolean(Settings.Default["isUdp"]))
            {
                byte[] data = Encoding.ASCII.GetBytes(msg);
                udp_client.Send(data, data.Length);
            }
            else
            {
                serial_port.Write(msg);
                serial_port.Write(new byte[] { 13 }, 0, 1);
            }
        }

        static bool IsSocketConnected(Socket s)
        {
            try {
                return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
            }
            catch (System.ObjectDisposedException e) {
                return false;
            }
        }

        public void TickThread() {
            while (_isRunning) {
                tick();
                Thread.Sleep(500);
            }
        }

        public void tick()
        {
            if (Convert.ToBoolean(Settings.Default["isUdp"]))
            {
                if (serial_port.IsOpen) {
                    serial_port.Close();
                }
            }
            else  
            {
                // if (IsSocketConnected(udp_socket.socket)) {

                    //udp_socket.Disconnect();
                //}
                if (!serial_port.IsOpen && Convert.ToBoolean(Settings.Default["isConnecting"]))
                {
                    OnConnectionChanged( new ConnectionChangedEvent(Convert.ToBoolean(Settings.Default["isConnecting"]) ? "COM port closed - trying to reconnect" : "COM port closed"));
                    new Thread(serialAutoConnect).Start();
                }
                else if (serial_port.IsOpen && !Convert.ToBoolean(Settings.Default["isConnecting"])) {
                    OnConnectionChanged( new ConnectionChangedEvent("COM port closed"));
                    serial_port.Close();
                }
            }
        }
        private void serialAutoConnect()
        {
            // static variable autoconnecting prevents multiple instance of this method running to prevent conflict
            if (serial_port.IsOpen || autoconnecting) { return; }
            autoconnecting = true;

            try
            {
                serial_port.BaudRate = Convert.ToInt32(Settings.Default["baudRate"]);
            }
            catch (Exception)
            {
                serial_port.BaudRate = Convert.ToInt32(Settings.Default.Properties["baudRate"].DefaultValue);
            }
            if (SerialPort.GetPortNames().Contains(Settings.Default["savedPort"]))
            {
                try
                {
                    serial_port.PortName = Convert.ToString(Settings.Default["savedPort"]);
                    serial_port.DiscardInBuffer();
                    serial_port.DiscardOutBuffer();
                    serial_port.Open();
                    autoconnecting = false;
                    OnConnectionChanged( new ConnectionChangedEvent("COM port opened"));
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetType() + " " + e.Message);
                    if (serial_port.IsOpen) serial_port.Close();
                }
            }
            autoconnecting = false;
        }

        public void Dispose()
        {
            _isRunning = false;
        }



    }
}
