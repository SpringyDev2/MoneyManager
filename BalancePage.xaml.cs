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
    /// Interaction logic for Balance.xaml
    /// </summary>
    public partial class BalancePage : Page
    {
        public BalancePage()
        {
            InitializeComponent();
            Loaded += BalanceLoad;

        }
        public void BalanceLoad(object sender, RoutedEventArgs e)
        {
            MMDal dal = new MMDal();
            Dashboard dashboard = Window.GetWindow(this) as Dashboard;
            Balance balance = new Balance();
            balance.TotalAmount = dal.GetTotalAmount(dal.GetUserId(dashboard.username, dashboard.password));
            this.DataContext = balance;


        }
        private void AddIncomeBtn(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = Window.GetWindow(this) as Dashboard;
            dashboard.MainFrame.Navigate(new AddTransactions());
        }
        private void AddExpenseBtn(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = Window.GetWindow(this) as Dashboard;
            dashboard.MainFrame.Navigate(new AddTransactions());
        }

    }
}
