
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace EvolveLite
{
	// TODO: Demo1: create a Manager for our database interactions
	public class ConferenceDatabase : SQLiteConnection
	{
		static object locker = new object ();
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "SessionDB.db3";
				
				#if NETFX_CORE
				// Windows Store (8/RT)
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#else
				
				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else
				
				#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif		
				
				#endif
				return path;	
			}
		}
		
		public ConferenceDatabase (string path) : base (path)
		{
			// create the SQLite database tables based on the Model classes
			CreateTable<Session> ();
			CreateTable<Speaker> ();
		}

		#region Sessions
		public List<Session> GetSessions () 
		{
			lock (locker) {
				return (from i in Table<Session> () select i).ToList ();
			}
		}
		
		public Session GetSession (int id)
		{
			lock (locker) {
				return Table<Session>().FirstOrDefault(x => x.Id == id);
			}
		}
		
		public int SaveSession (Session item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					Update (item);
					return item.Id;
				} else {
					return Insert (item);
				}
			}
		}

		public void SaveSessions (List<Session> sessions)
		{
			lock (locker) {
				InsertAll (sessions);
			}
		}

		public void DeleteSessions()
		{
			lock (locker) {
				DeleteAll<Session> ();
			}
		}

		public int DeleteSession (Session session) 
		{
			lock (locker) {
				return Delete<Session> (session.Id);
			}
		}
		#endregion Sessions

		#region Speakers
		public List<Speaker> GetSpeakers () 
		{
			lock (locker) {
				return (from i in Table<Speaker> () select i).ToList ();
			}
		}
		
		public Speaker GetSpeaker (string name)
		{
			lock (locker) {
				return Table<Speaker>().FirstOrDefault(x => x.Name == name);
			}
		}
		public Speaker GetSpeaker (int id)
		{
			lock (locker) {
				return Table<Speaker>().FirstOrDefault(x => x.Id == id);
			}
		}

		public int SaveSpeaker (Speaker item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					Update (item);
					return item.Id;
				} else {
					return Insert (item);
				}
			}
		}
		
		public void SaveSpeakers (List<Speaker> speakers)
		{
			lock (locker) {
				InsertAll (speakers);
			}
		}
		
		public void DeleteSpeakers()
		{
			lock (locker) {
				DeleteAll<Speaker> ();
			}
		}
		
		public int DeleteSession (Speaker speaker) 
		{
			lock (locker) {
				return Delete<Speaker> (speaker.Id);
			}
		}
		#endregion Speakers
	}
}

