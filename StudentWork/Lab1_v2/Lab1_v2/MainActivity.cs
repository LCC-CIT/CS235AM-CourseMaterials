using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Lab1_v2
{
	[Activity (Label = "Lab1_v2", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);

			Button aButton = FindViewById<Button> (Resource.Id.aButton);
			TextView aLabel = FindViewById<TextView> (Resource.Id.helloLabel);

			// Create the new reset button
			Button bButton = FindViewById<Button> (Resource.Id.bButton);

			// Call the button directly from the resource without calling a Button
			//(FindViewById<Button> (Resource.Id.aButton)).Click += (sender, e) => {
			//	aLabel.Text = "Hello from the button";
			//};

			// Reset the text using a hardcoded string
			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};

			// Reset the text using a hardcoded string
			bButton.Click += (sender, e) => {
				//aLabel.Text = "Hello Xamarin.Android";
				// Add it from the string directly
				aLabel.SetText(Resource.String.helloLabelText);
			};

		}
	}
}


