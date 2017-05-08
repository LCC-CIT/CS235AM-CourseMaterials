using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite {
	public class AboutViewController : EvolveFlyoutViewControllerBase {

		UIWebView webView;

		public AboutViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "About";

			webView = new UIWebView()
			{
				ScalesPageToFit = false,
			};

			webView.SizeToFit();
			webView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height - 44);
			// Add the table view as a subview
			View.AddSubview(webView);

			string homePageUrl = NSBundle.MainBundle.BundlePath + "/About.html";

			 webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));
		}
	}
}