
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab7Fragments
{
	[Table("stations")]
	public class Station
	{
		[PrimaryKey, Column("ID")]
		[MaxLength(8)]
		public string ID {get;set;}
		[MaxLength(100)]
		public string Name {get;set;}
		[MaxLength(2)]
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

