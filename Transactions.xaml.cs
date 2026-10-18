using Model;
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

namespace MoneyManager
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        public Transactions()
        {
            InitializeComponent();
            Loaded += Transactions_Loaded;
        }

        private void Transactions_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            MMDal dal = new MMDal();

            Dashboard dashboard = Window.GetWindow(this) as Dashboard;

            if (dashboard == null)
                return;

            int userId = dal.GetUserId(dashboard.username, dashboard.password);

            TransactionsGrid.ItemsSource = dal.GetTransactions(userId);
        }


    }
}
