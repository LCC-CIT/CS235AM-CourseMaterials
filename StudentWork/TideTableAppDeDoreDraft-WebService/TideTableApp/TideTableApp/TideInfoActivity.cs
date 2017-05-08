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

namespace TideTableApp
{
	[Activity (Label = "TideInfoActivity")]			
	public class TideInfoActivity : Android.Support.V4.App.FragmentActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			var index = Intent.Extras.GetInt ("selected", 0);

			var info = TideInfoFragment.NewInstance(index);
			var fragmentTransaction = SupportFragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, info);		//adds to the activities root view while inside the fragmentTransaction
			fragmentTransaction.Commit();
		}
	}
}


