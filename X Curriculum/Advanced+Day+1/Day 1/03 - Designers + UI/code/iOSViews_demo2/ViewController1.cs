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

		View1 vw;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			vw = new View1() {Frame = View.Frame};

			View.AddSubview(vw);
		}
	}
}

