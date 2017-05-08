using System;

namespace RpsLogic
{
	public enum rpsChoice {rock, paper, scissors, none};
	public enum player {human, computer, both, none};
	
	public class Rps
	{
		rpsChoice playerChoice = rpsChoice.none;
		rpsChoice compChoice = rpsChoice.none;
		Random rnd = new Random();
		
		public rpsChoice PlayerChoice
		{
			get{ return playerChoice;}
			set{playerChoice = value;}
		}
		
		public rpsChoice CompChoice
		{
			get{ return compChoice;}
			set{compChoice = value;}
		}
		
		public player whoWon()
		{
			compChoice = (rpsChoice)rnd.Next(0,3);
			player winner = player.none;
			if (compChoice == playerChoice)
				winner = player.both;
			//else if( ((int)playerChoice - (int)compChoice) % 3 == 1 )
			else if (compChoice - playerChoice == 1 ||
			         compChoice - playerChoice == -2)
				winner = player.computer;
			else
				winner = player.human;
			return winner;
		}
	}
}

