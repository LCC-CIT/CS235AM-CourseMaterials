using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MonoTouch.UIKit;

namespace EvolveLite
{
	public class MyTabBarController : UITabBarController 
	{
		public MyTabBarController ()
		{
		}

		/// <summary>
		/// This is the RootViewController for the app
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// TODO: Demo1a: Download web service data - blocks UI thread
			Downloader.DownloadSessionXml() ;

			// TODO: Demo1b: Download web service data - blocks UI thread
			Downloader.DownloadSpeakerJson();

			var vc1 = new SessionsViewController();
			var vc2 = new SpeakersViewController();
			var vc3 = new AboutViewController();

			var vcs = new UIViewController[] {vc1, vc2, vc3};
			ViewControllers = vcs;


			vc1.TabBarItem = new UITabBarItem ("Sessions", UIImage.FromBundle("Images/tabsession"), 0);
			vc2.TabBarItem = new UITabBarItem ("Speakers", UIImage.FromBundle("Images/tabspeaker"), 1);
			vc3.TabBarItem = new UITabBarItem ("About", UIImage.FromBundle("Images/tababout"), 2);

			SelectedIndex = 0;
		}
	}
}