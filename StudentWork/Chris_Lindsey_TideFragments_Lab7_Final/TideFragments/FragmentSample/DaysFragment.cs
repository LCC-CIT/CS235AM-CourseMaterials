using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.IO;
using TideChart;

#if USE_SUPPORT
using Android.Support.V4.App;
using FragmentUsingSupport;
#else
using Android.App;
#endif

namespace TideFragments
{
	public class DaysFragment : ListFragment
	{
		public StationInfo TheStation;
		private int _currentDayId;
		private bool _isDualPane;

		public override void OnActivityCreated(Bundle savedInstanceState)
		{ 
			base.OnActivityCreated(savedInstanceState);

			StreamReader reader = new StreamReader(Activity.Assets.Open("charleston.txt"));

			TheStation = new StationInfo (reader.ReadToEnd());

			var adapter = new ArrayAdapter<String>(Activity, 
			              Android.Resource.Layout.SimpleListItemChecked, TheStation.ListOfDays);
			ListAdapter = adapter;

			if (savedInstanceState != null)
			{
				_currentDayId = savedInstanceState.GetInt("current_day_id", 0);
			}

			View detailsFrame = Activity.FindViewById<View>(Resource.Id.details);
			_isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
			if (_isDualPane)
			{
				ListView.ChoiceMode = ChoiceMode.Single;		// Note: The walk-through had a cast that isn't needed.
				// ShowDetails(_currentPlayId);		// Note: this gets added later in the walk-through
			}
		}

		public override void OnListItemClick(ListView l, View v, int position, long id) 
		{
			ShowDetails(position);
		}

		private void ShowDetails(int dayId)
		{
			_currentDayId = dayId;
			if (_isDualPane) 
			{
				// We can display everything in place with fragments.
				// Have the list highlight this item and show the data.
				ListView.SetItemChecked(dayId, true);

				// Check what fragment is shown, replace if needed.
				var details = FragmentManager.FindFragmentById(Resource.Id.details) as DetailsFragment;
				if (details == null || details.ShownDayId != dayId)
				{
					// Make new fragment to show this selection. 
					details = DetailsFragment.NewInstance(dayId);

					// Execute a transaction, replacing any existing
					// fragment with this one inside the frame.

					var ft = FragmentManager.BeginTransaction(); 
					ft.Replace(Resource.Id.details, details);
#if USE_SUPPORT
					ft.SetTransition(FragmentTransaction.TransitFragmentFade);	
#else
					ft.SetTransition(FragmentTransit.FragmentFade);		
#endif
					ft.Commit();				}
			}
			else
			{
				// Otherwise we need to launch a new Activity to display
				// the dialog fragment with selected text.
				var intent = new Intent();
				intent.SetClass(Activity, typeof (DetailsActivity));		// Note: DetailsActivity needs to be added to the project but isn't described in the pdf
				intent.PutExtra("current_day_id", dayId);
				StartActivity(intent);
			}
		}
	}
}


