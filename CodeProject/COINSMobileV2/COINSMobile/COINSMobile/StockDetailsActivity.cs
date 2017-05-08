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

using System.Threading;
using System.Xml;
using COINSMobile.stockwebservice;

namespace COINSMobile
{
    [Activity(Label = "Stock Quote Details", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        Icon = "@drawable/icon_activity")]
    public class StockDetailsActivity : Activity
    {
        private bool IsLoading = false;
        private string _script = string.Empty;
        private string _scriptname = string.Empty;

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
            SetContentView(Resource.Layout.StockDetails);

            _script = Intent.GetStringExtra("Script");
            if (_script == string.Empty)
            {
                var t = Toast.MakeText(this, "Invalid Script...", ToastLength.Long);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
                return;
            }

            ImageView btnRefresh = FindViewById<ImageView>(Resource.Id.buttonRefresh);
            btnRefresh.Click += (sender, e) =>
            {
                if (!IsLoading)
                {
                    ProgressDialog progress = ProgressDialog.Show(this, "", "Loading Quote...", true);
                    new Thread(new ThreadStart(() =>
                    {
                        this.RunOnUiThread(() =>
                        {
                            doLoadDetails();
                            progress.Dismiss();
                        });
                    })).Start();
                    IsLoading = false;
                }
            };

            ImageView btnHome = FindViewById<ImageView>(Resource.Id.buttonHome);
            btnHome.Click += (sender, e) =>
            {
                StartActivity(typeof(MarketWatchActivity));
                return;
            };

            ProgressDialog progressMain = ProgressDialog.Show(this, "", "Loading Quote...", true);
            new Thread(new ThreadStart(() =>
            {
                this.RunOnUiThread(() =>
                {
                    doLoadDetails();
                    progressMain.Dismiss();
                });
            })).Start();
        }

        protected void doLoadDetails()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                TextView symbolCaption;
                TextView priceCaption;
                TextView changeCaption;
                TextView datetimeCaption;

                String[,] data = new String[12, 2];

                //conecting to webservice and get the xml
                string strXML = string.Empty;
                stockwebservice.StockWebservice quoteObject = null;
                try
                {
                    quoteObject = new stockwebservice.StockWebservice();
                    strXML = quoteObject.GetStockQuote(_script);
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

                //find the controls in layout from resource
                LinearLayout stockdetailsLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutstockdetails);
                symbolCaption = FindViewById<TextView>(Resource.Id.symbolcaption);
                symbolCaption.SetTextAppearance(this, Resource.Style.boldText);
                priceCaption = FindViewById<TextView>(Resource.Id.pricecaption);
                priceCaption.SetTextAppearance(this, Resource.Style.boldText19);
                changeCaption = FindViewById<TextView>(Resource.Id.changecaption);
                changeCaption.SetTextAppearance(this, Resource.Style.boldText19);
                datetimeCaption = FindViewById<TextView>(Resource.Id.datetimecaption);
                datetimeCaption.SetTextAppearance(this, Resource.Style.smallText);

                //load the xml to XmlDocument
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXML);

                XmlNodeList xnList = doc.SelectNodes("/stock/symbol");
                foreach (XmlNode xn in xnList)
                {
                    data[0, 0] = "Symbol";
                    data[0, 1] = xn["code"].InnerText.Trim();

                    data[1, 0] = "Name";
                    data[1, 1] = xn["company"].InnerText.Trim();

                    //store the company nae in _scriptname to be used in chart Activity passed through intent
                    _scriptname = data[1, 1].Trim();

                    symbolCaption.Text = xn["company"].InnerText.Trim();
                    datetimeCaption.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

                    priceCaption.Text = xn["last"].InnerText.Trim() + " " + xn["currency"].InnerText.Trim();
                    changeCaption.Text = xn["change"].InnerText.Trim();

                    View vLinePrice = new View(this);
                    vLinePrice.SetMinimumHeight(2);
                    vLinePrice.SetBackgroundColor(Color.Rgb(164, 164, 164));
                    stockdetailsLinearLayout.AddView(vLinePrice);

                    data[2, 0] = "Exchange";
                    data[2, 1] = xn["exchange"].InnerText.Trim();

                    data[3, 0] = "Open";
                    data[3, 1] = xn["open"].InnerText.Trim();

                    data[4, 0] = "Day's High";
                    data[4, 1] = xn["high"].InnerText.Trim();

                    data[5, 0] = "Day's Low";
                    data[5, 1] = xn["low"].InnerText.Trim();

                    data[6, 0] = "Change";
                    data[6, 1] = xn["change"].InnerText.Trim();

                    data[7, 0] = "Change %";
                    data[7, 1] = xn["changepercent"].InnerText.Trim();

                    data[8, 0] = "Volume";
                    data[8, 1] = xn["volume"].InnerText.Trim();

                    data[9, 0] = "Previous Close";
                    data[9, 1] = xn["previousclose"].InnerText.Trim();

                    data[10, 0] = "Trade Time";
                    data[10, 1] = xn["tradetime"].InnerText.Trim();

                    data[11, 0] = "Market Capital";

                    decimal marketcapital = 0;
                    if (decimal.TryParse(xn["marketcapital"].InnerText.Trim(), out marketcapital))
                        data[11, 1] = string.Format("{0:#,0}", marketcapital);
                    else
                        data[11, 1] = xn["marketcapital"].InnerText.Trim();


                    TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.deatlLayout);
                    tableLayout.RemoveAllViews();
                    tableLayout.RefreshDrawableState();

                    for (int i = 2; i < 12; i++)
                    {
                        // add all inforamation to your tablerow in such a maaner that you want to display on screen.
                        TableRow demoTableRow = new TableRow(this);
                        TextView tv_l = new TextView(this);
                        TextView tv_r = new TextView(this);

                        tv_l.SetPadding(3, 3, 3, 3);
                        tv_r.SetPadding(3, 3, 3, 3);
                        tv_r.Gravity = GravityFlags.Right;

                        tv_l.Text = data[i, 0];
                        tv_r.Text = data[i, 1];

                        demoTableRow.AddView(tv_l);
                        demoTableRow.AddView(tv_r);

                        if (i == 0)
                        {
                            tv_l.SetTextAppearance(this, Resource.Style.boldText);
                        }


                        tableLayout.AddView(demoTableRow);
                        View vLineRow = new View(this);
                        vLineRow.SetMinimumHeight(1);
                        vLineRow.SetBackgroundColor(Color.Rgb(88, 88, 88));
                        tableLayout.AddView(vLineRow);
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Market Watch").SetIcon(Resource.Drawable.ic_market_watch);
            menu.Add("Add Stock").SetIcon(Resource.Drawable.ic_quote_add);
            menu.Add("Chart").SetIcon(Resource.Drawable.ic_stock_get); ;
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.TitleFormatted.ToString())
            {
                case "Market Watch":
                    doOpenMarketWatch(); break;
                case "Add Stock":
                    doOpenAddStock(); break;
                case "Chart":
                    doOpenChart(); break;
            }
            return base.OnOptionsItemSelected(item);
        }

        void MenuItemClicked(string item)
        {
            Console.WriteLine(item + " option menuitem clicked");
            var t = Toast.MakeText(this, "Options Menu '" + item + "' clicked", ToastLength.Short);
            t.SetGravity(GravityFlags.Center, 0, 0);
            t.Show();
        }

        protected void doOpenMarketWatch()
        {
            StartActivity(typeof(MarketWatchActivity));
        }
        protected void doOpenAddStock()
        {
            StartActivity(typeof(AddStockActivity));
        }
        protected void doOpenChart()
        {
            //get the _script from the previous activity
            if (_script != string.Empty)
            {
                var intent = new Intent();
                intent.SetClass(this, typeof(ShowChartActivity));

                //sending the script and scriptname value to next activity (stock chart)
                intent.PutExtra("Script", _script);
                intent.PutExtra("ScriptName", _scriptname);
                StartActivity(intent);
            }
            else
            {
                var t = Toast.MakeText(this, "Chart cannot be shown for this script", ToastLength.Short);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
            }
        }
    }
}