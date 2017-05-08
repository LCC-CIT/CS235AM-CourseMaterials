//
// Phoneword_iOSViewController.cs: Manages our single view app and responds to button taps
//
// Author:
//   George Seiler (george.seiler@xamarin.com)
//
// Copyright (C) 2013 Xamarin, Inc (http://www.xamarin.com)
//
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.

			string translatedNumber = "";

			//TODO: Step 1a: uncomment to wire up TranslateButton
//
//			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
//
//				// Convert the phone number with text to a number - from PhoneTranslator.cs
//				translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);
//
//				// Dismiss the keyboard - if Text Field was tapped
//				PhoneNumberText.ResignFirstResponder ();
//
//				if (translatedNumber == "") {
//					CallButton.SetTitle ("Call ", UIControlState.Normal);
//					CallButton.Enabled = false;
//				} else {
//					CallButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
//					CallButton.Enabled = true;
//				}
//			};
//

			//TODO: Step 1b: uncomment to wire up CallButton
//
//			// On "Call" button-up, try to dial a phone number
//			CallButton.TouchUpInside += (object sender, EventArgs e) => {
//			
//				var url = new NSUrl ("tel:" + translatedNumber);
//
//				// Use URL handler with tel: prefix to invoke the Apple's Phone app, otherwise show alert dialog
//				if (!UIApplication.SharedApplication.OpenUrl (url)) {
//					var av = new UIAlertView ("Not supported",
//								  "Scheme 'tel:' is not supported on this device",
//								  null,
//								  "OK",
//								  null);
//					av.Show ();
//				}
//			};
//
		}
			
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}