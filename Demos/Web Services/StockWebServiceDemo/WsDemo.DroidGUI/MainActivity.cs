using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using WsDemo.DAL;
using System.Linq;

namespace WsDemo.DroidGUI
{
	[Activity (Label = "WsDemo.DroidGUI", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			string dbPath = "";
			SQLiteConnection db = null;

			Button pathButton = FindViewById<Button> (Resource.Id.myButton);
			TextView text = FindViewById<TextView> (Resource.Id.myTextView);
			pathButton.Click += delegate 
			{
				dbPath = Path.Combine (
					System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "stocks.db3");

				text.Text = dbPath;

				// It seems you can read a file in Assets, but not write to it
				// so we'll copy our file to a read/write location
				//if(!File.Exists (dbPath))
				{
					using (Stream inStream = Assets.Open ("stocks.db3"))
						using (Stream outStream = File.Create (dbPath))
							inStream.CopyTo(outStream);
					text.Text += "\r\nCopied stocks.db3 from Assets";
				}

				db = new SQLiteConnection (dbPath);
				text.Text += "\r\nCreated a database connection";
			};

			Button insertButton = FindViewById<Button> (Resource.Id.insertButton);
			TextView insertText = FindViewById<TextView> (Resource.Id.insertTextView);
			insertButton.Click += delegate 
			{
				int count = 0;
				if( db.Find<Stock>("AMD") == null )
				{
					count += db.Insert(new Stock() {Symbol = "AMD", Name = "Advanced Micro Devices"});
					count += db.Insert(new Stock() {Symbol = "INTC", Name = "Intel"});
					count += db.Insert (new Stock() {Symbol = "SNCR", Name = "Synchronoss"});
					count += db.Insert (new Stock() {Symbol = "TDC", Name = "Teradata"});
					count += db.Insert (new Stock() {Symbol = "BBRY", Name = "BlackBerry"});
					count += db.Insert (new Stock() {Symbol = "NOK", Name = "Nokia"});
					count += db.Insert (new Stock() {Symbol = "IBM", Name = "International Business Machines"});
				}
				insertText.Text = string.Format("{0} rows inserted", count);
			};

			Button queryButton = FindViewById<Button> (Resource.Id.queryButton);
			TextView queryText = FindViewById<TextView> (Resource.Id.queryTextView);
			queryButton.Click += delegate 
			{
				var existingItem = db.Get<Stock> (x => x.Name == "Google");
				queryText.Text = string.Format ("Stock Symbol for Google: {0}", existingItem.Symbol);

				// Use the Get method with a primary key
				existingItem = db.Get<Stock> ("FB");
				queryText.Text += string.Format ("\r\nStock Name for Symbol FB: {0}", existingItem.Name);

				// Query using  SQL
				var stocksStartingWithA = db.Query<Stock> ("SELECT * FROM Stocks WHERE Symbol LIKE ?", "A%"); 
				foreach(Stock stock in stocksStartingWithA)
					queryText.Text += string.Format ("\r\nStock starting with A: {0}", stock.Symbol);

				// Query using Linq
				var stocksStartingWithM = from s in db.Table<Stock> () where s.Symbol.StartsWith ("M") select s;
				foreach(Stock stock in stocksStartingWithM)
					queryText.Text += string.Format ("\r\nStock starting with M: {0}", stock.Symbol);
			};

			Button listViewButton = FindViewById<Button> (Resource.Id.listViewButton);
			ListView stocksListView = FindViewById<ListView> (Resource.Id.stocksListView);
			listViewButton.Click += delegate 
			{
				//var stockNamesArray = (from stock in db.Table<Stock>() select stock.Name).ToArray ();
				var stocks = (from stock in db.Table<Stock>() select stock).ToList ();
				// HACK: gets around "Default constructor not found for type System.String" error
				var stockNames = stocks.Select (s => s.Name).ToArray();
				var stockNamesArray = stockNames.ToArray ();
									
				stocksListView.Adapter = 
					new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, stockNamesArray);
			};

			stocksListView.OnClick += delegate {
				Toast.MakeText (this, "Oh ya!", ToastLength.Short).Show ();
			};
		}

	}
}


