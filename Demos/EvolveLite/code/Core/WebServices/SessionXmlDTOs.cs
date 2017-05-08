using System;
using System.Xml.Serialization;

namespace EvolveLite
{
// deserializing odd xml http://stackoverflow.com/questions/364253/how-to-deserialize-xml-document
/*
	<?xml version="1.0" encoding="UTF-8"?>
		<events>
			<event>
				<event_key>4</event_key>
				<active>Y</active>
				<title>Training Keynote/Introduction to Mobile Development</title>
				<event_start>2013-04-14 09:00</event_start>
				<event_end>2013-04-14 10:00</event_end>
				<description>Welcome to Xamarin Evolve Training Days and Introduction to Mobile Development</description>
				<venue>Dawkins Salon (4th Floor)</venue>
				<speakers>
					<person>
						<name>Nat Friedman</name>
						<bio></bio>
					</person>
					<person>
						<name>Bryan Costanich</name>
						<bio></bio>
					</person>
				</speakers>
				<tags>Fundamentals</tags>
			</event>
*/

	[Serializable]
	public class @event {
		
		[XmlElement("event_key")]
		public string EventKey {get;set;}
		
		[XmlElement("title")]
		public string title {get;set;}
		
		[XmlElement("description")]
		public string description {get;set;}
		
		[XmlElement("venue")]
		public string venue {get;set;}
		
		[XmlElement("event_type")]
		public string EventType {get;set;}
		
		[XmlElement("event_start")]
		public string EventStart {get;set;}
		[XmlElement("event_end")]
		public string EventEnd {get;set;}
		
		[XmlArray("speakers", IsNullable = true)]
		public person[] speakers {get;set;}
		
		[XmlElement("tags")]
		public string Tags {get;set;}
	}
	
	public class person {
		[XmlElement ("name", IsNullable = true)]
		public string name {get;set;}
	}
	
	[System.Xml.Serialization.XmlRootAttribute("events", Namespace = "", IsNullable = false)]
	public class EventCollection
	{
		[XmlElement("event")]
		public @event[] @event { get; set; }
	}
}

