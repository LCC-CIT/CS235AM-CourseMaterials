using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using Domain;
using System.Net;

namespace DAL
{
	public static class ReadFiles
	{
		private const int NUM_FIELDS = 6;
		public static string GetStationName(XDocument doc)
		{
			XElement el = doc.Root.Element ("stationname");
		
			return el.Value;
		}

		public static string GetStationId(XDocument doc)
		{
			XElement el = doc.Root.Element ("stationid");

			return el.Value;
		}

        public static string getLocation(XDocument doc)
        {
            XElement el = doc.Root.Element("state");

            return el.Value;
        }

        public static List<DailyTides> GetDays(XDocument doc)
        {
            var dataElem = doc.Root.Element("data");
            var items = dataElem.Elements("item");
            List<DailyTides> Days = new List<DailyTides>();

            foreach (var item in items)
                if (!DateExists(Days, item.Element("date").Value))
                    Days.Add(new DailyTides() {Date = item.Element("date").Value, Day = item.Element("day").Value });

            return Days;
        }

        public static int GetDateId(XDocument doc, string date, ref List<DailyTides> days)
        {
            return days.Find(t => t.Date == date).DailyTidesId;
        }

        private static bool DateExists(List<DailyTides> days, string date)
        {
            var day = days.Find(d => d.Date == date);
            return days.Contains(day);
        }

        public static List<TideInfo> GetTides(XDocument doc, ref List<DailyTides> days)
        {
            var dataElem = doc.Root.Element("data");
            var items = dataElem.Elements("item");
            List<TideInfo> Tides = new List<TideInfo>();


            foreach (var item in items)
            {
                TideInfo tempTide = new TideInfo();

                tempTide.TideStationId = GetStationId(doc);
                tempTide.DayId = GetDateId(doc, item.Element("date").Value, ref days);
                tempTide.Height = item.Element("predictions_in_cm").Value;
                tempTide.Time = item.Element("time").Value;
                tempTide.Hilo = item.Element("highlow").Value;

                Tides.Add(tempTide);
            }

            return Tides;
        }

		public static void GetTideDoc(string url)
		{
			using (var client = new WebClient ()) {
				Uri uri = new Uri (url, UriKind.Absolute);
				client.DownloadDataCompleted += Async_DownloadDone;
				client.DownloadDataAsync (new Uri (url));
			}

		}

		public static string DownloadString(string url)
		{
			string text;
			using (var client = new WebClient ()) {
				text = client.DownloadString (url);
			}
			return text;
		}

		public static void SaveXmlFile(string data, string name)
		{
			File.WriteAllText (name, data);
		}

		public static XDocument GetXml(string filename)
		{
			return XDocument.Load (filename);
		}

		public static void Async_DownloadDone(object sender, DownloadDataCompletedEventArgs e)
		{
			//XDocument doc = e.Result;
		}


        //public static List<string[]> GetItems(XDocument doc)
        //{
        //    List<string[]> Dates = new List<string[]> ();

        //    var dataElem = doc.Root.Element ("data");
        //    var items = dataElem.Elements ("item");

        //    foreach (var el in items) {

        //        string tempDate = el.Element("date").Value;
        //        string tempDay = el.Element ("day").Value;
        //        string tempTime = el.Element ("time").Value;
        //        string tempPTF = el.Element ("predictions_in_ft").Value;
        //        string tempPTC = el.Element ("predictions_in_cm").Value;
        //        string tempHighlo = el.Element ("highlow").Value;

        //        string[] tempArray = { tempDate, tempDay, tempTime, tempPTF, tempPTC, tempHighlo };

        //        if(!DateExists(Dates, tempDate))    
        //            Dates.Add (tempArray);
        //    }

        //    return Dates;
        //}
	}
}