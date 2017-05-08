using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace iOSViews
{
	[Register("View1")]
	public partial class View1 : UIView
	{
		public View1 (IntPtr h) : base(h)
		{
		}

		public View1 () 
		{
			var arr = NSBundle.MainBundle.LoadNib ("View1", this, null);

			var v = Runtime.GetNSObject (arr.ValueAt (0)) as UIView;

			AddSubview (v);

			label1.Text = "now set from codez";
		}
	}
}

