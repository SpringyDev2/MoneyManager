using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoneyManager.DAL;

namespace MoneyManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MMDal dal;
        public MainWindow()
        {
            InitializeComponent();
            dal = new MMDal();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = dal.Login(txtUsername.Text, txtPassword.Password);
            if (success)
            {
                LoginUserPasswordButtonPanel.Visibility = Visibility.Collapsed;
                LoginSuccessMsg.Visibility = Visibility.Visible;
                LoginFailMsg.Visibility = Visibility.Collapsed;
                Dashboard newWindow = new Dashboard(txtUsername.Text, txtPassword.Password);
                await Task.Delay(1000);
                newWindow.Show();
                this.Close();
            }
            else
            {
                LoginFailMsg.Visibility = Visibility.Visible;
            }

        }
    }
}
