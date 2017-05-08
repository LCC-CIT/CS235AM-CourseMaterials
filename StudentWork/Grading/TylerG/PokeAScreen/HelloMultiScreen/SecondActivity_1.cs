
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

namespace HelloMultiScreen
{
	[Activity (Label = "Screen 2")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here.
			SetContentView (Resource.Layout.Second);

			var label = FindViewById<TextView> (Resource.Id.screen2Label); 
			label.Text = Intent.GetStringExtra("Screen1Poke") ?? "Data not available";

			Button showFirst = FindViewById<Button> (Resource.Id.showFirst);
			
			showFirst.Click += (sender, e) => 
			{
				var first = new Intent(this, typeof(FirstActivity)); 
				first.PutExtra("Screen2Poke", "Screen 2 poked you"); 
				StartActivity(first);
			};

		}
	}
}