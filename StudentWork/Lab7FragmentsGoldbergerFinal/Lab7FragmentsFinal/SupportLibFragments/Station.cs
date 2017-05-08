
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

namespace Lab7Fragments
{
	public class Station
	{
		public string Name {get;set;}
		public string ID {get;set;}
		public string State {get;set;}
		public Station()
		{
			Name = ""; 
			State = "";
			ID = "";
		}



		public Station(string name, string id, string state)
		{
			Name = name; // place name
			ID = id;
			State = state; // null for non us states
		}
	}
}

