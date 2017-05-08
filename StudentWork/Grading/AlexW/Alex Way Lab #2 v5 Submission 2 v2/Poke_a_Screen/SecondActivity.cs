//Project made by Alex C. Way


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

namespace Poke_a_Screen
{
	[Activity (Label = "Screen 2")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Use UI created in Second.axml
			SetContentView (Resource.Layout.Second);
			var showFirst = FindViewById<Button> (Resource.Id.showFirst);

			//Button to go to and "poke" Screen 1 (FirstActivity):
			//Send data to and go to screen 1 when button is clicked:
			showFirst.Click += delegate {
				var first = new Intent(this, typeof(FirstActivity));
				
				first.PutExtra("SecondData", "Screen 2 poked you");
				
				StartActivity (first);
			};

			//Retrieve any data passed from Screen 1:
			var label = FindViewById<TextView> (Resource.Id.screen2Label);
			//Display any data passed from Screen 1:
			label.Text = Intent.GetStringExtra("FirstData") ?? "Data not available"; //Another form of a true or false statement.
		}
	}
}