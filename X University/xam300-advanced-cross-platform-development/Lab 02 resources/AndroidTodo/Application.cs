using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using System.IO;
using AndroidTodo;
using Portable;

namespace AndroidTodo {
    [Application]
	public class AppDelegate : Application {

		public static AppDelegate Current { get; private set; }

		public TodoItemManager TaskMgr { get; set; }

		public AppDelegate(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer) {
                Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

			// Calculate a file path for the database file
			var sqliteFilename = "TodoSqliteDB.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);

			// TODO: Android Step5: SQLite (don't forget to add NuGet)
//			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
//			var conn = new SQLite.Net.SQLiteConnection(plat, path);
//			TaskMgr = new TodoItemManager(conn);

        }
    }
}