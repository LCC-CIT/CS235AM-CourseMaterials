using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Xamarin.Data.Core.Model;
using Xamarin.Data.iOS.UserInterface;

namespace Xamarin.Data.iOS.ViewControllers
{
	public class SessionsViewController : UITableViewController
	{
		List<Session> sessions;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// set the table style to Grouped (otherwise Default is by default)
			TableView = new UITableView (Rectangle.Empty, UITableViewStyle.Grouped);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			RefreshAsync ();
		}

		public async Task RefreshAsync() {
			// TODO: Step 11a - iOS - Load from database and display
			sessions = await AppDelegate.DatabaseAsync.GetSessionsAsync ();

			TableView.Source = new SessionsTableSource (sessions);
			TableView.ReloadData ();
		}

		class SessionsTableSource : UITableViewSource
		{
			private const string sessionCellId = "SessionCell";

			readonly List<Session> sessions;

			public SessionsTableSource (List<Session> sessions) 
			{
				this.sessions = sessions;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				//return SessionsXmlParser.Instance.Sessions.Count ();
				return sessions.Count();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var session = sessions[indexPath.Row];//SessionsXmlParser.Instance.Sessions.ElementAt (indexPath.Row);

				new UIAlertView ("Session Selected", session.Title, null, "OK", null).Show ();

				tableView.DeselectRow (indexPath, true);
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (SessionCell) tableView.DequeueReusableCell (sessionCellId);

				var session = sessions[indexPath.Row];
				if (cell == null)
					cell = new SessionCell (sessionCellId);	

				cell.Session = session;
			
				return cell;
			}
		}
	}
}