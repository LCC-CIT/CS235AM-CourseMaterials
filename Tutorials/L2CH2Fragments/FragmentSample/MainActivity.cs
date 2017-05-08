using Android.OS;
using Android.App;

#if USE_SUPPORT
using Android.Support.V4.App;
using FragmentUsingSupport;
#endif

namespace L2CH2Fragments
{
	[Activity (Label = "Shakespere Fragments", MainLauncher = true)]
#if USE_SUPPORT
	public class MainActivity :FragmentActivity
#else
	public class MainActivity : Activity
#endif
	{												
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_main);
		}
	}
}


