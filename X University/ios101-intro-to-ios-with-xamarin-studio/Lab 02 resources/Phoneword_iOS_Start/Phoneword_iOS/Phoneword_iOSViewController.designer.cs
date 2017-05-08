// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Phoneword_iOS
{
	[Register ("Phoneword_iOSViewController")]
	partial class Phoneword_iOSViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton CallButton { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField PhoneNumberText { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton TranslateButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PhoneNumberText != null) {
				PhoneNumberText.Dispose ();
				PhoneNumberText = null;
			}

			if (TranslateButton != null) {
				TranslateButton.Dispose ();
				TranslateButton = null;
			}

			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}
		}
	}
}