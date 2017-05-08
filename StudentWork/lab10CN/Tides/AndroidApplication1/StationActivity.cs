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
using System.IO;
using DAL;
using SQLite;
using Domain;
using System.Xml.Linq;
using Xamarin.Geolocation;

namespace Application
{
	[Activity (Label = "StationActivity", MainLauncher = true, Icon = "@drawable/icon")]			
	public class StationActivity : ListActivity
	{
		// Get the path to the database that was deployed in Assets
		string dbPath = Path.Combine(
			System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Tides.db3");
		SQLiteConnection db = null;
		List<string> items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			CopyDb();

			db = new SQLiteConnection (dbPath);

			items = DbControls.GetAllStationsStrings (db);

			ListAdapter = new DayScreenAdapter (this, Android.Resource.Layout.SimpleListItem1, items);
			ListView.FastScrollEnabled = true;

			// This works in the console but not in android I'm not sure why
//			string sId = "TWC0863";
//			string newSave = Path.Combine (
//				                 System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "tidesxml.xml");
//			string text = ReadFiles.DownloadString (TideStation.Address + sId);
//			ReadFiles.SaveXmlFile (text, newSave);
//			XDocument newDoc = ReadFiles.GetXml(newSave);

			GeoLoc g = new GeoLoc (this);
			Position pos = g.GetPosition (this);
			double lat = pos.Latitude;
			double longi = pos.Longitude;
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			List<TideStation> stations = DbControls.GetAllStations (db);
			Intent intent;

			// Fragments aren't working.
			//if (GetScreenSize () < 500000) {
				intent = new Intent (this, typeof(DayActivity));
			//} else {
			//intent = new Intent (this, typeof(FragmentActivity));
			//}
			TideStation station = stations [position];

			intent.PutExtra ("STATIONID", station.TideStationId);

			StartActivity (intent);
		}

		private void CopyDb()
		{
			if(!File.Exists (dbPath))
			{
				using (Stream inStream = Assets.Open("Tides.db3"))
				using (Stream outStream = File.Create(dbPath))
					inStream.CopyTo(outStream);
			}
		}

		private int GetScreenSize()
		{
			var metrics = Resources.DisplayMetrics;
			var widthInDp = ConvertPixelsToDp (metrics.WidthPixels);
			var heightInDp = ConvertPixelsToDp (metrics.HeightPixels);
			return widthInDp * heightInDp;
		}

		private int ConvertPixelsToDp(float pixelValue)
		{
			return (int)((pixelValue) / Resources.DisplayMetrics.Density);
		}
	}
}

