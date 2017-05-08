using System;
using MonoTouch.UIKit;

namespace EvolveLite
{
	public class EvolveTabBarController : UITabBarController 
	{
		public EvolveTabBarController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			var vc1 = new UINavigationController ();
			vc1.PushViewController (new SessionsViewController (), false);
			var vc2 = new UINavigationController ();
			vc2.PushViewController (new SpeakersViewController(), false);
			var vc3 = new AboutViewController();


			var vcs = new UIViewController[] {vc1, vc2, vc3};
			ViewControllers = vcs;


			vc1.TabBarItem = new UITabBarItem ("Sessions", UIImage.FromBundle("images/tabsession"), 0);
			vc2.TabBarItem = new UITabBarItem ("Speakers", UIImage.FromBundle("images/tabspeaker"), 1);
			vc3.TabBarItem = new UITabBarItem ("About",    UIImage.FromBundle("images/tababout"), 2);

			SelectedIndex = 0;

			//vc2.TabBarItem.BadgeValue = "4";

			CustomizableViewControllers = new UIViewController[]{};
		}
	}
}

