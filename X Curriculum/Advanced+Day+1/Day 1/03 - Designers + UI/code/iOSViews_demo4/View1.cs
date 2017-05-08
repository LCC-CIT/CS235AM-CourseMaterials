using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace iOSViews
{
	public class View2 : UIView
	{
		UILabel label2;

		public View2 ()
		{
			InitView();
		}

		void InitView() {
			BackgroundColor = UIColor.White;

			label2 = new UILabel (new RectangleF(31, 15, 269, 21));
			label2.Text = "some default text";


		}

		public override void Draw (RectangleF rect) {
			base.Draw (rect);
			AddSubview(label2);
		}



		public override void LayoutSubviews () 
		{
			base.LayoutSubviews();

			label2.Frame = new RectangleF(0, Bounds.Top, 269, 21);

		}

	}
}

