using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

/* Demo app for LCC CIT course CS235AM
 * Written by Brian Bird, Fall 2014
 * Revised Spring 2016 - cleaned up images and moved instandiation of GameLogic out of the click event handler
 * 
 * A very simple app that demonstrates using a FrameLayout for a background image, 
 * dynamically changing images in ImageViews and separating logic from UI code.
 */ 

namespace RpsDemo
{
	[Activity (Label = "Rock, Paper, Scissors", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		GameLogic game = new GameLogic ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.playButton);
			ImageView image = FindViewById<ImageView> (Resource.Id.handImageView);

			// Get a new random hand image
			button.Click += delegate {
				switch(game.ChooseHand())
				{
					case 1:
					image.SetImageResource(Resource.Drawable.Rock);
						break;
					case 2:
					image.SetImageResource(Resource.Drawable.Paper);
						break;
					case 3:
					image.SetImageResource(Resource.Drawable.Scissors);
						break;
					default:
						break;
				}
			};
		}
	}
}


