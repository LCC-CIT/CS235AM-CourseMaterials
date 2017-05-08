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

using COINSMobile.Core.BusinessLayer;
using COINSMobile.stockwebservice;

namespace COINSMobile
{
    [Activity(Label = "Add Stock",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AddStockActivity : Activity
    {
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
            SetContentView(Resource.Layout.AddStock);

            EditText textStock = FindViewById<EditText>(Resource.Id.textStock);
            Button btnAddStock = FindViewById<Button>(Resource.Id.buttonAddStock);
            btnAddStock.Click += (sender, e) =>
            {
                if (textStock.Text == string.Empty)
                {
                    //message if no text is entered in script
                    var t = Toast.MakeText(this, "Please eneter Stock Script", ToastLength.Short);
                    t.SetGravity(GravityFlags.Center, 0, 0);
                    t.Show();

                    textStock.Focusable = true;
                    return;
                }

                //check script from webservice to see, if it is a valid script
                if (doCheckStockScript(textStock.Text.Trim()))
                {
                    //if valid, add script to sqlite database
                    bool ret = AddStockScript(textStock.Text.Trim());
                    if (!ret)
                    {
                        textStock.Focusable = true;
                        return;
                    }
                    else
                    {
                        textStock.Text = "";
                        textStock.Focusable = true;

                        StartActivity(typeof(MarketWatchActivity));
                    }
                }
                else
                {
                    //message if no text entered is not a valid script
                    var t = Toast.MakeText(this, "You have eneterd invalis Stock Script. Please eneter a valid Stock Script", ToastLength.Short);
                    t.SetGravity(GravityFlags.Center, 0, 0);
                    t.Show();

                    textStock.Focusable = true;
                    return;
                }
            };
        }

        private bool doCheckStockScript(string _stockscript)
        {
            //conecting to webservice and get the xml
            string strXML = string.Empty;
            stockwebservice.StockWebservice quoteObject = null;
            try
            {
                quoteObject = new stockwebservice.StockWebservice();
                strXML = quoteObject.StockExists(_stockscript);
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
                return false;
            }
            if (strXML.ToLower() == "exception")
            {
                var t = Toast.MakeText(this, "Service not available now. Please try after sometime...", ToastLength.Short);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
                return false;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXML);

            XmlNodeList xnList = doc.SelectNodes("/stock/symbol");
            foreach (XmlNode xn in xnList)
            {
                if (xn["exchange"].InnerText.Trim().ToUpper() == "NA")
                    return false;
            }

            return true;
        }

        private bool AddStockScript(string _stockscript)
        {
            bool Ok = false;
            StockManager.Message = string.Empty;
            Ok = StockManager.SaveStock(_stockscript);
            if (StockManager.Message != string.Empty)
            {
                var t = Toast.MakeText(this, StockManager.Message, ToastLength.Short);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();
            }

            return Ok;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Market Watch").SetIcon(Resource.Drawable.ic_market_watch);
            menu.Add("Add Stock").SetIcon(Resource.Drawable.ic_quote_add);
            menu.Add("Get Quote").SetIcon(Resource.Drawable.ic_stock_get); ;
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
                case "Get Quote":
                    doOpenGetStock(); break;
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
        }
        protected void doOpenGetStock()
        {
            StartActivity(typeof(AddStockActivity));
        }
    }
}