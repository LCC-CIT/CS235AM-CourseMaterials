using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Tide_Table
{
	[Activity (Label = "TideTable", MainLauncher = true)]
	public class TideActivity : Activity
	{



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//SetContentView (Resource.Layout.MainFragment);
			var tides = new TideFragment ();

			var ft = FragmentManager.BeginTransaction ();
			ft.Add (Android.Resource.Id.Content, tides);
			ft.Commit ();
		}
	}
}


