using System;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TideChart
{
	#region 
	public class TideInfo
	{
		public string Time {get; private set;}
		public float PredictionInFt {get; private set;}
		public float PredictionInCm {get; private set;}
		public string HighLow {get; private set;}

		public TideInfo(string time, float ft, float cm, string hl)
		{
			Time = time;
			PredictionInFt = ft;
			PredictionInCm = cm;
			HighLow = hl;
		}

		public override string ToString ()
		{
			return string.Format ("{0}, Ft={1}, Cm={2}, {3}", Time, PredictionInFt, PredictionInCm, (HighLow.Equals("h")) ? "High Tide" : "Low Tide");
		}
	}
	#endregion

	#region DayInfo Class
	public class DayInfo
	{
		public string Date {get; private set;}
		public string Day {get; private set;}

		List<TideInfo> _TheTides;
		public List<TideInfo> TheTides
		{
			get
			{
				return _TheTides;
			}
		}

		public DayInfo(string date, string day, string time, float ft, float cm, string hl)
		{
			Date = date;
			Day = day;

			_TheTides = new List<TideInfo>();
			_TheTides.Add(new TideInfo(time, ft, cm, hl));
		}

		public void AddTideInfo(TideInfo tide)
		{
			_TheTides.Add(tide);
		}
	}
	#endregion

	#region StationInfo Class
	public class StationInfo
	{
		//constant strings for getting station tide info
		const string TideInfoURL = @"http://140.90.78.215/noaatidepredictions/NOAATidesFacade.jsp?";

		const string File = @"";

		//Station Info
		public string StationName {get; private set;}
		public int StationID {get; private set;}

		public string[] ListOfDays
		{
			get 
			{
				List<DayInfo> temp = TheDays;
				List<string> days = new List<string> ();

				foreach (DayInfo day in temp) 
				{
					days.Add (day.Day + " " + day.Date);
				}
				return days.ToArray();
			}
		}
		
		private List<DayInfo> _TheDays;
		public List<DayInfo> TheDays
		{
			get
			{
				if (_TheDays == null)
				{
					CreateTideList("");
				}
				return _TheDays;
			}
		}
			
		public StationInfo()
		{
			StationName = "Charleston, OR";
			StationID = 9432780;
			_TheDays = null;
		}

		public StationInfo(string file)
		{
			StationName = "Charleston, OR";
			StationID = 9432780;
			_TheDays = null;
			CreateTideList (file);
		}

		private void CreateTideList(string file)
		{
			_TheDays = new List<DayInfo> ();
			string[] AllLines = file.Split (new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in AllLines) 
			{
				string[] cutLine = line.Split (new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
				if (cutLine.Length != 6)
					continue;

				string date = cutLine [0];
				string day = cutLine [1];
				string time = cutLine [2];
				float ft = float.Parse(cutLine[3]);
				float cm = float.Parse (cutLine[4]);
				string hl = cutLine [5];

				bool doesNotContain = true;

				foreach (DayInfo aday in _TheDays) 
				{
					if (aday.Date.Equals (date)) 
					{
						doesNotContain = false;
						aday.AddTideInfo (new TideInfo(time, ft, cm, hl));
						break;
					}
				}

				if (doesNotContain) 
				{
					_TheDays.Add (new DayInfo(date, day, time, ft, cm, hl));
				}
			}

			_TheDays.Sort ((x, y) => string.Compare(x.Date, y.Date, StringComparison.Ordinal));
		}
	}
	#endregion

	#region TideStations Class
	public class TideStations
	{
		//constants for TideInfo
		const string TideStationURL = @"http://tidesandcurrents.noaa.gov/station_retrieve.shtml?type=Tide Data";
		const string NoResponse = "No response from webserver";
		const string GoodResponse = "Response from webserver was good";

		//List of the Stations
		private List<StationInfo> _Stations;
		public List<StationInfo> Stations
		{
			get
			{
				return (_Stations == null)? new List<StationInfo>() : _Stations;
			}
		}

		//Status info for TideInfo
		public string GettingInfo = "Getting information from webserver";
		
		//constants for Parsing HTML
		const string StationStart = "<td><a href=\"data_menu.shtml?stn=";
		const string StationEnd = "&type=Tide Data";
		
		public TideStations ()
		{
			_Stations = new List<StationInfo>();
			try
			{
				WebRequest request = WebRequest.Create(TideStationURL);

				WebResponse response = request.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					StreamReader reader = new StreamReader(stream);

					SetStations(reader.ReadToEnd());
				}
				GettingInfo = GoodResponse;
				_Stations.Sort((x, y) => string.Compare(x.StationName, y.StationName, StringComparison.Ordinal));
			}
			catch
			{
				GettingInfo = NoResponse;
			}
		}

		void SetStations(string html)
		{
			string[] cutHtml = html.Split (new string[] {StationStart}, StringSplitOptions.None);
			for (int i = 1; i < cutHtml.Length; i++) 
			{
				string stationLine = cutHtml [i].Split (new string[] {StationEnd}, StringSplitOptions.RemoveEmptyEntries) [0] + "\n";
				try 
				{
					int id = int.Parse (stationLine.Substring(0,7));
					string name = stationLine.Substring (7).Trim ();
					_Stations.Add (new StationInfo());
				} 
				catch 
				{
				}
			}
		}
	}
	#endregion
}

