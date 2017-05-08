using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Portable
{
	public class XmlStorage
	{

		public XmlStorage ()
		{
		}
		#if __IOS__ || __ANDROID__
		public List<TodoItem> ReadXml (string filename)
		{
			if (File.Exists (filename)) {
				var serializer = new XmlSerializer (typeof(List<TodoItem>));
				using (var stream = new FileStream (filename, FileMode.Open)) {
					return (List<TodoItem>)serializer.Deserialize (stream);
				}
			}
			return new List<TodoItem> ();
		}

		public void WriteXml (List<TodoItem> tasks, string filename)
		{
			var serializer = new XmlSerializer (typeof(List<TodoItem>));
			using (var writer = new StreamWriter (filename)) {
				serializer.Serialize (writer, tasks);
			}
		}
		#elif WINDOWS_PHONE
		public List<Task> ReadXml(string filename)
		{
			IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();

			if (fileStorage.FileExists(filename))
			{
				var serializer = new XmlSerializer(typeof(List<Task>));

				using (var stream = new StreamReader(new IsolatedStorageFileStream(filename, FileMode.Open, fileStorage)))
				{
				return (List<Task>)serializer.Deserialize(stream);
				}
			}
			return new List<Task>();
		}

		public void WriteXml(List<Task> tasks, string filename)
		{
			IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();

			var serializer = new XmlSerializer(typeof(List<Task>));
			using (var writer = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.OpenOrCreate, fileStorage)))
			{
				serializer.Serialize(writer, tasks);
			}
		}
		#endif
	}
}

