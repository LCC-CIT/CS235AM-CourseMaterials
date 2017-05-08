
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
	public class Tides
	{
		public Tide[] datesArray;
		public Tide[] tidesArray;
		public string location;

		// Loads the data from NOAA text file returns a Tide array.
		public Tide[] LoadTideTables(string name, string id, string state)
		{
			string baseURL = "http://tidesandcurrents.noaa.gov/";

			string tideListURL = "noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+TXT&Stationid=";        
			tideListURL += id; // get station ID
			var tides = new List<Tide>();

			WebClient client = new WebClient();
			Stream data;
			try
			{
				data = client.OpenRead (baseURL + tideListURL);
			}
			catch (WebException ex)
			{
				var tide = new Tide("Unable to connect to NOAA at this time.");
				tides.Add(tide);
				tidesArray = tides.ToArray();
				return tidesArray;
			}
			StreamReader reader = new StreamReader(data);


			while (!reader.EndOfStream)
			{
				// Create a new tide object and add a line of data to it.
				var tide = new Tide(reader.ReadLine());

				// add tide object to tides list (as long as it is a tide, not header info in the file
				if (tide.data.Substring(0,1) == "2")
					tides.Add(tide);
			}

			reader.Close();
			// convert tides list to array and send it on back.
			tidesArray = tides.ToArray();
			return tidesArray;
		}
		
		// Get only unique dates from tides array.
		public Tide[] GetDates()
		{
			var dates = new List<Tide>();
			string date;
			string lastDate = "";
			for (int i=0; i< tidesArray.Length; i++)
			{
				date = tidesArray[i].data.Substring(0,10);
				// Only add the first tide per day to the dates list.
				if (date != lastDate) 
				{
					dates.Add(tidesArray[i]);
				}
				lastDate = date;
			}
			// convert list to Tide array and send it back
			datesArray = dates.ToArray();
			return datesArray;
		}
	}
}
