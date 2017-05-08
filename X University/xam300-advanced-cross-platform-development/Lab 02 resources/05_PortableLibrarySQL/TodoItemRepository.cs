using System;
using System.Collections.Generic;
using System.IO;
using SQLite.Net;


namespace Portable {

	public class TodoItemRepository {
		TodoItemDatabase db = null;
		protected static string dbLocation;		
		//protected static TaskRepository me;

        public TodoItemRepository(SQLiteConnection conn)
		{
			db = new TodoItemDatabase(conn);
		}

		public TodoItem GetTask(int id)
		{
            return db.GetItem<TodoItem>(id);
		}
		
		public IEnumerable<TodoItem> GetTasks ()
		{
			return db.GetItems<TodoItem>();
		}
		
		public int SaveTask (TodoItem item)
		{
			return db.SaveItem<TodoItem>(item);
		}

		public int DeleteTask(int id)
		{
			return db.DeleteItem<TodoItem>(id);
		}
	}
}

