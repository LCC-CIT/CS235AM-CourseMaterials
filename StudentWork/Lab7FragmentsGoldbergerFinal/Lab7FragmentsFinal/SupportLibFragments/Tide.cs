
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
	public class Tide
	{

		public Tide (string data) 
		{
			this.data = data;
		}
		// Data stores a raw line of tide data from the .txt file downloaded from NOAA's tide charts.
		public string data {get; set;}

		// Returns date in the form "Tue, Jan 1".
		public string getDate()
		{
			string day = getDay();
			string mon = getMonth();
			string date = data.Substring(8,2);
			if (date[0] == '0') date = date.Replace("0","");

			return day + ", " + mon + " " + date;
		}

		// Returns month in the form "Jan", "Feb" etc (or whatever the string resources in String.xml are set too - localization!!)
		public string getMonth()
		{
			string mon;
			var key = data.Substring(5,2);
			switch (key)
			{
			case "01":
				mon = MainActivity.context.GetString(Resource.String.jan);
				break;
			case "02":
				mon = MainActivity.context.GetString(Resource.String.feb);
				break;
			case "03":
				mon = MainActivity.context.GetString(Resource.String.mar);
				break;
			case "04":
				mon = MainActivity.context.GetString(Resource.String.apr);
				break;
			case "05":
				mon = MainActivity.context.GetString(Resource.String.may);
				break;
			case "06":
				mon = MainActivity.context.GetString(Resource.String.jun);
				break;
			case "07":
				mon = MainActivity.context.GetString(Resource.String.jul);
				break;
			case "08":
				mon = MainActivity.context.GetString(Resource.String.aug);
				break;
			case "09":
				mon = MainActivity.context.GetString(Resource.String.sep);
				break;
			case "10":
				mon = MainActivity.context.GetString(Resource.String.oct);
				break;
			case "11":
				mon = MainActivity.context.GetString(Resource.String.nov);
				break;
			case "12":
				mon = MainActivity.context.GetString(Resource.String.dec);
				break;
			default:
				mon = "???";
				break;
			}
			return mon;
		}

		// Returns Tide info in the form "High" or "Low" (again based on String.xml)
		public string getInfo()
		{
			string info = "";
			if (data.Substring(data.Length-1) == "H")
				info += MainActivity.context.GetString(Resource.String.high);
			else
				info += MainActivity.context.GetString(Resource.String.low);

			return info;
		}

		// Returns tide height in the form "(ft): 1.0 (cm): 24" (and again based on String.xml)
		public string getHeight()
		{
			string[] fields = data.Split(' ','\t');
			string info = MainActivity.context.GetString(Resource.String.ft) + fields[4];
			info += MainActivity.context.GetString(Resource.String.cm) +  fields[6];
			return info;
		}

		// Returns time in the form "01:10 AM" (Sorry, Euro's, you're gonna have to deal with AM/PM. Next version...
		public string getTime()
		{
			return data.Substring(15,8);
		}

		// Returns the day using String resources for possible later localization (Of course.)
		public string getDay()
		{
			var day = data.Substring(11,3);

			switch (day)
			{
			case "Sun":
				day = MainActivity.context.GetString(Resource.String.sun);
				break;
			case "Mon":
				day = MainActivity.context.GetString(Resource.String.mon);
				break;
			case "Tue":
				day = MainActivity.context.GetString(Resource.String.tue);
				break;
			case "Wed":
				day = MainActivity.context.GetString(Resource.String.wed);
				break;
			case "Thu":
				day = MainActivity.context.GetString(Resource.String.thu);
				break;
			case "Fri":
				day = MainActivity.context.GetString(Resource.String.fri);
				break;
			case "Sat":
				day = MainActivity.context.GetString(Resource.String.sat);
				break;
			}
			return day;
		}
	}
}

