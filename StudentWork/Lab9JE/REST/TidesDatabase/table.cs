using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Preloader
{
	[Table("TideTable")]
	class table
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public uint stationID { get; set; }
		public string stationName { get; set; }
		public DateTime date { get; set; }
		public string info { get; set; }

		public override string ToString ()
		{
			return date.ToString( "MMM dd yy: ddd" );
		}
	}
}
