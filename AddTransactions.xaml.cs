using DAL;
using MoneyManager.DAL;
using MoneyManager.Model;
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
    /// Interaction logic for AddTransactions.xaml
    /// </summary>
    public partial class AddTransactions : Page
    {
        MMDal dal;
        public AddTransactions()
        {
            dal = new MMDal();
            InitializeComponent();
            PopulateComboBox();
        }
        public void PopulateComboBox()
        {
            currencyComboBox.Items.Clear();
            List<Currency> list = dal.GetCurrencies();
            foreach(Currency currency in list)
            {
                var item = new ComboBoxItem
                {
                    Content = currency.CurrencyShort,
                    Tag = currency.Id
                };
                currencyComboBox.Items.Add(item);
            }
            currencyComboBox.SelectedIndex = 0;
        }

        private void ConfirmChangesBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                double amount = double.Parse(amountBox.Text);
                Dashboard dashboard = Window.GetWindow(this) as Dashboard;
                int userId = dal.GetUserId(dashboard.username, dashboard.password);
                int currencyId = currencyComboBox.SelectedIndex + 1;

                dal.AddTransaction(userId, amount, date, currencyId);
            }
            catch
            {
                MessageBox.Show("Please enter valid data");
            }

        }
    }
}
