using System;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace DAL
{
	[Table("TD")]
	public class TideData
	{
		[PrimaryKey, Column("Date")]
		[MaxLength(100)]
		//these are the properties that outside objects may access
		public string Date{ get; set;}
		public string Location{ get; set;}
		public string Info{ get; set;}
		public long ID{ get; set;}

		//Contains:	one DateTime for the year, month, day; multiple tideLevels with their times
		const string DATE_FORM = "D";		//the string conversion format for our dateTimes
		const string TIME_FORM = "t";		//the time only segment format

		public void SetProperties(string loc, DateTime d, string t, string tT, string tL, long id)
		{
			Location = loc;
			Date = d.ToString(DATE_FORM);
			ID = id;
			AddTide (t, tT, tL);
		}
		public void AddTide(string t, string tT, string tL)		//time, tide type(h/l), tide level
		{
			Info += (t + "\t\t" + tT + " " + tL + "cm \n");
		}
		
		public string GetTimeFormat()
		{
			return DATE_FORM;
		}
	}

	[Table("ST")]
	public class Station
	{
		//these are the properties that outside objects may access
		[PrimaryKey, Column("ID")]
		public long ID{ get; set;}
		[MaxLength(50)]
		public string Name{ get; set;}
	}

	public static class DataBaseHandler
	{
		//this is our DataBase connector. It will need to be accessed by other classes
		public static SQLiteConnection db{ get; set;}

		public static string WriteableDBPath{get; set;}

		public static void DataBaseConvertToWriteable(Stream oldDB)
		{
			//setting up for the database read in from assets
			WriteableDBPath = "";
			WriteableDBPath = Path.Combine (System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase1.db3");
			//copy the file found on the path to a new file with read/write enabled
			using (Stream inStream = oldDB)
			using (Stream outStream = File.Create (WriteableDBPath)) inStream.CopyTo(outStream);

			//create new dataBase on the new path, recreated with our old var to save memory
			db = new SQLiteConnection(WriteableDBPath);
		}

		public static void DeleteStationsFromDataBase()
		{
			db.DeleteAll<Station>();
		}

		public static void DeleteTidesFromDataBase()
		{
			db.DeleteAll<TideData>();
		}

		public static void AddStationToDatabase(string stName, long id)
		{
			db.CreateTable<Station> ();
			db.Insert (new Station(){Name = stName,ID = id});
		}
	}
}

