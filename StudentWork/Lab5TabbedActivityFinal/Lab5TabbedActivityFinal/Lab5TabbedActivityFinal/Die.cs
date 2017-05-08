
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TabbedWalkThrough
{
	public class Die
	{
		private Random rnd;
		public int currentRoll {get; set;}

		public Die()
		{
			rnd = new Random();
		}
		public int rollTheDie()
		{
			currentRoll = rnd.Next (1,7);
			return currentRoll;
		}
	}
}

