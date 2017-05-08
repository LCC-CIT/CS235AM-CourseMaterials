using System;

namespace RpsDemo
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

	}
}

