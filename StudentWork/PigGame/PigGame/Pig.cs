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

namespace PigGame
{
	static class Pig
	{
		static public int sides = 6;
		static public int numberOfDies = 1;
		static public int goal = 100;
		static public int pScore = 0;
		static public int cScore = 0;
		static public int wins = 0;
		static public int games = 0;
		static public int currentRoll = 0;
		static public int totalRoll = 0;
		static public bool playing = false;
		static public bool played = false;
		static public bool canRoll = false;
		static private Random roller = new Random( System.DateTime.Now.Millisecond );

		static public string displayData()
		{
			if ( canRoll )
				return String.Format( "Last roll was \n{0}\nYour total is: {1}\nPlayer score: {2}\nComputer score: {3}\n\nContinue?", currentRoll, totalRoll, pScore, cScore );
			else return String.Format( "Last roll was \n{0}\nYour total is: {1}\nPlayer score: {2}\nComputer score: {3}\n\nComputers turn", currentRoll, totalRoll, pScore, cScore );
		}

		static public string mustStart()
		{
			return "Please press start in the first tab to begin";
		}

		static public string totalPlays()
		{
			return String.Format( "Total games played: {0}", games );
		}

		static public string playerWins()
		{
			return String.Format( "You have won: {0}", wins );
		}

		static public void playNew()
		{
			pScore = cScore = currentRoll = totalRoll = 0;
			playing = played = true;
		}

		static private void rand()
		{
			currentRoll = 1;
			currentRoll += roller.Next( sides );
		}

		static public void roll()
		{
			for ( int i = 0; i < numberOfDies; i++ )
			{
				rand();
				if ( currentRoll % 6 == 1 )
				{
					canRoll = false;
					totalRoll = 0;
				}
				else
					totalRoll += currentRoll;
			}
		}

		static public string rollImageSelect()
		{
			return String.Format( "Die{0}", currentRoll );
		}

		static public void computer()
		{
			totalRoll = 0;

			do
			{
				rand();

				if ( currentRoll % 6 == 1 )
				{
					totalRoll = 0;
					break;
				}
				else
				{
					totalRoll += currentRoll;

					if ( totalRoll > 12 ) break;
				}

			} while (totalRoll != 0);

			cScore += totalRoll;
			totalRoll = 0;
		}

		static public bool winner()
		{
			if ( pScore >= goal || cScore >= goal )
				return true;
			else return false;
		}

		static public string finished()
		{
			if ( pScore >= goal )
				return String.Format( "Congradulations\nYour score is: {0}", pScore );
			return String.Format( "The Computer has won\nWith: {0} points", cScore );
		}
	}
}