using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloTutorial1
{
	[Activity (Label = "HelloTutorial1", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//Create the user interface in code
			var layout = new LinearLayout (this);
			layout.Orientation = Orientation.Vertical;
			var aLabel = new TextView (this);
			aLabel.SetText (Resource.String.helloLabelText);
			var aButton = new Button (this);
			aButton.SetText (Resource.String.helloButtonText);
			var resetButton = new Button (this);
			resetButton.SetText (Resource.String.resetButtonText);
			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};
			resetButton.Click += (sender, e) => {
				aLabel.SetText(Resource.String.helloLabelText);
			};
			layout.AddView (aLabel);
			layout.AddView (aButton);
			layout.AddView (resetButton);
			SetContentView (layout);
		}
	}
}


