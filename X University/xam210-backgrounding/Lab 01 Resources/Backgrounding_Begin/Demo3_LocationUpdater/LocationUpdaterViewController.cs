using System;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;

namespace Demo3LocationUpdater
{
    public partial class LocationUpdaterViewController : UIViewController
    {
        public LocationUpdaterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // TODO: Demo 3 - Step 2 - Subscribe to the LocationUpdated event
//            AppDelegate.Manager.LocationUpdated += HandleLocationChanged;

            // TODO: Demo3 - Step 3a - Subscribe to the LocationUpdated event when app is active
//            UIApplication.Notifications.ObserveDidBecomeActive ((sender, e) => {
//                AppDelegate.Manager.LocationUpdated += HandleLocationChanged;
//            });

            // TODO: Demo3 - Step 3b - Unsubscribe from the LocationUpdated event when the app is backgrounded
//            UIApplication.Notifications.ObserveDidEnterBackground ((sender, e) => {
//                AppDelegate.Manager.LocationUpdated -= HandleLocationChanged;
//            });
        }

        public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
        {
            // handle foreground updates
            CLLocation location = e.Location;

            LblAltitude.Text = location.Altitude + " meters";
            LblLongitude.Text = location.Coordinate.Longitude.ToString ();
            LblLatitude.Text = location.Coordinate.Latitude.ToString ();
            LblCourse.Text = location.Course.ToString ();
            LblSpeed.Text = location.Speed.ToString ();

            Console.WriteLine ("UI updated");
        }
    }
}

