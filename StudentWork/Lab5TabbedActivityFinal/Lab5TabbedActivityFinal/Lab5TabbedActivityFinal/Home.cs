using System;

namespace TabbedWalkThrough
{
	public enum rpsChoice {rock, paper, scissors, none};
	public enum Winner {Player, Computer, Tie};

	public class Home
	{
		public rpsChoice playerChoice = rpsChoice.none;
		public rpsChoice compChoice = rpsChoice.none;
		public Random rnd = new Random();
		public int pWins = 0;
		public int cWins = 0;
		public int ties = 0;
		public int imgHt;

		public void PlayerChoose(rpsChoice ch) {
			playerChoice = ch;
		}
		public void ComputerChoose() {
			compChoice = (rpsChoice)rnd.Next(0,3);
		}

		public Winner whoWon()
		{
			if (compChoice == playerChoice) {
				ties++;
				return Winner.Tie;
			}
			else if (compChoice - playerChoice == 1 || compChoice - playerChoice == -2) {
				cWins++;
				return Winner.Computer;
			}
			else {
				pWins++;
				return Winner.Player;
			}
		}

	}
}

