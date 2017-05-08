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
using DAL;

namespace Application
{
	[Activity (Label = "TideInfoActivity")]			
	public class TideInfoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.TideInfo);

			TextView info = FindViewById<TextView> (Resource.Id.TideInfoView);
			info.Text = Intent.GetStringExtra ("INFO") ?? "Data not found";
		}
	}
}

