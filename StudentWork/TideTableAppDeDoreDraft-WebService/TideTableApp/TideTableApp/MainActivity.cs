using System;
using System.IO;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using DAL;

namespace TideTableApp
{
	[Activity (Label = "TideTableApp")]
	public class MainActivity : Android.Support.V4.App.FragmentActivity
	{
		private string currentTideData = "";
		private const string TIDE_DATA_SAVE = "TideDataSave";
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activityMain);

			if (bundle != null)
			{
				currentTideData = bundle.GetString (TIDE_DATA_SAVE);
			}
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutString (TIDE_DATA_SAVE, currentTideData);
			base.OnSaveInstanceState (outState);
		}
	}

	public static class CurrentSelectionInfo
	{
		public static string Data{get; set;}
	}
}


