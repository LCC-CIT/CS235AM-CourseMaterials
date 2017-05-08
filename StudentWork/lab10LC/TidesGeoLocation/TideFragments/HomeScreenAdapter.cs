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
using TideFragments.DAL;

namespace TideFragments
{
	class HomeScreenAdapter: BaseAdapter<string>
	{
		List<string> items;
		Activity context;
		//	List<Tide> newItems=items.Distinct().ToList();

		public HomeScreenAdapter(Activity context, List<string>items) 
			: base()
		{
			this.context = context;
			this.items = items;
			/*
			alphaIndex = new Dictionary<string, int>();
			for(int i=0; i< items.Count; i++) {
				var  key = Convert.ToDateTime (items[i]).ToString("MMMM");
				if (!alphaIndex.ContainsKey(key))       
					alphaIndex.Add(key, i); // add each 'new' letter to the index
			}

			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections, 0); // convert letters list to string[]
			// Interface requires a Java.Lang.Object[], so we create one here
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (int i = 0; i < sections.Length; i++) { 
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}*/
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position] {  
			get { return items[position]; }
		}

		public override int Count {
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items [position];
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
			  view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
				//view.FindViewById<TextView> (Resource.Id.tvDate).Text = item.Date + " " + item.Day;
				view.FindViewById<TextView> (Resource.Id.tvDate).Text = item;


			return view;
		}

		//required variables  to implement ISectionIndexer methods
		//string[] sections;
		//Java.Lang.Object[] sectionsObjects;
		//Dictionary<string, int> alphaIndex;
		// -- ISectionIndexer --
		//GetPositionForSection allows you to map whatever indices are in your index list to whatever sections are in your list view
//		public int GetPositionForSection(int section)
//		{
//
//			return alphaIndex[sections[section]];
//		}
//
//		//
//		public int GetSectionForPosition(int position)
//		{
//			return 1;
//		}
//
//		public Java.Lang.Object[] GetSections()
//		{
//			return sectionsObjects;
//		}
	}
	}


