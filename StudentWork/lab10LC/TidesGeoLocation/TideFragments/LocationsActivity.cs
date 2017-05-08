using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections;
using SQLite;
using TideFragments.DAL;
using Xamarin.Geolocation;
using System.Threading.Tasks;



//eugene long -123.2144  lat 44.133

namespace TideFragments
{
		[Activity (Label = "", MainLauncher = true)]	
		#if USE_SUPPORT
		public class DetailsActivity : FragmentActivity
		#else		
		public class LocationsActivity : ListActivity
		#endif
		{
			string[] arrLoc;			
			private int _selectedLocId;
		double currentLat=0;
		double currentLong=0;
		string currentMonth=" ";
		string currentDay=" ";
		string currentYear=" ";
		string currentTime=" ";
		string currentDate=" ";
		double distance;
		string station;
		string stationName;
		string my = " ";
		string tides;// = "Tide Time         Height            High/Low    \t\n";
		const double  distanceToStation=2000;// using as a reference to look for Stations in a 100Km radios from current location
		List<TideTable> tideTableList = new List<TideTable> ();


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			string dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "GeoLocStations.db3");
			SetContentView (Resource.Layout.GeoLocation);	

			Button button = FindViewById<Button> (Resource.Id.Button1);
			TextView textView = FindViewById<TextView> (Resource.Id.tvDisplay);

			var locator = new Geolocator (this) { DesiredAccuracy = 100 };

			// Check if your DB has already been extracted.
			if (!File.Exists (dbPath)) {

				using (Stream inStream = Assets.Open ("StationsDB.db3"))
				using (Stream outStream = File.Create (dbPath))
					inStream.CopyTo (outStream);

			}

	      button.Click += delegate {
		 locator.GetPositionAsync (timeout: 1000)
		// After getting position info or timing out, execution will continue here
		// t represents a Task object (GetPositionAsync returns a Task object)
		.ContinueWith (t => {
						try{
			currentDate = t.Result.Timestamp.ToString ("yyyy/M/d");
			currentMonth = t.Result.Timestamp.ToString ("MM");
			currentDay = t.Result.Timestamp.ToString ("dd");
			currentYear = t.Result.Timestamp.ToString ("yyyy");
			currentTime = t.Result.Timestamp.ToString ("h:mm:ss tt");
			currentLat = Convert.ToDouble (t.Result.Latitude);
		    currentLong = Convert.ToDouble (t.Result.Longitude);
			
				var mydb = new SQLiteConnection (dbPath);

					var latandlong = mydb.Query<Location> ("SELECT LocationId,Latitude,Longitude FROM Location");

					//looping through Location table Longitude and Latitude  to check if there are any stations within 100Km from current 
					for (int ab = 0; ab < latandlong.Count; ab++) {
						
						double	distance = DistanceInKms (currentLat, currentLong, Convert.ToDouble(latandlong[ab].Latitude), Convert.ToDouble(latandlong[ab].Longitude) );
						string station=latandlong[ab].LocationId;
							if (distance <= distanceToStation)//if there is a station withing our range
							{
							//get the name of the closes station
							stationName = mydb.Query<Location> ("SELECT LocationName FROM Location WHERE LocationId=?", station).First().LocationName;
							//call the TidesFromCurrent method 
							textView.Text= TidesFromCurrent( station, currentDate, dbPath, currentLat, currentLong);
						    }
						else{
								textView.Text=" Sorry there is not a tide Station within 100 Km from your current Location,  try selecting from the list";
						   }									
		}
						} catch (Exception ex) {
					textView.Text += ex.ToString ();
				}
				// Specify the thread to continue on- it's the UI thread
			}, TaskScheduler.FromCurrentSynchronizationContext ());


};

			var db = new SQLiteConnection (dbPath);
				
			//var myLoc = db.Query<TideTable> ("SELECT * FROM TideTable ");// this works

			var allFromTT = db.Query<TideTable> ("SELECT * FROM TideTable");
			var myLoc = db.Query<Location> ("SELECT * FROM Location");
			string[] locations = new string[myLoc.Count];
			for (int lo = 0; lo < myLoc.Count; lo++) {
				locations [lo] = myLoc [lo].LocationName;
			}

			arrLoc = locations;
			//}

			ListAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, arrLoc);
		}


		
		//	}

			protected override  void OnListItemClick(ListView l,View v, int position, long id)//(ListView l, View v, int position, long id)
			{

				ShowDates(position);
				ListAdapter.Dispose ();

			}


			private void ShowDates(int locId)
			{
				_selectedLocId= locId;

				Intent dateIntent = new Intent (this, typeof(DatesActivity));
				dateIntent.PutExtra ("_selectedLocId", locId);
				StartActivity (dateIntent);

			}


		public static Double DistanceInKms(double lat1, double lon1, double lat2, double lon2)
		{
			// haversine formula to calculate distance between 2 lat and long 
			if (lat1 == lat2 && lon1 == lon2)
				return 0.0;

			double theta = Math.Abs(lat2 - lat1);
			double thetaR = theta * Math.PI / 180;
			double lambda = Math.Abs(lon2 - lon1);
			double lamdaR = lambda * Math.PI / 180;

			double a = Math.Sin (thetaR / 2)*Math.Sin (thetaR / 2) + Math.Cos (lat1) * Math.Cos (lat2) *Math.Sin (lamdaR/2 )*Math.Sin (lamdaR / 2);
			double c = 2 * Math.Atan2 (Math.Sqrt (a), Math.Sqrt (1 - a));
			double R = 6371;
			double distance = R * c;


			//distance = Math.Acos(distance);
			if (double.IsNaN(distance)) return 0.0;


			return (distance);
		}


		public  string TidesFromCurrent(string stationId, string currentDate,string dbPath, double currentLat, double currentLong)
		{

			using (var mydb = new SQLiteConnection (dbPath)) {
				tides = "Location                    Tide Time         Height            High/Low    \t\n";
				//var latandlong = mydb.Query<Location> ("SELECT LocationId,Latitude,Longitude FROM Location");

				//looping through Location table Longitude and Latitude  to check if there are any stations within 100Km from current 
				//for (int ab = 0; ab < latandlong.Count; ab++) {
				//distance = DistanceInKms (currentLat, currentLong, Convert.ToDouble(latandlong[ab].Latitude), Convert.ToDouble(latandlong[ab].Longitude) );
				//	double	distance = DistanceInKms (43.7833, 11.2500, Convert.ToDouble(latandlong[ab].Latitude), Convert.ToDouble(latandlong[ab].Longitude) );
				//	string station=latandlong[ab].LocationId;
				//	if (distance <= distanceToStation)//if there is a station withing our range
				//	{

				var todas = mydb.Query<TideTable> ("SELECT * FROM TideTable WHERE LocationId= ?", stationId); //getting all dates for that LocationId

				//if it is not in local database get it from web service
				if (todas.Count == 0) {

					NOAAApiClient noaaClient = new NOAAApiClient ();					
					noaaClient.GetAll (station);
					var webSerData = noaaClient.GetAll (station);
					//looping through list of DataInfo objects to add to table in database
					for (int i = 0; i < webSerData.Count (); i++) {

						string date = webSerData [i].date;
						string day = webSerData [i].day;
						string time = webSerData [i].time;
						string predft = webSerData [i].predictions_in_ft.ToString ();
						string hl = webSerData [i].highlow;

						mydb.Insert (new TideTable () {
							LocationId = station,
							Date = date,
							Day = day,
							Time = time,
							PredictedFt = predft,
							HighLow = hl
						});
					} 
					//query from database after added ...
					var resAfterAdded = mydb.Query<TideTable> ("SELECT * FROM TideTable WHERE LocationId=?", station);              

					//find the TideTable with the currentDate
					var selected = (from TideTable d in resAfterAdded
					                where d.Date == currentDate
					                select d).ToList ();

					for (int ii = 0; ii < resAfterAdded.Count (); ii++) {

						tides += resAfterAdded [ii];

					}
					//tideTableList = resAfterAdded.Distinct (new TideTableComparer ()).ToList ();
					return tides;

				} //end of if not in our database


				else {   //todas is a list of all dates for the station
					var fe = from n in todas
					           where n.Date.ToString () == currentDate
					           select new {
								n.Time,
								n.Day,
								n.PredictedFt,
								n.HighLow};

					foreach (var l in fe) {


						string tideInfo = Convert.ToString (stationName + l.Time + "   " + l.PredictedFt + "    ft" + "         " + l.HighLow + "\t\n");

						tides += tideInfo;
						my = tides;
					}

					mydb.Close ();
				}

				return tides;

			}		

		
	}	




		}

	}










