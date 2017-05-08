using System;
using System.Linq;
using System.Collections.Generic;

namespace Portable {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public class TodoItemManager {
		List<TodoItem> items;

		// TODO: Step1a: add Action
//		public Action Saved {get;set;}

		public TodoItemManager () 
		{
			items = new List<TodoItem> ();
			// TODO: Step1ab: set default 
//			Saved = () => {}; 
		}

		public TodoItem GetTask(int id)
		{
			return (from i in items
				where i.ID == id
				select i).FirstOrDefault ();
		}
		
		public List<TodoItem> GetTasks ()
		{
			return items;
		}
		int max;
		public int SaveTask (TodoItem item)
		{
			if (item.ID <= 0) {
				item.ID = ++max;
				items.Add (item);
			}
			// TODO: Step1c: call out to platform code
//			Saved ();
			return 1;
		}
		
		public int DeleteTask(TodoItem item)
		{
			return items.Remove (item)?1:0;
		}
	}
}