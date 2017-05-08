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

namespace Lab2
{
	public class ScreenAdapter : BaseAdapter<Days>, ISectionIndexer
	{
		List<Days> ListOfDays;
		Activity context;
	
		public ScreenAdapter(Activity c, List<Days> d) : base()
		{
			ListOfDays = d;
			context = c;
			BuildSection();
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Days this[int index] {
			get {
				return ListOfDays[index];
			}
		}

		public override int Count {
			get {
				return ListOfDays.Count;
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var item = ListOfDays [position];
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.TideView, null);

			// Date Row
			view.FindViewById<TextView> (Resource.Id.DayOfMonth).Text = item.ToString();

			return view;
		}

		string[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;

		public int GetPositionForSection(int section)
		{
			return alphaIndex[sections [section]];
		}

		public int GetSectionForPosition(int position)
		{
			return 1;
		}

		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}

		private void BuildSection()
		{
			string day = null;
			alphaIndex = new Dictionary<string, int> ();
			for (var i = 0; i < ListOfDays.Count; i++) {
				day = ListOfDays[i].ToString();
				var key = day.Substring(day.Length - 2);
				if (!alphaIndex.ContainsKey (key))
					alphaIndex.Add (key, i);
			}

			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo (sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)
				sectionsObjects [i] = new Java.Lang.String (sections [i]); 
		}		
	}
}