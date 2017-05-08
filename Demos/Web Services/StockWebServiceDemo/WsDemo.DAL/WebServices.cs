using System;
using System.Net;
using System.Xml.Linq;
using System.IO;
//using System.Xml.Serialization;

namespace WsDemo.DAL
{
	public class WebServices
	{
		const string stockInfoUrl = @"http://www.google.com/ig/api?stock=";
		private string stockInfoFile;
		WebClient wc = new WebClient();
//		XmlSerializer xs = new XmlSerializer();

		public WebServices ()
		{
			stockInfoFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StockInfo.xml");
		}

		public void DownloadStockSymbols()
		{
		}

		public void GetStockInfo(string symbol)
		{
			string url = stockInfoUrl + symbol;
			/* This shoudl work, but doesn't
			 XDocument stockInfoXml = XDocument.Load (url); */

			// This code doesn't work XDocument.Parse returns stockInfoXml	Unknown sig element type: ELEMENT_TYPE_END
			 //string stockString = wc.DownloadString (url);
			// XDocument stockInfoXml = XDocument.Parse (stockString);

			// This didn't work either
			 //byte[] stockBytes = wc.DownloadData (url);
			//MemoryStream stockStream = new MemoryStream(stockBytes);

			Uri stockUri = new Uri (url);
			HttpWebRequest stockWebRequest = (HttpWebRequest)WebRequest.Create (stockUri);
			stockWebRequest.ContentType = "application/xml";
			//WebResponse stockWebResponse = stockWebRequest.GetResponse ();
			HttpWebResponse stockWebResponse = (HttpWebResponse)stockWebRequest.GetResponse ();
			StreamReader stockStreamReader = new StreamReader (stockWebResponse.GetResponseStream());
			string stockString = stockStreamReader.ReadToEnd ();
			XDocument stockInfoXml = XDocument.Load (stockStreamReader); 

			System.Console.WriteLine (stockInfoXml.Element("company"));

		}
	}
}

