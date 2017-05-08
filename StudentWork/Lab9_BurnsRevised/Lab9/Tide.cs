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

namespace Lab9
{
	public class Tide
	{
		// Moved Date/Day to Date class

		public string time {get; set; }

		public float heightF {get; set; }
		public float heightC {get; set; }

		public char tideType {get; set; }
	}

	public class Date
	{
		public string date {get; set; }
		public string day {get; set; }

		public List<Tide> theTide;

		public Date(string newDate, string newDay)
		{
			date = newDate;
			day = newDay;
		}

		public void addTide(Tide tide)
		{
			if(theTide == null)
				theTide = new List<Tide>();

			theTide.Add (tide);
		}
	}




}

