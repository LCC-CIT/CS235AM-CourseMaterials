using System;

namespace RpsTabDemo
{
	public class GameLogic
	{
		Random rand = new Random();

		/// <summary>
		/// Randomly chooses a number to represent a hand position.
		/// </summary>
		/// <returns>1 for rock, 2 for paper, 3 for scissors.</returns>
		public int ChooseHand()
		{
			//TODO: Use an enum to represent the hand position
			return rand.Next(1, 4);
		}

		public static string DetermineWinner(int handShape1, int handShape2)
		{
			/*  1 beats 3  (rock beats scissors)
			 *  2 beats 1  (paper beats rock)
			 *  3 beats 2  (scissors beats paper)
			*/
			int winner = 0;

			if (handShape1 == handShape2) // Check for a tie
				winner = handShape1;
			else if (handShape1 == 1 && handShape2 == 3) // Check for rock beating scissors
				winner = handShape1; // Rock
			else if (handShape1 > handShape2) // paper beats rock, scissors beats paper
				winner = handShape1;
			else
				winner = handShape2;  // if handShape1 didn't win, then 2 did

			if (winner == 1)
				return "Rock";
			if (winner == 2)
				return "Paper";
			if (winner == 3)
				return "Scissors";

			return "none";
		}

	}
}

