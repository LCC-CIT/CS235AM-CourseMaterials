using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EvolveLite
{
	public static class Downloader
	{
		static string sessionsXmlUrl = "https://dl.dropbox.com/u/4073168/Evolve2013/Sessions.xml";
		static string speakersJsonUrl = "https://dl.dropbox.com/u/4073168/Evolve2013/Speakers.json";

		public static void DownloadSessionXml()
		{
			Console.WriteLine ("DownloadXml start");
			var file = SessionsXmlParser.Instance.SessionsXmlFilePath;
			if (File.Exists(file))
			{
				File.Delete(file);
			}
			
			var webclient = new WebClient();
			webclient.DownloadFile(sessionsXmlUrl, file);

			Console.WriteLine ("DownloadXml end " + SessionsXmlParser.Instance.SessionsXmlFilePath);
		}  

		public static void DownloadSpeakerJson()
		{
			Console.WriteLine ("DownloadJson start");
			var file = SpeakersJsonParser.Instance.SpeakersJsonFilePath;
			if (File.Exists(file))
			{
				File.Delete(file);
			}
			
			var webclient = new WebClient();
			webclient.DownloadFile(speakersJsonUrl, file);
			
			Console.WriteLine ("DownloadJson end" + SpeakersJsonParser.Instance.SpeakersJsonFilePath);
		}  
	}
}