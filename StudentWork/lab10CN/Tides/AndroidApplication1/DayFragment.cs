using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using SQLite;
using System.IO;
using DAL;

namespace Application
{
	public class DayFragment : ListFragment
	{
		string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Tides.db3");
		SQLiteConnection db = null;

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			db = new SQLiteConnection(dbPath);

			string[] items = DbControls.GetDaysAsStringArray (db);

			ListAdapter = new ArrayAdapter<string> (Activity, Android.Resource.Layout.SimpleListItemChecked, items);
			ListView.ChoiceMode = ChoiceMode.Single;
		}
	}
}

