using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace ActivityLifecycle
{
	[Activity (Label = "ActivityLifecycle", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int _counter = 1;	// Walkthrough step 1
		Button clickbutton;

		protected override void OnCreate (Bundle bundle)
		{
			// Walkthrough step 4 - this whole method
			Log.Debug(GetType().FullName, "Activity A - OnCreate");
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.Main);

			/*
			 * I don't think we really need this button
			// Event handler: myButton launches a second activity
			FindViewById<Button>(Resource.Id.myButton).Click += (sender, args) => {
				var intent = new Intent(this, typeof(SecondActivity));
				StartActivity(intent);
			};
			*/

			// Retrieve count from the Bundle
			if (bundle != null)
			{
				_counter = bundle.GetInt ("click_count", 0);
				Log.Debug(GetType().FullName, "Recovered instance state");
			}

			// Event handler: clickButton adds one to the button count
			clickbutton = FindViewById<Button> (Resource.Id.clickButton);
			UpdateButtonText ();
			clickbutton.Click += (object sender, System.EventArgs e) => {
				_counter++;
				UpdateButtonText();
			};
		}

		// Walkthrough step 2
		// Persist count by putting it in a Bundle
		// Automatically invoked when this Activity is being destroied
		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutInt ("click_count", _counter);
			Log.Debug(GetType().FullName, "Saving instance state");

			base.OnSaveInstanceState (outState);   // Saves the state of the View hierarchy
		}

		// Changes the number shown on the button
		void UpdateButtonText()
		{
			clickbutton.Text = Resources.GetString(Resource.String.counterbutton_text) + " " + _counter.ToString();
		}
	}
}


