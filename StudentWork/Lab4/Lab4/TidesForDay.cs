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

namespace Lab2
{
	public class TidesForDay
	{
		public TidesForDay(List<Tide> tide)
		{
			Tides = new List<Tide> ();

			foreach (var t in tide) {
				
			}
		}

		List<Tide> Tides { get; set; }
	}
}

