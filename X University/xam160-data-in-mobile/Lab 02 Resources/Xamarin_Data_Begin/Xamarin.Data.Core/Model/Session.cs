using System;

namespace Xamarin.Data.Core.Model
{
	public class Session
	{
		//TODO: Step 2 - Add a primary key
		//[SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
		public int Id { get; set; }

		//TODO: Step 3 - Mark column that you will not persist
		//[SQLite.Ignore()]
		public Speaker Speaker { get; set; }

		public string SpeakerName { get; set; }

		public string Title { get; set; }

		public string Abstract { get; set; }

		public string Location { get; set; }

		public DateTime Begins { get; set; }

		public DateTime Ends { get; set; }

		public Session ()
		{
		}
	}
}