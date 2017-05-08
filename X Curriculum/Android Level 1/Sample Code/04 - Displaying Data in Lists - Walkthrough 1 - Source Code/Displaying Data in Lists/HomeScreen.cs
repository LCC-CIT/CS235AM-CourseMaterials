namespace BasicTable
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Android.App;
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    [Activity(Label = "BasicTable", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : ListActivity
    {
        private Flora[] items;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            items = LoadListOfVegetablesFromAssets();
            ListAdapter = new HomeScreenAdapter(this, items);
            ListView.FastScrollEnabled = true;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var flora = items[position];
            Toast.MakeText(this, flora.Name, ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + flora);
        }

        /// <summary>
        ///   Loads the list of vegetables from the text file VegeData.txt which is an Android Asset.
        /// </summary>
        /// <returns> A sorted list of vegetables. </returns>
        private Flora[] LoadListOfVegetablesFromAssets()
        {
            var veges = new List<Flora>();
            var seedDataStream = Assets.Open(@"VegeData.txt");
            using (var reader = new StreamReader(seedDataStream))
            {
                while (!reader.EndOfStream)
                {
                    var flora = new Flora { Name = reader.ReadLine() };
                    veges.Add(flora);
                }
            }
            veges.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));
            items = veges.ToArray();

            return items;
        }
    }
}
