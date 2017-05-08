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
		UITextView displayText1, displayText2;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			aboutLabel = new UILabel (new RectangleF (10, 10, 300, 40));
			aboutLabel.Text = "About Evolve (Web Services)";

			displayText1 = new UITextView( new RectangleF(0, 50, 320, 200));
			displayText1.Editable = false;

			displayText2 = new UITextView( new RectangleF(0, 260, 320, 200));
			displayText2.Editable = false;

			View.Add (aboutLabel);
			View.Add (displayText1);
			View.Add (displayText2);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			// TODO: Demo1a: Check the XML in the About tab
			if (System.IO.File.Exists (SessionsXmlParser.Instance.SessionsXmlFilePath))
				displayText1.Text = System.IO.File.ReadAllText (SessionsXmlParser.Instance.SessionsXmlFilePath);
			// TODO: Demo1b: Check the JSON in the About tab
			if (System.IO.File.Exists (SpeakersJsonParser.Instance.SpeakersJsonFilePath))
				displayText2.Text = System.IO.File.ReadAllText (SpeakersJsonParser.Instance.SpeakersJsonFilePath);
		}
	}
}

