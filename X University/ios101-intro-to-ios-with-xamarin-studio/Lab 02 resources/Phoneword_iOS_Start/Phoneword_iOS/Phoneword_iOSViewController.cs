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

// Added to access List<string>
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		// translatedNumber was moved here from ViewDidLoad ()
		string translatedNumber = "";

		public List<String> PhoneNumbers { get; set; }

		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
			//TODO: Step 2a: uncomment - list of phone numbers called for Call History screen
			//PhoneNumbers = new List<String> ();

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

			// On "Translate" button-up, convert the phone number with text to a number
			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {

				// *** SHARED CODE from PhoneTranslator.cs ***
				translatedNumber = Core.PhonewordTranslator.ToNumber (PhoneNumberText.Text);

				PhoneNumberText.ResignFirstResponder ();

				if (translatedNumber == "") {
					CallButton.SetTitle ("Call ", UIControlState.Normal);
					CallButton.Enabled = false;
				} else {
					CallButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};

			// On "Call" button-up, try to dial a phone number
			CallButton.TouchUpInside += (object sender, EventArgs e) => {

				//TODO: Step 2b: uncomment to store the phone number that we're dialing in PhoneNumbers
				//PhoneNumbers.Add (translatedNumber);

				var url = new NSUrl ("tel:" + translatedNumber);

				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				}
			};	

			//TODO: Step 2d: uncomment to wire up the CallHistoryButton
			//
			//			// Launches a new instance of CallHistoryController
			//			CallHistoryButton.TouchUpInside += (object sender, EventArgs e) => {
			//				CallHistoryController callHistory = this.Storyboard.InstantiateViewController ("CallHistoryController") as CallHistoryController;
			//				if (callHistory != null) {
			//					callHistory.PhoneNumbers = PhoneNumbers;
			//				}
			//				this.NavigationController.PushViewController (callHistory, true);
			//			};

		}

		//TODO: Step 2c: uncomment to implement the PrepareForSegue method
		//		
		//		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		//		{
		//			base.PrepareForSegue (segue, sender);
		//		
		//			var callHistoryContoller = segue.DestinationViewController
		//						as CallHistoryController;
		//		
		//			if (callHistoryContoller != null) {
		//				callHistoryContoller.PhoneNumbers = PhoneNumbers;
		//			}
		//		}


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
