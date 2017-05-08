using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using System.Threading.Tasks;

namespace Lab7Fragments
{
    public class StationsFragment : ListFragment
    {
		private bool _isDualPane;

		public static int selectedStation = 0; // Last selected list item.
		public static Station[] stations;
		public static StationsAdapter adapter;
		public static ProgressDialog progDialog;

		public static ListView list;

        public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			var detailsFrame = Activity.FindViewById<View> (Resource.Id.details);

			// Needed to detect which layout loaded, normal or XLarge
			_isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;

			list = ListView;
			progDialog = ProgressDialog.Show (MainActivity.context, "Loading", "Stations", true, false);

			Task.Factory.StartNew (() => {
				var statObj = new Stations();
				stations = statObj.LoadStations();
				string[] strStations = new string[stations.Length];
				for (int i = 0; i < stations.Length; i++)
				{
					strStations[i] = stations[i].Name + ", " + stations[i].State;
				}
			}).ContinueWith (task  => {
				DisplayStations();
			}, TaskScheduler.FromCurrentSynchronizationContext ());
		

            if (savedInstanceState != null)
            {
				selectedStation = savedInstanceState.GetInt("selStat", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
			outState.PutInt("selStat", selectedStation);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
			// Set background colors of station list items. Black = no tide data yet, Green = tide data loaded, Grey = selected
			for (int i = 0; i< l.ChildCount; i++)
			{
				var view = l.GetChildAt (i);
				string statID = view.FindViewById<TextView>(Resource.Id.Text5).Text.Substring(12);
				var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), statID+".db3");
				if (File.Exists(dbPath)) 
					view.FindViewById<LinearLayout> (Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.DarkGreen);
				else
					view.FindViewById<LinearLayout> (Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.Black);
			}
			ShowDates(position);
			v.FindViewById<LinearLayout>(Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.Gray);
        }
		public override void OnResume()
		{
			base.OnResume();
			list.SetSelection(selectedStation);
		}

        private void ShowDates(int selStat)
        {
			selectedStation = selStat;
            if (_isDualPane)
            {
                // Check what fragment is shown, replace if needed.
                var datesList = FragmentManager.FindFragmentById(Resource.Id.details) as TidesFragment;
				if (datesList == null || datesList.name != stations[selectedStation].Name)
                {
                    // Make new fragment to show this selection.
					datesList = TidesFragment.NewInstance(stations[selectedStation].Name, stations[selectedStation].ID, stations[selectedStation].State, selectedStation);

                    // Execute a transaction, replacing any existing
                    // fragment with this one inside the frame.
                    var ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.details, datesList);
                    ft.SetTransition(FragmentTransaction.TransitFragmentFade);
                    ft.Commit();
                }
            }
            else
            {
                // Otherwise we need to launch a new activity to display
                // the dialog fragment with selected text.
                var intent = new Intent();

                intent.SetClass(Activity, typeof (TidesActivity));
				intent.PutExtra("selStat",selectedStation);
				intent.PutExtra("name",stations[selectedStation].Name);
				intent.PutExtra("id",stations[selectedStation].ID);
				intent.PutExtra("state",stations[selectedStation].State);
                StartActivity(intent);
            }
        }

		private void DisplayStations()
		{
			if (progDialog != null) {
				progDialog.Dismiss ();
				progDialog = null;
			}
			adapter = new StationsAdapter(Activity, stations);
			ListAdapter = adapter;
			list.FastScrollEnabled = true;
			list.ChoiceMode = ChoiceMode.Single;
			list.SetSelection(selectedStation);
			if (_isDualPane)
			{
				ShowDates(selectedStation);
			}
		}
    }
}