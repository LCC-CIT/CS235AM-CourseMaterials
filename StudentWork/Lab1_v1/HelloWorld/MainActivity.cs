using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloWorld
{
	[Activity (Label = "HelloWorld", MainLauncher = true)]
	public class Activity1 : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//Create the user interface in code
			var layout = new LinearLayout (this);

			layout.Orientation = Orientation.Vertical;
			var aLabel = new TextView (this);
			aLabel.Text = "Hello, Xamarin.Android";

			var aButton = new Button (this);
			aButton.Text = "Say Hello";

			// New button
			var bButton = new Button (this);
			// New button text
			bButton.Text = "Reset";

			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};

			// When we click the new reset button, reset the message that aButton changed
			// back to "Hello, Xamarin.Android"
			bButton.Click += (sender, e) => {
				aLabel.Text = "Hello, Xamarin.Android";
			};

			layout.AddView (aLabel);
			layout.AddView (aButton);

			// Load the new reset button
			layout.AddView (bButton);

			SetContentView (layout);
		}
	}
}


