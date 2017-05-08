
using System;
using SQLite;
using System.IO;
//using DataAccess.DAL; 
using TideFragments.DAL;


using System.Collections.Generic;

namespace L2Ch3.ConsoleApp
{
	static class MyClass
	{

	

	}
	class MainClass
	{


		public static void Main (string[] args)
		{

			Console.WriteLine ("Hello SQLite-net Data!");

			//string dbPath = @"../../../DataAccess-Android/Assets/stocks.db3";

			string dbPath=@"../../../TideFragments/Assets/StationsDB.db3";



			//var db = new SQLiteConnection (dbPath);
		    bool exists = File.Exists (dbPath);
			var db = new SQLiteConnection (dbPath);


			var myLoc = db.Query<Location> ("SELECT * FROM Location");
			foreach (Location l in myLoc) {
				Console.WriteLine (l.Latitude,l.Longitude,l.LocationName);

			}
			//db.CreateTable<Location> ();
			//db.CreateTable<TideTable> ();
			//db.DropTable<Location>();

			var test=db.Query<TideTable>("SELECT Date FROM TideTable WHERE LocationId =?","9434098");
			foreach (TideTable l in test) {
				Console.WriteLine (l.Date);
			}
			//if (exists) {

				//	db.DeleteAll<TideTable>();
			//	db.DeleteAll<Location> ();
			//}


			var mymyLoc = db.Query<TideTable> ("SELECT * FROM TideTable");

			//for testing 9434939
			foreach (TideTable l in mymyLoc) {
				Console.WriteLine (l.Date, l.Day, l.HighLow);
				Console.WriteLine (" s");
			}

//			db.CreateTable<Location> ();
//			db.Insert (new Location (){ LocationName = "Astoria" ,LocationId = "9439040" ,Latitude = "46.1240", Longitude ="-123.4610" });
//
//
//			db.Insert (new Location (){ LocationName = "Charleston" , LocationId = "9432780" , Latitude = "43.2070" , Longitude = "-124.1930" });
//			db.Insert (new Location (){ LocationName = "Florence " , LocationId = "9434939" , Latitude ="44.0010" , Longitude = "-124.7030" });
//
//			db.Insert (new Location (){ LocationName = "South Beach" , LocationId = "9435380" , Latitude = "44.3750",Longitude = "-124.2050" });
//			db.Insert (new Location (){ LocationName = "Wauna" , LocationId = "9439099" , Latitude = "46.9060" , Longitude = "-123.2430"});
//
//			db.Insert (new Location (){ LocationName = "Port Orford" , LocationId = "9431649" , Latitude = "42.4430" , Longitude ="-124.2990" });
//			//	db.Insert (new Location (){ LocationName = "Seaside" , LocationId = "9438478" });
//			db.Insert (new Location (){ LocationName = "St Helens" , LocationId = "9439201" , Latitude = "45.5190" , Longitude ="-122.4780" });
//			//db.Insert (new Location (){ LocationName = "Yaquina Swamp" , LocationId = "9435355" , Latitude = "44.3690" , Longitude = "-123.5440"});
//			db.Insert (new Location (){ LocationName = "Coos Bay" , LocationId = "9432845" , Latitude = "43.3800" , Longitude = "-124.2150"});
//			db.Insert (new Location (){ LocationName = "Tillamook" , LocationId = "9437331" , Latitude = "45.4600" , Longitude = "-123.8450"});
//			db.Insert (new Location () {LocationName = "Seaside" ,	LocationId = "9438478" ,Latitude = "46.0000",Longitude = "-123.9220"});
//			db.Insert (new Location () {LocationName = "Bay City" ,	LocationId = "TWC0865" ,Latitude = "45.5167",Longitude = "-123.9000"});
//			db.Insert (new Location () {LocationName = "Brighton" ,	LocationId = "9437815" ,Latitude = "45.6700",Longitude = "-123.9250"});
//			db.Insert (new Location () {LocationName = "Waldport" ,	LocationId = "9434939" ,Latitude = "44.4344",Longitude = "-124.0581"});
//			db.Insert (new Location () {LocationName = "Reedsport" ,	LocationId = "9433501" ,Latitude = "43.7083",Longitude = "-124.0980"});
//			db.Insert (new Location () {LocationName = "Half Moon Bay" ,	LocationId = "9433445" ,Latitude = "43.6750",Longitude = "-124.1920"});



			var locations = db.Query<Location > ("SELECT * FROM Location");

			//for testing 9434939
			foreach (Location l in locations) {
				Console.WriteLine (l.Id.ToString(),l.LocationId,l.Latitude,l.Longitude,l.LocationName);
				Console.WriteLine (" s");
			}

			}

				//http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9438478


	}}

	

