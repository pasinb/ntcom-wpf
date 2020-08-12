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
        }

        private void HandleOnRecv(RecvMsgEvent e) {
            Console.WriteLine("received handleOnRecv");
            Console.WriteLine(e.msg);
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
            stateGrid[2].data[2] = "shit";
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
