using System;
using SQLite;
using System.IO;

namespace TideInfoDB
{
	class MainClass
	{
		// All of our strings
		static string dbPathName = @"C:\Users\Leonard\Desktop\Lab8v2\Lab8\Assets\";
		static string dbFileName = "tideInfo.db3";

		static string[] tideInfoFiles = { "florence.txt", "gardiner.txt", "reedsport.txt" };
		static string[] tideInfoStationID = { "9434032", "TWC0831", "9433501" };

		public static void Main (string[] args)
		{
			// Combine the paths into one
			string dbPath = Path.Combine (dbPathName, dbFileName);

			// Open or create a database
			SQLiteConnection db = new SQLiteConnection (dbPath);
			// Db exists?
			bool dbExists = false;

			// Create the tide tables, try and catch errors
			try{
				db.Query<TideStation>("SELECT * FROM TideStation");
				dbExists = true;
			}
			catch{
				Console.WriteLine ("Table read error, or something wrong with table");
			}
			// If it doesn't exist then create it
			if (!dbExists) {

				db.CreateTable<TideStation>();
				db.CreateTable<TideInfo>();

				// Store all the inserts in memory, 100x's faster than without
				db.BeginTransaction();

				for (int i = 0; i < tideInfoFiles.Length; i++)
				{
					// Create the stationID and name in the TideStation table
					db.Insert(new TideStation() {stationId = tideInfoStationID[i], stationName = tideInfoFiles[i]});
					// New streamreader, using will clean up after we're done
					using (StreamReader reader = new StreamReader (dbPathName + tideInfoFiles[i]))
					{
						// Load to the end of the file
						while(!reader.EndOfStream)
						{
							string tmpLine = reader.ReadLine();

							// Cut out the tabs from each line and create a new string array for each line
							string[] tmpLineCut = tmpLine.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
							// Insert each line into the proper table in the DB
							db.Insert(new TideInfo() {  stationId = tideInfoStationID[i],
														date = tmpLineCut[0], 
														day = tmpLineCut[1], 
														time = tmpLineCut[2], 
														heightF = float.Parse(tmpLineCut[3]),
														heightC = float.Parse(tmpLineCut[4]),
														tideType = tmpLineCut[5] });
						}
					}
				}

				// Commit everything to disk
				db.Commit ();
			}
		}
	}
}
