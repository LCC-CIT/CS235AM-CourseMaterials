using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite {

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

		// TODO: DemoiOS1: Singleton reference to the database manager class
//		public static ConferenceDatabase Database { get { return databaseInstance; } }
//		static readonly ConferenceDatabase databaseInstance = new ConferenceDatabase (ConferenceDatabase.DatabaseFilePath);

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();
			window.RootViewController = new MyTabBarController ();

			return true;
		}
	}
}