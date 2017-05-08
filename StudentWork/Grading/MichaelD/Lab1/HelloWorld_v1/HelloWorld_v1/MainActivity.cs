using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloWorld_v1
{
	[Activity (Label = "HelloWorld_v1", MainLauncher = true)]
	public class Activity1 : Activity
	{
		//int count = 1;

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

			var resetButton = new Button(this);
			resetButton.Text = "Reset";

			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};

			resetButton.Click += (sender, e) => {
				aLabel.Text = "Hello, Xamarin.Android";
			};


			layout.AddView (aLabel);
			layout.AddView (aButton);
			layout.AddView(resetButton);

			SetContentView (layout);
		}
	}
}


