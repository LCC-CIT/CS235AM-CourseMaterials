/* Demo of ActionBar tabs and the Up button
 * Written by Brian Bird
 * 11/14/14
 */

using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RpsTabDemo
{
	[Activity (Label = "RpsTabDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		// Keep track of each palyer's turn
		int playerOnePlayed = 0;
		int playerTwoPlayed = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			// Set up the "one player' tab
			var onePlayerTab = ActionBar.NewTab();
			onePlayerTab.SetText("One Player");
			onePlayerTab.SetIcon (Resource.Drawable.Icon);		
			onePlayerTab.TabSelected += 
				delegate(object sender, ActionBar.TabEventArgs e) 
			{
				// load an instance of HandFrag into the top pane- for the one player
				var fragment1 = this.FragmentManager.FindFragmentById(Resource.Id.fragContainer1);
				if (fragment1 == null)	// Only load it if it isn't there already
					e.FragmentTransaction.Add(Resource.Id.fragContainer1, new HandFrag());

				// Make sure there's nothing in the bottom pane
				var fragment2 = this.FragmentManager.FindFragmentById(Resource.Id.fragContainer2);
				if (fragment2 != null)
					e.FragmentTransaction.Remove(fragment2);  
			};
			ActionBar.AddTab (onePlayerTab);

			// Set up the "two player' tab
			var twoPlayerTab = ActionBar.NewTab();
			twoPlayerTab.SetText("Two Player");
			twoPlayerTab.SetIcon (Resource.Drawable.Icon);		
			twoPlayerTab.TabSelected += 
				delegate(object sender, ActionBar.TabEventArgs e) 
			{
				// load an instance of HandFrag into the top pane- for the first player
				var fragment1 = this.FragmentManager.FindFragmentById(Resource.Id.fragContainer1);
				if (fragment1 == null)
					e.FragmentTransaction.Add(Resource.Id.fragContainer1, new HandFrag());

				// load an instance of HandFrag into the bottom pane- for the second player
				var fragment2 = this.FragmentManager.FindFragmentById(Resource.Id.fragContainer2);
				if (fragment2 == null)
					e.FragmentTransaction.Add(Resource.Id.fragContainer2, new HandFrag());
			};
			ActionBar.AddTab (twoPlayerTab);
		}

		// Called by Play button event on the HandFrag
		public void AnnounceWinner(int fragId, int handShape)
		{
			// check to see if both play buttons have been pressed
			if (fragId == Resource.Id.fragContainer1)
				playerOnePlayed = handShape;
			else
				playerTwoPlayed = handShape;

			if ((playerOnePlayed != 0) && (playerTwoPlayed != 0)) 
			{
				var intent = new Intent ();
				intent.SetClass (this, typeof(WinnerActivity));
				intent.PutExtra ("winning_hand_position", 
					GameLogic.DetermineWinner(playerOnePlayed, playerTwoPlayed));
				StartActivity (intent);

				playerOnePlayed = 0;
				playerTwoPlayed = 0;
			}
		}
	}
}


