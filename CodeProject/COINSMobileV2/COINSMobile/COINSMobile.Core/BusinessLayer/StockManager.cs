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

using COINSMobile.Core.DataLayer;

namespace COINSMobile.Core.BusinessLayer
{
    public static class StockManager
    {
        public static string Message { get; set; }
        static StockManager()
        {
        }

        public static bool IsStockExists(string _stockname)
        {
            return StockDatabase.IsStockExists(_stockname);
        }
        public static IEnumerable<Stock> GetStocks()
        {
            return StockDatabase.GetStocks();
        }
        public static bool SaveStock(string _stockname)
        {
            return StockDatabase.SaveStock(_stockname);
        }
        public static bool DeleteAllStocks()
        {
            return StockDatabase.DeleteAllStocks();
        }

    }
}