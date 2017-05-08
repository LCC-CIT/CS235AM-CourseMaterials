/*
 	Modify the default Activity
	
	Change it's button title to "Poke screen 2"
    Add an Intent that sends the message "Screen 1 poked you" to screen 2.
    Add an appropriate event handler to the button.
    Make any changes necessary so the TextView can display a message from screen 2.

*/
using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiScreen
{
	[Activity (Label = "FirstActivity", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		// Const strings for "good programming practices"
		const string secondActivity = "Poke";
		const string secondActivityText = "Screen 2 poked you";
		const string errorText = "Nothing to display yet.";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button showSecond = FindViewById<Button> (Resource.Id.showSecond);
			
			showSecond.Click += (sender, e) => {
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra(secondActivity, secondActivityText);
				StartActivity(second);
			};

			TextView label = FindViewById<TextView> (Resource.Id.screen1Label);
			label.Text = Intent.GetStringExtra(secondActivity) ?? errorText;
		}
	}
}


