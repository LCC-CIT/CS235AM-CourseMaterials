using System;
using RpsLogic;

namespace RpsTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Rps rps = new Rps();
			rps.PlayerChoice = rpsChoice.rock;
			Console.WriteLine ( rps.PlayerChoice.ToString() );
			
			rps.CompChoice = rpsChoice.scissors;
			Console.WriteLine ( rps.CompChoice.ToString() );
			
			// Expect computer to win
			rps.PlayerChoice = rpsChoice.rock;
			rps.CompChoice = rpsChoice.paper;
			Console.WriteLine ( rps.whoWon ().ToString() );
			
			rps.PlayerChoice = rpsChoice.paper;
			rps.CompChoice = rpsChoice.scissors;
			Console.WriteLine ( rps.whoWon ().ToString() );
			
			rps.PlayerChoice = rpsChoice.scissors;
			rps.CompChoice = rpsChoice.rock;
			Console.WriteLine ( rps.whoWon ().ToString() );
			
			// Expect human to win
			rps.PlayerChoice = rpsChoice.paper;
			rps.CompChoice = rpsChoice.rock;
			Console.WriteLine ( rps.whoWon ().ToString() );
			
			rps.PlayerChoice = rpsChoice.scissors;
			rps.CompChoice = rpsChoice.paper;
			Console.WriteLine ( rps.whoWon ().ToString() );
			
			rps.PlayerChoice = rpsChoice.rock;
			rps.CompChoice = rpsChoice.scissors;
			Console.WriteLine ( rps.whoWon ().ToString() );		}
	}
}
