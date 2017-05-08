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
using System.Xml.Serialization;
using System.Net;
using System.Xml;
using System.IO;
using TideFragments.DAL;


namespace TideFragments
{


	public class NOAAApiClient
	{
		private const string baseUrl="http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp";//http://tidesandcurrents.noaa.gov/api/datagetter";
		private const string product = "product=high_low";
		private const string units="units=english";
		private const string zone="time_zone=gmt";
		private const string app="application=TideFragments";
		private const string format="format=xml"; 
		private const string dt = "datatype=Annual+XML";
		private List<DataInfoItem> myresults= new List<DataInfoItem>();

	

		public List<DataInfoItem>GetAll(string stationId)
		{
			var url= string.Format(baseUrl+"?"+dt+"&Stationid="+stationId);
			// Syncronious Consumption
			var syncClient = new WebClient();
			var content = syncClient.DownloadString(url);  //we get this from webserver
			//List<string> myresults = new List<string> ();
			// Create an XML serializer and parse the response

			XmlSerializer serializer = new XmlSerializer(typeof(Datainfo));
			//CitiesWeather weatherData;
			Datainfo datainfo;
			using (XmlReader reader = XmlReader.Create(new StringReader(content)))  
			{
				// deserialize the XML object using the WeatherData type
				datainfo = (Datainfo)serializer.Deserialize(reader); //we need to call desirializer to start reading  -- xml2csharp.com
				datainfo.ToString ();
			}
			foreach (DataInfoItem item in datainfo.data) {
				//myresults.Add (item.day.ToString () + item.predictions_in_ft.ToString ());
				myresults.Add (item);
			}
			//item.date,item.day,item.predictions_in_ft,item.highlow
			// myresults;
			return myresults;
		}
			//I created this before I did the peer evaluation and realizing that I had to download into database :( 
		public List<string> GetDates(string stationId) 
		{
		
			var url= string.Format(baseUrl+"?"+dt+"&Stationid="+stationId);
			// Syncronious Consumption
				var syncClient = new WebClient();
				var content = syncClient.DownloadString(url);  //we get this from webserver
			List<string> myresults = new List<string> ();
			// Create an XML serializer and parse the response

			XmlSerializer serializer = new XmlSerializer(typeof(Datainfo));
			//CitiesWeather weatherData;
			Datainfo datainfo;
			using (XmlReader reader = XmlReader.Create(new StringReader(content)))  
			{
				// deserialize the XML object using the WeatherData type
				datainfo = (Datainfo)serializer.Deserialize(reader); //we need to call desirializer to start reading  -- xml2csharp.com
				datainfo.ToString ();
			}
			foreach (DataInfoItem item in datainfo.data) {
				//myresults.Add (item.day.ToString () + item.predictions_in_ft.ToString ());
				myresults.Add (item.date.ToString ());
			}
            
			return myresults;

		}

		public List<DataInfoItem> GetTidePredForCurrentLoc (string stationId, string date)//,string mon,string day,string year) 
		{

			var url = string.Format (baseUrl + "?" + dt + "&Stationid=" + stationId);

			// Syncronious Consumption
			var syncClient = new WebClient ();
			var content = syncClient.DownloadString (url);  //we get this from webserver
			//List<string> myresults = new List<string> ();
			List<string> finalResults = new List<string> ();
			// Create an XML serializer and parse the response

			XmlSerializer serializer = new XmlSerializer (typeof(Datainfo));
			//CitiesWeather weatherData;
			Datainfo datainfo;
			using (XmlReader reader = XmlReader.Create (new StringReader (content))) {
				// deserialize the XML object using the WeatherData type
				datainfo = (Datainfo)serializer.Deserialize (reader); //we need to call desirializer to start reading  -- xml2csharp.com
				datainfo.ToString ();
			}
			foreach (DataInfoItem item in datainfo.data) {
				// going through the whole list but adding only the items with the current date
				if (item.date == date) {
					myresults.Add (item);	
				}

				return myresults;
			}
			return myresults;
		}

	

		public List<string> GetTidePredictions(string stationId,string dateId) 
		{

			var url= string.Format(baseUrl+"?"+dt+"&Stationid="+stationId);
			// Syncronious Consumption
			var syncClient = new WebClient();
			var content = syncClient.DownloadString(url);  //we get this from webserver
			List<string> myresults = new List<string> ();
			List<string> finalResults = new List<string> ();
			// Create an XML serializer and parse the response

			XmlSerializer serializer = new XmlSerializer(typeof(Datainfo));
			//CitiesWeather weatherData;
			Datainfo datainfo;
			using (XmlReader reader = XmlReader.Create(new StringReader(content)))  
			{
				// deserialize the XML object using the WeatherData type
				datainfo = (Datainfo)serializer.Deserialize(reader); //we need to call desirializer to start reading  -- xml2csharp.com
				datainfo.ToString ();
			}
			foreach (DataInfoItem item in datainfo.data) {
				// going through the whole list but adding only the items with the selected date
				if (item.date == dateId) {
					myresults.Add ( item.time + "            " + item.predictions_in_ft + "       " + item.highlow);
				}

			}


			return myresults;

		}


//		public void  DisplayResults(DataInfo datainfo)
//		{
//			datainfo.ToString ();
//		  List<string> dr = new List<string> ();
//		foreach(DataItem item  in datainfo.list)
//		 {
//				dr.Add (item.day.ToString () + item.predictions_in_ft.ToString ());
//
//		 }
//		}
	
	}
}

