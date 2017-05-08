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
//			string phoneNumber = "";
//
//			//On "Translate" button-up, request translation from model
//			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
//				PhoneNumberText.ResignFirstResponder ();
//				phoneNumber = PhoneTranslator.Translate (PhoneNumberText.Text);
//				CallButton.SetTitle ("Call " + phoneNumber, UIControlState.Normal);
//				CallButton.Enabled = phoneNumber != "";
//			};
//
//			//On "Call" button-up, try to dial a phone number
//			CallButton.TouchUpInside += (object sender, EventArgs e) => {
//				var url = new NSUrl ("tel:" + phoneNumber);
//		
//				if (!UIApplication.SharedApplication.OpenUrl (url)) {
//					var av = new UIAlertView ("Not supported"
//			                         , "Scheme 'tel:' is not supported on this device"
//			                         , null
//			                         , "OK"
//			                         , null);
//					av.Show ();
//				}
//			};
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

