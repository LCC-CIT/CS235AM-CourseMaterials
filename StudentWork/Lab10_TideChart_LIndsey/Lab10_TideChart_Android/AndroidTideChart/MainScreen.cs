using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TideChartConsole;
using TideChart;
using SQLite;
using System.IO;
using Xamarin.Geolocation;
using Android.Locations;
using System.Net;

namespace AndroidTideChart
{
	[Activity (Label = "MainScreen",MainLauncher = true)]			
	public class MainScreen : Activity
	{
		private Button btnCurrentLocation;
		private Button btnChooseLocation;
		private TextView txtAddress;
		private TextView txtGps;
		private TextView txtStationInfo;
		private TextView txtTideInfo;

		double lat, lon;
		string zipcode;
		
		private Geolocator geolocator;
		private Geocoder geocoder;

		protected override void OnCreate (Bundle bundle)
		{
			if (!File.Exists (Connection.GetDBPath ())) {
				SQLiteConnection db = new SQLiteConnection (Connection.GetDBPath());
				db.CreateTable<TideStationRow> ();
				db.CreateTable<TideRow> ();
			} 
			/*else 
			{
				File.Delete (Connection.GetDBPath());
				string temp = "Done";
			}*/

			base.OnCreate (bundle);

			SetContentView(Resource.Layout.MainScreen);

			//get item references
			btnCurrentLocation = FindViewById<Button> (Resource.Id.btn_CurrentLocation);
			btnChooseLocation = FindViewById<Button> (Resource.Id.btn_ChooseLocation);
			txtAddress = FindViewById<TextView> (Resource.Id.txt_LocationAddress);
			txtGps = FindViewById<TextView> (Resource.Id.txt_LocationCoords);
			txtStationInfo = FindViewById<TextView> (Resource.Id.txt_StationInfo);
			txtTideInfo = FindViewById<TextView> (Resource.Id.txt_TideInfo);

			btnCurrentLocation.Click += OnGetPosition;

			btnChooseLocation.Click += (sender, e) => 
			{
				Intent stationSelection = new Intent(this, typeof(StationSelection));
				StartActivity(stationSelection);
			};

			// Create your application here
		}

		private void Setup()
		{
			if (geolocator != null)
				return;
			this.geolocator = new Geolocator (this) { DesiredAccuracy = 50 };
		}

		private void SetupCoder()
		{
			if (geocoder != null)
				return;
			geocoder = new Geocoder (this);
		}

		private void OnGetPosition (object sender, EventArgs e)
		{
			Setup ();
			SetupCoder ();

			this.geolocator.GetPositionAsync (timeout: 10000)
				.ContinueWith (t => RunOnUiThread (() =>
				{
					if (t.IsFaulted)
						txtGps.Text = ((GeolocationException)t.Exception.InnerException).Error.ToString();
					else
					{
						lat = t.Result.Latitude;
						lon = t.Result.Longitude;
						txtGps.Text = string.Format("Lat: {0}, Lon: {1}",lat.ToString("N4"),lon.ToString("N4"));
						
						try
						{
							IList<Address> addresses = geocoder.GetFromLocation(lat,lon,1);

							zipcode = addresses[0].PostalCode;

							txtAddress.Text = string.Format("{0}, {1} {2}", 
							                                addresses[0].GetAddressLine(0),
							                                addresses[0].GetAddressLine(1),
							                                addresses[0].GetAddressLine(2));
							GetStationInfo();
						}
						catch
						{
							txtAddress.Text = "Unable to get address from lat/lon";
						}
					}
				}));
		}

		private void GetStationInfo()
		{
			string url = @"http://tidesandcurrents.noaa.gov/geo/index.jsp?location=";
			string cut1 = @"station_info.shtml?stn=";

			string cut2 = "<a href=\"/geo/index.jsp?location=";

			try
			{
				WebRequest request = WebRequest.Create (url + zipcode);
				WebResponse response = request.GetResponse ();

				using (StreamReader reader = new StreamReader (response.GetResponseStream()))
				{
					string html = reader.ReadToEnd();
					string[] splitHtml = html.Split(new string[] {cut1}, StringSplitOptions.RemoveEmptyEntries);
					int ClosestStationID = int.Parse(splitHtml[1].Substring(0,7));

					int index = 1;

					string[] secondSplitHtml = html.Split(new string[] {cut2}, StringSplitOptions.RemoveEmptyEntries);

					TideStations stations = new TideStations();

					string tideInfo = string.Empty;

					while (string.IsNullOrEmpty(tideInfo))
					{
						foreach (StationInfo station in stations.Stations)
						{
							if (station.StationID == ClosestStationID)
							{
								txtStationInfo.Text = station.StationName + " : " + station.StationID.ToString();
								foreach (DayInfo day in station.TheDays)
								{
									if (DateTime.Today.ToShortDateString().Equals(day.Date))
									{
										tideInfo += day.Date + "\n";
										foreach(TideInfo tide in day.TheTides)
										{
											tideInfo += tide.ToString() + "\n";
										}
										break;
									}
								}
								break;
							}
						}

						//pick next closest station if tideinfo is empty. 
						if (string.IsNullOrEmpty(tideInfo))
							ClosestStationID = int.Parse(secondSplitHtml[index++].Substring(0,7));
						//	tideInfo = "No info for location";
					}
					txtTideInfo.Text = tideInfo;
				}
			}catch{
			}
		}
	}
}

