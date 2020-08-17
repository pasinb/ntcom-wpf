using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NTCOM_WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PwdDialog : Window
    {
        public PwdDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isPasswordCorrect(pwdBox.Password))
            {
                MessageBox.Show("Incorrect password", "" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                DialogResult = true;
            }
        }
        public bool isPasswordCorrect(string password)
        // DUPLICATE CODE IN MAINWINDOW + DIALOG
        {
            /* Fetch the stored value */
            string savedPasswordHash = Properties.Settings.Default.password;
            if (savedPasswordHash == "1234" )
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
    }
}
