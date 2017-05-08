using System;
using System.Drawing; // for RectangleF
using MonoTouch.UIKit;

namespace EvolveLite {
	/// <summary>
	/// For demo purposes only. This screen is not part of the course material
	/// </summary>
	public class AboutViewController : UIViewController {
		public AboutViewController ()
		{
		}

		UILabel aboutLabel;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			aboutLabel = new UILabel (new RectangleF (10, 10, 300, 40));
			aboutLabel.Text = "About Evolve (Lite)";

			View.Add (aboutLabel);
		}
	}
}

