using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Model
{
    internal class Currency
    {
        private int id;
        private string currencyShort;
        private string currencyName;

        public int Id { get { return id; } set { id = value; } }
        public string CurrencyShort { get { return currencyShort; } set { currencyShort = value; } }
        public string CurrencyName { get { return currencyName; } set { currencyName = value; } }

        public Currency(int id, string currencyShort, string currencyName)
        {
            Id = id;
            CurrencyShort = currencyShort;
            CurrencyName = currencyName;
        }
        public Currency(int id, string currencyShort)
        {
            Id = id;
            CurrencyShort = currencyShort;
        }
    }
}
