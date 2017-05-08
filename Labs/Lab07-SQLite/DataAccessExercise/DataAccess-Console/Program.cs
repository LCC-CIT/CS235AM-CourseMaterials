// Example of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise and completed
// By Brian Bird 5/12/16

using System;
using SQLite;
using System.IO;
using DataAccess.DAL;
using System.Collections.Generic;

namespace DataAccess.DOS
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello SQLite-net Data!");

			// We're using a file in Assets instead of the one defined above
			//string dbPath = Directory.GetCurrentDirectory ();
			string dbPath = @"../../../DataAccess-Android/Assets/stocks.db3";
			var db = new SQLiteConnection (dbPath);

			// Create a Stocks table
			if (db.CreateTable<Stock>() == 0)
			{
				// A table already exixts, delete any data it contains
				db.DeleteAll<Stock> ();
			}
				
			AddStocksToDb (db, "GOOG", "Google", "GoogleStocks.csv");
			AddStocksToDb (db, "EA", "Electronic Arts", "EAStocks.csv");
			AddStocksToDb (db, "SNE", "Sony", "SonyStocks.csv");

		}

		private static void AddStocksToDb(SQLiteConnection db, string symbol, string name, string file)
		{
			// parse the stock csv file
			const int NUMBER_OF_FIELDS = 7;    // The text file will have 7 fields, The first is the date, the last is the adjusted closing price
			TextParser parser = new TextParser(",", NUMBER_OF_FIELDS);     // instantiate our general purpose text file parser object.
			List<string[]> stringArrays;    // The parser generates a List of string arrays. Each array represents one line of the text file.
			stringArrays = parser.ParseText(File.Open(@"../../../DataAccess-Console/DAL/" + file,FileMode.Open));     // Open the file as a stream and parse all the text

			// Don't use the first array, it's a header
			stringArrays.RemoveAt(0);
			// Copy the List of strings into our Database
			int pk = 0;
			foreach (string[] stockInfo in stringArrays) {
				pk += db.Insert (new Stock () {
					Symbol = symbol,
					Name = name,
					Date = Convert.ToDateTime (stockInfo [0]),
					ClosingPrice = decimal.Parse (stockInfo [6])
				});
				// Give an update every 100 rows
				if (pk % 100 == 0)
					Console.WriteLine ("{0} {1} rows inserted", pk, symbol);
			}
			// Show the final count of rows inserted
			Console.WriteLine ("{0} {1} rows inserted", pk, symbol);			
		}
	}
}
