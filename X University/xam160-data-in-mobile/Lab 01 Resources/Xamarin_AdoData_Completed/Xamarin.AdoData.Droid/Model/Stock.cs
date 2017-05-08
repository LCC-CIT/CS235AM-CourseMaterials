using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.AdoData.Droid.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
