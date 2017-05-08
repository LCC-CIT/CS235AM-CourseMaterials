using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.Linq;

namespace Xamarin.WebServicesAsync.Soap.iOS.ViewControllers
{
	public class SoapTableViewControllerController : UITableViewController
	{

		private ViewControllers.SoapTableViewControllerSource tableViewSource;

		public SoapTableViewControllerController () : base (UITableViewStyle.Grouped)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "SOAP";

			RefreshControl = new UIRefreshControl ();
			RefreshControl.ValueChanged += async (sender, e) => {
				await ReloadDataAsync();
				RefreshControl.EndRefreshing();
			};
			
			// Register the TableView's data source
			tableViewSource = new SoapTableViewControllerSource (Enumerable.Empty<Model.ConceptProperty>());
			this.TableView.Source = tableViewSource;

			ReloadDataAsync ();
		}

		public async Task ReloadDataAsync(){

			tableViewSource.ConceptProperties.Clear ();

			TableView.ReloadData ();

			var soapClient = new Client.SoapClient ();

			tableViewSource.ConceptProperties.AddRange (await soapClient.GetDataAsync ());

			TableView.ReloadData ();
		}
	}
}

