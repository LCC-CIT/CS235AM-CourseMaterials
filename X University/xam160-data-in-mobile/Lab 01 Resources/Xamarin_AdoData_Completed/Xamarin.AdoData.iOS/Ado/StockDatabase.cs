using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;

namespace Xamarin.AdoData.iOS.Ado
{
    public class StockDatabase
    {
        private static object locker = new object();

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "StockDB.db3";

                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder

                var path = Path.Combine(libraryPath, sqliteFilename);

                return path;
            }
        }

        public Boolean CreateDatabaseIfNotExists()
        {
            if (File.Exists(DatabaseFilePath))
                return true;

            var creationSuccessful = false;

            lock (locker)
            {

                // Need to create the database and seed it with some data.
                SqliteConnection.CreateFile(DatabaseFilePath);

                using (var connection = new SqliteConnection("Data Source=" + DatabaseFilePath))
                {
                    connection.Open();
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = "CREATE TABLE [Stock] (_id ntext, Symbol ntext);";
                        creationSuccessful = c.ExecuteNonQuery() > 0;
                    }
                }
            }

            return creationSuccessful;
        }

        public Boolean InsertStock(Model.Stock stock)
        {

            var insertSuccessful = false;

            lock (locker)
            {
                using (var connection = new SqliteConnection("Data Source=" + DatabaseFilePath))
                {
                    connection.Open();
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = String.Format("INSERT INTO [Stock] ([_id], [Symbol]) VALUES ('{0}', '{1}')", stock.Id, stock.Symbol);
                        insertSuccessful = c.ExecuteNonQuery() > 0;
                    }
                }
            }

            return insertSuccessful;
        }

        public List<Model.Stock> SelectStock()
        {

            var stockInDatabase = new List<Model.Stock>();

            lock (locker)
            {
                using (var connection = new SqliteConnection("Data Source=" + DatabaseFilePath))
                {
                    connection.Open();
                    using (var contents = connection.CreateCommand())
                    {
                        contents.CommandText = "SELECT [_id], [Symbol] from [Stock]";
                        var r = contents.ExecuteReader();
                        while (r.Read())
                            stockInDatabase.Add(
                                new Model.Stock()
                                {
                                    Id = Convert.ToInt32(r["_id"]),
                                    Symbol = r["Symbol"].ToString()
                                });
                    }
                }
            }

            return stockInDatabase;
        }


        public static Model.Stock GenerateStock()
        {
            return new Model.Stock()
            {
                Id = (new Random()).Next(),
                Symbol = GenerateSymbol()
            };
        }

        private static string GenerateSymbol()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();

            return new string(
                Enumerable.Repeat(chars, 4)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
