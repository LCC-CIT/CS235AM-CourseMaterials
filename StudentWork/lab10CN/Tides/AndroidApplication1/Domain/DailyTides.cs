using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Domain
{
	[Table("DailyTides")]
	public class DailyTides
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int DailyTidesId { get; set; }
		public string TideStationId { get; set; }
		public string Day { get; set; }
		public string Date { get; set; }

		public override string ToString ()
		{
			return string.Format ("{0}\n{1}", Day, Date);
		}
	}
}

