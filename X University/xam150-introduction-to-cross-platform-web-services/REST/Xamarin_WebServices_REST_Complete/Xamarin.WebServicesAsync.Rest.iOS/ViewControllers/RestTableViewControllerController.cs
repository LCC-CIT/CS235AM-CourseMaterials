using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.Linq;

namespace Xamarin.WebServicesAsync.Rest.iOS.ViewControllers
{
	public class RestTableViewControllerController : UITableViewController
	{

		private ViewControllers.RestTableViewControllerSource tableViewSource;

		public RestTableViewControllerController () : base (UITableViewStyle.Grouped)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "REST";

			// Register the TableView's data source
			tableViewSource = new RestTableViewControllerSource (Enumerable.Empty<Model.ConceptProperty>());
			this.TableView.Source = tableViewSource;

			RefreshControl = new UIRefreshControl ();
			RefreshControl.ValueChanged += async (sender, e) => {
				await ReloadDataAsync();
				RefreshControl.EndRefreshing();
			};

			ReloadDataAsync ();
		}

		public async Task ReloadDataAsync(){

			tableViewSource.ConceptProperties.Clear ();

			TableView.ReloadData ();

			var soapClient = new Client.RestClient ();

			tableViewSource.ConceptProperties.AddRange (await soapClient.GetDataAsync ());

			TableView.ReloadData ();
		}
	}
}

