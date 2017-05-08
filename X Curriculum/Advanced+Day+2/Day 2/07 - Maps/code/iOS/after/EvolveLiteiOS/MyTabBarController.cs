using System;
using MonoTouch.UIKit;

namespace EvolveLite
{
	public class MyTabBarController : UITabBarController 
	{
		public MyTabBarController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var vc1 = new AboutViewController();
			var vc2 = new SessionsViewController();
			var vc3 = new SpeakersViewController();
			var vc4 = new EvolveMapViewController();

			// MORE view controllers, to demonstrate the [More...] tab
			var vc5 = new FavoritesViewController();
			var vc6 = new TwitterViewController();

			var vcs = new UIViewController[] {vc1, vc2, vc3, vc4, vc5, vc6};
			ViewControllers = vcs;


			vc1.TabBarItem = new UITabBarItem ("About", UIImage.FromBundle("images/tababout"), 0);
			vc2.TabBarItem = new UITabBarItem ("Sessions", UIImage.FromBundle("images/tabsession"), 1);
			vc3.TabBarItem = new UITabBarItem ("Speakers", UIImage.FromBundle("images/tabspeaker"), 2);

			vc4.TabBarItem = new UITabBarItem ("Map", null, 3);
			vc5.TabBarItem = new UITabBarItem (UITabBarSystemItem.Favorites, 4);
			vc6.TabBarItem = new UITabBarItem ("Twitter", null, 5);

			SelectedIndex = 3;

			vc2.TabBarItem.BadgeValue = "4";
		}
	}
}

