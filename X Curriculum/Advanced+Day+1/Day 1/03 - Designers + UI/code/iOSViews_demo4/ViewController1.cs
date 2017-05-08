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

		UIView view2;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			view2 = new View2() {Frame = UIScreen.MainScreen.Bounds};

			View.AddSubview(view2);

			view2.AddGestureRecognizer (new UITapGestureRecognizer ((g) => {
				view2.Frame = new RectangleF(0, 
				                             view2.Frame.Top + 50,
				                             view2.Frame.Width,
				                             view2.Frame.Height - 50);


			}));
		}
	}
}

