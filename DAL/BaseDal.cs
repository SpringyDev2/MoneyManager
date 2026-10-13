using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{

    public class BaseDal
    {
        protected string ConnectionString;
        protected SqlConnection conn;

        public BaseDal()
        {
            string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DAL", "DatabaseMM.mdf");
            ConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = databasePath,
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                MultipleActiveResultSets = true,
                ConnectRetryCount = 3,
                ConnectRetryInterval = 10
            }.ConnectionString;
            conn = new SqlConnection(ConnectionString);
        }

        // =========================
        // NON-QUERY (INSERT/UPDATE/DELETE)
        // =========================
        protected int ExecuteQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = conn;
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleError(ex, cmd.CommandText);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        protected int ExecuteInsertAndReturnId(SqlCommand cmd)
        {
            cmd.Connection = conn;

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result);
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public int ExecuteInsertQuery(SqlCommand cmd)
        {
            return ExecuteQuery(cmd);
        }

        public int ExecuteUpdateQuery(SqlCommand cmd)
        {
            return ExecuteQuery(cmd);
        }

        public int ExecuteDeleteQuery(SqlCommand cmd)
        {
            return ExecuteQuery(cmd);
        }

        // =========================
        // SELECT ALL (TABLE)
        // =========================
        public DataTable ExecuteSelectAllQuery(SqlCommand cmd)
        {
            DataTable table = new DataTable();

            try
            {
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                HandleError(ex, cmd.CommandText);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return table;
        }

        // =========================
        // SCALAR HELPERS
        // =========================
        private object ExecuteSelectOneData(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = conn;
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleError(ex, cmd.CommandText);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public string ExecuteSelectStringQuery(SqlCommand cmd)
        {
            return Convert.ToString(ExecuteSelectOneData(cmd));
        }

        public bool ExecuteSelectBoolQuery(SqlCommand cmd)
        {
            return Convert.ToBoolean(ExecuteSelectOneData(cmd));
        }

        public int ExecuteSelectIntQuery(SqlCommand cmd)
        {
            return Convert.ToInt32(ExecuteSelectOneData(cmd));
        }

        // =========================
        // ERROR HANDLING
        // =========================
        public virtual void HandleError(Exception ex, string sql)
        {
            Console.WriteLine("SQL Error:");
            Console.WriteLine(sql);
            Console.WriteLine(ex.Message);
        }
    }
}



