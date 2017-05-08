using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Lab2
{
	[Activity (Label = "Lab2", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
			// Get our button from the layout resource,
			// and attach an event to it
			Button showSecond = FindViewById<Button> (Resource.Id.showSecond);

			var label = FindViewById<TextView> (Resource.Id.screen1Label);
			label.Text = Intent.GetStringExtra("SecondData") ?? "Data not available";

			showSecond.Click += (sender, e) => {
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra ("FirstData", "Screen 1 Poked You!");
				StartActivity(second);
			};
		}
	}
}


