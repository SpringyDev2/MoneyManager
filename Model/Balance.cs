using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Balance
    {
        private double _totalamount;
        private int _currid;
        public double TotalAmount
        {
            get { return _totalamount; }
            set { _totalamount = value; }
        }
        public int CurrId
        {
            get { return _currid; }
            set { _currid = value; }
        }
    }
}
