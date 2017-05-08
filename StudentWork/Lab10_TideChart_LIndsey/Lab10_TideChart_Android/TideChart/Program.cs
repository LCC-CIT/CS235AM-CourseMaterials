using System;
using System.Net;
using System.Collections.Generic;
using SQLite;
using System.IO;

namespace TideChart
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TideStations test = new TideStations();
			foreach (StationInfo station in test.Stations)
			{
				Console.WriteLine(station.StationID + " - " + station.StationName);
			}
			Console.WriteLine(test.Stations.Count);
			Console.ReadKey();

			int index = 0;

			List<DayInfo> testTides = test.Stations[index].TheDays;
			foreach (DayInfo day in testTides)
			{
				if (day.Day == DayOfWeek.Sunday.ToString())
				{
					Console.WriteLine(day.Date+ "," +day.Day);
					foreach (TideInfo tide in day.TheTides)
						Console.WriteLine("\t" + tide.Time +","+ tide.PredictionInFt +","+ tide.PredictionInCm +","+ tide.HighLow);
				}
			}
			Console.WriteLine("Total days: " + testTides.Count + ", Should Be " + 365);
			Console.ReadKey();
		}
	}
}
