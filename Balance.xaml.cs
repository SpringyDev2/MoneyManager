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

namespace MoneyManager
{
    /// <summary>
    /// Interaction logic for Balance.xaml
    /// </summary>
    public partial class Balance : Page
    {
        public Balance()
        {
            InitializeComponent();
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
