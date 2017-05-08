using System;
using Android.App;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

namespace EvolveMapsDemo
{
    [Activity(Label = "@string/app_name", MainLauncher = true, ConfigurationChanges=ConfigChanges.Orientation)]
    public class MainActivity : FragmentActivity
    {
        static readonly LatLng Austin = new LatLng(30.2652233534254, -97.73815460962083);

        GoogleMap map;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            InitMap();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SupportMapFragment mapFragment =  (SupportMapFragment) SupportFragmentManager.FindFragmentByTag("map");

            // The value of mapFragment.Map may be null if the mapFragment isn't completely initialize.
            // Set the GoogleMap to a class variable in OnResume, where the MapFragment should be fully instantiated.
            map = mapFragment.Map;

            // Create an instance of CameraUpdate and move the map to it.
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(Austin, 15);
            map.MoveCamera(cameraUpdate);

            // Add a marker
            AddEvolveMarker ();

            // Handle marker click
            map.MarkerClick += (object sender, GoogleMap.MarkerClickEventArgs e) => {
                Marker marker = e.P0;
                Toast.MakeText(this, String.Format("Marker ID {0}", marker.Id), ToastLength.Short).Show();
            };
        }

        void InitMap()
        {
            // set up map options
            GoogleMapOptions mapOptions = new GoogleMapOptions ()
                .InvokeMapType (GoogleMap.MapTypeSatellite)
                .InvokeZoomControlsEnabled (true);

            // add a SupportMapFragment
            Android.Support.V4.App.FragmentTransaction ftrans = SupportFragmentManager.BeginTransaction();
            SupportMapFragment mapFragment = SupportMapFragment.NewInstance(mapOptions);
            ftrans.Add(Resource.Id.map, mapFragment, "map");
            ftrans.Commit();
        }

        void AddEvolveMarker()
        {
            // image to use for the marker
            BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.conference);

            // create a marker in Austin, TX
            MarkerOptions markerOptions = new MarkerOptions ()
                .SetSnippet ("Welcome to Evolve!")
                .SetPosition (Austin)
                .SetTitle ("Evolve 2013")
                .InvokeIcon (icon);
                   
            // add the marker to the map
            Marker evolveMarker = map.AddMarker (markerOptions);
            evolveMarker.ShowInfoWindow ();

        }
    }
}
