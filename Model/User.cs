using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class User
    {
       private int _id; 
       private string _username;
       private string _password;
       private Balance _balance; 
       public int Id
	     {
                get { return _id; }
	            set { _id = value; }
         }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public Balance Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
    }
}
