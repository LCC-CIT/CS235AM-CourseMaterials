using Android.App;
using Android.OS;
using Android.Support.V4.App;

namespace Lab7Fragments
{
	[Activity(Label = "Lab7 Fragments", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : FragmentActivity
	{
		public static FragmentActivity context;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			context = this;

			SetContentView (Resource.Layout.activity_main);

		}
	}
}