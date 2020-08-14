using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NTCOM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>




    public partial class MainWindow : Window
    {

        private ConnectionManager connectionManager;

        public ObservableCollection<DataRow> stateGrid;
        public string log;

        public MainWindow()
        {
            InitializeComponent();

            connectionManager = new ConnectionManager();
            connectionManager.OnRecvMsg += new RecvMsgEventHandler(HandleOnRecv);
            connectionManager.OnConnectionChanged += new ConnectionChangedHandler(HandleConnectionChanged);

            stateGrid = new ObservableCollection<DataRow>();
            for (int i = 0; i < 8; i++)
            {
                stateGrid.Add(new DataRow() { 
                    index= i+1, 
                    data = new ObservableCollection<string> { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" }, 
                    isRed = new ObservableCollection<bool> { false,false,false,false,false,false,false,false,false,false } 
                });
            }
            mainGrid.ItemsSource = stateGrid;

            //log ="1111";/
        }

        private void HandleOnRecv(RecvMsgEvent e) {
            string m = e.msg;

            Application.Current.Dispatcher.Invoke(new Action(() => {
                logTextBox.AppendText("RECV: " + m + "\n");
            }));
             m = m.Substring(m.Length - 9);
            if (m.Length == 9 && m.StartsWith(":NT")) {
               string addrStr =  m.Substring(3, 2);
                string countStr=  m.Substring(5, 4);

                bool validInt = int.TryParse(m.Substring(3, 2), out int addr);
                if (validInt && addr > 0 && addr <= 80) {
                    if (countStr == "TEST")
                    {

                    }
                    else {

                        bool validInt2 = int.TryParse(m.Substring(5, 4), out int countInt);
                        if (validInt2) {
                            stateGrid[(int)addrStr[0]].data[addr % 8] = countInt.ToString();
                        }
                    }
                }
            }

            /// if ()

        }

        private void HandleConnectionChanged(ConnectionChangedEvent e)
        {
            Console.WriteLine("received HandleConnectionChanged");
            Console.WriteLine(e.desc);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            connectionManager.Dispose();
            Properties.Settings.Default.Save();

            base.OnClosing(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                Properties.Settings.Default.csvLocation = dialog.SelectedPath;
            }
        }

        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
               // mainGrid.Items.Refresh();
                //mainGrid.UpdateLayout();
               // mainGrid.upd
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // TODO remove
        }
    }

    public class RadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == parameter.ToString())
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(parameter);
        }
    }

    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value)) {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff9999"));
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DataRow
    {
        public int index { get; set; }
        public ObservableCollection<string> data { get; set; }
        public ObservableCollection<bool> isRed { get; set; }

    }
}
