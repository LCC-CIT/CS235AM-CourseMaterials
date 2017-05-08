using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    class DayScreenAdapter : BaseAdapter<string> 
    {
        List<string> items;
        Activity context;
		int res;

		public DayScreenAdapter(Activity f, int lv , List<string> i) : base()
		{
			items = i;
			context = f;
			res = lv;
		}

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count(); }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
				view = context.LayoutInflater.Inflate(res, null);
            view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = items[position];

            return view;
        }
    }
}