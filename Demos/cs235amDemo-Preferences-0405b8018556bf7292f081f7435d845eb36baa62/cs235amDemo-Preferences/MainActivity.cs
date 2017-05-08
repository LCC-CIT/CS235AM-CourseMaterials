using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace cs235amDemoPreferences
{
	[Activity (Label = "cs235amDemo-Preferences", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
				if(pref.GetBoolean("count_clicks", true))
				{
					button.Text = string.Format ("{0} clicks!", count++);
				}
				else
				{
					button.Text = "Hello " + pref.GetString("name", "Donald Duck");
				}
			};
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.SettingsActivityActions, menu);       
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			// Handle presses on the action bar items
			switch (item.ItemId) 
			{
				case Resource.Id.action_settings:
					StartActivity(typeof(SettingsActivity));
					return true;
				default:
				return base.OnOptionsItemSelected (item);
			}
		}

	}
}


