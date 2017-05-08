
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

namespace MultiScreen
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		// Const strings for "good programming practices"
		const string firstActivity = "Poke";
		const string firstActivityText = "Screen 2 poked you";
		const string errorText = "Nothing to display yet.";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Second);

			Button showFirst = FindViewById<Button> (Resource.Id.showFirst);
			
			showFirst.Click += (sender, e) => {
				var first = new Intent(this, typeof(FirstActivity));
				first.PutExtra(firstActivity, firstActivityText);
				StartActivity(first);
			};

			// Create a label with data from FirstActivity
			TextView label = FindViewById<TextView> (Resource.Id.screen2Label);
			label.Text = Intent.GetStringExtra(firstActivity) ?? errorText;
		}
	}
}

