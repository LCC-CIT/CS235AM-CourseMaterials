using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Flow
{
	public class TideDays
	{
		public DateTime date;
		List<Tide> tides;
		private string format = "yyyy MMM dd";

		public TideDays ( List<Tide> input )
		{
			tides = input;
			date = tides [ 0 ].date;
		}

		public string textReturn1 ()
		{
			return date.ToString( format );
		}

		public string toasting ()
		{
			string info = string.Empty;
			for ( int i = 0; i < tides.Count; i++ )
			{
				info += tides [ i ].toasting();
				info += "\n";
			}
			return info;
		}

		public override string ToString ()
		{
			return date.ToString( format );
		}

		public string stationname ()
		{
			return tides [ 0 ].StationName;
		}

		public uint stationID ()
		{
			return tides [ 0 ].StationID;
		}
	}
}