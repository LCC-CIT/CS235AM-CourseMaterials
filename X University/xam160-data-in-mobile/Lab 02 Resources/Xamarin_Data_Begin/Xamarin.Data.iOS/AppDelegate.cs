using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Xamarin.Data.Core.Orm;

namespace Xamarin.Data.iOS {

	public class Application {
		public static void Main (string[] args)
		{
			try {
				UIApplication.Main (args, null, "AppDelegate");
			} catch (Exception e) {
				Console.WriteLine (e.ToString ());
			}
		}
	}

	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public static ConferenceDatabaseAsync DatabaseAsync { get { return databaseInstanceAsync; } }
		static readonly ConferenceDatabaseAsync databaseInstanceAsync = new ConferenceDatabaseAsync (ConferenceDatabaseAsync.DatabaseFilePath);

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();
			window.RootViewController = new MyTabBarController ();

			return true;
		}
	}
}