using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab02
{
	[Activity (Label = "Activity2")]			
	public class Activity2 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			//Load the TextView and Button stored in the 'Second' layout file.
			SetContentView (Resource.Layout.Second);
			
			//Check for data from 'MainActivity', and load it if found.
			var secondView = FindViewById<TextView> (Resource.Id.view2);
			secondView.Text = Intent.GetStringExtra ("First data") ?? "Data not found";
			
			//Set up this Activity's Button so that it loads 'MainActivity'.
			Button secondButton = FindViewById<Button> (Resource.Id.button2);
			
			secondButton.Click += (sender, e) =>
			{
				//Send additional data to the first Activity.
				var dataPack2 = new Intent(this, typeof(Activity1));
				dataPack2.PutExtra ("Second data", "Screen 2 poked you");
				StartActivity(dataPack2);
			};
		}
	}
}
