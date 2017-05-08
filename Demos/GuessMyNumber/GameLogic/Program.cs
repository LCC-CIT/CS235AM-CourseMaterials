using System;

namespace GameLogic
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			GuessNumber num = new GuessNumber();
			Console.WriteLine (num.Number.ToString());
		}
	}
}
