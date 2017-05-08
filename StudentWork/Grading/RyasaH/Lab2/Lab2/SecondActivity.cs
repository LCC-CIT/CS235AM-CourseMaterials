
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

namespace Lab2
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Create your application here
			SetContentView (Resource.Layout.Second);

			//Displaying Screen 1 data in Screen 2 label
			var label = FindViewById<TextView> (Resource.Id.screen2Label);
			label.Text = Intent.GetStringExtra("FirstData") ?? "Data not available";

			// Create Screen 2 Button
			Button showFirst = FindViewById<Button> (Resource.Id.showFirst);

			//Loads first screen on button click
			showFirst.Click += (sender, e) => {
				var first = new Intent(this, typeof(FirstActivity));
				first.PutExtra ("SecondData", "Screen 2 Poked Back!");
				StartActivity(first);
			};
		}
	}
}

