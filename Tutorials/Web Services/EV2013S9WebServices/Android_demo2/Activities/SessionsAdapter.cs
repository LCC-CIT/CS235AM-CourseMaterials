using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace EvolveLite
{
	public class SessionsAdapter : BaseAdapter<Session>
	{
		private readonly Activity activity;
		private readonly List<Session> sessions;
		
		public SessionsAdapter(Activity context, IEnumerable<Session> sessions)
		{
			this.sessions = sessions.ToList();
			activity = context;
		}
		
		public override int Count { get { return sessions.Count; } }
		
		public override Session this[int position] { get { return sessions[position]; } }
		
		public override long GetItemId(int position)
		{
			return position;
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			
			var session = sessions[position];

			var sessionTitleView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			sessionTitleView.Text = session.Title;

			var sessionTimeView = view.FindViewById<TextView>(Android.Resource.Id.Text2);
			sessionTimeView.Text = session.Begins.ToString ("ddd MM HH:mm");

			return view;
		}
	}
}
