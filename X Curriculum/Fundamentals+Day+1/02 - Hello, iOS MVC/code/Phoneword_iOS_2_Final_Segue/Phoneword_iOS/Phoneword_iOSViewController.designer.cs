// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Phoneword_iOS
{
	[Register ("Phoneword_iOSViewController")]
	partial class Phoneword_iOSViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton CallButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton TranslateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PhoneNumberText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (TranslateButton != null) {
				TranslateButton.Dispose ();
				TranslateButton = null;
			}

			if (PhoneNumberText != null) {
				PhoneNumberText.Dispose ();
				PhoneNumberText = null;
			}
		}
	}
}
