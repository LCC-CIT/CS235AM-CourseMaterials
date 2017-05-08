/*
 * Jonathan David Alley
 * Saturday, 13 April 2013
 * CS 235 M - Intermediate Mobile Application Development
 * 		CRN 44432, Room 19-132, MW 4:00-5:50 PM
 * Lab 2
 * 		Objective: Create two Activities with buttons that switch between them.
 * 			Also send data back and forth between each Activity.
 * Updates
 * 		Version #.# - //Date goes here
 * 			//Changes go here
 */

using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Lab02
{
	[Activity (Label = "Lab02", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Load the TextView and Button stored in the 'Main' layout file.
			SetContentView (Resource.Layout.Main);
			
			//Check for data from 'Activity2', and load it if found.
			var firstView = FindViewById<TextView> (Resource.Id.view1);
			firstView.Text = Intent.GetStringExtra ("Second data") ?? "Data not found";

			//Set up this Activity's Button so that it loads 'Activity2'.
			Button firstButton = FindViewById<Button> (Resource.Id.button1);

			firstButton.Click += (sender, e) =>
			{
				//Send additional data to the second Activity.
				var dataPack1 = new Intent(this, typeof(Activity2));
				dataPack1.PutExtra ("First data", "Screen 1 poked you");
				StartActivity(dataPack1);
			};
		}
	}
}
