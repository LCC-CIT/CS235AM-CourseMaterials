using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace Xamarin.AdoData.iOS
{
    public class StockViewController : UITableViewController
    {
        List<Model.Stock> stocks;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Xamarin ADO Data";

            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += async (sender, e) =>
            {
                await LoadStockData();
                RefreshControl.EndRefreshing();
            };

            // set the table style to Grouped (otherwise Default is by default)
            TableView = new UITableView(Rectangle.Empty, UITableViewStyle.Grouped);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            LoadStockData();
        }

        private async Task LoadStockData()
        {
			//TODO: Step 5 - iOS - Integrate the ADO.Net client into your UI
//            var adoDatabase = new Ado.StockDatabase();
//
//            stocks = await Task.Run(() =>
//            {
//                adoDatabase.CreateDatabaseIfNotExists();
//
//                for (int i = 0; i < 5; i++)
//                {
//                    adoDatabase.InsertStock(Ado.StockDatabase.GenerateStock()/* Generates a fake stock */);
//                }
//
//                return adoDatabase.SelectStock();
//            });
//
//            TableView.Source = new StocksTableSource(stocks);
//            TableView.ReloadData();
        }

        class StocksTableSource : UITableViewSource
        {
            static readonly string stockCellId = "StockCell";

            readonly List<Model.Stock> stocks;

            public StocksTableSource(List<Model.Stock> stocks)
            {
                this.stocks = stocks;
            }

            public override int RowsInSection(UITableView tableview, int section)
            {
                return stocks.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var stock = stocks[indexPath.Row];

                new UIAlertView("Stock Selected", stock.Symbol, null, "OK", null).Show();

                tableView.DeselectRow(indexPath, true);
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(stockCellId);

                var stock = stocks[indexPath.Row];
                if (cell == null)
                    cell = new UITableViewCell(UITableViewCellStyle.Subtitle, stockCellId);

                cell.TextLabel.Text = stock.Symbol;
                cell.DetailTextLabel.Text = stock.Id.ToString();

                return cell;
            }
        }
    }
}