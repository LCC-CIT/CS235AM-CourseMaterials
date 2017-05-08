using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin.AdoData.Droid
{
    [Activity(Label = "Xamarin ADO Data", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        private IMenuItem loadDataMenuItem;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.primary_menu, menu);

            loadDataMenuItem = menu.GetItem(0);

            // We will load here since this will be called after OnResume
            LoadStockData();

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    LoadStockData();
                    break;
                default:
                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async Task LoadStockData()
        {
            try
            {
                loadDataMenuItem.SetEnabled(false);

				//TODO: Step 5 - Android - Integrate the ADO.Net client into your UI
                var adoDatabase = new Ado.StockDatabase();

                var selectAllStock = 
                    await Task.Run(() =>
                    {

                        adoDatabase.CreateDatabaseIfNotExists();

                        for (int i = 0; i < 5; i++)
                        {
                            adoDatabase.InsertStock(Ado.StockDatabase.GenerateStock()/* Generates a fake stock */);
                        }

                        return adoDatabase.SelectStock();
                    });

                this.ListAdapter = new ArrayAdapter<Model.Stock>(this, Android.Resource.Layout.SimpleListItem1, selectAllStock);
            }
            finally
            {
                loadDataMenuItem.SetEnabled(true);
            }
        }
    }
}