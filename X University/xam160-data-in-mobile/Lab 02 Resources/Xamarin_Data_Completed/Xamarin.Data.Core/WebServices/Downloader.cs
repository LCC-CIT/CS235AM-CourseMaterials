using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Xamarin.Data.Core.WebServices
{
	public static class Downloader
	{
		static string sessionsXmlUrl = "https://dl.dropbox.com/u/4073168/Evolve2013/Sessions.xml";

		public async static Task DownloadSessionXmlAsync()
		{
			Console.WriteLine ("DownloadXml start");

			var file = SessionsXmlParser.Instance.SessionsXmlFilePath;

			if (File.Exists(file))
				File.Delete(file);

			await Task.Run (() => {
				using(var webclient = new WebClient())
					webclient.DownloadFile(sessionsXmlUrl, file);
			});

			Console.WriteLine ("DownloadXml end " + SessionsXmlParser.Instance.SessionsXmlFilePath);
		}  
	}
}