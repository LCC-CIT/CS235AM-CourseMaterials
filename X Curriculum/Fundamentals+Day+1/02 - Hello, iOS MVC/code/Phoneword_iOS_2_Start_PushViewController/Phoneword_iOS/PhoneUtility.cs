using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public static class PhoneUtility
	{
		public static void Dial(string phoneNumber)
		{
			var url = new NSUrl("tel:" + phoneNumber);
			
			if (!UIApplication.SharedApplication.OpenUrl(url))
			{
				var av = new UIAlertView("Not supported"
				                         , "Scheme 'tel:' is not supported on this device"
				                         , null
				                         , "OK"
				                         , null);
				av.Show();
			}
		}
	}
}

