using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace EvolveLite
{
	public partial class EvolveMapViewController : UIViewController
	{
//		MKMapView map;
//		MapDelegate mapDelegate;
        
		public override void LoadView ()
		{
//			map = new MKMapView (UIScreen.MainScreen.Bounds);
//			View = map;
		}
        
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// change map type, show user location, and allow zooming and panning
//			map.MapType = MKMapType.Standard;
//			map.ShowsUserLocation = true;
//			map.ZoomEnabled = true;
//			map.ScrollEnabled = true;

			// set map center and region
//			double lat = 30.2652233534254;
//			double lon = -97.73815460962083;
//			CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D (lat, lon);
//			MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance (mapCenter, 200, 200);
//			map.CenterCoordinate = mapCenter;
//			map.Region = mapRegion;

			// set the map delegate
//			mapDelegate = new MapDelegate ();
//			map.Delegate = mapDelegate;

			// add a custom annotation at the map center
//			map.AddAnnotation (new ConferenceAnnotation ("Evolve Conference", mapCenter));

			// add an overlay of the hotel
//			MKPolygon hotelOverlay = MKPolygon.FromCoordinates (
//				new CLLocationCoordinate2D[]{
//				new CLLocationCoordinate2D (30.2649977168594, -97.73863627705),
//				new CLLocationCoordinate2D (30.2648461170005, -97.7381627734755),
//				new CLLocationCoordinate2D (30.2648355402574, -97.7381750192576),
//				new CLLocationCoordinate2D (30.2647791309417, -97.7379872505988),
//				new CLLocationCoordinate2D (30.2654525150319, -97.7377341711021),
//				new CLLocationCoordinate2D (30.2654807195004, -97.7377994819399),
//				new CLLocationCoordinate2D (30.2655089239607, -97.7377994819399),
//				new CLLocationCoordinate2D (30.2656428950368, -97.738346460207),
//				new CLLocationCoordinate2D (30.2650364981811, -97.7385709662122),
//				new CLLocationCoordinate2D (30.2650470749025, -97.7386199493406)
//			});

//			map.AddOverlay (hotelOverlay);
		}

//		class MapDelegate : MKMapViewDelegate
//		{
//			static string annotationId = "ConferenceAnnotation";
//			UIImageView venueView;
//			UIImage venueImage;
//
//			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
//			{
//				MKAnnotationView annotationView = null;
//
//				if (annotation is MKUserLocation)
//					return null; 
//
//				if (annotation is ConferenceAnnotation) {
//
//					// show conference annotation
//					annotationView = mapView.DequeueReusableAnnotation (annotationId);
//
//					if (annotationView == null)
//						annotationView = new MKAnnotationView (annotation, annotationId);
//                
//					annotationView.Image = UIImage.FromFile ("images/conference.png");
//					annotationView.CanShowCallout = false;
//				} 
//
//				return annotationView;
//			}

//			public override void DidSelectAnnotationView (MKMapView mapView, MKAnnotationView view)
//			{
//				// show an image view when the conference annotation view is selected
//				if (view.Annotation is ConferenceAnnotation) {
//
//					venueView = new UIImageView ();
//					venueView.ContentMode = UIViewContentMode.ScaleAspectFit;
//					venueImage = UIImage.FromFile ("images/venue.png");
//					venueView.Image = venueImage;
//					view.AddSubview (venueView);
//
//					UIView.Animate (0.4, () => {
//						venueView.Frame = new RectangleF (-75, -75, 200, 200); });
//				}
//			}

//			public override void DidDeselectAnnotationView (MKMapView mapView, MKAnnotationView view)
//			{
//				// remove the image view when the conference annotation is deselected
//				if (view.Annotation is ConferenceAnnotation) {
//
//					venueView.RemoveFromSuperview ();
//					venueView.Dispose ();
//					venueView = null;
//				}
//			}

//			public override MKOverlayView GetViewForOverlay (MKMapView mapView, NSObject overlay)
//			{
//				// return a view for the polygon
//				MKPolygon polygon = overlay as MKPolygon;
//				MKPolygonView polygonView = new MKPolygonView (polygon);
//				polygonView.FillColor = UIColor.Purple;
//				polygonView.StrokeColor = UIColor.Gray;
//				return polygonView;
//			}
//		}
        

	}
}

