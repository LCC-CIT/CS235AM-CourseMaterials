
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace TabbedWalkThrough
{
	[Activity (Label = "PlayActivity")]			
	public class PlayActivity : Activity
	{
		public static TextView currentScore;
		public static TextView whoseMove;
		public static TextView currentTally;
		public static TextView currentRolls;
		public static ImageView die;
		public static Button roll;
		public static Button pass;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Play);

			// Get references to layout items
			currentScore = FindViewById<TextView>(Resource.Id.CurrentScore);
			whoseMove = FindViewById<TextView>(Resource.Id.WhoseMove);
			currentTally = FindViewById<TextView>(Resource.Id.CurrentTally);
			currentRolls = FindViewById<TextView>(Resource.Id.CurrentRolls);
			die = FindViewById<ImageView>(Resource.Id.Die);
			roll = FindViewById<Button>(Resource.Id.Roll);
			pass = FindViewById<Button>(Resource.Id.Pass);

			// Got stuff saved in the bundle? (also make sure there is a game object)
			if (bundle != null && HomeActivity.game != null)
			{
				HomeActivity.game.wTurn = bundle.GetInt("whoseTurn");
				HomeActivity.game.die.currentRoll = bundle.GetInt("currentRoll");
				if (HomeActivity.game.wTurn == 0)
				{
					HomeActivity.game.player1.rollsThisTurn = bundle.GetInt("rollsThisTurn");
					HomeActivity.game.player1.turnTotal = bundle.GetInt("turnTotal");
					HomeActivity.game.player1.currentTally = bundle.GetInt("currentTally");
				}
				else
				{
					HomeActivity.game.player2.rollsThisTurn = bundle.GetInt("rollsThisTurn");
					HomeActivity.game.player2.turnTotal = bundle.GetInt("turnTotal");
					HomeActivity.game.player2.currentTally = bundle.GetInt("currentTally");
				}
				HomeActivity.game.player1.score = bundle.GetInt("playerScore");
				HomeActivity.game.player2.score = bundle.GetInt("compScore");
				HomeActivity.game.gPlayed = bundle.GetInt("gamesPlayed");
				HomeActivity.game.player1.gamesWon = bundle.GetInt("gamesWon");
			}
//============================Event handlers==================================================//
			// Set handler for the die image
			die.Click += (sender, e) => 
			{
				var rotateImage = AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_centre);
				var turnTest = HomeActivity.game.wTurn; // save whose turn to test for a player switch
				var winner = HomeActivity.game.roll(); // will return 0 for no winner, or 1 for player 1, and 2 for player 2
				// Only animate the die if the turn has not changed or the roll is a 1 
				// this keeps the die from animating after a computer passes (as the die has not been rolled) 
				// unless the pass is due to a 1 roll, in which case animate 
				if (HomeActivity.game.wTurn == turnTest || HomeActivity.game.die.currentRoll == 1)
					die.StartAnimation(rotateImage);
				// calling this with "winner" let's the updateDisplay function customize the text output
				// depending on who, if anyone, won after this roll.
				updateDisplay(winner);  
			};
			// Same as above for the roll button
			roll.Click += (sender, e) => 
			{
				var rotateImage = AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_centre);
				var turnTest = HomeActivity.game.wTurn;
				var winner = HomeActivity.game.roll();
				if (HomeActivity.game.wTurn == turnTest || HomeActivity.game.die.currentRoll == 1)
					die.StartAnimation(rotateImage);
				updateDisplay(winner);
			};
			// Set handler for the pass button (only active during a human player's turn)
			pass.Click += (sender, e) => 
			{
				var winner = HomeActivity.game.pass();
				updateDisplay(winner);
			};
		}

//================= end on create ===================================================================//
//============================Save and resume state==================================================//		
		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			if (HomeActivity.game != null)
			{
				outState.PutInt("whoseTurn", HomeActivity.game.wTurn);
				outState.PutInt("currentRoll", HomeActivity.game.die.currentRoll);
				if (HomeActivity.game.wTurn == 0)
				{
					outState.PutInt("rollsThisTurn", HomeActivity.game.player1.rollsThisTurn);
					outState.PutInt("turnTotal", HomeActivity.game.player1.turnTotal);
					outState.PutInt("currentTally", HomeActivity.game.player1.currentTally);
				}
				else
				{
					outState.PutInt("rollsThisTurn", HomeActivity.game.player2.rollsThisTurn);
					outState.PutInt("turnTotal", HomeActivity.game.player2.turnTotal);
					outState.PutInt("currentTally", HomeActivity.game.player2.currentTally);
				}
				outState.PutInt("playerScore", HomeActivity.game.player1.score);
				outState.PutInt("compScore", HomeActivity.game.player2.score);
				outState.PutInt("gamesPlayed", HomeActivity.game.gPlayed);
				outState.PutInt("gamesWon", HomeActivity.game.player1.gamesWon);
			}
		}

		// Resume state
		protected override void OnResume()

		{
			base.OnResume();

			updateDisplay(0);
		}
//============================ my methods ==================================================//	
		private void setDieImage(int roll)
		{
			switch (roll)
			{
			case 1:
				die.SetImageResource(Resource.Drawable.Die1);
				break;
			case 2:
				die.SetImageResource(Resource.Drawable.Die2);
				break;
			case 3:
				die.SetImageResource(Resource.Drawable.Die3);
				break;
			case 4:
				die.SetImageResource(Resource.Drawable.Die4);
				break;
			case 5:
				die.SetImageResource(Resource.Drawable.Die5);
				break;
			case 6:
				die.SetImageResource(Resource.Drawable.Die6);
				break;
			}
		}

		private void updateDisplay(int winner)
		{
			// Always needed
			currentScore.Text = GetString(Resource.String.currentScore);
			currentScore.Text += GetString(Resource.String.playerScore);
			// If there is a previous game, display it's stats
			if (HomeActivity.game != null)
			{
				// if there is a winner, display the score of the last game until the next player rolls.
				if (winner !=0)
				{
					currentScore.Text += HomeActivity.game.player1.lastScore;
					if (HomeActivity.game.player2.playerIsComp)
						currentScore.Text += GetString(Resource.String.compScore); 
					else
						currentScore.Text += GetString(Resource.String.player2Score);
					currentScore.Text += HomeActivity.game.player2.lastScore;
				}
				else // no winner, display current score
				{
					currentScore.Text += HomeActivity.game.player1.score;
					if (HomeActivity.game.player2.playerIsComp)
						currentScore.Text += GetString(Resource.String.compScore); 
					else
						currentScore.Text += GetString(Resource.String.player2Score);
					currentScore.Text += HomeActivity.game.player2.score;
				}
				// Always display the current tally and number of rolls for the current turn
				currentTally.Text = GetString(Resource.String.currentTally) + (HomeActivity.game.wTurn == 0 ? HomeActivity.game.player1.currentTally : HomeActivity.game.player2.currentTally);
				currentRolls.Text = GetString(Resource.String.currentRolls) + (HomeActivity.game.wTurn == 0 ? HomeActivity.game.player1.rollsThisTurn : HomeActivity.game.player2.rollsThisTurn);

				whoseMove.Text = ""; // clear for the following...
				// This inserts the winner before saying whose turn it is, if there was a winner this roll.
				if (winner == 1)
					whoseMove.Text = GetString(Resource.String.youWon);
				else if (winner == 2)
				{
					if (HomeActivity.game.player2.playerIsComp)
						whoseMove.Text = GetString(Resource.String.compWon);
					else
						whoseMove.Text = GetString(Resource.String.p2Won);
				}
				// Isn't this next bit fun? It's a double ternary! Or a sextary?
				// Anyway, this displays whose turn it is, taking into account whether player 2 is the computer or another player
				whoseMove.Text += (HomeActivity.game.wTurn == 0 ? 
				                  GetString(Resource.String.playerMove) : 
				                  (HomeActivity.game.player2.playerIsComp ? 
				 							GetString(Resource.String.compMove) : 
				 							GetString(Resource.String.player2Move)));
				// Seems obvious, yes?
				setDieImage(HomeActivity.game.die.currentRoll);
				// Set the state and text of the buttons
				if (HomeActivity.game.wTurn == 1)
				{
					roll.Enabled = true;
					die.Enabled = true;
					if (HomeActivity.game.player2.playerIsComp)
					{
						roll.Text = GetString (Resource.String.rollc);
						pass.Enabled = false;
					}
					else
					{
						roll.Text = GetString (Resource.String.roll2);
						pass.Enabled = true;
					}
				}
				else if (HomeActivity.game.wTurn == 0)
				{
					pass.Enabled = true;
					roll.Enabled = true;
					die.Enabled = true;
					roll.Text = GetString (Resource.String.roll1);
				}
			}
			// No current game object? Disable buttons and display default text 
			// with instructions to start a new game from the home page.
			else
			{
				currentScore.Text += "0";
				currentScore.Text += GetString(Resource.String.compScore) + "0";
				currentTally.Text = GetString(Resource.String.currentTally) + "0";
				currentRolls.Text = GetString(Resource.String.currentRolls) + "0";
				whoseMove.Text = GetString (Resource.String.stNewGame);
				setDieImage (1);
				pass.Enabled = false;
				roll.Enabled = false;
				die.Enabled = false;
			}
		}



	}
}
