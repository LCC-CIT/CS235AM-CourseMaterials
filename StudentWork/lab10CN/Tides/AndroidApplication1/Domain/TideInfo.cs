using System;
using SQLite;

namespace Domain
{
	[Table("TideInfo")]
	public class TideInfo
	{
		public TideInfo ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("_id")]
		public int TideInfoId { get; set; }
        public string TideStationId { get; set; }
        public int DayId { get; set; }
		public string Time { get; set; }
		public string Height { get; set; }
		public string Hilo { get; set; }

        public override string ToString()
        {
            return String.Format("{0},\t{1},\t{2}", Time, Height, Hilo);
        }
	}
}

