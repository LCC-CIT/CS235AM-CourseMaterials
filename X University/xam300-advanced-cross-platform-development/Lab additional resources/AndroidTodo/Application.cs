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



			TaskMgr = new TodoItemManager();
			// TODO: Android Step 1d: implement Action
//			TaskMgr.Saved = () => {
//				Android.Widget.Toast.MakeText(this, "Saved", Android.Widget.ToastLength.Short).Show();
//			};

			// TODO: Android Step2: XML PCL
//			var xmlFilename = "TodoList.xml";
//			var path = Path.Combine(documentsPath, xmlFilename);
//			var xmlStorage = new AndroidTodo.XmlStorageImplementation ();
//			TaskMgr = new TodoItemManager(path, xmlStorage);


			// TODO: Android Step3: XML LINKED FILES
//			var xmlFilename = "TodoList.xml";
//			var path = Path.Combine(documentsPath, xmlFilename);
//			TaskMgr = new TodoItemManager(path);


			// TODO: Android Step4: SQLite LINKED FILES
//			var conn = new Connection(path, plat);
//			TaskMgr = new TodoItemManager(conn);

        }
    }
}
