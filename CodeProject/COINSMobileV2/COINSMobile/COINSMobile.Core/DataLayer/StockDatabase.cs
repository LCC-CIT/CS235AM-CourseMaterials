using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Mono.Data.Sqlite;
using System.IO;
using COINSMobile.Core.BusinessLayer;

namespace COINSMobile.Core.DataLayer
{
    public class StockDatabase
    {
        private static string db_file = "stockdata.db3";
        private string stockName = string.Empty;
        private static string sMessage;

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; }
        }

        private static SqliteConnection GetConnection()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), db_file);
            bool exists = File.Exists(dbPath);

            if (!exists)
                SqliteConnection.CreateFile(dbPath);

            var conn = new SqliteConnection("Data Source=" + dbPath);

            if (!exists)
                CreateDatabase(conn);

            return conn;
        }

        private static void CreateDatabase(SqliteConnection connection)
        {
            var sql = "CREATE TABLE STOCKTABLE (Id INTEGER PRIMARY KEY AUTOINCREMENT, StockName VARCHAR);";

            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static IEnumerable<Stock> GetStocks()
        {
            try
            {
                var sql = "SELECT * FROM STOCKTABLE ORDER BY ID;";

                using (var conn = GetConnection())
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                yield return new Stock(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
            }
            finally
            {
                StockManager.Message = sMessage;
            }
        }
        public static bool IsStockExists(string _stockname)
        {
            bool Ok = false;
            var sql = string.Format("SELECT * FROM STOCKTABLE WHERE STOCKNAME='{0}';", _stockname);

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                Ok = true;
                        }
                    }
                }
            }
            finally
            {
                StockManager.Message = sMessage;
            }
            return Ok;
        }

        public static bool SaveStock(string _stockname)
        {
            try
            {
                bool Ok = IsStockExists(_stockname.Trim().ToUpper());
                if (Ok)
                {
                    sMessage = string.Format("Stock Script '{0}' is already added.", _stockname);
                    return false;
                }
                using (var conn = GetConnection())
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {

                        try
                        {
                            // Do an insert
                            cmd.CommandText = "INSERT INTO STOCKTABLE (StockName) VALUES (@StockName);";
                            cmd.Parameters.AddWithValue("@StockName", _stockname.ToUpper());
                            cmd.ExecuteNonQuery();

                            sMessage = string.Format("Stock Script '{0}' is added successfully.", _stockname.ToUpper());
                            return true;
                        }
                        catch (SqliteException ex)
                        {
                            sMessage = ex.Message;
                            return false;
                        }
                    }
                }
            }
            finally
            {
                StockManager.Message = sMessage;
            }
        }

        public static bool DeleteAllStocks()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {

                        try
                        {
                            // Do an insert
                            cmd.CommandText = "DELETE FROM STOCKTABLE;";
                            cmd.ExecuteNonQuery();

                            sMessage = "All Stocks are deleted successfully...\nTo view the stocks in Market Watch, you need to add your custom stock";
                            return true;
                        }
                        catch (SqliteException ex)
                        {
                            sMessage = ex.Message;
                            return false;
                        }
                    }
                }
            }
            finally
            {
                StockManager.Message = sMessage;
            }
        }
    }
}