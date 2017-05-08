using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using DataAccess.DAL;
using System.Linq;
using Android;
using System.Collections.Generic;

// Note: the namespace DataAccess.Android caused resolve problems, so I cahnged it
namespace DataAccess.Droid
{
	[Activity (Label = "L2Ch3.DroidGUI", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			/* ------ SQLiteNet ORM initialization ------ */

			string dbPath = "";
			SQLiteConnection db = null;

			// Get the path to the database file that was deployed in Assets
			dbPath = Path.Combine (
				System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "stocks.db3");

			// copy the dB file to a read/write location
			{
				using (Stream inStream = Assets.Open ("stocks.db3"))
					using (Stream outStream = File.Create (dbPath))
						inStream.CopyTo(outStream);
			}

			// Open the database
			db = new SQLiteConnection (dbPath);
		
			/* ------ Spinner initialization ------ */  //See: https://developer.xamarin.com/guides/android/user_interface/spinner/

			Spinner stockSpinner = FindViewById<Spinner> (Resource.Id.stockSpinner);

			// Query databasae for stock symbols to show in the spinner
			var distinctStocks = db.Table<Stock>().GroupBy(s => s.Symbol).Select(s => s.First());
			var stockNames = distinctStocks.Select (s => s.Symbol).ToArray();

			// Set up the spinner adapter
			var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, stockNames);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			stockSpinner.Adapter = adapter;

			string selectedStockSymbol;

			stockSpinner.ItemSelected += delegate(object sender, AdapterView.ItemSelectedEventArgs e) 
			{
				Spinner spinner = (Spinner)sender;
				selectedStockSymbol = (string)spinner.GetItemAtPosition (e.Position);
			};

			/* ------ DatePicker initialization ------ */

			var stockDatePicker = FindViewById<DatePicker> (Resource.Id.stockDatePicker);

			/*
			 // Min and Max date not being found correctly
			DateTime minDate = (from s in db.Table<Stock> ()
				select s).Min(s => s.Date);
			//stockDatePicker.DateTime = minDate;
			stockDatePicker.MinDate = minDate.Ticks;

			stockDatePicker.MaxDate = (from s in db.Table<Stock> ()
				select s).Max(s => s.Date).Ticks;
			*/
			/*
			// Trying this again with SQL, but
			// both min and max return Dec 31, 1969, which isn't even in the dB!
			List<Stock> minDateStocks = db.Query<Stock> ("select min(Date) from stocks", "");
			DateTime minDate = minDateStocks[0].Date;
			stockDatePicker.MinDate = minDate.Ticks;
			stockDatePicker.DateTime = minDate;
			*/

			// Min and Max doen't seem to work on a DateTime field, so I'll fudge
			// The first row inserted is the most recent and the last row is the oldest
			Stock maxDateStock = db.Get<Stock>((from s in db.Table<Stock>() select s).Min(s => s.ID));
			DateTime maxDate = maxDateStock.Date;
			stockDatePicker.DateTime = maxDate;

			/* ------ Button click: Query for stock price history, display in ListView ------ */

			TableQuery<Stock> stocksInDateRange = null;
			int dayCount = 0;

			Button listViewButton = FindViewById<Button> (Resource.Id.listViewButton);
			listViewButton.Click += delegate 
			{
				// Query for stock prices using Linq
				DateTime endDate = stockDatePicker.DateTime;
				DateTime startDate = endDate.AddDays(-14.0);
				stocksInDateRange = from s in db.Table<Stock> () 
						where (s.Date >= startDate) && (s.Date <= endDate) && (s.Symbol == selectedStockSymbol)
					select s;

				// Create an array of strings with symbols, dates and closing prices
				// and give it to the ListView adapter
				dayCount = stocksInDateRange.Count();
				string[] stockInfoArray = new string[dayCount];
				int i = 0;
				foreach(Stock s in stocksInDateRange)
					stockInfoArray[i++] = s.Symbol + "\t\t" + s.Date.ToShortDateString() + "\t\t" + s.ClosingPrice.ToString("C");

				ListView stocksListView = FindViewById<ListView> (Resource.Id.stocksListView);
				stocksListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, stockInfoArray);
			};
		}


	}
}


