/// Author:         Anthony Baker
/// Date:           May 4th, 2013
/// Description:    JsonAPIClient - Sample JSON API Consumption
/// Modified by		Brian Bird, 	11/24/2013 to work with XML data

using System;

namespace xmlApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define Location Params
			string latitude = "44.2.47";		// Lat and Lon for Srpingfield, OR
			string longitude = "-123.1.14";
            int stationQuantity = 10;

			WeatherApiClient weatherClient = new WeatherApiClient ();
            // Get the weather forecaste data synchronously
			weatherClient.GetWeatherForecast(latitude, longitude, stationQuantity);

            // Get the weather forecast data asynchronously
			weatherClient.GetWeatherForecastAsync(latitude, longitude, stationQuantity);

            // Wait for user input - keep the program runnin
            Console.ReadLine();
        }
    }
}
