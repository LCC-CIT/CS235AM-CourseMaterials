using System;
using System.Collections.Generic;
using System.IO;
using Tasky.BL;
using Tasky.DL.SQLiteBase;

namespace Tasky.DAL {
	public class TaskRepository {
		DL.TaskDatabase db = null;
		protected static string dbLocation;		
		//protected static TaskRepository me;

        public TaskRepository(SQLiteConnection conn, string dbLocation)
		{
			// set the db location
//			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new Tasky.DL.TaskDatabase(conn, dbLocation);
		}
		
//        public static string DatabaseFilePath {
//            get { 
//                var sqliteFilename = "TaskDB.db3";
//#if SILVERLIGHT
//                // Windows Phone expects a local path, not absolute
//                var path = sqliteFilename;
//#else

//#if __ANDROID__
//                // Just use whatever directory SpecialFolder.Personal returns
//                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
//#else
//                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
//                // (they don't want non-user-generated data in Documents)
//                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
//                string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
//#endif
//                var path = Path.Combine (libraryPath, sqliteFilename);
//#endif		
//                return path;	
//            }
//        }

		public Task GetTask(int id)
		{
            return db.GetItem<Task>(id);
		}
		
		public IEnumerable<Task> GetTasks ()
		{
			return db.GetItems<Task>();
		}
		
		public int SaveTask (Task item)
		{
			return db.SaveItem<Task>(item);
		}

		public int DeleteTask(int id)
		{
			return db.DeleteItem<Task>(id);
		}
	}
}

