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




namespace TideFragments
{
	[Activity (Label = "DatesActivity")]
	public class DatesActivity : Activity
	{
			
		private List<Tide> tempTideList = new List<Tide> ();
		//List<Tide> myList = new List<Tide> ();
		//List<string>LucysList= new List<string>();
		string[]arrLoc;
		ListView listview;
		string my="";
		int myLocId;
		int dateId;// = -1;
		string date;
		List<string>QueryList;
		List<TideTable> tideTableList = new List<TideTable> ();


		protected override void OnCreate(Bundle bundle) 
		{

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HomeScreen);
			myLocId = Intent.Extras.GetInt ("_selectedLocId");

			myLocId++;  //I have to add one because my list starts with Id=1;

			listview = FindViewById<ListView> (Resource.Id.list);

			if (bundle != null) {
				dateId = bundle.GetInt ("selectedDateId", -1);
				//LucysList= bundle.GetString("datesList",
				myLocId = bundle.GetInt ("_selectedLocId");
				listview.SetSelection (dateId);				
				//	ShowDetails (dateId);
			}

			string dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "GeoLocStations.db3");

			// Check if your DB has already been extracted.
			if (!File.Exists(dbPath))
			{

				using (Stream inStream =Assets.Open("StationsDB.db3"))
				using (Stream outStream = File.Create (dbPath))
					inStream.CopyTo(outStream);
			}

			using (var db = new SQLiteConnection (dbPath)) {

				var name = db.Query<Location> ("SELECT LocationId FROM Location WHERE Id = ?", myLocId).First ().LocationId;  //getting LocationId
				var res = db.Query<TideTable> ("SELECT Date FROM TideTable WHERE LocationId= ?", name); //getting all dates for that LocationId
				//if it is not in local database get it from web service
				if (res.Count == 0) {

					NOAAApiClient noaaClient = new NOAAApiClient ();
					noaaClient.GetDates (name);//
					//LucysList = noaaClient.GetDates (name).Distinct().ToList();
					var webSerData = noaaClient.GetAll (name);
					//looping through list of DataInfo objects to add to table in database
					for (int i = 0; i < webSerData.Count (); i++) {

						string date = webSerData [i].date;
						string day = webSerData [i].day;
						string time = webSerData [i].time;
						string predft = webSerData [i].predictions_in_ft.ToString ();
						string hl = webSerData [i].highlow;

						db.Insert (new TideTable () {
							LocationId = name,
							Date = date,
							Day = day,
							Time = time,
							PredictedFt = predft,
							HighLow = hl
						});
					}
					//query from database after added ...
					var resAfterAdded = db.Query<TideTable> ("SELECT Date FROM TideTable WHERE LocationId=?", name); 
					tideTableList = resAfterAdded.Distinct (new TideTableComparer ()).ToList ();
				} 
				else {
					//var ires = res.Distinct (new TideTableComparer ());
					tideTableList = res.Distinct (new TideTableComparer ()).ToList ();
			  }
				string[] dates = new string[tideTableList.Count];

				for (int lo = 0; lo < tideTableList.Count; lo++) {
					dates [lo] = tideTableList [lo].Date;
					}

					QueryList = dates.ToList ();
					arrLoc = dates;
				//}

			}

			listview.Adapter = new HomeScreenAdapter (this, QueryList );
			listview.FastScrollEnabled = true;
			listview.FastScrollAlwaysVisible = true;
			listview.ItemClick += OnListItemClick;
		}


		protected  void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)//(ListView l, View v, int position, long id)
		{
			ShowDetails (e.Position);
		}


		private void ShowDetails(int dateId)
		{

			date =QueryList [dateId];  //pointer to the selected index in LucysList
			my = " ";
			string tides = "Tide Time         Height            High/Low    \t\n";

			string dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "GeoLocStations.db3");

			// Check if your DB has already been extracted.
			if (!File.Exists(dbPath))
			{
				using (Stream inStream =Assets.Open("StationsDB.db3"))
				using (Stream outStream = File.Create (dbPath))
					inStream.CopyTo(outStream);

			}

			using (var db=new SQLiteConnection(dbPath))
			{

				var name = db.Query<Location> ("SELECT LocationId FROM Location WHERE Id = ?",myLocId).First ().LocationId;//getting LocationId for selected Location			
				var todas=db.Query<TideTable>("SELECT * FROM TideTable WHERE LocationId = ?", name);//all data for that location

				//if it is not in my local database call web service
				if (todas.Count == 0) {
					     
					NOAAApiClient noaaClient = new NOAAApiClient ();
					noaaClient.GetTidePredictions (name,date);//
					var DetailsList = noaaClient.GetTidePredictions (name,date);


					for(int det=0; det < DetailsList.Count();det++)
					{	
						//string tideInfo = Convert.ToString (l.Time + "   " + l.PredictedFt + "    ft" + "         " + l.HighLow + "\t\n");
						tides += DetailsList[det]+"\r\n";
						my = tides;
					}

					//arrLoc = lucysList.ToArray ();
					TextView tvtides = FindViewById<TextView> (Resource.Id.tvTimeInfo);
					tvtides.Text = tides;
					my = tides;

				} else {
					var res = db.Query<TideTable> ("SELECT DISTINCT Date FROM TideTable WHERE LocationId = ?", name); //unique date values
					var fechas = db.Query<TideTable> ("SELECT Date FROM TideTable WHERE Date = ?", res [dateId].Date); //selected  date
					var fecha = (from n in fechas
					            select n).First ().ToString ();
					var fe = from n in todas
					        where n.Date.ToString () == res [dateId].Date
					        select new {
					n.Time,
					n.Day,
					n.PredictedFt,
					n.HighLow};

					foreach (var l in fe) {
						string tideInfo = Convert.ToString (l.Time + "   " + l.PredictedFt + "    ft" + "         " + l.HighLow + "\t\n");

						tides += tideInfo;
						my = tides;
					}

					db.Close ();

					TextView mytides = FindViewById<TextView> (Resource.Id.tvTimeInfo);
					mytides.Text = tides;
					my = tides;
				}
			}
		}
		protected  override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);

			outState.PutInt("selectedDateId", dateId);
			outState.PutInt ("_selectedLocId", myLocId);//
		}



	}
}




