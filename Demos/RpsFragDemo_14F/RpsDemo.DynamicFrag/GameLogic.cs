using System;

namespace RpsDemo.DynamicFrag
{
	public class GameLogic
	{
		private Random rand = new Random();
		private int handShape = 1;	// Rock is the default
		//TODO: Use an enum to represent the hand shape

		/// <summary>
		/// Randomly chooses a number to represent a hand shape.
		/// </summary>
		/// <returns>1 for rock, 2 for paper, 3 for scissors.</returns>
		public int ChooseHand()
		{
			handShape = rand.Next (1, 4);
			return handShape;
		}

		/// <summary>
		/// Read-only property for the hand number.
		/// </summary>
		/// <value>1 = Rock, 2 = Paper, 3 = Scissors</value>
		public int HandNumber
		{
			get { return handShape; }
		}

		/// <summary>
		/// Read-only property for the name of the hand shape
		/// </summary>
		public string HandName 
		{
			get 
			{
				string name = "none";
				switch (handShape) 
				{
					case 1:
						name = "Rock";
						break;
					case 2:
						name = "Paper";
						break;
					case 3:
						name = "Scissors";
						break;
					default:
						name = "Ooops!";	// we should never get here
						break;
				}
				return name;
			}
		}
	}
}

