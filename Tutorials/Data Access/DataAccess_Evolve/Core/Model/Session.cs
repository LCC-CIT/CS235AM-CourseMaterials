using System;
#if __ANDROID__
using Android.Runtime;
#else
using MonoTouch.Foundation;
#endif

namespace EvolveLite
{
	[Preserve(AllMembers=true)]
	public class Session
	{
		[SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
		public int Id { get; set; }

		[SQLite.Ignore()]
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