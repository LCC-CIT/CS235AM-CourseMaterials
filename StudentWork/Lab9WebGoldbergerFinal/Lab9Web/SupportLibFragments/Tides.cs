using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab7Fragments
{			
	public class Tides
	{
		public Tide[] datesArray;
		public Tide[] tidesArray;
		public string location;

		// Loads the tide data from tide database for this station or HTML, if a tide database for this station is not present.
		// If not present, creates the tide database for this station.
		// Returns a Tide array.
		public Tide[] LoadTideTables(string name, string id, string state)
		{

			var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), id+".db3");
			if (!File.Exists (dbPath)) {
				var db = new SQLiteConnection (dbPath);
				db.CreateTable<Tide> ();
				db.BeginTransaction ();
				string baseURL = "http://tidesandcurrents.noaa.gov/";

				string tideListURL = "noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+TXT&Stationid=";        
				tideListURL += id; // get station ID
				var tides = new List<Tide>();

				WebClient client = new WebClient();
				Stream data;
				try
				{
					data = client.OpenRead (baseURL + tideListURL);
				}
				catch (WebException ex)
				{
					string msg;
					if (name.Substring (0, 6) == "Unable")
						msg = "Unable to connect to NOAA at this time.";
					else 
						msg = "Unable to get tide data for " + name + ", " + state + " at this time.";
					var tide = new Tide(msg);
					tides.Add(tide);
					tidesArray = tides.ToArray();
					db.Close ();
					File.Delete (dbPath);
					return tidesArray;
				}
				StreamReader reader = new StreamReader(data);


				while (!reader.EndOfStream)
				{
					// Create a new tide object and add a line of data to it.
					var tide = new Tide(reader.ReadLine());
					if (tide.data.Substring (0, 1) != "2")
						continue;
					// add tide object to tides list (as long as it is a tide, not header info in the file
					else if (tide.data.Substring(0,4) == "2013")
					{
						tides.Add(tide);
						db.Insert (tide);
					}
				}

				reader.Close();
				db.Commit ();
				db.Close ();
				// convert tides list to array and send it on back.
				tidesArray = tides.ToArray();
			}
			else {
				var db = new SQLiteConnection (dbPath);
				tidesArray = db.Table<Tide> ().ToArray();
				db.Close ();
			}
			return tidesArray;
		}
		
		// Get only unique dates from tides array.
		public Tide[] GetDates()
		{
			var dates = new List<Tide>();
			string date;
			string lastDate = "";
			for (int i=0; i< tidesArray.Length; i++)
			{
				date = tidesArray[i].data.Substring(0,10);
				// Only add the first tide per day to the dates list.
				if (date != lastDate) 
				{
					dates.Add(tidesArray[i]);
				}
				lastDate = date;
			}
			// convert list to Tide array and send it back
			datesArray = dates.ToArray();
			return datesArray; 
		}
	}
}
