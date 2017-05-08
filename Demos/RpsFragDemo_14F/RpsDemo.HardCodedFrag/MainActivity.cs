using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RpsDemo.HardCodedFrag
{
	[Activity (Label = "Rock, Paper, Scissors", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			// Only the single pane layout (portrait) has a translate button
			Button translateButton = FindViewById<Button> (Resource.Id.translateButton);
			bool isDualPane = false;
			if (translateButton == null)
				isDualPane = true;

			Button playButton = FindViewById<Button> (Resource.Id.playButton);
			ImageView image = FindViewById<ImageView> (Resource.Id.handImageView);

			string handPositionName = "Paper";	// "Paper" matches the default hand image

			// Get a new random hand image
			playButton.Click += delegate {
				GameLogic game = new GameLogic ();
				switch(game.ChooseHand())
				{
					case 1:
					image.SetImageResource(Resource.Drawable.Rock);
					handPositionName = "Rock";
						break;
					case 2:
					image.SetImageResource(Resource.Drawable.Paper);
					handPositionName = "Paper";
						break;
					case 3:
					image.SetImageResource(Resource.Drawable.Scissors);
					handPositionName = "Scissors";
						break;
					default:
						break;
				}
				if (isDualPane)
					setTextFragText(handPositionName);
			};

			if (!isDualPane) {
				translateButton.Click += delegate(object sender, EventArgs e) {
					var intent = new Intent ();
					intent.SetClass (this, typeof(TranslateActivity));
					intent.PutExtra ("hand_position_name", handPositionName);
					StartActivity (intent);
				};
			} else {
				setTextFragText (handPositionName);
			}
		
		}

		private void setTextFragText(string handName)
		{
			var text = FindViewById<TextView> (Resource.Id.handTextView);
			text.Text = handName;
		}
	}
}


