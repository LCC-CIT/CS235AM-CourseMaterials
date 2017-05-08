using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActivityExperiments
{
	[Activity (Label = "MainActivity", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnStart = FindViewById<Button> (Resource.Id.startButton);
			
			btnStart.Click += delegate {
				Intent mainIntent = new Intent(this, typeof(DemoActivity));
				StartActivity(mainIntent);
			};
		}
	}
}


