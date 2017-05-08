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

#if USE_SUPPORT
using Android.Support.V4.App;
#endif

namespace TideFragments
{
	[Activity (Label = "DetailsActivity")]	
	#if USE_SUPPORT
	public class DetailsActivity : FragmentActivity
	#else		
	public class DetailsActivity : Activity
	#endif
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			var index = Intent.Extras.GetInt ("selectedDateId", 0);
			var locId = Intent.Extras.GetInt ("_selectedLocId", 0);

			var details = DetailsFragment.NewInstance (index,locId);
			#if USE_SUPPORT
			var fragmentTransaction = SupportFragmentManager.BeginTransaction();
			#else
			var fragmentTransaction = FragmentManager.BeginTransaction(); 
			#endif            
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();


			// Create your application here
		}
	}
}

