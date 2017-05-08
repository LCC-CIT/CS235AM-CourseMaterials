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
using TideInfoDB;

namespace Lab9
{
	class HomeScreenAdapter : BaseAdapter<TideInfo>, ISectionIndexer
	{

		TideInfo[] items;
		Activity context;
		
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;
		
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
		
		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();
			for (int i = 0; i < items.Length; i ++)
			{
				// Grab the month from the 2013/02/21 format, maybe change this to text?
				string key = items[i].date.Substring(5,2);
				if(!alphaIndex.ContainsKey(key))
					alphaIndex.Add(key,i);
			}
			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections,0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (int i = 0; i < sections.Length; i++)
				sectionsObjects[i] = sections[i];
			
		}
		
		public HomeScreenAdapter(Activity context, TideInfo[] items) : base() 
		{
			this.context = context;
			this.items = items;
			BuildSectionIndex();
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override TideInfo this[int position]
		{
			get { return items[position]; }
			
		}
		
		public override int Count 
		{
			get { return items.Length; }
		}
		
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			// Reuse existing view if one is available
			View view = convertView;
			
			if (view == null)
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.TwoLineListItem, null);
			
			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = items [position].date;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = items[position].day;

			return view;
			
		}
		
	}
	
	
}

