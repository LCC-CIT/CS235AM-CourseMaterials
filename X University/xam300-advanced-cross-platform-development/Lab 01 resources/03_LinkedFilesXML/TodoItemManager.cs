using System;
using System.Collections.Generic;

namespace Portable {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public class TodoItemManager {
		TodoItemRepository repository;

		public TodoItemManager (string filename) 
		{
			repository = new TodoItemRepository(filename);
		}

		public TodoItem GetTask(int id)
		{
			return repository.GetTask(id);
		}
		
		public List<TodoItem> GetTasks ()
		{
			return new List<TodoItem>(repository.GetTasks());
		}
		
		public int SaveTask (TodoItem item)
		{
			return repository.SaveTask(item);
		}
		
		public int DeleteTask(TodoItem item)
		{
			return repository.DeleteTask(item.ID);
		}
	}
}