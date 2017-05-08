using System;
using System.Collections.Generic;
using SQLite;
using DAL;
using System.Xml.Linq;
using Domain;

namespace Tests
{
    class MainClass
    {
        // Console DB path and reference
        private static string dbPath = @"../../../Domain/Assets/Tides.db3";
        private static SQLiteConnection db = new SQLiteConnection(dbPath);

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello XML!");
         

            // Open our xml document for reading

            
			List<XDocument> docs = new List<XDocument> ();
			// Database stuff
			DbControls.BuildSchema (db);
			DbControls.EmptyDB (db);

			string xmlfile = @"../../../Domain/Assets/CoosBay2013.xml";
			docs.Add (XDocument.Load(xmlfile));
			xmlfile = @"../../../Domain/Assets/Florance2013.xml";
			docs.Add (XDocument.Load(xmlfile));
			xmlfile = @"../../../Domain/Assets/PortOrford2013.xml";
			docs.Add (XDocument.Load(xmlfile));
			xmlfile = @"../../../Domain/Assets/Reedsport2013.xml";
			docs.Add (XDocument.Load(xmlfile));
			xmlfile = @"../../../Domain/Assets/Seaside2013.xml";
			docs.Add (XDocument.Load(xmlfile));
            string[] stationIds = new string[5];

			DbControls.InsertDays (db, ReadFiles.GetDays(docs[0]));

			var days = DbControls.GetDaysForStation (db);
			foreach (var day in days)
				Console.WriteLine (day);

			foreach (var doc in docs) {

				DbControls.Seed (db, doc);
                int counter = 0;
				stationIds[counter] = ReadFiles.GetStationId (doc);

				//var Tides = DbControls.GetAllTides (db, currentStationId);

				// Test returning the station name and station id from an xml file
				Console.WriteLine ("Station Name: {0}", DbControls.GetStationName (db, stationIds[counter]));
				Console.WriteLine ("Station ID: {0}", ReadFiles.GetStationId (doc));
				Console.WriteLine ("Location: {0}", DbControls.GetStationLocation (db, stationIds[counter]));
                counter++;
			}

            
			string newSave = @"../../../Domain/Assets/newxml2013.xml";
			string sId = "TWC0831";
			string text = ReadFiles.DownloadString (TideStation.Address + sId);
			ReadFiles.SaveXmlFile (text, newSave);
			XDocument newDoc = ReadFiles.GetXml(newSave);
			Console.WriteLine ("Download Complete");

			DbControls.Seed (db, newDoc);
			Console.WriteLine ("Tide info saved to database");

			foreach (var day in DbControls.GetDaysForStation(db))
			{
				Console.WriteLine(day);
				foreach (var tide in DbControls.GetTidesForDay(db, day.DailyTidesId, sId))
					Console.WriteLine(tide);

				Console.WriteLine();
			}
        }
		
    }
}
