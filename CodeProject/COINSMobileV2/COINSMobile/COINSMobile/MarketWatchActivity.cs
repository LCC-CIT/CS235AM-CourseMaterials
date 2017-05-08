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
using Android.Graphics;

using System.Xml;
using System.Threading;
using Android.Database.Sqlite;
using COINSMobile.Core.BusinessLayer;
using COINSMobile.stockwebservice;

namespace COINSMobile
{
    [Activity(MainLauncher = true, Label = "Market Watch", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, 
        Icon = "@drawable/icon_activity")]
    public class MarketWatchActivity : Activity
    {
        private bool IsLoading = false;
        private TableLayout tablelayout = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            View titleView = Window.FindViewById(Android.Resource.Id.Title);
            //update the default title
            if (titleView != null)
            {
                IViewParent parent = titleView.Parent;
                if (parent != null && (parent is View))
                {
                    View parentView = (View)parent;
                    parentView.SetBackgroundColor(Color.Rgb(28, 28, 28));
                    parentView.SetMinimumHeight(32);
                    parentView.SetMinimumHeight(32);
                }
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MarketWatch);

            tablelayout = FindViewById<TableLayout>(Resource.Id.deatlWatchLayout);

            ProgressDialog progress = ProgressDialog.Show(this, "", "Loading Market Watch...", true);
            new Thread(new ThreadStart(() =>
            {
                this.RunOnUiThread(() =>
                {
                    doLoadMarketWatch();
                    progress.Dismiss();
                });
            })).Start();
            IsLoading = false;

            ImageView btnRefresh = FindViewById<ImageView>(Resource.Id.watchRefresh);
            btnRefresh.Click += (sender, e) =>
            {
                if (!IsLoading)
                {
                    ProgressDialog progress1 = ProgressDialog.Show(this, "", "Loading Quote...", true);
                    new Thread(new ThreadStart(() =>
                    {
                        this.RunOnUiThread(() =>
                        {
                            doLoadMarketWatch();
                            progress1.Dismiss();
                        });
                    })).Start();
                    IsLoading = false;
                }
            };

            
        }
        private bool doLoadMarketWatch()
        {
            bool ret = false;
            string strStocks = string.Empty; //create a comma delimited stock name from database

            IEnumerable<Stock> stockdata = StockManager.GetStocks();
            foreach (Stock rec in stockdata)
            {
                strStocks += rec.StockName + ",";
            }
            if (strStocks.Length > 0) strStocks.TrimEnd(',');
            if (strStocks != string.Empty)
            {
                PopulateDataToControls(strStocks);
            }
            else
            {
                //clear all views from table layout
                tablelayout.RemoveAllViews();
                tablelayout.RefreshDrawableState();
            }
            return ret;
        }

        private void PopulateDataToControls(string _stocks)
        {
            //we have now the stocks delimited by comma
            //this string will be passed to Webservice as a paramete to fetch the stck block in xml

            string strXML = string.Empty;
            stockwebservice.StockWebservice quoteObject = null;
            try
            {
                quoteObject = new stockwebservice.StockWebservice();
                strXML = quoteObject.GetStockQuote(_stocks);
            }
            finally
            {
                quoteObject.Dispose();
                quoteObject = null;
            }

            //if error occurred while connect8ing to web service
            if (strXML.Substring(0, 5) == "error")
            {
                var t = Toast.MakeText(this, "Error connecting to web service. Please check your Internet connection...", ToastLength.Short);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
                return;
            }
            if (strXML.ToLower() == "exception")
            {
                var t = Toast.MakeText(this, "Service not available now. Please try after sometime...", ToastLength.Short);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
                return;
            }

            //load the xml to XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXML);

            tablelayout.RemoveAllViews();
            tablelayout.RefreshDrawableState();

            XmlNodeList xnList = doc.SelectNodes("/stock/symbol");
            foreach (XmlNode xn in xnList)
            {
                if (xn != null)
                {
                    TableRow demoTableRow = new TableRow(this);
                    TextView tv_l = new TextView(this);
                    TextView tv_r = new TextView(this);
                    
                    tv_l.SetPadding(3, 3, 3, 3);
                    tv_r.SetPadding(3, 3, 3, 3);
                    tv_r.Gravity = GravityFlags.Right;
                    tv_l.SetTextSize(Android.Util.ComplexUnitType.Px, 9);


                    tv_l.Text = xn["code"].InnerText.Trim() + "-" + xn["exchange"].InnerText.Trim();
                    tv_r.Text = "(" + xn["change"].InnerText.Trim() + ") " + xn["last"].InnerText.Trim();

                    demoTableRow.Clickable = true;
                    demoTableRow.Click += (sender, e) =>
                    {
                        doRowClick(sender);
                    };

                    demoTableRow.AddView(tv_l);
                    demoTableRow.AddView(tv_r);

                    tablelayout.AddView(demoTableRow);
                    View vLineRow = new View(this);
                    vLineRow.SetMinimumHeight(1);
                    vLineRow.SetBackgroundColor(Color.Rgb(88, 88, 88));
                    tablelayout.AddView(vLineRow);
                }
            }
        }

        private void doRowClick(object Sender)
        {
            TableRow tr = Sender as TableRow;
            if (tr != null)
            {
                TextView v = tr.GetChildAt(0) as TextView;
                string _script = v.Text.Trim();
                int _index = _script.IndexOf('-');
                if (_index > 0) _script = _script.Substring(0, _index);

                if (_script != string.Empty)
                {
                    var t = Toast.MakeText(this, "Loading " + _script, ToastLength.Short);
                    t.SetGravity(GravityFlags.Center, 0, 0);
                    t.Show();


                    var intent = new Intent();
                    intent.SetClass(this, typeof(StockDetailsActivity));
                    intent.PutExtra("Script", _script);
                    StartActivity(intent);
                }
            }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Get Quote").SetIcon(Resource.Drawable.ic_stock_get); ;
            menu.Add("Add Stock").SetIcon(Resource.Drawable.ic_quote_add);
            menu.Add("Delete Stocks").SetIcon(Resource.Drawable.ic_stock_delete);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.TitleFormatted.ToString())
            {
                case "Delete Stocks":
                    doDeleteStocks(); break;
                case "Add Stock":
                    doOpenAddStock(); break;
                case "Get Quote":
                    doOpenGetStock(); break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected void doDeleteStocks()
        {
            StockManager.DeleteAllStocks();
            if (!IsLoading)
            {
                ProgressDialog progress1 = ProgressDialog.Show(this, "", "Deleting Stock...", true);
                new Thread(new ThreadStart(() =>
                {
                    this.RunOnUiThread(() =>
                    {
                        doLoadMarketWatch();
                        progress1.Dismiss();
                    });
                })).Start();
                IsLoading = false;
            };
        }
        protected void doOpenAddStock()
        {
            StartActivity(typeof(AddStockActivity));
        }
        protected void doOpenGetStock()
        {
            StartActivity(typeof(GetStockActivity));
        }
    }
}