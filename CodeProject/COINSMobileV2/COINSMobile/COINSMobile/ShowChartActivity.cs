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
using System.Net;
using System.IO;
using COINSMobile.stockwebservice;
using COINSMobile.Core.BusinessLayer;

namespace COINSMobile
{
    [Activity(Label = "Stock Chart", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    
    public class ShowChartActivity : Activity
    {
        private bool IsLoading = false;
        private string _script = string.Empty;
        private string _scriptname=string.Empty;
        private ImageView imgChart = null;
        private TextView stockchartcaption = null;

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
            SetContentView(Resource.Layout.ShowChart);

            _script = Intent.GetStringExtra("Script");
            if (_script == string.Empty)
            {
                var t = Toast.MakeText(this, "Invalid Script...", ToastLength.Long);
                t.SetGravity(GravityFlags.Center, 0, 0);
                t.Show();

                //if script is blank for some reason, return back to Market Watch
                StartActivity(typeof(MarketWatchActivity));
            }
            
            
            //get the script name from previous screen
            _scriptname = Intent.GetStringExtra("ScriptName");
            
            //set the scriptname in text viw id:chartcaption defined in ChartLayout
            stockchartcaption = FindViewById<TextView>(Resource.Id.stockchartcaption);
            stockchartcaption.Text = _scriptname;

            //get all the button controls and attach click event handler
            Button btn1D = FindViewById<Button>(Resource.Id.button1D);
            Button btn5D = FindViewById<Button>(Resource.Id.button5D);
            Button btn3M = FindViewById<Button>(Resource.Id.button3M);
            Button btn6M = FindViewById<Button>(Resource.Id.button6M);
            Button btn1Y = FindViewById<Button>(Resource.Id.button1Y);
            Button btn2Y = FindViewById<Button>(Resource.Id.button2Y);
            Button btn5Y = FindViewById<Button>(Resource.Id.button5Y);

            btn1D.Click += (sender, e) => { doLoadChart(sender, "1D"); };
            btn5D.Click += (sender, e) => { doLoadChart(sender, "5D"); };
            btn3M.Click += (sender, e) => { doLoadChart(sender, "3M"); };
            btn6M.Click += (sender, e) => { doLoadChart(sender, "6M"); };
            btn1Y.Click += (sender, e) => { doLoadChart(sender, "1Y"); };
            btn2Y.Click += (sender, e) => { doLoadChart(sender, "2Y"); };
            btn5Y.Click += (sender, e) => { doLoadChart(sender, "5Y"); };

            doLoadChart(_script, "1D");//default Day 1 - Chart
            IsLoading = false;
        }

        private void doLoadChart(object _sender, string _option)
        {
            string str_script = string.Empty;
            Button btn = _sender as Button;
            if (btn != null)
            {
                str_script = btn.Text.Trim();
                if (str_script == string.Empty)
                {
                    var t = Toast.MakeText(this, "Invalid script, cannot load Chart", ToastLength.Short);
                    t.SetGravity(GravityFlags.Center, 0, 0);
                    t.Show();
                    StartActivity(typeof(StockDetailsActivity));
                    return;
                }
            }


            ProgressDialog progress = ProgressDialog.Show(this, "", "Loading Chart...", true);
            new Thread(new ThreadStart(() =>
            {
                this.RunOnUiThread(() =>
                {
                    if (!IsLoading)
                    {
                        doLoadChart(_script, _option);
                        IsLoading = false;
                    }
                    progress.Dismiss();
                });
            })).Start();
            IsLoading = false;
        }

        private void doLoadChart(string _stock, string _type)
        {
            //get the image control
            imgChart = FindViewById<ImageView>(Resource.Id.imageChart);

            byte[] image_data;
            stockwebservice.StockWebservice quoteObject = null;
            try
            {
                quoteObject = new stockwebservice.StockWebservice();
                image_data = quoteObject.GetStckChart(_stock, _type);
            }
            finally
            {
                quoteObject.Dispose();
                quoteObject = null;
            }

            //convert byte array to image
            Bitmap bitmapChart = BitmapFactory.DecodeByteArray(image_data, 0, image_data.Length);
            imgChart.SetImageBitmap(bitmapChart);
            imgChart.SetBackgroundResource(Resource.Color.transparent);

            //set the option selected (1 day, 5 days, 3 months etc)
            stockchartcaption = FindViewById<TextView>(Resource.Id.stockchartcaption);
            stockchartcaption.Text = _scriptname + " - " + _type;

            IsLoading = false;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Market Watch").SetIcon(Resource.Drawable.ic_stock_get); ;
            menu.Add("Stock Details").SetIcon(Resource.Drawable.ic_quote_add);
            menu.Add("Get Stock").SetIcon(Resource.Drawable.ic_stock_delete);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.TitleFormatted.ToString())
            {
                case "Market Watch":
                    doMarketWatch(); break;
                case "Add Stock":
                    doOpenAddStock(); break;
                case "Get Quote":
                    doOpenGetStock(); break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected void doMarketWatch()
        {
            StartActivity(typeof(MarketWatchActivity));
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