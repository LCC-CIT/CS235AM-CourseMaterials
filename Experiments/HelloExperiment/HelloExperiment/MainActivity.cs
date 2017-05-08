using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloExperiment
{
	[Activity (Label = "HelloExperiment", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			var aButton = FindViewById<Button>(Resource.Id.aButton);
			var aLabel = FindViewById<TextView>(Resource.Id.helloLabel);

			aButton.Click += (object sender, EventArgs e) => {
				aLabel.Text = "Hello from the button";
			};

		}
	}
}


