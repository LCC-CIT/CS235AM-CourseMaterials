using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloMultiScreen
{
	[Activity (Label = "Screen 1", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var label = FindViewById<TextView> (Resource.Id.screen1Label); 
			label.Text = Intent.GetStringExtra("Screen2Poke") ?? "Data not available";

			// Get our button from the layout resource,
			// and attach an event to it
			Button showSecond = FindViewById<Button> (Resource.Id.showSecond);
			
			showSecond.Click += (sender, e) => 
			{
				var second = new Intent(this, typeof(SecondActivity)); 
				second.PutExtra("Screen1Poke", "Screen 1 poked you"); 
				StartActivity(second);
			};
		}
	}
}


