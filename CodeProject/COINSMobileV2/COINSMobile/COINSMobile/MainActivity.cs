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

namespace COINSMobile
{
    [Activity(Theme = "@style/Theme.Splash",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        NoHistory = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(MarketWatchActivity));
        }
    }
}