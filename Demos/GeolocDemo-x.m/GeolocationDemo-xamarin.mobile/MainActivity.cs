using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Geolocation;
using System.Threading.Tasks;

namespace GeolocationDemoxamarin.mobile
{
	[Activity (Label = "GeolocationDemo-xamarin.mobile", MainLauncher = true)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate 
			{
				var locator = new Geolocator(this) { DesiredAccuracy = 50 };

				locator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
					button.Text = String.Format("Time: {0}\n", t.Result.Timestamp);
					button.Text += String.Format("Latitude: {0}\n", t.Result.Latitude);
					button.Text += String.Format("Longitude: {0}\n", t.Result.Longitude);
				}, TaskScheduler.FromCurrentSynchronizationContext());
				button.Text = "waiting...";
			};
		}
	}
}


