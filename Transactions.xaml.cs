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

            //List<Transaction> transactions = new List<Transaction>
            //{
            //     new Transaction() { Date = DateTime.Now, Currid = 1, Amount = 5000},
            //     new Transaction() { Date = DateTime.Now, Currid = 1, Amount = 5000},
            //     new Transaction() { Date = DateTime.Now, Currid = 1, Amount = 5000}
            //};
            //TransactionsGrid.DataContext = transactions;
            //  List<Transaction> transactions = DAL.GetTransaction(23232);

            TransactionsGrid.ItemsSource = new MMDal().GetTransactions(1);

        }


    }
}
