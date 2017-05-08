// Example of using SQLite-net ORM
// Brian Bird, 5/20/13

using System;
using SQLite;
using System.IO;
//using DataAccess.DAL; 
using DataAccess.DAL;

namespace L2Ch3.ConsoleApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello SQLite-net Data!");

			/*
			//Get the path to the folder where the database is stored.
			// Notes: The Path class performs operations on strings that contain file path info
			//        Path.Combine appends the second path to the first path
			//        Environment.GetSpecialFolderPath gets the path to special pre-defined folders
			//              on Windows, the SpecialFolder enum defines: ProgramFiles, System, AppData, etc.
			//              on Android ...
			string dbPath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal), "stocks.db3");
			*/

			/*
			// Check for an existing db file and only create a new one if it doesn't exist
			bool exists = File.Exists (dbPath);
			if (!exists) {
				// Need to create the database and seed it with some data.
				SQLiteConnection.CreateFile (dbPath);
			*/

			// We're using a file in Assets instead of the one defined above
			//string dbPath = Directory.GetCurrentDirectory ();
			string dbPath = @"../../../DataAccess-Android/Assets/stocks.db3";
			var db = new SQLiteConnection (dbPath);

			// Create a Stocks table
			//if (db.CreateTable (Mono.CSharp.TypeOf(Stock)) != 0) 
			if (db.CreateTable<Stock>() == 0)
			{
				// A table already exixts, delete any data it contains
				db.DeleteAll<Stock> ();
			}

			// Create a new stock and insert it into the database
			var newStock = new Stock ();
			newStock.Symbol = "APPL";
			newStock.Name = "Apple";
			int numRows = db.Insert (newStock);
			Console.WriteLine ("Number of rows inserted = {0}", numRows);

			// Insert some more stocks
			db.Insert(new Stock() {Symbol = "MSFT", Name = "Microsoft"});
			db.Insert (new Stock() {Symbol = "GOOG", Name = "Google"});
			db.Insert (new Stock() {Symbol = "SSNLF", Name = "Samsung"});
			db.Insert (new Stock() {Symbol = "AMZN", Name = "Amazon"});
			db.Insert (new Stock() {Symbol = "MMI", Name = "Motorola Mobility"});
			db.Insert (new Stock() {Symbol = "FB", Name = "Facebook"});

			// Read the stock from the database
			// Use the Get method with a query expression
			var existingItem = db.Get<Stock> (x => x.Name == "Google");
			Console.WriteLine ("Stock Symbol for Google: {0}", existingItem.Symbol);

			// Use the Get method with a primary key
			existingItem = db.Get<Stock> ("FB");
			Console.WriteLine ("Stock Name for Symbol FB: {0}", existingItem.Symbol);

			// Query using  SQL
			var stocksStartingWithA = db.Query<Stock> ("SELECT * FROM Stocks WHERE Symbol LIKE ?", "A%"); 
			foreach(Stock stock in stocksStartingWithA)
				Console.WriteLine ("Stock starting with A: {0}", stock.Symbol);

			// Query using Linq
			var stocksStartingWithM = from s in db.Table<Stock> () where s.Symbol.StartsWith ("M") select s;
			foreach(Stock stock in stocksStartingWithM)
				Console.WriteLine ("Stock starting with M: {0}", stock.Symbol);


		}
	}
}
