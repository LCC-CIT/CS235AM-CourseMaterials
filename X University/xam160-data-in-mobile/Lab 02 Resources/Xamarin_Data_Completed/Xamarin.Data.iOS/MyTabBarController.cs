using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BigTed;
using MonoTouch.UIKit;

using Xamarin.Data.Core.Orm;
using Xamarin.Data.Core.WebServices;
using Xamarin.Data.iOS.ViewControllers;

namespace Xamarin.Data.iOS {
	public class MyTabBarController : UITabBarController {

		public MyTabBarController ()
		{
		}

		private SessionsViewController sessionsViewController;

		/// <summary>
		/// This is the RootViewController for the app
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			BTProgressHUD.ShowContinuousProgress("Updating", ProgressHUD.MaskType.Gradient);
		
			// Now set-up the tabs
            sessionsViewController = new SessionsViewController();

			var aboutViewController = new AboutViewController();
		 

			var vcs = new UIViewController[] {sessionsViewController, aboutViewController};
			ViewControllers = vcs;


			sessionsViewController.TabBarItem = new UITabBarItem ("Sessions", UIImage.FromBundle("images/tabsession"), 0);
			aboutViewController.TabBarItem = new UITabBarItem ("About", UIImage.FromBundle("images/tababout"), 1);

			SelectedIndex = 0;

			LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			await Downloader.DownloadSessionXmlAsync();
			// At this point, the files are downloaded & saved locally

			var sessions = SessionsXmlParser.Instance.Sessions;
			// At this point, the files are parsed into these two local vars

            // TODO: Step 10 - iOS - Delete and save new data
			await AppDelegate.DatabaseAsync.DeleteSessionsAsync ();
			await AppDelegate.DatabaseAsync.SaveSessionsAsync (sessions);

			// At this point, the data is in the database, ready to be queried!

            // TODO: Step 11 - iOS - Load from database and display
			await sessionsViewController.RefreshAsync();

            // Hide the 'loading' message
			BTProgressHUD.Dismiss();
		}
	}
}