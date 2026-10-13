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
using System.Windows.Shapes;

namespace MoneyManager
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard(string username,string password)
        {
            InitializeComponent();
            MainFrame.Navigate(new Balance());
            this.username = username;
            this.password = password;
        }
        public string username { get; set; }
        public string password { get; set; }

        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Balance());
        }

        private void Profile_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Profile());
        }
        private void Teachers_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Actions page (UI only)
            MainFrame.Navigate(new ActionsPage());
        }

        private void LogoutBtnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void Transactions_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Transactions());
        }
    }
}
