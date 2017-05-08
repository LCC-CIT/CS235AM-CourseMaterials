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
		SLComposeViewController slComposer;
		MFMailComposeViewController mailComposer;
		ABPeoplePickerNavigationController peoplePicker;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Feeling Social?";
			View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 64);
			View.BackgroundColor = UIColor.White;

			image = UIImage.FromBundle ("images/blank");
			imageView = new UIImageView { 
				Frame = new RectangleF (40 , 20, View.Frame.Width - 80, View.Frame.Height - 200),
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = image
			};
			View.AddSubview (imageView);

			var takePhoto = UIButton.FromType (UIButtonType.RoundedRect);
			takePhoto.Frame = new RectangleF (10, 280, 120, 30);
			takePhoto.SetTitle ("Take Photo", UIControlState.Normal);
			takePhoto.TouchUpInside += (sender, e) => TakePhoto();
			View.AddSubview (takePhoto);

			var choosePhoto = UIButton.FromType (UIButtonType.RoundedRect);
			choosePhoto.Frame = new RectangleF (10, 320, 120, 30);
			choosePhoto.SetTitle ("Choose Photo", UIControlState.Normal);
			choosePhoto.TouchUpInside += (sender, e) => ChoosePhoto();
			View.AddSubview (choosePhoto);

			var tintPhoto = UIButton.FromType (UIButtonType.RoundedRect);
			tintPhoto.Frame = new RectangleF (10, 360, 120, 30);
			tintPhoto.SetTitle ("Tint Photo", UIControlState.Normal);
			tintPhoto.TouchUpInside += (sender, e) => ApplyFilter();
			View.AddSubview (tintPhoto);


			//TODO: iOSDemo2a: button to send a tweet
//			var tweetPhoto = UIButton.FromType (UIButtonType.RoundedRect);
//			tweetPhoto.Frame = new RectangleF (160, 280, 120, 30);
//			tweetPhoto.SetTitle ("Tweet Photo", UIControlState.Normal);
//			tweetPhoto.TouchUpInside += (sender, e) => SendTweet();
//			View.AddSubview (tweetPhoto);


			//TODO: iOSDemo2b: button to send photo to an existing contact
//			var emailPhoto = UIButton.FromType (UIButtonType.RoundedRect);
//			emailPhoto.Frame = new RectangleF (160, 320, 120, 30);
//			emailPhoto.SetTitle ("Email Photo", UIControlState.Normal);
//			emailPhoto.TouchUpInside += (sender, e) => PickContactSendEmail();
//			View.AddSubview (emailPhoto);

		}

		void ChoosePhoto ()
		{
			var picker = new MediaPicker ();

			picker.PickPhotoAsync ().ContinueWith (t => {
				if (t.IsCanceled)
					return;

				InvokeOnMainThread (delegate {
					image = UIImage.FromFile (t.Result.Path);
					imageView.Image = image;
				});
			});
		}

		void TakePhoto ()
		{		
			var picker = new MediaPicker ();

			if (!picker.IsCameraAvailable || !picker.PhotosSupported) {
				new UIAlertView ("Camera Unavailable", "This device does not have a camera", null, "OK").Show ();
				return;
			}

			picker.TakePhotoAsync (new StoreCameraMediaOptions{
				Name = "temp.png",
				Directory = "temp"
			}).ContinueWith (t => {

				if (t.IsCanceled)
					return;

				InvokeOnMainThread (delegate {
					image = UIImage.FromFile (t.Result.Path);
					imageView.Image = image;	
				});
			});
		}

		void ApplyFilter ()
		{
			if (image != null) {

				var ciImage = new CIImage (image.CorrectOrientation ());
			
				var sepia = new CISepiaTone (); 
				sepia.Image = ciImage;
				sepia.Intensity = 1.0f;
			
				var ctx = CIContext.FromOptions (null);
				var output = sepia.OutputImage;
				var cgImage = ctx.CreateCGImage (output, output.Extent);
			
				image = UIImage.FromImage (cgImage);
				imageView.Image = image;

			} else {
				new UIAlertView ("Image Unavailable", "Please select an image or take one with the camera", null, "OK").Show ();
			}
		}

		//TODO: iOSDemo2a: send a tweet
//		void SendTweet ()
//		{
//			if (SLComposeViewController.IsAvailable (SLServiceKind.Twitter)) {
//				slComposer = SLComposeViewController.FromService (SLServiceType.Twitter);
//				slComposer.SetInitialText ("Hello from #Evolve2013");
//				if (image != null)
//					slComposer.AddImage (image);
//				slComposer.CompletionHandler += (result) => {
//					InvokeOnMainThread (() => {
//						DismissViewController (true, null);
//						new UIAlertView ("Tweet Result", result.ToString (), null, "OK").Show ();
//					});
//				};
//				PresentViewController (slComposer, true, null);
//			}
//		}







		//TODO: iOSDemo2b: pick a contact then send the email
//		void PickContactSendEmail ()
//		{
//			peoplePicker = new ABPeoplePickerNavigationController ();
//			
//			peoplePicker.Cancelled += (sender, e) => 
//			{
//				DismissViewController (true, null);
//			};
//			
//			peoplePicker.SelectPerson += delegate(object sender, ABPeoplePickerSelectPersonEventArgs e) {
//
//				string emailAddr = "";
//
//				var person = e.Person;  
//				var emails = person.GetEmails ();
//				
//				if (emails.Count > 0) {
//					emailAddr = emails [0].Value;                  
//				}
//				
//				DismissViewController (true, () => {
//					SendEmail (emailAddr); // Sends the email! 
//				});
//			};
//
//			PresentViewController (peoplePicker, true, null);
//		}

		//TODO: iOSDemo2b: method that sends an email using iOS Mail
//		void SendEmail (string address)
//		{	
//			mailComposer = new MFMailComposeViewController ();
//			mailComposer.SetToRecipients (new string[]{address}); 
//			mailComposer.SetSubject ("Evolve 2013");
//			mailComposer.SetMessageBody ("Hello from Evolve 2013", false);
//			if(image != null)
//				mailComposer.AddAttachmentData (image.CorrectOrientation().AsPNG (), "image/png", "PhotoFromEvolve");		
//			mailComposer.Finished += (sender, e) => {
//				DismissViewController (true, null); 
//			};
//			PresentViewController (mailComposer, true, null);
//		}

	}
}