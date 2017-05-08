using System;
using SQLite;

namespace TideChartConsole
{
	[Table("TideStations")]
	public class TideStationRow
	{
		[PrimaryKey, Column("StationID")]
		public int StationID { get; set; }
		[MaxLength(30)]
		public string StationName { get; set; }
	}

	[Table("Tides")]
	public class TideRow
	{
		[PrimaryKey, AutoIncrement, Column("TideKey")]
		public int TideKey { get; set; }
		public int StationID { get; set; }
		public DateTime Date { get; set; }
		[MaxLength(10)]
		public string Time { get; set; }
		public float Ft { get; set; }
		public int Cm { get; set; }
		[MaxLength(2)]
		public string HighLow { get; set; }
	}
}

