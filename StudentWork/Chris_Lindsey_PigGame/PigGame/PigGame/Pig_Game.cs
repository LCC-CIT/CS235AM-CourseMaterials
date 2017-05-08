using System;
using System.Collections.Generic;

namespace PigGame
{
	public class ScoreSheet
	{
		public string Player1 {get; private set;}
		public string Player2 {get; private set;}
		public int Player1Score {get; private set;}
		public int Player2Score {get; private set;}
		public int Player1Wins {get; private set;}
		public int Player2Wins {get; private set;}

		public ScoreSheet(string p1, string p2)
		{
			Player1 = p1;
			Player2 = p2;
		}

		public string Player1Info
		{
			get
			{
				return string.Format("{0}total score:{1}total wins:{2}",Player1.PadRight(30),Player1Score.ToString().PadRight(6), Player1Wins.ToString().PadRight(3));
			}
		}

		public string Player2Info
		{
			get
			{
				return string.Format("{0}total score:{1}total wins:{2}",Player2.PadRight(30),Player2Score.ToString().PadRight(6), Player2Wins.ToString().PadRight(3));
			}
		}

		public void SetPlayer1Stats(int score, bool won)
		{
			Player1Score += score;
			if (won)
				Player1Wins++;
		}

		public void SetPlayer2Stats(int score, bool won)
		{
			Player2Score += score;
			if (won)
				Player2Wins++;
		}
	}

	public class RollInfo
	{
		public string Name{get; private set;}
		public int Roll{get; private set;}
		public int Round{get; private set;}

		public RollInfo(string name, int roll, int round)
		{
			Name = name;
			Roll = roll;
			Round = round;
		}

		public override string ToString ()
		{
			return string.Format ("     {0}, Round:{1}", Name.PadRight(30), Round);
		}
	}

	public class Pig_Game
	{
		public bool GameOver {get; private set;}

		public int WinningTotal {get{return 100;}}

		private Random rand;
		public string GameInfo {get; private set;}
		public List<RollInfo> TheRolls {get; private set;}
		public bool Player1Turn {get; private set;}
		public bool Player2Turn {get {return !Player1Turn;}}
		public int Player1Total {get; private set;}
		public int Player2Total {get; private set;}
		public string Player1Name {get; private set;}
		public string Player2Name {get; private set;}

		public bool Player1CanContinue {get; private set;}
		public bool Player2CanContinue {get; private set;}

		private int Player1RoundTotal;
		private int Player2RoundTotal;

		public int RoundNumber {get; private set;}

		private bool Player1Computer;
		private bool Player2Computer;

		public string Player1Info {get{ return Player1Name + ", Total = " + Player1Total;}}
		public string Player2Info {get{ return Player2Name + ", Total = " + Player2Total;}}
	
		public Pig_Game(string p1, bool p1comp, string p2, bool p2comp)
		{
			GameOver = false;
			rand = new Random(DateTime.Now.Millisecond);
			Player1Name = p1;
			Player2Name = p2;
			Player1Computer = p1comp;
			Player2Computer = p2comp;

			TheRolls = new List<RollInfo>();

			Player1Turn = true;
			Player1CanContinue = true;
			Player2CanContinue = false;
		}

		private int RollDie()
		{
			return (rand.Next() % 6) + 1;
		}

		public void Player1Roll()
		{
			if (Player1Turn && Player1CanContinue)
			{
				if (Player1Computer)
				{
					int count = -1;
					int roll = 7;
					while(true)
					{
						roll = RollDie();
						TheRolls.Add(new RollInfo(Player1Name, roll, RoundNumber));
						Player1RoundTotal += roll;

						if (roll == 1)
							break;

						if (roll > 4) count += 2;
						else count++;

						if (rand.Next() % 10 < count)
							break;

						if (Player1Total + Player1RoundTotal >= WinningTotal)
							break;
					}

					if (roll == 1)
					{
						Player1RoundTotal = 0;
					}
					Player1Hold();
				}
				else
				{
					int roll = RollDie();
					TheRolls.Add(new RollInfo(Player1Name, roll, RoundNumber));
					if (roll == 1)
					{
						Player1RoundTotal = 0;
						Player1CanContinue = false;
					}
					else
					{
						Player1RoundTotal += roll;
					}
				}
			}
		}

		public void Player2Roll()
		{
			if (Player2Turn && Player2CanContinue)
			{
				if (Player2Computer)
				{
					int count = -1;
					int roll = 7;
					while(true)
					{
						roll = RollDie();
						TheRolls.Add(new RollInfo(Player2Name, roll, RoundNumber));
						Player2RoundTotal += roll;
						
						if (roll == 1)
							break;
						
						if (roll > 4) count += 2;
						else count++;
						
						if (rand.Next() % 10 < count)
							break;

						if (Player2Total + Player2RoundTotal >= WinningTotal)
							break;
					}
					
					if (roll == 1)
					{
						Player2RoundTotal = 0;
					}
					Player2Hold();
				}
				else
				{
					int roll = RollDie();
					TheRolls.Add(new RollInfo(Player2Name, roll, RoundNumber));
					if (roll == 1)
					{
						Player2RoundTotal = 0;
						Player2CanContinue = false;
					}
					else
					{
						Player2RoundTotal += roll;
					}
				}
			}
		}

		public void Player1Hold()
		{
			Player1CanContinue = false;
			Player1Total += Player1RoundTotal;
			if (Player1Total >= WinningTotal)
			{
				GameOver = true;
				GameInfo = Player1Name + " has won the game!";
			}
			else
			{
				Player1RoundTotal = 0;
				Player1Turn = false;
				Player2CanContinue = true;
				Player2Roll();
			}
		}

		public void Player2Hold()
		{
			RoundNumber++;
			Player2CanContinue = false;

			Player2Total += Player2RoundTotal;
			if (Player2Total >= WinningTotal)
			{
				GameOver = true;
				GameInfo = Player2Name + " has won the game!";
			}
			else
			{
				Player2RoundTotal = 0;
				Player1Turn = true;
				Player1CanContinue = true;
				Player1Roll();
			}
		}
	}
}