
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

namespace EvolveListView {
	/// <summary>
	/// Demo 4: Custom row layout
	/// </summary>
	public class SpeakersAdapter: BaseAdapter<Speaker> {
		private List<Speaker> data;
		private Activity context;
		
		public SpeakersAdapter(Activity activity, IEnumerable<Speaker> speakers) 
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

		/// <summary>
		/// CUSTOM ROW STYLE !!
		/// </summary>
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null) {
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
				//TODO: Demo4: inflate the custom AXML layout
				//view = context.LayoutInflater.Inflate(Resource.Layout.speaker_row, null);
			}
			
			var speaker = data[position];

			//TODO: Demo4: remove this old crufty code with the built-in id
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = speaker.Name;

			//TODO: Demo4: uncomment these items to set the UI controls in the custom view
//			var imageView = view.FindViewById<ImageView>(Resource.Id.headshotImageView);
//			var headshot = GetHeadShot(speaker.HeadshotUrl);
//			imageView.SetImageDrawable(headshot);
//			
//			var speakerNameView = view.FindViewById<TextView>(Resource.Id.speakerNameTextView);
//			speakerNameView.Text = speaker.Name;
//			
//			var companyNameTextView = view.FindViewById<TextView> (Resource.Id.companyNameTextView);
//			companyNameTextView.Text = speaker.Company;
//			
//			var twitterHandleView = view.FindViewById<TextView>(Resource.Id.twitterTextView);
//			twitterHandleView.Text = "@" + speaker.TwitterHandle;
			
			return view;
		}
		
		private Drawable GetHeadShot(string url) 
		{
			Drawable headshotDrawable = null;
			try  {
				headshotDrawable = Drawable.CreateFromStream(context.Assets.Open(url), null);
			} catch (Exception ex)  {
				Log.Debug (GetType().FullName, "Error getting headshot for " + url + ", " + ex.ToString ());
				headshotDrawable = null;
			}
			return headshotDrawable;
		}
	}
}

