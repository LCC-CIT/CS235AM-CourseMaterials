using System;
using Android.App;
using Android.OS;
using Android.Widget;

using Android.Webkit;

namespace Xamarin.Data.Droid.Activities {
	/// <summary>
	/// Example WebView showing 'About' HTML info
	/// </summary>
	[Activity (Label = "About")]
	public class AboutActivity : Activity {
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.about);
			
			var web = FindViewById<WebView>(Resource.Id.aboutwebview);
			web.LoadUrl("file:///android_asset/about.html");
		}
	}
}