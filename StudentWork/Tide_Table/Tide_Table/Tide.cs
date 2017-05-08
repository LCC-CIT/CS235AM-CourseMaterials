
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
	public class Tide 
	{
		public  string 	Date 	{get; set;}
		public  string	Day		{get; set;}
		public Dictionary<int, string> Tides = new Dictionary<int, string> ();




	}


}

