using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DAL;
using SQLite;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace TideTableApp
{
	[Activity (Label = "StartActivity", MainLauncher = true)]			
	public class StartActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//load station data from a webservice into the database, if it does not exist 
			//GetTideStations (new Uri("http://tidesandcurrents.noaa.gov/tide_predictions.html?gid=252"));

			//TEMPORARY: we use a preset station id to download a tidetable.
			GetTideData(new Uri("http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9434939"));


			//WE NEED TO STRAIGHTEN OUT THE DL FILES FIRST
			//var data = (from st in DataBaseHandler.db.Table<Station>() select st).ToList ();
			//var dataN = data.Select (d => d.Name ).ToArray();
			//var dataArray = dataN.ToArray ();

			//var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemChecked, dataArray);
			//ListAdapter = adapter;
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);

			string loc = "";
			var dat = DataBaseHandler.db.Query<Station> ("SELECT * FROM ST WHERE ID LIKE ?", position); 
			foreach (Station s in dat) 
			{
				loc = s.Name;
			}

			Intent i = new Intent (this, typeof(MainActivity));
			StartActivity (i);
		}

		public static void GetTideStations(Uri u)
		{
			// Async Consumption IS CURRENTLY COMMENTED OUT, need to format stationobj class to handle conversion from xml
			var asyncClient = new WebClient();
			var content = asyncClient.DownloadString (u);
			//asyncClient.DownloadStringCompleted += asyncClient_DownloadStationsCompleted;
			//asyncClient.DownloadStringAsync(new Uri(url));
		}

		private static void asyncClient_DownloadStationsCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			// Create an XML serializer and parse the response
			XmlSerializer serializer = new XmlSerializer(typeof(StationWebObj));
			StationWebObj st;
			using (XmlReader reader = XmlReader.Create(new StringReader(e.Result)))
			{
				// deserialize the XML object using the WeatherData type
				st = (StationWebObj)serializer.Deserialize(reader);
			}
		}

		public static void GetTideData(Uri u)
		{
			var asyncClient = new WebClient();
			//asyncClient.DownloadStringCompleted += DownloadTidesCompleted;
			//asyncClient.DownloadStringAsync(u);

			//the synchronous version mainly for testing-----------------------------------Synchronous version
			XmlSerializer serializer = new XmlSerializer (typeof(TideDataObj));
			var rawData = asyncClient.DownloadString (u);

			using (XmlReader reader = XmlReader.Create (new StringReader (rawData))) 
			{
				// deserialize the XML object using the WeatherData type
				TideDataObj st = (TideDataObj)serializer.Deserialize (reader);
			}
		}

		private static void DownloadTidesCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			// Create an XML serializer and parse the response
			XmlSerializer serializer = new XmlSerializer (typeof(TideDataObj));
			TideDataObj st;

			string[] seperator = new string[]{ "<data>", "</data>" };
			string rawData = e.Result;
			string[] splitData;
			splitData = rawData.Split (seperator, StringSplitOptions.RemoveEmptyEntries);

			StringReader s;
			using (XmlReader reader = XmlReader.Create (s = new StringReader (splitData [1]))) 
			{
				// deserialize the XML object using the WeatherData type
				st = (TideDataObj)serializer.Deserialize (reader);
			}
		}
	}

	public class StationWebObj
	{
		//need to create from xml to c# website
	}

	[Serializable, XmlRoot("datainfo")]
	public class TideDataObj
	{
		[XmlElement]
		public string origin { get; set; }
		[XmlElement]
		public string disclaimer { get; set; }
		[XmlElement]
		public string producttype { get; set; }
		[XmlElement]
		public string stationname { get; set; }
		[XmlElement]
		public string state { get; set; }
		[XmlElement]
		public int stationid { get; set; }
		[XmlElement]
		public string stationtype { get; set; }
		[XmlElement]
		public string referencedToStationName { get; set; }
		[XmlElement]
		public int referencedToStationId { get; set; }
		[XmlElement]
		public string HeightOffsetLow { get; set; }
		[XmlElement]
		public string HeightOffsetHigh { get; set; }
		[XmlElement]
		public byte TimeOffsetLow { get; set; }
		[XmlElement]
		public byte TimeOffsetHigh { get; set; }
		[XmlElement]
		public string BeginDate { get; set; }
		[XmlElement]
		public string EndDate { get; set; }
		[XmlElement]
		public string dataUnits { get; set; }
		[XmlElement]
		public string Timezone { get; set; }
		[XmlElement]
		public string Datum { get; set; }
		[XmlElement]
		public string IntervalType { get; set; }
		[XmlElement]
		public Data data { get; set; }
	}

	[Serializable]
	public class Data
	{
		[XmlElement]
		public Item[] item { get; set; }
	}

	[Serializable]
	public class Item
	{
		[XmlElement]
		public string date { get; set; }
		[XmlElement]
		public string day { get; set; }
		[XmlElement]
		public string time { get; set; }
		[XmlElement]
		public Decimal predictions_in_ft { get; set; }
		[XmlElement]
		public Decimal predictions_in_cm { get; set; }
		[XmlElement]
		public string highlow { get; set; }
	}
}


