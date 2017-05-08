using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Preloader
{
	[Table( "StationTable" )]
	public class Station
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string station_name { get; set; }
		public uint stationID { get; set; }

		public override string ToString ()
		{
			return station_name;
		}
	}
}
