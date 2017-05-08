using System;
using MonoTouch.UIKit;
using System.Drawing;

using SatelliteMenu;

namespace SatelliteMenuSample
{
	public class MainViewController : UIViewController
	{
		const int BUTTON_SIZE = 44;
		const int MARGIN = 10;
	
		public MainViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.Add (CreateMenuButton ());
			View.BackgroundColor = UIColor.Gray;
		}

		SatelliteMenuButton CreateMenuButton ()
		{
			int tag;
			var frame = new RectangleF (MARGIN, View.Frame.Height - MARGIN - BUTTON_SIZE, BUTTON_SIZE, BUTTON_SIZE);
			var menu = new SatelliteMenuButton (View, UIImage.FromFile ("Img/menu.png"), new [] { 
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon1.png"), tag = 0, "Search"), 
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon2.png"), ++tag, "Tag"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon3.png"), ++tag, "Upload"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon4.png"), ++tag, "Locate"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon5.png"), ++tag, "Magic"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("Img/icon6.png"), ++tag, "Refresh")
			}, frame);

			menu.MenuItemClick += (_, args) => {
				new UIAlertView ("", "Selected item: " + args.MenuItem.Name, null, "OK", null).Show ();
			};

			return menu;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation orientation)
		{
			// Return true for supported orientations
			return orientation == UIInterfaceOrientation.Portrait;
		}
	}
}

