using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Transaction
    {
        private int _id;
        private int _userid;
        private double _amount;
        private int _currid;
        private DateTime _date;
        private string _category; // optional category field
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int Currid
        {
            get { return _currid; }
            set { _currid = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Category
        {
            get { return _category; }
            set { _category = value; }

            // Optional category for the transaction (may be null/empty if not stored in DB)
        }
    }
}
