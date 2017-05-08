using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RpsDemo
{
	[Activity (Label = "Rock, Paper, Scissors", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.playButton);
			ImageView image = FindViewById<ImageView> (Resource.Id.handImageView);

			// Get a new random hand image
			button.Click += delegate {
				GameLogic game = new GameLogic ();
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


