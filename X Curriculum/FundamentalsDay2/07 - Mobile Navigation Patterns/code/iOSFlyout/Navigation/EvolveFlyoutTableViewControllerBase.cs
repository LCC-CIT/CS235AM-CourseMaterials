using System;
using MonoTouch.UIKit;

namespace EvolveLite
{
	public class EvolveFlyoutTableViewControllerBase : UITableViewController
	{
		public EvolveFlyoutTableViewControllerBase ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var bbi = new UIBarButtonItem(UIImage.FromBundle ("images/slideout.png"), UIBarButtonItemStyle.Plain, (sender, e) => {
				AppDelegate.FlyoutNav.ToggleMenu();
			});
			NavigationItem.SetLeftBarButtonItem (bbi, false);
		}
	}
}