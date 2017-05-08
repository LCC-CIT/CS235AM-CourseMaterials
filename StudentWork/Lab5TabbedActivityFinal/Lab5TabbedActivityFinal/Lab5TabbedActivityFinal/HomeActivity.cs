
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TabbedWalkThrough
{
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity
	{
		public static TextView currentScore;
		public static TextView gamesPlayed;
		public static TextView winsLosses;
		public static Button playCompBtn;
		public static Button playPlayerBtn;

		public static Pig game;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Home);
			
			// Get references to layout items
			currentScore = FindViewById<TextView>(Resource.Id.CurrentGameScore);
			gamesPlayed = FindViewById<TextView>(Resource.Id.GamesPlayed);
			winsLosses = FindViewById<TextView>(Resource.Id.WinsLosses);
			playCompBtn = FindViewById<Button>(Resource.Id.playCompBtn);
			playPlayerBtn = FindViewById<Button>(Resource.Id.playPlayerBtn);
			
			// Got stuff saved in the bundle? (Make sure there is a game object also)
			if (bundle != null && game != null)
			{
				game.player2.score = bundle.GetInt("compScore");
				game.player1.score = bundle.GetInt("playerScore");
				game.gPlayed = bundle.GetInt("gamesPlayed");
				game.player1.gamesWon = bundle.GetInt("gamesWon");
			}
//============================Event handlers==================================================//
			// Play vs Android
			playCompBtn.Click += (sender, e) => {
				game = new Pig(true);
				var parent = (PigTabActivity)this.Parent;
				parent.TabHost.CurrentTab = 1;
				//updateDisplay();
			};
			// Play vs other player
			playPlayerBtn.Click += (sender, e) => {
				game = new Pig(false);
				var parent = (PigTabActivity)this.Parent;
				parent.TabHost.CurrentTab = 1;
				//updateDisplay();
			};
		}

//============================Save and resume state==================================================//		

		// Save state
		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			if (game != null)
			{
				outState.PutInt ("compScore", game.player2.score);
				outState.PutInt ("playerScore", game.player1.score);
				outState.PutInt ("gamesPlayed", game.gPlayed);
				outState.PutInt ("gamesWon", game.player1.gamesWon);
			}
		}

		// Resume state
		protected override void OnResume()
		{
			base.OnResume();
			// Restore from previous state.
			updateDisplay();
			
		}
//============================ my methods ==================================================//		
		// Update display
		private void updateDisplay()
		{
			// Display current score and game info
			currentScore.Text = GetString(Resource.String.currentGameScore);
			if (game != null)
			{
				currentScore.Text += GetString(Resource.String.playerScore) + game.player1.score.ToString();
				if (game.player2.playerIsComp)
					currentScore.Text += GetString(Resource.String.compScore); 
				else
					currentScore.Text += GetString(Resource.String.player2Score);
				currentScore.Text += game.player2.score.ToString();
				gamesPlayed.Text = GetString(Resource.String.gamesPlayed) + game.gPlayed.ToString();
				winsLosses.Text = GetString(Resource.String.wins) + game.player1.gamesWon.ToString() + ", " + GetString(Resource.String.losses) + (game.gPlayed-game.player1.gamesWon).ToString();
			}
			else // or if no current game, zero the display out
			{
				currentScore.Text += GetString(Resource.String.playerScore) + "0";
				currentScore.Text += GetString(Resource.String.compScore) + "0";
				gamesPlayed.Text =  GetString(Resource.String.gamesPlayed) + "0";
				winsLosses.Text = GetString(Resource.String.wins) + "0, " + GetString(Resource.String.losses) + "0";
			}

		}
	}
}