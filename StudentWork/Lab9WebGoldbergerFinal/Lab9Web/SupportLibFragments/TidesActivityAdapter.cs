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

namespace Lab7Fragments
	{
    public class TidesActivityAdapter : BaseAdapter<Tide>, ISectionIndexer {

        Tide[] dates;
        Activity context;
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;

        public TidesActivityAdapter(Activity context, Tide[] dates) : base() {
            this.context = context;
            this.dates = dates;

			// Build section index by three letter month.
			alphaIndex = new Dictionary<string, int>();
			string key;
			for (int i = 0; i < dates.Length; i++)
			{
				key = dates [i].getMonth ().Substring (0, 3);
				if (!alphaIndex.ContainsKey(key))
					alphaIndex.Add(key, i);

			}
			
			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)
			{
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}

        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Tide this[int position] {   
            get { return dates[position]; } 
        }
        public override int Count {
            get { return dates.Length; } 
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
			var tide = dates[position];
			View view = convertView;
			if (view == null)
            	view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
			view.FindViewById<TextView>(Resource.Id.Text1).Text = tide.getDate();
			if (tide.getDate () == TidesFragment.dates[TidesFragment.selTide].getDate()) {
				view.FindViewById<LinearLayout> (Resource.Id.Text).SetBackgroundColor (Android.Graphics.Color.Gray);
			}
			else
				view.FindViewById<LinearLayout> (Resource.Id.Text).SetBackgroundColor (Android.Graphics.Color.Black);
            return view;
        }

        // -- ISectionIndexer --
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
    }
}

