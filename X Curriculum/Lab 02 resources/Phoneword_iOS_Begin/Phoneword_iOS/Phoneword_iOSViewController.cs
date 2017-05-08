using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public class Phoneword_iOSViewController : UIViewController
	{
		// translatedNumber was moved here from ViewDidLoad ()
		string translatedNumber = "";

		public List<String> PhoneNumbers { get; set; }

		public Phoneword_iOSViewController ()
		{
			//TODO: Step 1d: uncomment to give View Controller a title.
//			this.Title = "Phoneword";

			//TODO: Step 3a: uncomment - list of phone numbers called for Call History screen
//			PhoneNumbers = new List<String> ();

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			View.BackgroundColor = UIColor.White;

			// Create the UI to display.
			var enterPasswordLabel = new UILabel(new RectangleF(20, 80, 280, 21));
			enterPasswordLabel.Text = "Enter a Phoneword:";
			View.Add (enterPasswordLabel);

			var phoneNumberText = new UITextField(new RectangleF(20, 119, 280, 30));
			phoneNumberText.BorderStyle = UITextBorderStyle.RoundedRect;
			phoneNumberText.Text = "1-855-XAMARIN";
			View.Add (phoneNumberText);

			var translateButton = UIButton.FromType (UIButtonType.RoundedRect);
			translateButton.Frame = new RectangleF(20, 192, 280, 30);
			translateButton.SetTitle ("Translate", UIControlState.Normal);
			View.Add (translateButton);

			var callButton = UIButton.FromType (UIButtonType.RoundedRect);
			callButton.Frame = new RectangleF(20, 269, 280, 30);
			callButton.SetTitle ("Call", UIControlState.Normal);
			callButton.Enabled = false;
			View.Add (callButton);

			//TODO: Step 2: uncomment to add new Call History button to the UI.

//			var callHistoryButton = UIButton.FromType (UIButtonType.RoundedRect);
//			callHistoryButton.Frame = new RectangleF(20, 346, 280, 30);
//			callHistoryButton.SetTitle ("Call History", UIControlState.Normal);
//			View.Add (callHistoryButton);

			// On "Translate" button-up, convert the phone number with text to a number
			translateButton.TouchUpInside += (object sender, EventArgs e) => {

				// *** SHARED CODE from PhoneTranslator.cs ***
				translatedNumber = Core.PhonewordTranslator.ToNumber (phoneNumberText.Text);

				phoneNumberText.ResignFirstResponder ();

				if (translatedNumber == "") {
					callButton.SetTitle ("Call ", UIControlState.Normal);
					callButton.Enabled = false;
				} else {
					callButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					callButton.Enabled = true;
				}
			};

			// On "Call" button-up, try to dial a phone number
			callButton.TouchUpInside += (object sender, EventArgs e) => {

				//TODO: Step 3b: uncomment to store the phone number that we're dialing in PhoneNumbers
//				PhoneNumbers.Add (translatedNumber);

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
					
			//TODO: Step 3c: uncomment to wire up the CallHistoryButton

//			// Launches a new instance of CallHistoryController
//			callHistoryButton.TouchUpInside += (object sender, EventArgs e) => {
//				CallHistoryController callHistory = new CallHistoryController();
//				callHistory.PhoneNumbers = PhoneNumbers;
//
//				this.NavigationController.PushViewController (callHistory, true);
//			};
		}
	}
}