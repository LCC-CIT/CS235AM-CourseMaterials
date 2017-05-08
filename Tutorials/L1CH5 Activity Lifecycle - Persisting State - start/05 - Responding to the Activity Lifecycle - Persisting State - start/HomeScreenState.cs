using System;
using System.Collections.Generic;

namespace PersistStateWalkthrough
{
	// Persistence: Used to create a custom object for persisting state
	public class HomeScreenState : Java.Lang.Object
	{
		public HomeScreenState (List<Flora> list)
		{
			ListOfFlora = list;
		}
		
		public List<Flora> ListOfFlora { get; private set; }
	}
}

