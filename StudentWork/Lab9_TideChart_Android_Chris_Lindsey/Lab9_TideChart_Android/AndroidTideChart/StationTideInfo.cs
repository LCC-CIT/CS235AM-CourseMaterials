
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
using TideChart;

namespace AndroidTideChart
{
	[Activity (Label = "StationTideInfo")]			
	public class StationTideInfo : Activity
	{
		const string listView_SelectedItemPosition = "listview_selecteditemposition";

		private StationInfo stationInfo;
		private TextView dayInfo;
		private TextView tideInfo;
		private ListView listView;
		private string StationName;
		private int StationID;
		private int selectedPosition = -1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main);
			listView = FindViewById<ListView>(Resource.Id.List);
			dayInfo = FindViewById<TextView>(Resource.Id.selected_day);
			tideInfo = FindViewById<TextView>(Resource.Id.tide_info);

			StationTideInfoRetainedState previousState = LastNonConfigurationInstance as StationTideInfoRetainedState;
			if (previousState == null)
			{
				StationID = Intent.GetIntExtra("StationID", -1);
				StationName = Intent.GetStringExtra("StationName") ?? "Unknown";
				if (StationID.Equals(-1) || StationName.Equals("Unknown")) //error occured just return to StationSelection
					Finish();

				//this sets the title
				Title = StationID.ToString() + " - " + StationName + " - Tide Info";
				stationInfo = new StationInfo(StationName, StationID);
			}
			else
			{
				stationInfo = previousState.StationInfo; 
				StationID = previousState.StationID;
				StationName = previousState.StationName;
				Title = StationID.ToString() + " - " + StationName + " - Tide Info";
			}

			listView.Adapter = new StationTideInfoAdapter(this, stationInfo);
			listView.ItemClick += OnItemClick;
			listView.FastScrollEnabled = true;
			if (bundle != null)
			{
				selectedPosition = bundle.GetInt(listView_SelectedItemPosition, -1);
				DisplaySelectedDay();
			}
		}

		protected void OnItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			selectedPosition = e.Position;
			DisplaySelectedDay();
		}

		private void DisplaySelectedDay()
		{
			if (selectedPosition > -1)
			{
				string info = string.Empty;
				List<TideInfo> tides = stationInfo.TheDays[selectedPosition].TheTides;
				for(int i = 0; i < tides.Count; i++)
				{
					info += "Time: " + tides[i].Time + " - Ft : " + tides[i].PredictionInFt + " - Cm : " + tides[i].PredictionInCm + " - " + tides[i].HighLow;
					if (i != tides.Count - 1)
						info += "\n";
				}
				if (tides.Count < 4)
				{
					int count = tides.Count;
					while (count < 4)
					{
						info += "\n";
						count++;
					}
				}
				dayInfo.Text = stationInfo.TheDays[selectedPosition].Date + " - " + stationInfo.TheDays[selectedPosition].Day;
				tideInfo.Text = info;
			}
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutInt(listView_SelectedItemPosition, selectedPosition);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			DisplaySelectedDay();
			if (selectedPosition != -1)
				listView.SetSelection(selectedPosition);
		}

		public override Java.Lang.Object OnRetainNonConfigurationInstance ()
		{
			StationTideInfoRetainedState savedState = new StationTideInfoRetainedState(stationInfo, StationID, StationName);
			return savedState;
		}
	}
}

