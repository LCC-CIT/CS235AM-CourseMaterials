
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
	class StationTideInfoRetainedState : Java.Lang.Object
	{
		public StationInfo StationInfo {get; private set;}
		public int StationID {get; private set;}
		public string StationName {get; private set;}

		public StationTideInfoRetainedState(StationInfo info, int stationID, string stationName)
		{
			StationInfo = info;
			StationID = stationID;
			StationName = stationName;
		}
	}

	class StationSelectionRetainedState : Java.Lang.Object
	{
		public TideStations tideStations{get; private set;}

		public StationSelectionRetainedState(TideStations stations)
		{
			tideStations = stations;
		}
	}
}

