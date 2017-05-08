using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		Phoneword_iOSViewController viewController;
		UINavigationController navigationController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			viewController = new Phoneword_iOSViewController ();

			//TODO: Step 1a: uncomment to create a Navigation Controller to be the new root.

//			navigationController = new UINavigationController (viewController);

			//TODO: Step 1b: delete or comment out the old Root View Controller assignment.

			window.RootViewController = viewController;

			//TODO: Step 1c: uncomment to make the Navigation Controller the new Window root.

//			// This gives us a new starting point for the app, similar to moving the
//			// sourceless segue in a storyboard app to another Controller.
//			window.RootViewController = navigationController;

			window.MakeKeyAndVisible ();

			return true;
		}
	}
}