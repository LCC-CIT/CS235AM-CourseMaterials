using System;
using System.Collections.Generic;
//using System.Json;
using System.Collections;

namespace EvolveListView.Model
{
	/// <summary>
	/// Session class - not used in the ListView examples
	/// </summary>
	public class Session
	{
		public Session()
		{
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string Abstract { get; set; }

		public string Location { get; set; }

		public DateTime Begins { get; set; }

		public DateTime Ends { get; set; }

		public string Speaker { get; set; }
	}
}