using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SQLite;
using TideChartConsole;

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
		const string TideInfoURL = @"http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?";

		//Station Info
		public string StationName {get; private set;}
		public int StationID {get; private set;}

		private List<DayInfo> _TheDays;
		public List<DayInfo> TheDays
		{
			get
			{
				if (_TheDays == null)
				{
					CreateTideList();
				}
				return _TheDays;
			}
		}
			
		public StationInfo(string name, int id)
		{
			StationName = name;
			StationID = id;
			_TheDays = null;
		}

		private void CreateTideList()
		{
			SQLiteConnection db = new SQLiteConnection (Connection.GetDBPath());
			_TheDays = new List<DayInfo> ();

			List<TideRow> Tides = db.Query<TideRow> ("SELECT * FROM Tides WHERE StationID=?", StationID);

			if (Tides.Count > 0) 
			{
				List<string> AListOfDays = new List<string> ();
				//get tides from database
				for (int i = 0; i < Tides.Count; i++) {
					TideRow row = Tides [i];
					bool hadDay = false;

					if (AListOfDays.Contains (row.Date.ToShortDateString())) {
						for (int index = _TheDays.Count - 1; index >= 0; index--) {
							if (DateTime.Parse (_TheDays[index].Date) == row.Date) {
								_TheDays [index].AddTideInfo (new TideInfo(row.Time, row.Ft, row.Cm, row.HighLow));
								hadDay = true;
								break;
							}
						}
					}

					if (!hadDay) {
						AListOfDays.Add (row.Date.ToShortDateString());
						_TheDays.Add (new DayInfo(row.Date.ToShortDateString(), row.Date.DayOfWeek.ToString(), row.Time, row.Ft, row.Cm, row.HighLow));
					}
				}
			} 
			else 
			{
				//get tides from webservice
				try
				{
					string URL = TideInfoURL + "Stationid=" + StationID + "&datatype=Annual XML"; 
					WebRequest request = WebRequest.Create(URL);

					WebResponse response = request.GetResponse();

					using (Stream stream = response.GetResponseStream())
					{	
						XmlReader reader = XmlReader.Create(stream);

						string Date = string.Empty;
						string Day = string.Empty;
						string Time = string.Empty;
						float PredictionInFt = 0;
						int PredictionInCm = 0;
						string HighLow = string.Empty;

						while (reader.Read())
						{
							switch (reader.NodeType)
							{
								case XmlNodeType.Element:
								if (reader.Name == "date")
								{
									try {reader.Read();} catch{}
									Date = reader.Value;
								}
								else if (reader.Name == "day")
								{
									try {reader.Read();} catch{}
									Day = reader.Value;
								}
								else if (reader.Name == "time")
								{
									try {reader.Read();} catch{}
									Time = reader.Value;
								}
								else if (reader.Name == "predictions_in_ft")
								{
									try {reader.Read();} catch{}
									float.TryParse(reader.Value, out PredictionInFt);
								}
								else if (reader.Name == "predictions_in_cm")
								{
									try {reader.Read();} catch{}
									int.TryParse(reader.Value, out PredictionInCm);
								}
								else if (reader.Name == "highlow")
								{
									try {reader.Read();} catch{}
									HighLow = reader.Value;

									//At the end of the Item create object
									bool wasAdded = false;
									foreach(DayInfo day in _TheDays)
									{
										if (day.Date == Date)
										{
											day.AddTideInfo(new TideInfo(Time, PredictionInFt, PredictionInCm, HighLow));
											wasAdded = true;
										}
									}

									if (!wasAdded)
										_TheDays.Add(new DayInfo(Date,Day,Time,PredictionInFt,PredictionInCm,HighLow));

									//reset all values to blank
									Date = string.Empty;
									Day = Time = HighLow = string.Empty;
									PredictionInFt = PredictionInCm = 0;
								}
								break;
							}
						}
					}
					db.BeginTransaction ();
					foreach (DayInfo day in _TheDays) 
					{
						foreach (TideInfo tide in day.TheTides) 
						{
							db.Insert (new TideRow() {
								StationID = StationID,
								Date = DateTime.Parse(day.Date),
								Time = tide.Time,
								Ft = tide.PredictionInFt,
								Cm = (int)tide.PredictionInCm,
								HighLow = tide.HighLow});
						}
					}
					db.Commit ();
				}
				catch
				{}
			}
		}
	}
	#endregion

	#region TideStations Class
	public class TideStations
	{
		//constant for list of tides in HTML
		const string TideStationURL = @"http://tidesandcurrents.noaa.gov/station_retrieve.shtml?type=Tide Data";

		//constants for Parsing HTML
		const string StationStart = "<td><a href=\"data_menu.shtml?stn=";
		const string StationEnd = "&type=Tide Data";

		//List of the Stations
		private List<StationInfo> _Stations;
		public List<StationInfo> Stations
		{
			get
			{
				return (_Stations == null)? new List<StationInfo>() : _Stations;
			}
		}
		
		public TideStations ()
		{
			SQLiteConnection db = new SQLiteConnection (Connection.GetDBPath());
			_Stations = new List<StationInfo>();

			List<TideStationRow> TheStations = db.Query<TideStationRow> ("SELECT * FROM TideStations");

			if (TheStations.Count > 0) 
			{
				foreach (TideStationRow row in TheStations) 
				{
					_Stations.Add (new StationInfo(row.StationName, row.StationID));
					_Stations.Sort ((x,y) => string.Compare(x.StationName, y.StationName, StringComparison.Ordinal));
				}
			} 
			else 
			{
				//TODO get list of stations from Webservice
				try
				{
					WebRequest request = WebRequest.Create(TideStationURL);

					WebResponse response = request.GetResponse();

					using (Stream stream = response.GetResponseStream())
					{
						StreamReader reader = new StreamReader(stream);

						SetStations(reader.ReadToEnd());
					}
					_Stations.Sort((x, y) => string.Compare(x.StationName, y.StationName, StringComparison.Ordinal));

					db.BeginTransaction();
					foreach(StationInfo station in _Stations)
					{
						db.Insert( new TideStationRow(){ 
							StationID = station.StationID,
							StationName = station.StationName});
					}
					db.Commit();
				}
				catch {}
			}
		}

		void SetStations(string html)
		{
			string[] cutHtml = html.Split(new string[] {StationStart}, StringSplitOptions.None);
			for (int i = 1; i < cutHtml.Length; i++)
			{
				string stationLine = cutHtml[i].Split(new string[] {StationEnd},StringSplitOptions.RemoveEmptyEntries)[0] +"\n";
				try
				{
					int id = int.Parse(stationLine.Substring(0,7));
					string name = stationLine.Substring(7).Trim();
					_Stations.Add(new StationInfo(name, id));
				}
				catch
				{
				}
			}
		}
	}
	#endregion

	public static class Connection
	{
		public static string GetDBPath()
		{
			string dbPath = string.Empty;
			dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "TideChart.db3");
			return dbPath;
		}
	}
}

