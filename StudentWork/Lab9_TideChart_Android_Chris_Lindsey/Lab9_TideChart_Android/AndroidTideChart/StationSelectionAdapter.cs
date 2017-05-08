
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
	class StationSelectionAdapter : BaseAdapter<StationInfo>, ISectionIndexer
	{
		TideStations TheStations;
		Activity Context;
		
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;
		
		public StationSelectionAdapter(Activity context, TideStations stations) : base()
		{
			Context = context;
			TheStations = stations;
			BuildSectionIndex();
		}
		
		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();
			for (int i = 0; i < TheStations.Stations.Count; i++)
			{
				string key = TheStations.Stations[i].StationName.Substring(0,1);
				if (!alphaIndex.ContainsKey(key))
				{
					alphaIndex.Add(key,i);
				}
			}
			
			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections,0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (int i = 0 ; i < sections.Length; i++)
			{
				sectionsObjects[i] = sections[i];
			}
		}
		
		#region BaseAdapter override functions
		public override long GetItemId(int position)
		{
			return position;
		}
		
		public override StationInfo this[int position]
		{
			get {return TheStations.Stations[position];}
		}
		
		public override int Count 
		{
			get {return TheStations.Stations.Count;}
		}
		
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
			{
				view = Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			}
			
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = TheStations.Stations[position].StationName;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = TheStations.Stations[position].StationID.ToString();
			return view;
		}
		#endregion
		
		#region ISectionIndexer override functions
		public int GetPositionForSection(int section)
		{
			return alphaIndex[sections[section]];
		}
		
		public int GetSectionForPosition(int position)
		{
			return 1;
		}
		
		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}
		#endregion
	}
}

