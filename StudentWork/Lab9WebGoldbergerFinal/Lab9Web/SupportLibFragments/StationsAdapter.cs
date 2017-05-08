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
using System.IO;

namespace Lab7Fragments
	{
    public class StationsAdapter: BaseAdapter<Station>, ISectionIndexer {

        Station[] stats;
        Activity context;
		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;

        public StationsAdapter(Activity context, Station[] pStats) : base() {
            this.context = context;
            this.stats = pStats;

			// Build section index by State.
			alphaIndex = new Dictionary<string, int>();
			string key;
			for (int i = 0; i < pStats.Length; i++)
			{
				key = pStats[i].State;
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

        public override Station this[int position] {   
            get { return stats[position]; } 
        }
        public override int Count {
            get { return stats.Length; } 
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
			var stat = stats[position];
			var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), stat.ID+".db3");
			View view = convertView;
			if (view == null)
            	view = context.LayoutInflater.Inflate(Resource.Layout.CustomView2, null);
			view.FindViewById<TextView>(Resource.Id.Text4).Text = stat.Name + ", " + stat.State; 
			view.FindViewById<TextView>(Resource.Id.Text5).Text =  "Station ID: " + stat.ID;

			// Set background colors of station list items. Black = no tide data yet, Green = tide data loaded, Grey = selected
			if (stat.ID == StationsFragment.stations[StationsFragment.selectedStation].ID) 
				view.FindViewById<LinearLayout> (Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.Gray);
			else if (File.Exists(dbPath))
				view.FindViewById<LinearLayout> (Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.DarkGreen);
			else
				view.FindViewById<LinearLayout> (Resource.Id.Text3).SetBackgroundColor (Android.Graphics.Color.Black);

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

