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

namespace COINSMobile.Core.BusinessLayer
{
    public class Stock : Java.Lang.Object
    {
        public long Id { get; set; }
        public string StockName { get; set; }

        public Stock()
        {
            Id = -1;
            StockName = string.Empty;
        }

        public Stock(long id, string stockName)
        {
            Id = id;
            StockName = stockName;
        }
        public override string ToString()
        {
            return StockName.ToString();
        }
    }

}