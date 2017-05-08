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

namespace CustomRowView
{
    public class HomeScreenAdapter : BaseAdapter<Flora>
    {
        List<Flora> items;
        Activity context;

        public HomeScreenAdapter(Activity context, List<Flora> items)
        : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Flora this [int position]
        {
            get { return items [position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items [position];

            View view = convertView;
            if (view == null)
            {
                // no view to re-use, create new view based on a default layout.
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;

            return view;
        }
    }
}