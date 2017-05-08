
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
	public class Player
	{
		public int score {get; set;}
		public int lastScore {get; set;}
		public int rollsThisTurn {get; set;}
		public int currentTally {get; set;}
		public int turnTotal {get; set;}
		public int gamesWon {get; set;}
		public bool playerIsComp {get; set;}
		public int playerNumber {get; set;}

		// "true" passed in for isComp for a vs Android game
		// The "pl" argument is automatically set to 1 for player 1 and 
		// 2 for player 2 when the game object instantiates the player objects
		public Player(bool isComp, int pl)
		{
			score = 0;
			rollsThisTurn = 0;
			currentTally = 0;
			turnTotal = 0;
			gamesWon = 0;
			playerIsComp = isComp;
			playerNumber = pl;
		}

		public int playerRoll()
		{
			// this will be set to a player number if a player wins this roll
			// (only the computer can win here because only the computer can "decide" to pass
			// when the roll button is pressed, and you can only win on a pass
			var didWin = 0; 
			// So here we let the computer decide what to do, roll or pass, if this player is a computer player, of course
			// This is the "AI" such as it is. The computer will roll a minimum of three times. 
			// If, after three rolls, the average of the rolls is 4 or better, pass
			// or if the computer has a winning score, pass
			// but whatever you do, don't roll more than 6 times, so pass if you've rolled 6 times... period.
			if (this.playerIsComp && (this.rollsThisTurn > 5 || (this.rollsThisTurn > 2 && this.currentTally/this.rollsThisTurn >= 4) || this.score + this.currentTally > 99))
			    didWin = this.playerPass();
			else 
			{
				var roll = HomeActivity.game.die.rollTheDie(); // roll the die
				this.rollsThisTurn++; // add 1 to rollsThisTurn
				if (roll == 1) // tough luck. reset for next turn
				{
					this.currentTally = 0;
					this.rollsThisTurn = 0;
					this.turnTotal = 0;
					HomeActivity.game.setNextPlayer();
				}
				else // Update the tally
				{
					this.currentTally += roll; 
				}
			}
			return didWin;
		}

		public int playerPass()
		{
			var didWin = 0; // this will be set to a player number if a player wins this turn

			this.score += this.currentTally; // Update the player's score
			this.turnTotal = this.currentTally; // save the total for the turn
			this.currentTally = 0; // reset for next turn
			this.rollsThisTurn = 0; // ditto
			if (this.score > 99) // Did ya win?
			{
				didWin = this.playerNumber; // return the winning player number
				this.gamesWon++; // increment this player's games won count
				HomeActivity.game.player1.lastScore = HomeActivity.game.player1.score; // save last scores
				HomeActivity.game.player2.lastScore = HomeActivity.game.player2.score;
				HomeActivity.game.player1.score = 0; // reset scores for next game
				HomeActivity.game.player2.score = 0;
				HomeActivity.game.gPlayed++; // increment gamesPLayed
				HomeActivity.game.setNextPlayer();
			}
			else 
			{
				HomeActivity.game.setNextPlayer();
			}
			return didWin;
		}
	}
}

