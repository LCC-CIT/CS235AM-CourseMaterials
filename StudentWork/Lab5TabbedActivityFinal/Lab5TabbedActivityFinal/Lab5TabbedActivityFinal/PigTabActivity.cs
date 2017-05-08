using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TabbedWalkThrough
{
	[Activity (MainLauncher = true, Label = "@string/app_name", Theme="@android:style/Theme.NoTitleBar", Icon = "@drawable/icon")]
	public class PigTabActivity : TabActivity
	{
		public static Activity context;
		public static int currentTab;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.PigTabActivity);

			context = this;

			TabHost.TabSpec spec; //Reusable tab specs for each tab
			Intent intent; // Reusable intent for each tab

			// create an intent to launch an acativity for the tab ( to be reused)
			intent = new Intent (this, typeof(HomeActivity));
			intent.AddFlags(ActivityFlags.NewTask);

			// Initialize a TabSpec for each tab and add it to the tabhost
			spec = TabHost.NewTabSpec("home");
			spec.SetIndicator("Home", Resources.GetDrawable(Resource.Drawable.ic_tab_home));
			spec.SetContent (intent);
			TabHost.AddTab(spec);

			// ditto for other tabs
			// create an intent to launch an acativity for the tab ( to be reused)
			intent = new Intent (this, typeof(PlayActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			
			// Initialize a TabSpec for each tab and add it to the tabhost
			spec = TabHost.NewTabSpec("game");
			spec.SetIndicator("Game", Resources.GetDrawable(Resource.Drawable.ic_tab_play));
			spec.SetContent (intent);
			TabHost.AddTab(spec);

			if (bundle != null)
				currentTab = bundle.GetInt("currTab");
		}
//============================Save and resume state==================================================//		
		protected override void OnResume()
		{
			base.OnResume();
			TabHost.CurrentTab = currentTab;
		}
		protected override void OnPause()
		{
			base.OnPause();
			currentTab = TabHost.CurrentTab;
		}
		// Save state
		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			currentTab =  TabHost.CurrentTab;
			outState.PutInt("currTab", currentTab);
		}
	}
}


