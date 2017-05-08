
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab7Fragments
{
				
	public class Stations
	{
		public string[] usStates = {"AL","PR","VI","AK","AZ","AR","CA","CO","CT","DE","DC","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"};
	
		// Loads the data from HTML , returns a Station array sorted by state.
		public Station[] LoadStations()
		{
			var stats = new List<Station>();

			string baseURL = "http://tidesandcurrents.noaa.gov/";
			string statListURL = "station_retrieve.shtml?type=Tide%20Data&state=All%20Stations&id1=";
			Stream data;
			WebClient client = new WebClient();
			try 
			{
				data = client.OpenRead (baseURL + statListURL);
			}
			catch (WebException ex)
			{	
				var stat = new Station();
				stat.Name = "Unable to connect to NOAA at this time.";
				stats.Add(stat);
				return stats.ToArray();
			}

			StreamReader reader = new StreamReader(data);

			string str = "";

			for (int i = 0; i < 160; i++)
			{
				str = reader.ReadLine ();
			}
			for (int i = 0; i < 500; i++)
			{
				str = reader.ReadLine ();
				if (str.Contains("data_menu.shtml?stn="))
				 {
					//var heading = FindViewById<TextView>(Resource.Id.Heading2);
					//heading.Text += str;

					string[] fields = str.Split ("><".ToCharArray());

					var stat = new Station();
					stat.ID = fields[4];
					stat.Name = fields[8]; 
					if (usStates.Contains (stat.Name.Substring (stat.Name.Length-2)))
					{
					    stat.State = stat.Name.Substring (stat.Name.Length-2);
						stat.Name = stat.Name.Substring(0, stat.Name.Length-4);
						stats.Add(stat);
					}
				}
			}
			stats.Sort((x, y) => { return x.State.CompareTo(y.State); });
			reader.Close();
			return stats.ToArray();
		} 
	}
}
