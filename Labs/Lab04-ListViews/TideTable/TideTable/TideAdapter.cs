// Brian Bird, 4/30/13

using System;
using Android.Widget;
using Android.App;
using Android.Views;
using System.Collections.Generic;

namespace TideTable
{

	public class TideAdapter : BaseAdapter<string>, ISectionIndexer
	{
		Activity context;
		Tide[] tides;

		public TideAdapter (Activity c, Tide[] t )
		{
			context = c;
			tides = t;
			BuildSectionIndex();
		}

		// Code for the ISectionIndexer implementation
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> monthIndex;  // maps a row in the list to a month
		

		public int GetPositionForSection(int section)
		{
			return monthIndex [sections [section]];
		}

		// Unused
		public int GetSectionForPosition(int position)
		{
			return 1;
		}
		
		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}

		// Fill the dictionary with month to row mappings
		private void BuildSectionIndex()
		{
			monthIndex = new Dictionary<string, int>();
			for (var i = 0; i < tides.Length; i++)
			{
				// Use the first character in the name as a key.
				var key = tides[i].DateAndTime.ToString ("MMM");
				if (!monthIndex.ContainsKey(key))
				{
					monthIndex.Add(key, i);
				} 
			}
			
			sections = new string[monthIndex.Keys.Count];
			monthIndex.Keys.CopyTo(sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)
			{
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}
		} 


		// These three methods are needed just to make the adapter work
		// Not a part of Section Indexing
		public override long GetItemId(int position)
		{ 
			return position;
		}

		public override String this[int position] 
		{   
			get { return tides[position].ToString(); } 
		}
		
		public override int Count 
		{
			get { return tides.Length; } 
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.TwoLineListItem, null); 
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = tides[position].GetDateAndDay();
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = tides[position].GetTideTime();

			return view;
		}
	}

	/*
	public class TideAdapter : ArrayAdapter<string>
	{

		Activity context;

		public TideAdapter (Activity c, int resId, string[] tides ) : base(c, resId, tides)
		{
			context = c;
		}
	}
	*/

}

