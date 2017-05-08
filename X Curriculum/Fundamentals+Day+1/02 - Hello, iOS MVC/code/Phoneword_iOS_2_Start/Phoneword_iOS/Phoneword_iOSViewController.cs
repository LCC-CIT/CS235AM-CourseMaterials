using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		string translatedNumber = "";

//		public List<String> PhoneNumbers { get; set; }

		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
//			PhoneNumbers = new List<String> ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {

				// *** SHARED CODE ***
				translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);
				
				
				if (translatedNumber == "") {
					CallButton.SetTitle ("Call ", UIControlState.Normal);
					CallButton.Enabled = false;
				} else {
					CallButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};

			CallButton.TouchUpInside += (object sender, EventArgs e) => {

//				PhoneNumbers.Add (translatedNumber);
				PhoneUtility.Dial (translatedNumber);
			};
		}

//		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
//		{
//			base.PrepareForSegue (segue, sender);
//
//			var callHistoryContoller = segue.DestinationViewController as CallHistoryController;
//
//			if (callHistoryContoller != null) {
//				callHistoryContoller.PhoneNumbers = PhoneNumbers;
//			}
//		}
	}
}

