using DAL;
using Microsoft.Data.SqlClient;
using Model;
using MoneyManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.DAL
{
    internal class MMDal : BaseDal
    {
        public bool Login(string username, string password)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password");

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            return ExecuteSelectIntQuery(cmd) > 0;
        }

        /// <summary>
        /// Adds a transaction record to the Transactions table.
        /// Assumes a Transactions table with columns: Id (IDENTITY), UserId (int), Amount (float), Currid (int), Date (datetime)
        /// </summary>
        /// <param name="userId">Id of the user who made the transaction</param>
        /// <param name="amount">Transaction amount (positive income, negative expense or use Type elsewhere)</param>
        /// <param name="CurrencyId">Currency id (optional)</param>
        /// <param name="date">Transaction date/time</param>
        /// <returns>true when insert affected a row</returns>
        private double CurrencyConversion(int currencyId1,int currencyId2, double amount1,double amount2)
        {
            double[] Val = {1, 3.06, 3.43, 4.05};
            return amount2 * Val[currencyId1-1];
        }
        public void AddTransaction(int userId, double amount, DateTime date, int CurrencyId = 1)
        {
            SqlCommand cmd = new SqlCommand(
            @"INSERT INTO [dbo].[Transaction]
              (UserId, Amount, CurrencyId, [Date])
              VALUES (@UserId, @Amount, @CurrencyId, @Date)");

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
            cmd.Parameters.AddWithValue("@Date", date);

            ExecuteInsertQuery(cmd);

            SqlCommand cmd2 = new SqlCommand("UPDATE Users SET TotalAmount=@totalAmount WHERE Id = @userId");
            cmd2.Parameters.AddWithValue("@totalAmount", CurrencyConversion(GetUserCurrencyId(userId),CurrencyId,GetTotalAmount(userId),amount));
            cmd2.Parameters.AddWithValue("@userId", userId);
            ExecuteUpdateQuery(cmd2);
        }
        public double GetTotalAmount(int userId)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT TotalAmount FROM Users WHERE Id = @userId");

            cmd.Parameters.AddWithValue("@userId", userId);

            return ExecuteSelectIntQuery(cmd);
        }
        public int GetUserCurrencyId(int userId)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT CurrencyId FROM Users WHERE Id = @userId");

            cmd.Parameters.AddWithValue("@userId", userId);

            return ExecuteSelectIntQuery(cmd);
        }
        public int GetUserId(string username,string password)
        {
            SqlCommand cmd = new SqlCommand(
            "SELECT Id FROM Users WHERE Username = @Username AND Password = @Password");
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            return ExecuteSelectIntQuery(cmd);
        }
        public List<Currency> GetCurrencies()
        {
            SqlCommand cmd = new SqlCommand("SELECT Id,Currency FROM Currencies");
            DataTable dt = ExecuteSelectAllQuery(cmd);
            List<Currency> list = new List<Currency>();
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string currencyShort = row["Currency"].ToString();
                Currency currency = new Currency(id, currencyShort);
                list.Add(currency);
            }
            return list;
        }

        public List<Transaction> GetTransactions(int userId)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Id, UserId, Amount, CurrencyId, [Date]
              FROM [dbo].[Transaction]
              WHERE UserId = @UserId");

                cmd.Parameters.AddWithValue("@UserId", userId);

                System.Data.DataTable table = ExecuteSelectAllQuery(cmd);

                foreach (System.Data.DataRow row in table.Rows)
                {
                    Transaction t = new Transaction();

                    t.Id = Convert.ToInt32(row["Id"]);
                    t.UserId = Convert.ToInt32(row["UserId"]);
                    t.Amount = Convert.ToDouble(row["Amount"]);
                    t.Currid = Convert.ToInt32(row["CurrencyId"]);
                    t.Date = Convert.ToDateTime(row["Date"]);

                    // Your current SQL query does not select Category
                    t.Category = "";

                    transactions.Add(t);
                }
            }
            catch (Exception ex)
            {
                // Log the error so you can identify future SQL problems
                System.Diagnostics.Debug.WriteLine(
                    "GetTransactions error: " + ex.Message);
            }

            return transactions;
        }
        public void UpdateUser(int userId, string username, string password)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Users SET Username=@username, Password=@password WHERE Id = @userId");
            cmd.Parameters.AddWithValue("@userId",userId);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            ExecuteUpdateQuery(cmd);
        }
    }
}
    
