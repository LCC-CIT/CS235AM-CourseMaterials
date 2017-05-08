
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

namespace RpsTabDemo
{
	public class HandFrag : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.handFrag, container, false);

			ImageView image = view.FindViewById<ImageView> (Resource.Id.handImageView);
			image.SetImageResource(Resource.Drawable.Rock);

			// Get a new random hand image
			Button playButton = view.FindViewById<Button> (Resource.Id.playButton);
			playButton.Click += delegate {
				GameLogic game = new GameLogic ();
				int handShape = game.ChooseHand();
				switch(handShape)
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

				// Get this fragment's ID


				// Call a method on the hosting Activity
				((MainActivity)this.Activity).AnnounceWinner(this.Id, handShape);
			};


			return view;
		}
	}
}

