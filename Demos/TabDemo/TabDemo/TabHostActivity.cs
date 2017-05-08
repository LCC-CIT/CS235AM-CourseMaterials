using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TabDemo
{
	[Activity (Label = "TabDemo", MainLauncher = true)]
	public class TabHostActivity : TabActivity
	{
		// Properties that can be accessed from the tabbed activities
		// This is where you can store information that is passed from one tab to another
		public string Info { get; set; }
		public int Count{ get; set; }


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			TabHost.TabSpec spec;	// Resusable TabSpec for each tab
			Intent intent; 			// Resusable intent fo reach tab

			// Restore state if this activity had been stopped
			if (bundle != null) {
				Info = bundle.GetString ("info");
				Count = bundle.GetInt ("count");
			}

			// Create an intent to launch an Actiity for tab1 (to be reused)
			intent = new Intent(this, typeof (FirstActivity));
			intent.AddFlags (ActivityFlags.NewTask);	// NewTask: if this activity is already running, 
														// a new instance won't be created

			// Initialize a TabSpec for each tab and add it to the TabHost
			spec = TabHost.NewTabSpec ("first");
			spec.SetIndicator ("First", Resources.GetDrawable(Resource.Drawable.Tab1));
			spec.SetContent(intent);
			TabHost.AddTab (spec);

			// Do the same for tab 2
			intent = new Intent(this, typeof (SecondActivity));
			intent.AddFlags (ActivityFlags.NewTask);

			spec = TabHost.NewTabSpec ("second");
			spec.SetContent(intent);
			spec.SetIndicator ("Second", Resources.GetDrawable(Resource.Drawable.Tab2));
			TabHost.AddTab (spec);

			TabHost.CurrentTab = 0;
		}


		// Called if you rotate, click on Home, or do something else that might kill the app
		// Not called when you click the back-button
		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutString ("info", Info);
			outState.PutInt ("count", Count);
		}


		// Called when this activity is resumed
		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
			Info = savedInstanceState.GetString ("info");
			Count = savedInstanceState.GetInt ("count");
		}
	}
}


