using System;
using SQLite;
using System.Collections.Generic;

namespace Domain
{
	[Table("TideStation")]
	public class TideStation
	{
		public TideStation ()
		{
			// http://tidesandcurrents.noaa.gov/api/datagetter?begin_date=20130808 15:00&end_date=20130808 15:06&station=8454000&product=water_temperature&
			// units=english&time_zone=gmt&application=ports_screen&format=xml
 
			Address = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=";
		}

		[Ignore]
		public static string Address { get; private set; }
		
		[PrimaryKey]
		public string TideStationId { get; set; }
		public string StationName { get; set; }
		public string Location { get; set; }

        public override string ToString()
        {
            return string.Format("{0},\t{1}", StationName, Location);
        }
	}
}