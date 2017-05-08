//Project made by Alex C. Way


/*
This lab will allow you to practice what you learned in the Multiscreen tutorial that we did in class. Create a new Xamarin Studio Solution and add the following.

    Modify the default Activity
        Change it's button title to "Poke screen 2"
        Add an Intent that sends the message "Screen 1 poked you" to screen 2.
        Add an appropriate event handler to the button.
        Make any changes necessary so the TextView can display a message from screen 2.
    Add a second Activity.
        Add a button and a TextView to the second activity
        The title on the this button should be "Poke screen 1"
        Use an intent to send the message "Screen 2 poked you" to screen 1.
        Add an event handler for the button and any other code necessary so that clicking on a button sends a message to the ohter screen and displays it.

Zip the solution and upload it to moodle.
*/


using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Poke_a_Screen
{
	[Activity (Label = "Screen 1", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Use UI created in Main.axml
			SetContentView (Resource.Layout.Main);
			var showSecond = FindViewById<Button> (Resource.Id.showSecond);

			//Button to go to and "poke" Screen 2 (SecondActivity):
			//Send data to and go to screen 2 when button is clicked:
			showSecond.Click += delegate {
				var second = new Intent(this, typeof(SecondActivity));
				
				second.PutExtra("FirstData", "Screen 1 poked you");
				
				StartActivity (second);
			};

			//Retrieve any data passed from Screen 2:
			var label = FindViewById<TextView> (Resource.Id.screen1Label);
			//Display any data passed from Screen 2:
			label.Text = Intent.GetStringExtra("SecondData") ?? "Data not available"; //Another form of a true or false statement.
		} 
	}
}
