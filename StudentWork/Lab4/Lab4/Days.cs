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
	public class Days
	{
		public Days()
		{
			Tides = new List<Tide> ();
		}

		//public string Hilo { get; set; } 
		public List<Tide> Tides { get; set; }

		public override string ToString ()
		{
			return Tides.FirstOrDefault ().Day + " " + Tides.FirstOrDefault ().Date;
		}
	}
}

