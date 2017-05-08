using System;
using System.Collections.Generic;
using System.IO;

using System.Xml.Serialization;

namespace EvolveLite
{
	public class SessionsXmlParser
	{
		private static readonly SessionsXmlParser instance = new SessionsXmlParser();
		
		private List<Session> sessions;

		private SessionsXmlParser()
		{
			SessionsXmlFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sessions.xml");
		}
		
		public static SessionsXmlParser Instance { get { return instance; } }
		
		public string SessionsXmlFilePath { get; private set;  }

		/// <summary>
		/// Lazy creation of the list of sessions
		/// </summary>
		public List<Session> Sessions
		{
			get
			{
				if (sessions == null || sessions.Count == 0)
				{
					sessions = GetSessions();
				}
				return sessions;
			}
		}

			
		/// <summary>
		/// Example of parsing a JSON data file into 
		/// </summary>
		/// <returns>The sessions.</returns>
		private List<Session> GetSessions()
		{
			if (!File.Exists(SessionsXmlFilePath))
				return new List<Session>();

			var localSessions = new List<Session>();

			// Deserialize XML into DTO that matches the transport XML format (since we don't have control over it)
			try {
				XmlSerializer serializerXml = new XmlSerializer(typeof(EventCollection));
				object o;
				using (System.IO.Stream stream = new System.IO.FileStream(SessionsXmlFilePath, System.IO.FileMode.Open)) {
					o = serializerXml.Deserialize(stream);
					stream.Close();
				}
				EventCollection events = (EventCollection)o;
				
				foreach (var evnt in events.@event) {
					Console.WriteLine (" Title: " + evnt.title);
					var session = new Session() {
						Id = Convert.ToInt32 (evnt.EventKey),
						Title = evnt.title,
						Location = evnt.venue,
						Abstract = evnt.description,
						Begins = DateTime.Parse (evnt.EventStart),
						Ends = DateTime.Parse (evnt.EventEnd),
					};
					
					if (evnt.speakers != null) { // which happens for LUNCH and TBA sessions without a Speaker
						foreach (var sp in evnt.speakers) {
							if (!String.IsNullOrWhiteSpace(sp.name)) 
								session.Speaker = new Speaker() {Name = sp.name}; // hacky: replaces speaker if more than one
						}
					}
					localSessions.Add(session);
				}
				Console.WriteLine ("[ParseXml] Parsing succeeded: {0} sessions", localSessions.Count);
			} catch (Exception ex) {
				// something in the parsing failed
				Console.WriteLine ("[ParseXml] Parsing failed " + ex);
				return localSessions;
			}

			return localSessions;
		}

	}
}