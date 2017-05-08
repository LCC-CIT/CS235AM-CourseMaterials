using System;
using MonoTouch.UIKit;
using System.Drawing;

using Xamarin.Media;

using Xamarin.Geolocation;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.Social;
using MonoTouch.MessageUI;
using MonoTouch.AddressBookUI;

namespace EvolveLite {
	public class PhotosViewController : UIViewController {
		UIImageView imageView;
		UIImage image;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Feeling Social?";
			View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 64);
			View.BackgroundColor = UIColor.White;

			image = UIImage.FromBundle ("images/blank");
			// ImageView that will show photos we take or choose
			imageView = new UIImageView { 
				Frame = new RectangleF (40, 20, 240, 240), //View.Frame.Width - 80, View.Frame.Height - 200),
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = image
			};

			View.AddSubview (imageView);

			//TODO: iOSDemo1a: uncomment for button to Take Photo
//			var takePhoto = UIButton.FromType (UIButtonType.RoundedRect);
//			takePhoto.Frame = new RectangleF (10, 280, 120, 30);
//			takePhoto.SetTitle ("Take Photo", UIControlState.Normal);
//			takePhoto.TouchUpInside += (sender, e) => TakePhoto();
//			View.AddSubview (takePhoto);

			//TODO: iOSDemo1b: uncomment for button to Choose Existing Photo
//			var choosePhoto = UIButton.FromType (UIButtonType.RoundedRect);
//			choosePhoto.Frame = new RectangleF (10, 320, 120, 30);
//			choosePhoto.SetTitle ("Choose Photo", UIControlState.Normal);
//			choosePhoto.TouchUpInside += (sender, e) => ChoosePhoto();
//			View.AddSubview (choosePhoto);

			//TODO: iOSDemo1c: uncomment
//			var tintPhoto = UIButton.FromType (UIButtonType.RoundedRect);
//			tintPhoto.Frame = new RectangleF (10, 360, 120, 30);
//			tintPhoto.SetTitle ("Tint Photo", UIControlState.Normal);
//			tintPhoto.TouchUpInside += (sender, e) => ApplyFilter();
//			View.AddSubview (tintPhoto);

		}

		//TODO: iOSDemo1a: take a photo using Xamarin.Mobile cross-platform API 
//		void TakePhoto ()
//		{		
//			var picker = new MediaPicker (); // Xamarin.Mobile!
//
//			if (!picker.IsCameraAvailable || !picker.PhotosSupported) {
//				new UIAlertView ("Camera Unavailable", "This device does not have a camera", null, "OK").Show ();
//				return;
//			}
//
//			var mediaOptions = new StoreCameraMediaOptions{
//				Name = "temp.png",
//				Directory = "temp"
//			};
//
//			picker.TakePhotoAsync (mediaOptions).ContinueWith (t => {
//				if (t.IsCanceled)
//					return;
//
//				InvokeOnMainThread (delegate {
//					image = UIImage.FromFile (t.Result.Path);
//					imageView.Image = image;	
//				});
//			});
//		}

		//TODO: iOSDemo1b: pick an existing photo using Xamarin.Mobile cross-platform API 
//		void ChoosePhoto ()
//		{
//			var picker = new MediaPicker ();
//			
//			picker.PickPhotoAsync ().ContinueWith (t => {
//				if (t.IsCanceled)
//					return;
//				
//				InvokeOnMainThread (delegate {
//					image = UIImage.FromFile (t.Result.Path);
//					imageView.Image = image;
//				});
//			});
//		}

		//TODO: iOSDemo1c: use iOS-specific CoreImage to apply an image filter 
//		void ApplyFilter ()
//		{
//			if (image != null) {
//
//				var ciImage = new CIImage (image.CorrectOrientation ());
//			
//				var sepia = new CISepiaTone (); 
//				sepia.Image = ciImage;
//				sepia.Intensity = 1.0f;
//			
//				var ctx = CIContext.FromOptions (null);
//				var output = sepia.OutputImage;
//				var cgImage = ctx.CreateCGImage (output, output.Extent);
//			
//				image = UIImage.FromImage (cgImage);
//				imageView.Image = image;
//
//			} else {
//				new UIAlertView ("Image Unavailable", "Please select an image or take one with the camera", null, "OK").Show ();
//			}
//		}

	}
}