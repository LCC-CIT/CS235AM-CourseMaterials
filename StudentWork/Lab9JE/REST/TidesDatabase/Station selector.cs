using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Flow;
using Preloader;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace TidesDatabase
{
	[Activity( Label = "My Activity", MainLauncher = true )]
	public class Station_selector : Activity
	{
		string select;
		string dbPath = "";
		List<Station> stations;

		protected override void OnCreate ( Bundle bundle )
		{
			base.OnCreate( bundle );
			SetContentView( Resource.Layout.StationLayout );

			SQLiteConnection db = null;

			// Create your application here

			TextView text = FindViewById<TextView>( Resource.Id.myTextView );

			var spinner = FindViewById<Spinner>( Resource.Id.StationSpinner );
			spinner.ItemSelected += spinner_ItemSelected;

			var sender = FindViewById<Button>( Resource.Id.sendButton );
			sender.Click += sender_Click;

			Button pathButton = FindViewById<Button>( Resource.Id.myButton );

			var REST = FindViewById<Button>( Resource.Id.RESTButton );
			REST.Click += delegate
			{
				spinner.Clickable = true;

				sender.Enabled = true;

				REST.Enabled = false;

				pathButton.Enabled = false;


				List<Tide> LT = new List<Tide>();
				List<TideDays> LTD = new List<TideDays>();
				List<table> tidetable = new List<table>();

				int row = 0;
				text.Text = "getting path for database";
				dbPath = Path.Combine(
						System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal ), "Database.db3" );

				text.Text = dbPath;

				// It seems you can read a file in Assets, but not write to it
				// so we'll copy our file to a read/write location
				if ( !File.Exists( dbPath ) )
				{
					using ( Stream inStream = Assets.Open( "Database.3db" ) )
					using ( Stream outStream = File.Create( dbPath ) )
						inStream.CopyTo( outStream );
					text.Text += "\r\nCopied Database.db3 from Assets";
				}
				text.Text = "Connecting to new database";
				db = new SQLiteConnection( dbPath );
				text.Text = "Connected";
				string [] urls = new string [ 5 ];

				urls [ 0 ] = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9434032";
				urls [ 1 ] = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9433501";
				urls [ 2 ] = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9434939";
				urls [ 3 ] = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9438478";
				urls [ 4 ] = "http://tidesandcurrents.noaa.gov/noaatidepredictions/NOAATidesFacade.jsp?datatype=Annual+XML&Stationid=9432845";


				if ( db.CreateTable<Station>() == 0 || db.CreateTable<table>() == 0 )
				{
					// A table already exixts, delete any data it contains
					db.DeleteAll<Station>();

					text.Text = "Deleted old stations";

					// A table already exixts, delete any data it contains
					db.DeleteAll<table>();

					text.Text = "Deleted old Tides";
				}
				text.Text = "created tables";

				var sync = new WebClient();

				for ( int i = 0; i < urls.Length; i++ )
				{
					text.Text = String.Format( "Downloading {0} url", i + 1 );
					var content = sync.DownloadString( urls [ i ] );
					text.Text = "finished";

					XmlSerializer serializer = new XmlSerializer( typeof( datainfo ) );
					datainfo tide;
					using ( XmlReader reader = XmlReader.Create( new StringReader( content ) ) )
					{
						text.Text = "serializing";
						// deserialize the XML object using the WeatherData type
						tide = ( datainfo ) serializer.Deserialize( reader );
					}

					text.Text = "Creating lists";
					bool first = true;
					for ( int j = 0; j < tide.data.Length; j++ )
					{
						if ( first != true )
						{
							if ( DateTime.Parse( tide.data [ j ].date ) != LT [ 0 ].date )
							{
								LTD.Add( new TideDays( LT ) );

								LT = new List<Tide>();
							}
						}
						else
							first = false;

						LT.Add( new Tide( tide.data [ j ].date, tide.stationid, tide.stationname, tide.data [ j ].day, tide.data [ j ].time, tide.data [ j ].predictions_in_ft.ToString(), tide.data [ j ].predictions_in_cm.ToString(), tide.data [ j ].highlow ) );

					}
					text.Text = "finished creating TideDay lists";
				}

				text.Text = "Inserting Stations";

				db.Insert( new Station() { station_name = "Florence", stationID = 9434032 } );
				db.Insert( new Station() { station_name = "Reedsport", stationID = 9433501 } );
				db.Insert( new Station() { station_name = "Waldport", stationID = 9434939 } );
				db.Insert( new Station() { station_name = "Seaside", stationID = 9438478 } );
				db.Insert( new Station() { station_name = "Coos Bay", stationID = 9432845 } );

				text.Text = "Creating list of tables";

				foreach ( TideDays day in LTD )
				{
					tidetable.Add( new table() { stationName = day.stationname(), stationID = day.stationID(), date = day.date, info = day.toasting() } );
				}
				text.Text = "Done\n";
				text.Text = String.Format( "Inserting {0} rows for tides", LTD.Count );
				text.Text = String.Format( "should see {0} . ", LTD.Count / 50 );

				foreach ( table day in tidetable )
				{
					db.Insert( day );
					row++;
					if ( row % 50 == 0 )
						text.Text += ". ";
					if ( row % 500 == 0 )
						text.Text += "\n";
				}

				text.Text += String.Format( "\nfinished inserting {0} rows\nDatabase complete", row );

				text.Text = "Checking for 5 entries for Station names";
				var stationslist = db.Query<Station>( "SELECT * FROM StationTable" );
				text.Text += String.Format( "\nThere are {0} Station names", stationslist.Count );

				var numbers = db.Query<table>( "SELECT * FROM TideTable WHERE stationID = 9434032" );
				text.Text = String.Format( "There are {0} entries for Station {1}: {2}", numbers.Count, numbers [ 0 ].stationID, numbers [ 0 ].stationName );

				numbers = db.Query<table>( "SELECT * FROM TideTable WHERE stationID = 9433501" );
				text.Text += String.Format( "\nThere are {0} entries for Station {1}: {2}", numbers.Count, numbers [ 0 ].stationID, numbers [ 0 ].stationName );

				numbers = db.Query<table>( "SELECT * FROM TideTable WHERE stationID = 9434939" );
				text.Text += String.Format( "\nThere are {0} entries for station {1}: {2}", numbers.Count, numbers [ 0 ].stationID, numbers [ 0 ].stationName );

				numbers = db.Query<table>( "SELECT * FROM TideTable WHERE stationID = 9438478" );
				text.Text += String.Format( "\nThere are {0} entries for station {1}: {2}", numbers.Count, numbers [ 0 ].stationID, numbers [ 0 ].stationName );

				numbers = db.Query<table>( "SELECT * FROM TideTable WHERE stationID = 9432845" );
				text.Text += String.Format( "\nThere are {0} entries for station {1}: {2}", numbers.Count, numbers [ 0 ].stationID, numbers [ 0 ].stationName );

				stations = db.Query<Station>( "SELECT * FROM StationTable" );

				var adapter = new StationAdapter( this, stations );
				spinner.Adapter = adapter;
			};

			pathButton.Click += delegate
			{
				// Get the path to the database that was deployed in Assets
				dbPath = Path.Combine(
					System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal ), "Database.db3" );

				text.Text = dbPath;

				// It seems you can read a file in Assets, but not write to it
				// so we'll copy our file to a read/write location
				if ( !File.Exists( dbPath ) )
				{
					using ( Stream inStream = Assets.Open( "Database.3db" ) )
					using ( Stream outStream = File.Create( dbPath ) )
						inStream.CopyTo( outStream );
					text.Text += "\r\nCopied Database.db3 from Assets";
				}

				// Open the database
				db = new SQLiteConnection( dbPath );
				text.Text += "\r\nCreated a database connection";
				stations = db.Query<Station>( "SELECT * FROM StationTable" );

				var adapter = new StationAdapter( this, stations );
				spinner.Adapter = adapter;
				REST.Enabled = false;
				spinner.Clickable = true;
				sender.Enabled = true;
			};
		}

		/// <summary>
		/// Take the selected spinner item and send it to the main activity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void spinner_ItemSelected ( object sender, AdapterView.ItemSelectedEventArgs e )
		{
			Spinner spin = ( Spinner ) sender;
			select = spin.GetItemAtPosition( e.Position ).ToString();
		}

		void sender_Click ( object sender, EventArgs e )
		{
			var intent = new Intent();
			intent.SetClass( this, typeof( MainActivity ) );
			intent.PutExtra( "database", dbPath );
			intent.PutExtra( "Selection", select );
			StartActivity( intent );
		}
	}

	public class StationAdapter : BaseAdapter<Station>
	{
		List<Station> items;
		Activity context;

		public StationAdapter ( Activity c, List<Station> i )
			: base()
		{
			items = i;
			context = c;
		}

		public override long GetItemId ( int position )
		{
			return position;
		}

		public override Station this [ int position ]
		{
			get { return items [ position ]; }
		}

		public override int Count
		{
			get { return items.Count; }
		}

		public override View GetView ( int position, View convertView, ViewGroup parent )
		{
			var item = items [ position ];
			var view = ( convertView ?? context.LayoutInflater.Inflate( Android.Resource.Layout.SimpleSpinnerDropDownItem,
				parent,
				false ) );
			var name = view.FindViewById<TextView>( Android.Resource.Id.Text1 );
			name.Text = item.station_name;
			return view;
		}

		public Station GetItemAtPosition ( int position )
		{
			return items [ position ];
		}

	}
}