using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Lab7Fragments
{
[Activity(Label = "Tides Activity")]
public class TidesActivity : FragmentActivity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);
        var index = Intent.Extras.GetInt("selStat", 0);

		var details = TidesFragment.NewInstance(StationsFragment.stations[StationsFragment.selectedStation].Name, StationsFragment.stations[StationsFragment.selectedStation].ID, StationsFragment.stations[StationsFragment.selectedStation].State, StationsFragment.selectedStation); // Details
        var fragmentTransaction = SupportFragmentManager.BeginTransaction();
        fragmentTransaction.Add(Android.Resource.Id.Content, details);
        fragmentTransaction.Commit();
    }
}
}