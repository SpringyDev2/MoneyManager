using MoneyManager.DAL;
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
using Model;

namespace MoneyManager
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            Loaded += LoadData;
        }
        public void LoadData(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = Window.GetWindow(this) as Dashboard;
            User user = new User();
            user.Username = dashboard.username;
            user.Password = dashboard.password;
            this.DataContext = user;
        }

        private void ConfirmChangesBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MMDal dal = new MMDal();
                Dashboard dashboard = Window.GetWindow(this) as Dashboard;
                dal.UpdateUser(dal.GetUserId(dashboard.username, dashboard.password), UsernameBox.Text, PasswordBox.Text);
                dashboard.username = UsernameBox.Text;
                dashboard.password = PasswordBox.Text;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
    }
}
