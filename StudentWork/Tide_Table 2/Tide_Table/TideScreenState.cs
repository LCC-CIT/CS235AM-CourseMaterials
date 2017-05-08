
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Tide_Table
{
	class TideScreenState : Java.Lang.Object
	{
		public TideScreenState(Tide[] tides)
		{
			this.tides = tides;
		}

		public Tide [] tides {get; private set;}
	}
}

