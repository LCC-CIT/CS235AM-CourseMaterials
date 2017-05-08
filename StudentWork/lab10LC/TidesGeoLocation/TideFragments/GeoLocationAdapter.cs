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
//	class GeoLocationAdapter: BaseAdapter<string>
//	{
//		List<string> items;
//		Activity context;
//		//	List<Tide> newItems=items.Distinct().ToList();
//
//		public GeoLocationAdapter(Activity context, List<string>items) 
//			: base()
//		{
//			this.context = context;
//			this.items = items;
//		
//		}
//
//		public override long GetItemId(int position)
//		{
//			return position;
//		}
//
//		public override string this[int position] {  
//			get { return items[position]; }
//		}
//
//		public override int Count {
//			get { return items.Count; }
//		}
//		public override View GetView(int position, View convertView, ViewGroup parent)
//		{
//			var item = items [position];
//			View view = convertView; // re-use an existing view, if one is available
//			if (view == null) // otherwise create a new one
//				view = context.LayoutInflater.Inflate(Resource.Layout.CustomLocView, null);
//			//view.FindViewById<TextView> (Resource.Id.tvDate).Text = item.Date + " " + item.Day;
//			view.FindViewById<TextView> (Resource.Id.tvDisplay).Text = item;
//			//view.FindViewById<Button> (Resource.Id.button1).Text = " Display Location";
//
//
//			return view;
//		}
//
//	
//	}
}


