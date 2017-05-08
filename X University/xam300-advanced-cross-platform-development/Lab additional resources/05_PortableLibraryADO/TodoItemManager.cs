using System;
using System.Collections.Generic;

namespace Portable
{
	public class TodoItemManager
	{
        ITodoItemDatabase database;

        public TodoItemManager(ITodoItemDatabase database) 
        {
            this.database = database;
        }

		public TodoItem GetTask(int id)
		{
            return database.GetItem(id);
		}
		
		public List<TodoItem> GetTasks ()
		{
            return new List<TodoItem>(database.GetItems());
		}
		
		public int SaveTask (TodoItem item)
		{
            return database.SaveItem(item);
		}
		
		public int DeleteTask(TodoItem item)
		{
            return database.DeleteItem(item.ID);
		}
		
	}
}