
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RpsDemo.DynamicFrag
{
	// This fragment contains the UI code for displaying a random hand shape
	public class HandFrag : Fragment
	{

		GameLogic game;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// There's nothing to do here. We didn't need to override this method.
		}

		// This is where you must inflate the fragment from it's axml layout and return it as a View
		// This doesn't incorporate the fragment into an Activity, that has to be done in the Activity
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.handFrag, container, false);

			// Set up the event handler for the play button
			Button playButton = view.FindViewById<Button> (Resource.Id.playButton);
			playButton.Click += delegate 
			{
				// Get a new random number from the game object and use it to choose a hand image
				SetImage(view, game.ChooseHand());
				((MainActivity)this.Activity).setTextFragText();
			};
			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			// Get a reference to the game object in the MainActivity.
			// We need to do it in OnResume so that we get our reference
			// back after the MainActivity has been paused.
			game = ((MainActivity)this.Activity).Game;

			SetImage(this.View, game.HandNumber);	// Set the image to the current hand picture
		}

		/// <summary>
		/// Select a picture of a hand
		/// </summary>
		/// <param name="view">The current View.</param>
		/// <param name="hand">1 = Rock, 2 = Paper, 3 = Scissors.</param>
		private void SetImage(View view, int hand)
		{
			ImageView image = view.FindViewById<ImageView> (Resource.Id.handImageView);
			switch( game.HandNumber)
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
		}
			
	}
}

