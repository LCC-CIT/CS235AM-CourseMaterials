namespace PersistStateWalkthrough
{
    using System.Collections.Generic;

    using Android.App;
    using Android.Views;
    using Android.Widget;


    public class HomeScreenAdapter : BaseAdapter<Flora>
    {
        private readonly Activity context;
        private readonly List<Flora> items;

        public HomeScreenAdapter(Activity context, List<Flora> items)
        {
            this.context = context;
            this.items = items;
        }

        public override int Count { get { return items.Count; } }

        public override Flora this[int position] { get { return items[position]; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            var view = convertView;
            if (view == null)
            {
                // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            }
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.ItemCount;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);

            return view;
        }
    }
}