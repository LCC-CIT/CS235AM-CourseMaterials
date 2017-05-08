using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RpsLogic;

namespace RockPaperScissors
{
	[Activity (Label = "RockPaperScissors", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button rockButton = FindViewById<Button> (Resource.Id.rockButton);

			var rps = new RpsLogic.Rps();
			rockButton.Click += delegate 
			{
				//rps.PlayerChoice = rpsChoice.scissors;
				var choice = new Intent(this, typeof(PlayActivity));
				choice.PutExtra ("PlayerChoice", rpsChoice.scissors);
				StartActivity (choice);
			};
		}
	}
}


