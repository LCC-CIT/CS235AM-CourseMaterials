using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TabbedWalkThrough
{
	public class Pig
	{
		public int wTurn {get; set;}
		public int gPlayed {get; set;}
		public Die die;
		public Player player1;
		public Player player2;

		public Pig(bool player2isComp)
		{
			wTurn = 0; 
			gPlayed = 0;
			die = new Die();
			player1 = new Player(false, 1);
			player2 = new Player(player2isComp, 2);
		}
		public void setNextPlayer()
		{
			if (wTurn == 0)
				wTurn = 1;
			else
				wTurn = 0;
		}

		public int roll()
		{
			var didWin = 0;
			if (wTurn == 0)
			{
				didWin = player1.playerRoll();
			}
			else
			{
				didWin = player2.playerRoll();
			}
			return didWin;
		}


		public int pass()
		{
			var didWin = 0;
			if (wTurn == 0)
			{
				didWin = player1.playerPass();
			}
			else
			{
				didWin = player2.playerPass();
			}
			return didWin;
		}
	}
}

