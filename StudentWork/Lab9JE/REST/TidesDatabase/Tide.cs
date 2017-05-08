using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow
{
	public class Tide
	{
		public DateTime date { get; set; }

		public uint StationID { get; set; }

		public string StationName { get; set; }

		public string day { get; set; }

		public string time { get; set; }

		public string sizef { get; set; }

		public string sizec { get; set; }

		public string highLow { get; set; }

		public Tide ( string dat, uint id, string Name, string d, string t, string sf, string sc, string hl )
		{
			date = DateTime.Parse( dat );
			StationID = id;
			StationName = Name;
			day = d;
			time = t;
			sizef = sf;
			sizec = sc;
			highLow = hl;
		}

		public override string ToString ()
		{
			return string.Format ("{0} {1}", date, day);
		}

		public string textReturn1 ()
		{
			return string.Format ("{0} {1}", date, day);
		}

		public string textReturn2 ()
		{
			return string.Format ("{0}: {1}", highLow, time);
		}

		public string toasting ()
		{
			return string.Format( "{2}: {0} ft or {1} cm", sizef, sizec, time );
		}
	}
}

