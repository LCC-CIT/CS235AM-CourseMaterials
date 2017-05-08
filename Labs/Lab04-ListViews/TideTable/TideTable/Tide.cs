// Brian Bird, 4/30/13

using System;

namespace TideTable
{
	public class Tide
	{
		public DateTime DateAndTime {get; set;}
		public float Height {get; set;}
		public bool High {get; set;}

		public string GetHeight()
		{
			return (High ? "High" : "Low") + ": " + Height.ToString () + " feet";
		}

		public string GetTideTime()
		{
			return (High ? "High" : "Low") + ": " + DateAndTime.TimeOfDay.ToString();
		}

		public string GetDateAndDay()
		{
			return DateAndTime.Date .ToString("D");
		}
	}
}

