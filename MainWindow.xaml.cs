﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Threading;
using System.Text;
using System.Windows.Documents;

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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateText.Text = DateTime.Now.ToString("dd/MM/yyyy");
                for (int r = 0; r < 8; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        stateGrid[r].idleCount[c]++;
                        if (stateGrid[r].idleCount[c] > 10)
                        {
                            stateGrid[r].cellStatus[c] = 2;
                        }
                        //Console.WriteLine(stateGrid[r].idleCount[c]);
                    }
                };
            }, this.Dispatcher);

            connectionManager = new ConnectionManager();
            connectionManager.OnRecvMsg += new RecvMsgEventHandler(HandleOnRecv);
            connectionManager.OnConnectionChanged += new ConnectionChangedHandler(HandleConnectionChanged);

            stateGrid = new ObservableCollection<DataRow>();
            for (int i = 0; i < 8; i++)
            {
                stateGrid.Add(new DataRow()
                {
                    index = Convert.ToString(i + 1),
                    data = new ObservableCollection<string> { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                    cellStatus = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    idleCount = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                });
            }
            stateGrid.Add(new DataRow()
            {
                index = "Sum: ",
                data = new ObservableCollection<string> { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
                cellStatus = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                idleCount = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

            });
            stateGrid.Add(new DataRow()
            {
                index = "Total: ",
                data = new ObservableCollection<string> { "0", "", "", "", "", "", "", "", "", "" },
                cellStatus = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                idleCount = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            });
            mainGrid.ItemsSource = stateGrid;
        }

        private void HandleOnRecv(RecvMsgEvent e)
        {
            string m = e.msg;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                //logTextBox.AppendText("RECV: " + m + "\n");
            }));
            int ntIdx = m.IndexOf(":NT");
            if (ntIdx >= 0)
            {
                m = m.Substring(ntIdx).Trim();
                if (m.Length == 9)
                {
                    string addrStr = m.Substring(3, 2);
                    string countStr = m.Substring(5, 4);

                    bool validInt = int.TryParse(addrStr, out int addr);
                    if (validInt && addr > 0 && addr <= 80)
                    {
                        int rowIdx = (addr - 1) % 8;
                        int colIdx = (int)Math.Floor((double)((addr - 1) / 8));
                        if (countStr == "TEST")
                        {
                            stateGrid[rowIdx].cellStatus[colIdx] = 1;
                            stateGrid[rowIdx].idleCount[colIdx] = 0;
                            if (
                                  stateGrid[rowIdx].cellStatus[colIdx] == 2
                                  )
                            {
                                stateGrid[rowIdx].cellStatus[colIdx] = 0;
                            }
                        }
                        else
                        {
                            bool validInt2 = int.TryParse(countStr, out int countInt);
                            if (validInt2)
                            {
                                stateGrid[rowIdx].data[colIdx] = countInt.ToString();
                                stateGrid[rowIdx].idleCount[colIdx] = 0;
                                if (
                                    stateGrid[rowIdx].cellStatus[colIdx] == 2
                                    )
                                {
                                    stateGrid[rowIdx].cellStatus[colIdx] = 0;
                                }

                                // calculate sum
                                int[] sum = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                for (int r = 0; r < 8; r++)
                                {
                                    for (int c = 0; c < 10; c++)
                                    {
                                        bool valid = int.TryParse(stateGrid[r].data[c], out int v);
                                        if (valid)
                                        {
                                            sum[c] += Convert.ToInt32(v);
                                        }
                                    }
                                };
                                // Console.WriteLine(sum);
                                int totalSum = sum.Sum();
                                for (int c = 0; c < 10; c++)
                                {
                                    stateGrid[8].data[c] = sum[c].ToString();
                                }
                                stateGrid[9].data[0] = totalSum.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void HandleConnectionChanged(ConnectionChangedEvent e)
        {
            if (e.isSuccess)
            {
                statusLabel.Content = e.desc;
                if (e.isOpen)
                {
                    notifier.ShowSuccess("UDP listening");
                    statusLabel.Foreground = Brushes.Green;
                }
                else
                {
                    notifier.ShowSuccess("UDP stopped listening");
                    statusLabel.Foreground = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show(e.desc, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            connectionManager.Dispose();
            Properties.Settings.Default.Save();

            base.OnClosing(e);
        }

        public void save_to_csv()
        {
            String save_dir = Properties.Settings.Default.csvLocation;
            String save_filename = Properties.Settings.Default.csvFileName;

            String full_path;
            if (save_dir == "")
            {
                full_path = save_filename;
            }
            full_path = String.Format("{0}/{1}", save_dir, save_filename);
            //if file exist, clear
            if (File.Exists(full_path))
            {
                File.WriteAllText(full_path, "");
            }
            //add data to CSV file
            try
            {

                //using (StreamWriter sw = File.AppendText(full_path))
                using (StreamWriter sw = new StreamWriter(File.OpenWrite(full_path), new UTF8Encoding(false)))
                {
                    sw.WriteLine(String.Format(",{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                        Properties.Settings.Default.name1,
                        Properties.Settings.Default.name2,
                        Properties.Settings.Default.name3,
                        Properties.Settings.Default.name4,
                        Properties.Settings.Default.name5,
                        Properties.Settings.Default.name6,
                        Properties.Settings.Default.name7,
                        Properties.Settings.Default.name8,
                        Properties.Settings.Default.name9,
                        Properties.Settings.Default.name10));
                    for (int r = 0; r < 10; r++)
                    {
                        sw.WriteLine(
                           stateGrid[r].index + "," + string.Join(",", stateGrid[r].data)
                            );
                    };
                }
                notifier.ShowSuccess("CSV saved");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetType() + ": " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // InvalidOperationException occur when table was modified while program is in loop
                // Occurs when user set the Bridge count number, causing table rows to be modified
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                Properties.Settings.Default.csvLocation = dialog.SelectedPath;
            }
        }
        private void listenButton_Click(object sender, RoutedEventArgs e)
        {
            connectionManager.udpStart();
        }

        private void stopListenButton_Click(object sender, RoutedEventArgs e)
        {
            connectionManager.udpStop();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            save_to_csv();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    stateGrid[r].cellStatus[c] = 0;
                }
            };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var dialog = new PwdDialog();
            if (dialog.ShowDialog() == true)
            {
                new Thread(send_reset).Start();
            }

        }

        private void send_reset()
        {
            int attempt = 0;
            bool success = true;
            try
            {
                while (true)
                {
                    attempt++;
                    if (attempt > 1)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                notifier.ShowSuccess("Sending reset (attempt " + attempt + ")");
                            }));
                        }
                        catch { }

                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            connectionManager.send(":NT00RSET");
                        }));
                        Thread.Sleep(1000);
                    }

                    success = true;
                    for (int r = 0; r < 8; r++)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            if (stateGrid[r].data[c] != "0" && stateGrid[r].data[c] != "-")
                            {
                                success = false;
                            }
                        }
                    };
                    if (success)
                    {
                        break;
                    }
                }
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    notifier.ShowSuccess("Reset successful");
                }));

            }
            catch (Exception ee)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    MessageBox.Show(ee.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }));
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                //Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                // sizing of the element.

                FlowDocument fd = new FlowDocument();
                //fd.PagePadding = new Thickness(standardThickness);
                fd.ColumnGap = 0;
                fd.ColumnWidth = printDialog.PrintableAreaWidth;

                Paragraph p = new Paragraph(new Run(DateTime.Now.ToString("dd/MM/yyyy")));
                p.FontSize = 18;
                p.TextAlignment = TextAlignment.Right;
                fd.Blocks.Add(p);

                //Table table = new Table();
                //TableRowGroup tableRowGroup = new TableRowGroup();
                //TableRow r = new TableRow();
                //fd.PageWidth = printDialog.PrintableAreaWidth;
                //fd.PageHeight = printDialog.PrintableAreaHeight;
                //fd.BringIntoView();

                Table table = new Table();
                table.CellSpacing = 5;

                //Thickness myThickness = new Thickness();
                //myThickness.Bottom = 1;
                //myThickness.Left = 1;
                //myThickness.Right = 1;
                //myThickness.Top = 1;
                //table.BorderThickness = myThickness;
                //table.BorderBrush = Brushes.Gray;

                table.Background = Brushes.White;
                table.FontSize = 12;

                int numberOfColumns = 11;
                for (int x = 0; x < numberOfColumns; x++)
                {
                    TableColumn tc = new TableColumn();
                    tc.Width = (GridLength)new GridLengthConverter().ConvertFromString("60");
                    table.Columns.Add(tc);

                }

                table.RowGroups.Add(new TableRowGroup());

                table.RowGroups[0].Rows.Add(new TableRow());
                TableRow currentRow = table.RowGroups[0].Rows[0];
                currentRow = table.RowGroups[0].Rows[0];
                currentRow.FontWeight = FontWeights.Bold;

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name1))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name2))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name3))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name4))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name5))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name6))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name7))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name8))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name9))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Properties.Settings.Default.name10))));

                for (int r = 0; r < 10; r++)
                {
                    table.RowGroups[0].Rows.Add(new TableRow());
                    currentRow = table.RowGroups[0].Rows[r + 1];
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].index))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[0]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[1]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[2]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[3]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[4]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[5]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[6]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[7]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[8]))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(stateGrid[r].data[9]))));
                };

                fd.Blocks.Add(table);
                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
            }
        }

        private void Save_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isPasswordCorrect(oldPwdBox.Password))
            {
                MessageBox.Show("Old password incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pwdBox.Password != pwdBoxConfirm.Password)
            {
                MessageBox.Show("Password and confirm password does not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(pwdBox.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            Properties.Settings.Default.password = Convert.ToBase64String(hashBytes);
            Properties.Settings.Default.Save();
            oldPwdBox.Password = "";
            pwdBox.Password = "";
            pwdBoxConfirm.Password = "";
            notifier.ShowSuccess("Password set");
        }
        public bool isPasswordCorrect(string password)
        // DUPLICATE CODE IN MAINWINDOW + DIALOG
        {
            /* Fetch the stored value */
            string savedPasswordHash = Properties.Settings.Default.password;
            if (savedPasswordHash == "1234")
            {
                return password == "1234";
            }
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                mainTab.SelectedIndex = 4;
            }));
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                mainTab.SelectedIndex = 0;
            }));
        }

        private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (mainTab.SelectedIndex == 1)
            {
                Properties.Settings.Default.csvFileName = DateTime.Now.ToString("yyyyMMdd") + ".csv";
            }
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
            if (System.Convert.ToInt32(value) == 1)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff9999"));
            }
            else if (System.Convert.ToInt32(value) == 2)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#9999ff"));
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class IdleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToInt32(value) > 10)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#0000ff"));
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
        public string index { get; set; }
        public ObservableCollection<string> data { get; set; }
        // 1 = red, 2 = blue
        public ObservableCollection<int> cellStatus { get; set; }
        public ObservableCollection<int> idleCount { get; set; }

    }
}
