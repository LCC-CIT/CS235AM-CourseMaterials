using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TideInfoDB
{
	[Table("TideStation")]
	public class TideStation
	{
		[PrimaryKey, Indexed]
		[MaxLength(8)]
		public string stationId { get; set; }
		[MaxLength(25)]
		public string stationName { get; set; }
	}

	[Table("TideInfo")]
	public class TideInfo
	{
		[PrimaryKey, AutoIncrement, Indexed]
		public int tideKey { get; set; }
		[MaxLength(10)]
		public string stationId { get; set; }
		[MaxLength(10)]
		public string date { get; set; }
		[MaxLength(10)]
		public string day { get; set; }
		[MaxLength(10)]
		public string time { get; set; }
		public float heightF { get; set; }
		public float heightC { get; set; }
		[MaxLength(2)]
		public string tideType { get; set; }
	}
}

