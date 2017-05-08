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


namespace DesignerWalkthrough {
	/// <summary>
	/// Demo 5: Add an index
	/// </summary>		
	public class SessionsAdapter: BaseAdapter<Session> 
	{
		private List<Session> data;
		private Activity context;
		
		public SessionsAdapter(Activity activity, IEnumerable<Session> sessions) 
		{
			data = (from s in sessions orderby s.Title select s).ToList ();
			context = activity;
		}


		public override long GetItemId(int position)
		{
			return position;
		}
		
		public override Session this[int position]
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
			ViewHolder holder;

			if (convertView == null) {
				convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

				holder = new ViewHolder();
				holder.Text = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);

				//convertView.SetTag (0, holder);
			} else {
				//holder = (ViewHolder) convertView.GetTag (0);
			}

			
			var speaker = data[position];

			//holder.Text.Text = speaker.Title;

			var speakerNameView = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
			speakerNameView.Text = speaker.Title;

			return convertView;
		}
		class ViewHolder : Java.Lang.Object {
			public TextView Text;
		}
	}
}

