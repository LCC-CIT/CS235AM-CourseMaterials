using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MapsAndLocationDemo
{
	[Activity (Label = "MapsAndLocationDemo", MainLauncher = true)]
    public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
             
			var showMapsApp = FindViewById<Button> (Resource.Id.showMapsApp);
            
			showMapsApp.Click += (sender, e) => {           
				var geoUri = Android.Net.Uri.Parse ("geo:42.374260,-71.120824");
				//var geoUri = Android.Net.Uri.Parse ("geo:0,0?q=coop+cambridge");
				var mapIntent = new Intent (Intent.ActionView, geoUri);
				
				StartActivity (mapIntent);
			};
                      
			var showStreetView = FindViewById<Button> (Resource.Id.showStreetView);
            
			showStreetView.Click += (sender, e) => {           
      	
				var streetViewUri = Android.Net.Uri.Parse ("google.streetview:cbll=42.374260,-71.120824&cbp=1,90,,0,1.0&mz=20");	
				var streetViewIntent = new Intent (Intent.ActionView, streetViewUri);	
				StartActivity (streetViewIntent);
			};
			
			var showMapActivity = FindViewById<Button> (Resource.Id.showSampleMapActivity);
            
			showMapActivity.Click += (sender, e) => {           
      	
				var mapIntent = new Intent (this, typeof(SampleMapActivity));
				StartActivity (mapIntent);
			}; 
			
			var showMapWithOverlays = FindViewById<Button> (Resource.Id.showMapWithOverlays);
			
			showMapWithOverlays.Click += (sender, e) => {
				var mapIntent = new Intent (this, typeof(MapWithOverlayActivity));
				StartActivity (mapIntent);
			};
			
			var demoLocationServices = FindViewById<Button> (Resource.Id.demoLocationServices);
			
			demoLocationServices.Click += (sender, e) => {
				var locationIntent = new Intent (this, typeof(LocationActivity));
				StartActivity (locationIntent);
			};
		}
	}
}