
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
	class StationTideInfoAdapter : BaseAdapter<DayInfo>, ISectionIndexer
	{
		StationInfo stationInfo;
		Activity Context;
		
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;
		
		public StationTideInfoAdapter(Activity context, StationInfo station) : base()
		{
			Context = context;
			stationInfo = station;
			BuildSectionIndex();
		}
		
		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();
			for (int i = 0; i < stationInfo.TheDays.Count; i++)
			{
				DateTime date = DateTime.Parse(stationInfo.TheDays[i].Date);
				int month = date.Month;
				string key = string.Empty;
				switch (month)
				{
				case 1:
					key = "Jan";
					break;
				case 2:
					key = "Feb";
					break;
				case 3:
					key = "Mar";
					break;
				case 4:
					key = "Apr";
					break;
				case 5:
					key = "May";
					break;
				case 6:
					key = "Jun";
					break;
				case 7:
					key = "Jul";
					break;
				case 8:
					key = "Aug";
					break;
				case 9:
					key = "Sep";
					break;
				case 10:
					key = "Oct";
					break;
				case 11:
					key = "Nov";
					break;
				case 12:
					key = "Dec";
					break;
				}
				if (!alphaIndex.ContainsKey(key) && !string.IsNullOrEmpty(key))
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
		
		public override DayInfo this[int position]
		{
			get {return stationInfo.TheDays[position];}
		}
		
		public override int Count 
		{
			get {return stationInfo.TheDays.Count;}
		}
		
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
			{
				view = Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			}
			
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = stationInfo.TheDays[position].Date;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = stationInfo.TheDays[position].Day;
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

