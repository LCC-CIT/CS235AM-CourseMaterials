using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		UILabel enterPasswordLabel;
		UITextField phoneNumberText;
		UIButton translateButton;
		UIButton callButton;

		public Phoneword_iOSViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			View.Frame = UIScreen.MainScreen.Bounds;
			View.BackgroundColor = UIColor.White;
			View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

			string translatedNumber = "";

			//TODO: Step 1: uncomment to create UI to display.

//			enterPasswordLabel = new UILabel(new RectangleF(20, 80, 280, 21));
//			enterPasswordLabel.Text = "Enter a Phoneword:";
//			View.Add (enterPasswordLabel);
//
//			phoneNumberText = new UITextField(new RectangleF(20, 119, 280, 30));
//			phoneNumberText.BorderStyle = UITextBorderStyle.RoundedRect;
//			phoneNumberText.Text = "1-855-XAMARIN";
//			View.Add (phoneNumberText);
//
//			translateButton = UIButton.FromType (UIButtonType.RoundedRect);
//			translateButton.Frame = new RectangleF(20, 192, 280, 30);
//			translateButton.SetTitle ("Translate", UIControlState.Normal);
//			View.Add (translateButton);
//
//			callButton = UIButton.FromType (UIButtonType.RoundedRect);
//			callButton.Frame = new RectangleF(20, 269, 280, 30);
//			callButton.SetTitle ("Call", UIControlState.Normal);
//			callButton.Enabled = false;
//			View.Add (callButton);

			//TODO: Step 2a: uncomment to wire up TranslateButton

//			translateButton.TouchUpInside += (object sender, EventArgs e) => {
//
//				// Convert the phone number with text to a number - from PhoneTranslator.cs
//				translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
//
//				// Dismiss the keyboard - if Text Field was tapped
//				phoneNumberText.ResignFirstResponder ();
//
//				if (translatedNumber == "") {
//					callButton.SetTitle ("Call ", UIControlState.Normal);
//					callButton.Enabled = false;
//				} else {
//					callButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
//					callButton.Enabled = true;
//				}
//			};

			//TODO: Step 2b: uncomment to wire up CallButton

			// On "Call" button-up, try to dial a phone number
//			callButton.TouchUpInside += (object sender, EventArgs e) => {
//
//				var url = new NSUrl ("tel:" + translatedNumber);
//
//				// Use URL handler with tel: prefix to invoke the Apple's Phone app, otherwise show alert dialog
//				if (!UIApplication.SharedApplication.OpenUrl (url)) {
//					var av = new UIAlertView ("Not supported",
//						"Scheme 'tel:' is not supported on this device",
//						null,
//						"OK",
//						null);
//					av.Show ();
//				}
//			};
		}
	}
}