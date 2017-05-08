using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MonoTouch.UIKit;

using MBProgressHUD;

namespace EvolveLite
{
	public class MyTabBarController : UITabBarController 
	{

		MTMBProgressHUD hud;


		public MyTabBarController ()
		{
		}

		SessionsViewController vc1; 

		/// <summary>
		/// This is the RootViewController for the app
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			hud = new MTMBProgressHUD(View) 
			{
				LabelText = "Updating",
				DetailsLabelText = "Downloading info",
				RemoveFromSuperViewOnHide = true,
				DimBackground = true
			};
			View.AddSubview(hud);
			hud.Show(true);

			Task.Factory.StartNew(() => {
				Downloader.DownloadSessionXml() ;
				Downloader.DownloadSpeakerJson();
			}).ContinueWith(task1 => {
				vc1.Refresh();
				hud.Hide(true);
				hud = null;
			}, 
			TaskScheduler.FromCurrentSynchronizationContext ());
		
			vc1 = new SessionsViewController();
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