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
	class MainScreenAdapter : BaseAdapter<TideStation>
	{
		TideStation[] items;
		Activity context;

		public MainScreenAdapter(Activity context, TideStation[] items) : base() 
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override TideStation this[int position]
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
			
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "ID: " + items[position].stationId;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "Station Name: " + items[position].stationName;

			return view;
			
		}
		
	}
	
	
}

