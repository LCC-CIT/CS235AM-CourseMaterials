using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;


namespace iOSViews
{
	public class ViewController1 : UIViewController
	{
		public ViewController1 ()
		{
		}

		UILabel label;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			label = new UILabel(new RectangleF(0,0,200,150));
			label.Text = "label created in code";

			label.AutoresizingMask = UIViewAutoresizing.All;

			View.AddSubview(label);
		}
	}
}

