using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Preloader;
using SQLite;
using System.Collections.Generic;

#if USE_SUPPORT
using Android.Support.V4.App;
#endif

namespace Flow
{
	#if USE_SUPPORT
	internal class DetailsFragment : Android.Support.V4.App.Fragment
#else
	internal class TideFragment : Android.App.ListFragment
#endif
	{
		
		private int currentTide;
		private bool isDualPane;
		string dbPath = "";
		string select = "";
		List<table> tides;


		public override void OnCreate ( Bundle savedInstanceState )
		{
			base.OnCreate( savedInstanceState );
		}

		public override void OnActivityCreated ( Bundle savedInstanceState )
		{
			base.OnActivityCreated( savedInstanceState );

			dbPath = Activity.Intent.GetStringExtra( "database" );
			select = Activity.Intent.GetStringExtra( "Selection" );
			var db = new SQLiteConnection( dbPath );

			View detailsFrame = Activity.FindViewById<View>( TidesDatabase.Resource.Id.details );
			isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;

			if ( isDualPane )
			{
				ListView.ChoiceMode = ChoiceMode.Single;		// Note: The walk-through had a cast that isn't needed.
				// ShowDetails(_currentPlayId);		// Note: this gets added later in the walk-through
			}
			tides = db.Query<table>( "SELECT * FROM TideTable WHERE stationName = ?", select );

			var adapter = new ArrayAdapter<table>( Activity, Android.Resource.Layout.SimpleListItemChecked, tides );

			ListAdapter = adapter;

		}


		public override void OnListItemClick ( ListView l, View v, int position, long id )
		{
			ShowDetails( position );
		}
		
		/// <summary>
		/// /* If using dual fragments, starts a transaction and puts the tide info into the new fragments data
		/// <para>/* If single, starts a new activity and displays the data there</para>
		/// <example>Takes The int of which tide you select</example>
		/// </summary>
		/// <param name="tideID"></param>
		private void ShowDetails ( int tideID )
		{
			currentTide = tideID;
			if ( isDualPane )
			{
				ListView.SetItemChecked( tideID, true );

				var details = FragmentManager.FindFragmentById( TidesDatabase.Resource.Id.details ) as DetailsFragment;
				if ( details == null || details.ShownTideId != tideID )
				{
					details = DetailsFragment.NewInstance( tideID, tides [ tideID ].info );

					var ft = FragmentManager.BeginTransaction();
					ft.Replace( TidesDatabase.Resource.Id.details, details );
#if USE_SUPPORT
					ft.SetTransition(FragmentTransaction.TransitFragmentFade);	
#else
					ft.SetTransition( FragmentTransit.FragmentFade );
#endif
					ft.Commit();
				}
			}
			else
			{
				var intent = new Intent();
				intent.SetClass( Activity, typeof( DetailsActivity ) );
				intent.PutExtra( "tide_data", tides [ tideID ].info );
				intent.PutExtra( "current_tide_id", tideID );
				
				StartActivity( intent );
			}
		}

	}
}