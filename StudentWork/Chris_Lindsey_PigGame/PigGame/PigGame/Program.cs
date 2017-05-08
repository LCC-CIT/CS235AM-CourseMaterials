using System;

namespace PigGame
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Pig_Game Game = new Pig_Game("testa", true, "testb", true);

			//for (int i = 0; i < 10; i++)
			//	Console.WriteLine(Game.RollDie().ToString());

			Game.Player1Roll();
			foreach(RollInfo roll in Game.TheRolls)
				Console.WriteLine(roll.ToString());

			Console.WriteLine(Game.Player1Info);
			Console.WriteLine(Game.Player2Info);
			Console.WriteLine(Game.GameInfo);

			Console.ReadKey();
		}
	}
}
