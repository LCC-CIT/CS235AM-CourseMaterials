using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TideChart;
using TideChartConsole;
using System.IO;
using SQLite;

namespace AndroidTideChart
{
	[Activity (Label = "StationSelection", MainLauncher = true)]
	public class StationSelection : ListActivity
	{
		private TideStations TheStations;

		protected override void OnCreate (Bundle bundle)
		{
			if (!File.Exists (Connection.GetDBPath ())) {
				SQLiteConnection db = new SQLiteConnection (Connection.GetDBPath());
				db.CreateTable<TideStationRow> ();
				db.CreateTable<TideRow> ();
			} 
			/*else 
			{
				File.Delete (Connection.GetDBPath());
				string temp = "Done";
			}*/


			base.OnCreate (bundle);
			StationSelectionRetainedState previousState = LastNonConfigurationInstance as StationSelectionRetainedState;
			if (previousState == null)
			{
				TheStations = new TideStations();
			}
			else
			{
				TheStations = previousState.tideStations;
			}

			ListAdapter = new StationSelectionAdapter(this, TheStations);
			ListView.FastScrollEnabled = true;
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			//this is for testing
			//string info = TheStations.Stations[position].StationName;
			//Android.Widget.Toast.MakeText(this, info, Android.Widget.ToastLength.Short).Show();

			//TODO this should start new activity of StationTideInfo
			Intent tideInfo = new Intent(this, typeof(StationTideInfo));
			tideInfo.PutExtra("StationID", TheStations.Stations[position].StationID);
			tideInfo.PutExtra("StationName", TheStations.Stations[position].StationName);
			StartActivity(tideInfo);
		}

		public override Java.Lang.Object OnRetainNonConfigurationInstance ()
		{
			StationSelectionRetainedState savedState = new StationSelectionRetainedState(TheStations);
			return savedState;
		}
	}
}


