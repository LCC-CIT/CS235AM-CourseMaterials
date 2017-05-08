using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(@"http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9434939"); // Here is my error occurs...
           // XmlSerializer serializer = new XmlSerializer(typeof(datainfo));
            Console.WriteLine(content);
        }
    }
}
