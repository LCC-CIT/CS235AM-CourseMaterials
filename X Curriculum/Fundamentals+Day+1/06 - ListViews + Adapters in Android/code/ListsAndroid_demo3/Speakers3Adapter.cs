
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using EvolveListView.Model;

namespace EvolveListView
{
	/// <summary>
	/// Demo 3: Row styles
	/// </summary>
	public class Speakers3Adapter: BaseAdapter<Speaker>
	{
		private List<Speaker> data;
		private Activity context;

		public Speakers3Adapter(Activity activity, IEnumerable<Speaker> speakers) 
		{
			data = (from s in speakers orderby s.Name select s).ToList ();
			context = activity;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Speaker this[int position]
		{
			get { return data[position]; }
		}
		public override int Count
		{
			get { return data.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null) {
				//TODO: Demo3: uncomment different layouts to see how they look
				//view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
				//view = context.LayoutInflater.Inflate(Android.Resource.Layout.TwoLineListItem, null);
				//view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);  // image
			}

			var speaker = data[position];

			//TODO: Demo3: change which UI controls are populated, depending on which layout is used
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = speaker.Name;
			// Does not work in SimpleListItem1 or ActivityListItem
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = speaker.Company;
			// ActivityListItem only - the others don't support an image
			//view.FindViewById<ImageView> (Android.Resource.Id.Icon).SetImageDrawable (GetHeadShot (speaker.HeadshotUrl));

			return view;
		}



		/// <summary>
		/// Helper to load images
		/// </summary>
		private Drawable GetHeadShot(string url) 
		{
			Drawable headshotDrawable = null;
			try 
			{
				headshotDrawable = Drawable.CreateFromStream(context.Assets.Open(url), null);
			}
			catch (Exception ex) 
			{
				Log.Debug (GetType().FullName, "Error getting headshot for " + url + ", " + ex.ToString ());
				headshotDrawable = null;
			}
			return headshotDrawable;
		}
	}
}