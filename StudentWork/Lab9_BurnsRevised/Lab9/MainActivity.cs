/* 
Modify the Tide Application from lab 8 so that it gets it's tide information from the internet.

When the App is started for the first time, it should load a list of  tide prediction tables for Oregon,
by location name, from the NOAA web site. The list of locations should be stored in a SQLite database.

When the user selects a location, the tide table for that location should be downloaded, stored in the 
database, and displayed. If the tide table has already been stored in the database, it should just be 
displayed.

Tide information should be displayed just as it was in the previous version of this app. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using TideInfoDB;

namespace Lab9
{
	[Activity (Label = "Lab 9 - Stations", MainLauncher = true, Icon = "@drawable/icon")]			
	public class MainActivity : Activity
	{		
		// Static strings for all the file and DB things
		static string intentString = "stationID";
		static string stationsLoaded = "Total Stations loaded: ";

		// Entire path for the DB using the special folder location
		static string dbPathFileName = Path.Combine (
			System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "tideInfo.db3");
		// List of oregon tide stations
		// http://tidesandcurrents.noaa.gov/station_retrieve.shtml?type=Tide%20Data&state=Oregon&id1=943
		
		static string tideStationUrl = "http://tidesandcurrents.noaa.gov/station_retrieve.shtml?type=Tide%20Data&state=Oregon&id1=943";

		static string catchFailureStr = "Failed to load file, or insert into DB";

		private TextView tideInfo;
		private ListView tideList;

		private int selectedPos = -1;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);
			
			tideInfo = FindViewById<TextView>(Resource.Id.tideTextView);
			tideList = FindViewById<ListView>(Resource.Id.dateListView);

			// Check if the DB exists, if it doesn't we make a new one and load
			// the oregon stations into it
			if (!File.Exists (dbPathFileName)) {

				// Open or create a database
				SQLiteConnection dbStart = new SQLiteConnection (dbPathFileName);

				dbStart.CreateTable<TideStation>();
				dbStart.CreateTable<TideInfo>();

				// Download the stations and do things with it
				downloadStationList();
			}
//			} else { // In case we need to delete the db
//				File.Delete (dbPathFileName);
//				string tmpString = "Done";
//			}

			// Then we connect to the DB and start reading stuff in
			SQLiteConnection db = new SQLiteConnection (dbPathFileName);
			// Select all the data from the TideStation table
			var tideStations = db.Query<TideStation> ("SELECT * FROM TideStation");
			//tideInfo.Text = tideStations.Count.ToString();
			// Load it all in, set the textVew to show how many were loaded, testing
			//tideInfo.Text = stationsLoaded + tideStations.Count ().ToString ();

			// If we load a DB and its empty, redownload things
			// this may happen if the webclient times out or something fails to i
			if (tideStations.Count <= 0)
				downloadStationList ();

			tideList.Adapter = new MainScreenAdapter (this, tideStations.ToArray ());

			tideList.FastScrollEnabled = true;

			// Load the tide info to the next activity
			tideList.ItemClick += (sender, e) => { selectedPos = e.Position; 
				Intent tideIntent = new Intent(this, typeof(HomeScreen));
				tideIntent.PutExtra(intentString, tideStations[selectedPos].stationId);
				StartActivity(tideIntent);
				//tideInfoUpdate(); 
			};
		}

		// Function to download and load the station listing
		public void downloadStationList()
		{
			// Strings we need to parse things out
			string htmlStringBegin = "<td><a href=\"data_menu.shtml?stn=";
			string htmlStringEnd = @"&type=Tide Data";

			try
			{
				// Start up the wc
				WebClient wc = new WebClient();
				Stream tideStationFile = wc.OpenRead(tideStationUrl);
				// Read in the file into a stream
				StreamReader sr = new StreamReader(tideStationFile);

				// Store the page in a string
				string tideString = sr.ReadToEnd();
				// Done with the stream
				sr.Close();

				// Start parsing chunks out of the stream
				string[] parsedHtmlString = tideString.Split(new string[] { htmlStringBegin }, StringSplitOptions.RemoveEmptyEntries);

				SQLiteConnection db = new SQLiteConnection (dbPathFileName);
				// Super fast mode engaged! gooooooal!
				db.BeginTransaction();
				// Set the tide text to how many stations we found.
				tideInfo.Text = stationsLoaded + parsedHtmlString.Length.ToString();

				// Loop through until we get all of the stations out
				for (int i = 1; i < parsedHtmlString.Length; i++)
				{
					string[] tmpLine = parsedHtmlString[i].Split(new string[] { htmlStringEnd }, StringSplitOptions.RemoveEmptyEntries);
					// Grab pieces of the 98381891 Gold Beach, OR chunk
					string tmpId = tmpLine[0].Substring(0,7);
					string tmpName = tmpLine[0].Substring(8);
					// Insert it in, fails if we try to build the string right in the insert, weird eh?
					db.Insert(new TideStation() { stationId = tmpId, stationName = tmpName });
				}
				//tideInfo.Text = parsedHtmlString[1];
				db.Commit();
			}
			catch 
			{
				// Something went wrong, failed to load the file or insert
				tideInfo.Text = catchFailureStr;
			}
		}

	}
}