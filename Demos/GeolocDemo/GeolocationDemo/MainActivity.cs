using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Geolocation;
using System.Threading.Tasks;

namespace GeolocationDemo
{
	[Activity (Label = "GeolocationDemo", MainLauncher = true)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var locator = new Geolocator(this) { DesiredAccuracy = 50 };

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate {
				locator.GetPositionAsync (timeout: 10000).ContinueWith (t =>  {
					TextView textView = FindViewById<TextView> (Resource.Id.positionTextView);
					textView.Text = "Testing";
					textView.Text = String.Format ("Position Status: {0}\r\n", t.Result.Timestamp);
					textView.Text += String.Format ("Position Latitude: {0}\r\n", t.Result.Latitude);
					textView.Text += String.Format ("Position Longitude: {0}\r\n", t.Result.Longitude);
				}, TaskScheduler.FromCurrentSynchronizationContext() );
			};

		}
	}
}


