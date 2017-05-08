using System;

namespace EvolveLite
{
	public class Speaker
	{
		[SQLite.PrimaryKey, SQLite.AutoIncrement]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Company { get; set; }

		public string Bio { get; set; }

		public string TwitterHandle { get; set; }

		public string ImageUrl { get; set; }

		public Speaker ()
		{
		}
	}
}

