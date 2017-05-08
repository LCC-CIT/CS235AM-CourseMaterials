/// Author:         Anthony Baker
/// Date:           May 4th, 2013
/// Description:    JsonAPIClient - Sample JSON API Consumption
/// Modified to use XML by Brian Bird, 11/24/2013

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace xmlApiClient
{
    public class WeatherApiClient
    {
        #region fields

        /// <summary>
        /// Base URL for the Weather Endpoint URL
        /// </summary>
		private const string baseUrl = "http://api.openweathermap.org/data/2.5/find?lat={0}&lon={1}&cnt={2}&mode=xml";

        #endregion

        #region methods

        /// <summary>
        /// Retrieves the Weather Forecast data synchronously.
        /// </summary>
        /// <param name="latitude">latitude of the geolocation</param>
        /// <param name="longitude">longitud of the geolocation</param>
        /// <param name="stationQuantity">number of weather stations to be included</param>
		public void GetWeatherForecast(string latitude, string longitude, int stationQuantity) 
        {
            // Customize URL according to geo location parameters
            var url = string.Format(baseUrl, latitude, longitude, stationQuantity);

            // Syncronious Consumption
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);

			// Create an XML serializer and parse the response
			XmlSerializer serializer = new XmlSerializer(typeof(CitiesWeather));
			CitiesWeather weatherData;
			using (XmlReader reader = XmlReader.Create(new StringReader(content)))
			{
				// deserialize the XML object using the WeatherData type
				weatherData = (CitiesWeather)serializer.Deserialize(reader);
			}
			// DisplayResults (weatherData);
        }

        /// <summary>
        /// Retrieves the Weather Forecast data asynchronously.
        /// </summary>
        /// <param name="latitude">latitude of the geolocation</param>
        /// <param name="longitude">longitud of the geolocation</param>
        /// <param name="stationQuantity">number of weather stations to be included</param>
		public void GetWeatherForecastAsync(string latitude, string longitude, int stationQuantity)
        {
            // Customize URL according to geo location parameters
            var url = string.Format(baseUrl, latitude, longitude, stationQuantity);

            // Async Consumption
            var asyncClient = new WebClient();
            asyncClient.DownloadStringCompleted += asyncClient_DownloadStringCompleted;
            asyncClient.DownloadStringAsync(new Uri(url));

            // Do something else...
            System.Threading.Thread.Sleep(1000);     // wait a second

            Console.WriteLine("Done doing seomething else");
        }

        /// <summary>
        /// Parses the weather data once the response download is completed.
        /// </summary>
        /// <param name="sender">object that originated the event</param>
        /// <param name="e">event arguments</param>
        void asyncClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
			// Create an XML serializer and parse the response
			XmlSerializer serializer = new XmlSerializer(typeof(CitiesWeather));
			CitiesWeather weatherData;
			using (XmlReader reader = XmlReader.Create(new StringReader(e.Result)))
			{
				// deserialize the XML object using the WeatherData type
				weatherData = (CitiesWeather)serializer.Deserialize(reader);
			}
			DisplayResults (weatherData);
        }


		void DisplayResults(CitiesWeather citiesWeather)
		{
			citiesWeather.calctime.ToString ();
			foreach(CitiesItem item in citiesWeather.list)
			{
				Console.WriteLine(item.city.name);
				Console.WriteLine(item.weather.value);
                Console.WriteLine("Windspeed: {0}, direction {1}", item.wind.speed.value, item.wind.direction.value);
				Console.WriteLine();
			}
		}
			
        #endregion
    }
}
