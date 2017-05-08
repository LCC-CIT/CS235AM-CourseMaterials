using System;
using SQLite;

namespace DataAccess.DAL
{
	[Table("Stocks")]
	public class Stock
	{
		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		[MaxLength(8)]
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
		public string Name { get; set; }
		public decimal ClosingPrice { get; set;}
	}
}