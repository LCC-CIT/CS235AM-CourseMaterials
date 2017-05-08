using System;

namespace EvolveLite
{
	public class Session
	{
		public Speaker Speaker { get; set; }

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

