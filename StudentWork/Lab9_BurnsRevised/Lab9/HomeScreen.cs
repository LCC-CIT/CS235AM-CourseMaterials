using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;

using Object = Java.Lang.Object;
using SQLite;
using TideInfoDB;
using System.Net;
using System.Xml.Linq;

namespace Lab9
{
	[Activity (Label = "Lab9 - Tides")]
	public class HomeScreen : Activity
	{
		// Strings
		static string dbFileName = "tideInfo.db3";
		static string intentString = "stationID";

		static string tideStationUrl = @"http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?Stationid=";
		static string tideStationUrl2 = @"&datatype=Annual XML";
		static string catchFailureStr = "Failed to load tide info, data may not exist, try another station.";

		static string dbPathFileName = Path.Combine (
			System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), dbFileName);

		// New list of tides
		private List<Tide> listOfTides = new List<Tide>();

		private TextView tideInfo;
		private ListView tideList;

		private int selectedPos = -1;

		List<TideInfo> tmpTideInfos;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);

			tideInfo = FindViewById<TextView>(Resource.Id.tideTextView);
			tideList = FindViewById<ListView>(Resource.Id.dateListView);


			SQLiteConnection db = new SQLiteConnection (dbPathFileName);

			bool stationExists = false;

			// Check if the tide station exists in the db
			try {
				List<TideInfo> tmpQuery = db.Query<TideInfo>("SELECT * FROM TideInfo WHERE stationId=?",Intent.GetStringExtra(intentString));

				if(tmpQuery.Count > 0)
					stationExists = true;
			}
			catch{
				//Console.WriteLine ("Table read error, or something wrong with table");
			}

			// Check if the station exists in the db, if it doesn't then we load all the stuff in
			if (!stationExists) {

				// Download the stations and do things with it
				downloadTideList();
			}

			// Connect to the DB
			SQLiteConnection db2 = new SQLiteConnection (dbPathFileName);

			// Select the proper station from the DB
			tmpTideInfos = db2.Query<TideInfo> ("SELECT * FROM TideInfo WHERE stationId=?", Intent.GetStringExtra(intentString)); 

			tideList.Adapter = new HomeScreenAdapter (this, tmpTideInfos.ToArray());
			tideList.FastScrollEnabled = true;

			var previousState = LastNonConfigurationInstance as HomeScreenRetainState;
			
			if (previousState != null)
			{
				// Use the listofTides and selectedPos from the previous instance of this activity
				listOfTides = previousState.listofTides;
				selectedPos = previousState.selectedPos;
				updateTideText ();
			}
			// Clicked something, now show the tide info
			tideList.ItemClick += (sender, e) => { selectedPos = e.Position; 
				updateTideText();
			};
		}
		
		public override Java.Lang.Object OnRetainNonConfigurationInstance()
		{
			var savedState = new HomeScreenRetainState(listOfTides, selectedPos);
			return savedState;
		}

		// We clicked something, update the textview
		public void updateTideText()
		{
			if (selectedPos == -1)
				return;

			TideInfo tmpTide = tmpTideInfos[selectedPos];

			tideInfo.Text = "Time: " + tmpTide.time + ", Height: " + tmpTide.heightF + "ft" +
				" / " + tmpTide.heightC + "cm, Type:  " + tmpTide.tideType + "\n";

		}
		// Function to download the list of tides and insert it
		public void downloadTideList()
		{
			try
			{
				string fileString = tideStationUrl + Intent.GetStringExtra(intentString) + tideStationUrl2;
				string tmpStation = Intent.GetStringExtra(intentString);
				// Connect to our wc
				WebClient wc = new WebClient();
				// Pull down the file into a streamreader and have it parsed into an XDocument
				StreamReader xmlDocSr = new StreamReader(wc.OpenRead(fileString));
				XDocument parsedXdoc = XDocument.Parse(xmlDocSr.ReadToEnd());

				SQLiteConnection db = new SQLiteConnection (dbPathFileName);
				// Super fast mode engaged!
				db.BeginTransaction();
				// Dump all of the tide info into the DB
				foreach (XElement element in parsedXdoc.Descendants("item"))
				{
					string tideDate = element.Element("date").Value;
					string tideDay = element.Element("day").Value;
					string tideTime = element.Element("time").Value;
					float tideFt = float.Parse(element.Element("predictions_in_ft").Value);
					float tideCm = float.Parse(element.Element("predictions_in_cm").Value);
					string tideHighLow = element.Element("highlow").Value;

					db.Insert(new TideInfo() { stationId = tmpStation, date = tideDate, day = tideDay, time = tideTime, 
											   heightF = tideFt, heightC = tideCm, tideType = tideHighLow });
					// Testing value
					//tideInfo.Text += element.ToString();
				}
				db.Commit();
			}
			catch {
				tideInfo.Text = catchFailureStr;
			}
		}
	}
}


