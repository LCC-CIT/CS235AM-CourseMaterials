using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DAL;
using SQLite;
using System.IO;
using Domain;
using System.Collections.Generic;

namespace Application
{
	[Activity(Label = "Tide Table")]
	public class DayActivity : ListActivity
    {
		string dbPath = Path.Combine(
			System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Tides.db3");
		SQLiteConnection db = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			db = new SQLiteConnection(dbPath);

			List<string> items = DbControls.GetDaysAsStrings(db);

			ListAdapter = new DayScreenAdapter(this, Android.Resource.Layout.SimpleListItem1, items);
			ListView.FastScrollEnabled = true;
        }

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);

			Intent intent = new Intent (this, typeof(TideInfoActivity));

			List<TideInfo> tides = DbControls.GetTidesForDay (db, position, Intent.GetStringExtra("STATIONID"));

			string info = "";

			foreach (TideInfo tide in tides)
				info += tide + "\n";

			intent.PutExtra ("INFO", info);

			StartActivity (intent);
		}
    }
}