
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

namespace Tide_Table
{
	public class TideAdapter : BaseAdapter<Tide>, ISectionIndexer
	{
		Tide[] items;
		Activity context;

		public TideAdapter(Activity context, Tide[] tides) : base(){
			this.context = context;
			this.items = tides;
			BuildSectionIndex ();
		}

		public override long GetItemId(int position){		
			return position;
		}
		
		public override Tide this[int position]{
			get{ return items[position];}
		}
		
		public override int Count {
			get{ return items.Length; } 
		}		

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;

			if(view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.TideListItem, null);
			view.FindViewById<TextView>(Resource.Id.TideListItemView).Text = items[position].Date + " " + items[position].Day;

			return view;
		}

		//ISectionIndexer implementation
		Dictionary<string, string> months = new Dictionary<string, string>()
		{
			{"01", "Jan"},
			{"02", "Feb"},
			{"03", "Mar"},
			{"04", "Apr"},
			{"05", "May"},
			{"06", "Jun"},
			{"07", "Jul"},
			{"08", "Aug"},
			{"09", "Sep"},
			{"10", "Oct"},
			{"11", "Nov"},
			{"12", "Dec"}
		};
		
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;
		
		public int GetPositionForSection(int section)
		{
			return alphaIndex [sections [section]];
		}
		
		public int GetSectionForPosition(int position)
		{
			return 1;
		}
		
		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}
		
		private void BuildSectionIndex(){
			alphaIndex = new Dictionary<string, int>();
			for (var i = 0; i < items.Length; i++)
			{
				var key = months[items[i].Date.Substring (5,2)];
				if (!alphaIndex.ContainsKey (key))
				{
					alphaIndex.Add (key, i);
				}
			}
			
			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo (sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)		
			{
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}
		}
	}
}

