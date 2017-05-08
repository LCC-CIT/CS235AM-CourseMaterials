using System;
using MonoTouch.UIKit;
using System.Drawing;
using SatelliteMenu;
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


			//TODO: iOSDemo3: implement the SatelliteMenu component to replace ugly buttons
			#region Add SatelliteMenu component
//			var menuImage = UIImage.FromFile ("images/menu.png");
//			var y = View.Frame.Height - menuImage.Size.Height - 10;
//			var frame = new RectangleF (10, y, menuImage.Size.Width, menuImage.Size.Height);
//
//			var items = new [] { 
//				new SatelliteMenuButtonItem (UIImage.FromFile ("images/photos.png"), 1, "Choose Photo"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("images/camera.png"), 2, "Take Photo"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("images/filter.png"), 3, "Filter"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("images/twitter.png"), 4, "Twitter"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("images/email.png"), 5, "Email")           
//			};
//
//			var menu = new SatelliteMenuButton (View, menuImage, items, frame){
//				Radius = 115
//			};
//			
//			menu.MenuItemClick += (_, args) => {
//				Console.WriteLine ("{0} was clicked!", args.MenuItem.Name);
//
//				switch (args.MenuItem.Tag) {
//				case 1:
//					ChoosePhoto ();
//					break;
//				case 2:
//					TakePhoto ();
//					break;
//				case 3:
//					ApplyFilter ();
//					break;
//				case 4:
//					SendTweet ();
//					break;
//				case 5:
//					PickContactSendEmail ();
//					break;
//				}
//			};
//			View.AddSubview (menu);
			#endregion
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

		void SendTweet ()
		{
			if (SLComposeViewController.IsAvailable (SLServiceKind.Twitter)) {
				slComposer = SLComposeViewController.FromService (SLServiceType.Twitter);
				slComposer.SetInitialText ("Hello from #Evolve2013");
				if (image != null)
					slComposer.AddImage (image);
				slComposer.CompletionHandler += (result) => {
					InvokeOnMainThread (() => {
						DismissViewController (true, null);
						new UIAlertView ("Tweet Result", result.ToString (), null, "OK").Show ();
					});
				};
				PresentViewController (slComposer, true, null);
			}
		}

		void PickContactSendEmail ()
		{
			peoplePicker = new ABPeoplePickerNavigationController ();
			
			peoplePicker.Cancelled += (sender, e) => 
			{
				DismissViewController (true, null);
			};
			
			peoplePicker.SelectPerson += delegate(object sender, ABPeoplePickerSelectPersonEventArgs e) {

				string emailAddr = "";

				var person = e.Person;  
				var emails = person.GetEmails ();
				
				if (emails.Count > 0) {
					emailAddr = emails [0].Value;                  
				}
				
				DismissViewController (true, () => {
					SendEmail (emailAddr);
				});
			};

			PresentViewController (peoplePicker, true, null);
		}

		void SendEmail (string address)
		{	
			mailComposer = new MFMailComposeViewController ();
			mailComposer.SetToRecipients (new string[]{address}); 
			mailComposer.SetSubject ("Evolve 2013");
			mailComposer.SetMessageBody ("Hello from Evolve 2013", false);
			if(image != null)
				mailComposer.AddAttachmentData (image.CorrectOrientation().AsPNG (), "image/png", "PhotoFromEvolve");		
			mailComposer.Finished += (sender, e) => {
				DismissViewController (true, null); 
			};
			PresentViewController (mailComposer, true, null);
		}

	}
}