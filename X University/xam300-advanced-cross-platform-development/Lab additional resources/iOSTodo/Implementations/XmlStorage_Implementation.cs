//using System;
//using Portable;
//using System.Xml.Serialization;
//using System.Collections.Generic;
//using System.IO;
//
//namespace iOSTodo
//{
//	public class XmlStorageImplementation : IXmlStorage
//	{
//		public XmlStorageImplementation ()
//		{
//		}
//
//		public List<TodoItem> ReadXml (string filename)
//		{
//			// the File class is not available in PCL
//			if (File.Exists (filename)) {
//				var serializer = new XmlSerializer (typeof(List<TodoItem>));
//				using (var stream = new FileStream (filename, FileMode.Open)) {
//					return (List<TodoItem>)serializer.Deserialize (stream);
//				}
//			}
//			return new List<TodoItem> ();
//		}
//
//		public void WriteXml (List<TodoItem> tasks, string filename)
//		{
//			var serializer = new XmlSerializer (typeof(List<TodoItem>));
//			using (var writer = new StreamWriter (filename)) {
//				serializer.Serialize (writer, tasks);
//			}
//		}
//	}
//}
//
