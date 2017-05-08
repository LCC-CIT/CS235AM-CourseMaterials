using System;
using SQLite;

namespace WsDemo.DAL
{
	[Table("Stocks")]
	public class Stock
	{
		[PrimaryKey, Column("Symbol")]
		[MaxLength(8)]
		public string Symbol { get; set; }
		public string Name { get; set; }
	}

	public class StockData
	{
		public string Symbol { get; set; }
		public DateTime DateAndTime { get; set;}
		public Decimal High { get; set; }
		public Decimal Low {get; set; }
		public Decimal Close {get; set;}
	}
}

