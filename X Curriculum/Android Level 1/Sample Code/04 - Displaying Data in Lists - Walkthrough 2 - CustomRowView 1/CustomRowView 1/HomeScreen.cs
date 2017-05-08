using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace CustomRowView
{
    [Activity(Label = "CustomRowView", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {

        List<Flora> tableItems;
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeScreen);

            PopulateTableItems();

            listView = FindViewById<ListView>(Resource.Id.vegetable_list);
            listView.Adapter = new HomeScreenAdapter(this, tableItems);
            listView.ItemClick += OnListItemClick;
        }

        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = tableItems [e.Position];
            Android.Widget.Toast.MakeText(this, t.Name, Android.Widget.ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + t.Name);
        }

        private void PopulateTableItems() 
        {
            tableItems = new List<Flora>();
            tableItems.Add(new Flora() { Name = "Vegetables", ItemCount = "65 items", ImageResourceId = Resource.Drawable.Vegetables });
            tableItems.Add(new Flora() { Name = "Fruits", ItemCount = "17 items", ImageResourceId = Resource.Drawable.Fruits });
            tableItems.Add(new Flora() { Name = "Flower Buds", ItemCount = "5 items", ImageResourceId = Resource.Drawable.FlowerBuds });
            tableItems.Add(new Flora() { Name = "Legumes", ItemCount = "33 items", ImageResourceId = Resource.Drawable.Legumes });
            tableItems.Add(new Flora() { Name = "Bulbs", ItemCount = "18 items", ImageResourceId = Resource.Drawable.Bulbs });
            tableItems.Add(new Flora() { Name = "Tubers", ItemCount = "43 items", ImageResourceId = Resource.Drawable.Tubers });

        }
    }
}
