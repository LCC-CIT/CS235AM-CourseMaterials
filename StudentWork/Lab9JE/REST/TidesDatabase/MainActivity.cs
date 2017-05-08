using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.Collections.Generic;

#if USE_SUPPORT
using Android.Support.V4.App;
using FragmentUsingSupport;
#endif

namespace TidesDatabase
{
	[Activity( Label = "Tides Database")]
#if USE_SUPPORT
	public class MainActivity :FragmentActivity
#else
	public class MainActivity : Activity
#endif
	{
		protected override void OnCreate ( Bundle bundle )
		{
			base.OnCreate( bundle );
			SetContentView( Resource.Layout.MainView );
		}

	}

}

