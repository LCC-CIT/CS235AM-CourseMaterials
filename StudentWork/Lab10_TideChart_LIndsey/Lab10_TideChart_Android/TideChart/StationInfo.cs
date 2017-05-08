using System;
using System.Xml;

namespace TideChart
{
	public class TideInfo
	{
		public string Date {get; private set;}
		public string Day {get; private set;}
		public string Time {get; private set;}
		public float PredictionInFt {get; private set;}
		public float PredictionInCm {get; private set;}
		public string HighLow {get; private set;}

		public TideInfo(string date, string day, string time, float ft, float cm, string hl)
		{
			Date = date;
			Day = day;
			Time = time;
			PredictionInFt = ft;
			PredictionInCm = cm;
			HighLow = hl;
		}
	}

	public class StationInfo
	{
		public string StationName {get; private set;}
		public int StationID {get; private set;}

		public StationInfo(string name, int id)
		{
			StationName = name;
			StationID = id;
		}
	}
}

