using System;
using SQLite;
using System.Collections.Generic;
using Domain;
using System.Xml.Linq;
using System.Linq;

namespace DAL
{
	static public class DbControls
	{
		public static string _PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

		public static void BuildSchema(SQLiteConnection db)
		{
			// Lock the Database so we don't access it from another thread
			lock (db) {
				db.BeginTransaction ();

				// Build Tables
				db.CreateTable<TideInfo> ();
				db.CreateTable<DailyTides> ();
				db.CreateTable<TideStation> ();

				db.Commit ();
			}
		}

		public static void EmptyDB(SQLiteConnection db)
		{
			// Lock the Database so we don't access it from another thread
			lock (db) {
				db.BeginTransaction ();

				// Empty tables
				db.DeleteAll<TideStation> ();
				db.DeleteAll<DailyTides> ();
				db.DeleteAll<TideInfo> ();

				db.Commit ();
			}
		}

        public static void Seed(SQLiteConnection db, XDocument doc)
        {
			var Days = GetDaysForStation (db);
            
			DbControls.InsertStation(db, ReadFiles.GetStationId(doc), ReadFiles.GetStationName(doc), ReadFiles.getLocation(doc));
            var Tides = ReadFiles.GetTides(doc, ref Days);

            DbControls.InsertTides(db, Tides);
        }

        #region /* Insert records to the db */

        public static void InsertStation(SQLiteConnection db, string id, string station, string location  )
		{
            lock (db) {
                db.BeginTransaction();

                db.Insert(new TideStation { TideStationId = id, StationName = station, Location = location });

                db.Commit();
            }
		}

        public static void InsertStation(SQLiteConnection db, TideStation station)
        {
            lock(db) {
                db.BeginTransaction();

                db.Insert(station);

                db.Commit();
            }
        }

        public static void InsertStations(SQLiteConnection db, List<TideStation> stations)
        {
            lock(db) {
                db.BeginTransaction();
                foreach (var station in stations)
                    db.Insert(station);
                db.Commit();
            }
        }

        public static void InsertStations(SQLiteConnection db, TideStation[] stations)
        {
            lock(db) {
                db.BeginTransaction();
                foreach (var station in stations)
                    db.Insert(station);
                db.Commit();
            }
        }

        public static void InsertDay(SQLiteConnection db, string stationId, string day, string date)
        {
            lock (db)
            {
                db.BeginTransaction();

				db.Insert(new DailyTides { Day = day, Date = date });

                db.Commit();
            }
        }

        public static void InsertDay(SQLiteConnection db, DailyTides day)
        {
            lock (db)
            {
                db.BeginTransaction();

                db.Insert(day);

                db.Commit();
            }
        }

        public static void InsertDays(SQLiteConnection db, List<DailyTides> days)
        {
            lock (db) {
                db.BeginTransaction();
				foreach (var day in days) {
					db.Insert (day);
				}
                db.Commit();
            }
        }

        public static void InsertDays(SQLiteConnection db, DailyTides[] days)
        {
            lock (db) {
                db.BeginTransaction();
                foreach (var day in days)
                    db.Insert(day);
                db.Commit();
            }
        }

        
        public static void InsertTide(SQLiteConnection db, int dayId, string stationId, string time, string height, string hilo)
        {
            lock (db) {
                db.BeginTransaction();

                db.Insert(new TideInfo() { DayId = dayId, TideStationId = stationId, Height = height, Hilo = hilo });

                db.Commit();
            }
        }

        public static void InsertTide(SQLiteConnection db, TideInfo tide)
        {
            lock (db) {
                db.BeginTransaction();

                db.Insert(tide);

                db.Commit();
            }
        }

        public static void InsertTides(SQLiteConnection db, List<TideInfo> tides)
        {
            lock (db) {
                db.BeginTransaction();

                foreach (var tide in tides)
                    db.Insert(tide);

                db.Commit();
            }
        }

        public static void InsertTides(SQLiteConnection db, TideInfo[] tides)
        {
            lock (db)
            {
                db.BeginTransaction();

                foreach (var tide in tides)
                {
                    db.Insert(tide);
                }

                db.Commit();
            }
        }

        private static int FindDateIdByDate(SQLiteConnection db, string date)
        {
            return db.Find<DailyTides>(t => t.Date == date).DailyTidesId;
        }
        #endregion

        #region /* Get Station Info by ID */

        public static TideStation GetStationById(SQLiteConnection db, string id)
        {
            TideStation existingItem = db.Get<TideStation>(t => t.TideStationId == id);
            return existingItem;
        }

        public static string GetStationName(SQLiteConnection db, string id)
        {
            TideStation existingItem = db.Get<TideStation>(t => t.TideStationId == id);
            return existingItem.StationName;
        }

        public static string GetStationLocation(SQLiteConnection db, string id)
        {
            TideStation existingItem = db.Get<TideStation>(t => t.TideStationId == id);
            return existingItem.Location;
        }

        #endregion

        #region /* Retrieve Info, returns tables from the db as list of C# Objects */

        public static List<TideStation> GetAllStations(SQLiteConnection db)
        {
            var StationTable = db.Table<TideStation>();
            List<TideStation> Stations = new List<TideStation>();

            lock (db)
            {
                db.BeginTransaction();
                foreach (var station in StationTable)
                    Stations.Add(station);
                db.Commit();
            }

            return Stations;
        }

		public static List<TideInfo> GetAllTides(SQLiteConnection db)
		{
			List<TideInfo> Tides = new List<TideInfo> ();

			lock(db){
				db.BeginTransaction ();
				var TideTable = db.Table<TideInfo> ();

				foreach (var tide in TideTable)
					Tides.Add (tide);

				db.Commit ();

			}

			return Tides;
		}

		public static List<DailyTides> GetDaysForStation(SQLiteConnection db)
		{
            var DayTable = db.Table<DailyTides>();
            List<DailyTides> Days = new List<DailyTides>();

            lock (db)
            {
                db.BeginTransaction();
                foreach (var day in DayTable)
                    Days.Add(day);
                db.Commit();
            }

            return Days;
        }

		public static List<TideInfo> GetTidesForDay(SQLiteConnection db, int dayId, string stationId)
		{
            var TideTable = db.Table<TideInfo>();
            List<TideInfo> Tides = new List<TideInfo>();

            lock (db)
            {
                db.BeginTransaction();
                foreach (var tide in TideTable.Where(t => t.TideStationId == stationId && t.DayId == dayId))
                    Tides.Add(tide);
                db.Commit();
            }

            return Tides;
        }

        public static List<TideInfo> GetAllTides(SQLiteConnection db, string stationId)
        {
            var TideTable = db.Table<TideInfo>();
            List<TideInfo> Tides = new List<TideInfo>();

            lock (db) 
            {
                db.BeginTransaction();

                foreach (var tide in TideTable.Where(t => t.TideStationId == stationId))
                    Tides.Add(tide);

                db.Commit();
            }

            return Tides;
        }
        #endregion

		#region /* Get Entry as String */

		public static List<string> GetAllTidesStrings(SQLiteConnection db, string stationId)
		{
			var TideTable = db.Table<TideInfo>();
			List<string> Tides = new List<string>();

			lock (db) 
			{
				db.BeginTransaction();

				foreach (var tide in TideTable.Where(t => t.TideStationId == stationId))
					Tides.Add(tide.ToString());

				db.Commit();
			}

			return Tides;
		}

		public static List<string> GetDaysAsStrings(SQLiteConnection db)
		{
			var DayTable = db.Table<DailyTides>();
			List<string> Days = new List<string>();

			lock (db)
			{
				db.BeginTransaction();
				foreach (var day in DayTable)
					Days.Add(day.ToString());
				db.Commit();
			}

			return Days;
		}

		public static string[] GetDaysAsStringArray(SQLiteConnection db)
		{
			var DayTable = db.Table<DailyTides>();
			List<string> Days = new List<string>();

			lock (db)
			{
				db.BeginTransaction();
				foreach (var day in DayTable)
					Days.Add(day.ToString());
				db.Commit();
			}

			return Days.ToArray ();
		}

		public static List<string> GetAllStationsStrings(SQLiteConnection db)
		{
			var StationTable = db.Table<TideStation>();
			List<string> Stations = new List<string>();

			lock (db)
			{
				db.BeginTransaction();
				foreach (var station in StationTable)
					Stations.Add(station.ToString());
				db.Commit();
			}

			return Stations;
		}
		#endregion
    }
}