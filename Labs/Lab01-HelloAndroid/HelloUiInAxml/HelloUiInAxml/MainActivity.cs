using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloTutorial2
{
	[Activity (Label = "HelloTutorial2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);
			var aButton = FindViewById<Button> (Resource.Id.aButton);
			var resetButton = FindViewById<Button> (Resource.Id.resetButton);
			var aLabel = FindViewById<TextView> (Resource.Id.helloLabel);
			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};
			resetButton.Click += (sender, e) => {
				aLabel.SetText(Resource.String.helloLabelText);
			};
		}
	}
}


