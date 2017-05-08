using System;
using SQLite;

namespace L2Ch3.DAL
{
	[Table("Stocks")]
	public class Stock
	{
		[PrimaryKey, Column("Symbol")]
		[MaxLength(8)]
		public string Symbol { get; set; }
		public string Name { get; set; }
	}
}

