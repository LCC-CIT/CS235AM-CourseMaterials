using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite
{
	public class SessionsViewController : UITableViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			TableView = new UITableView (Rectangle.Empty, UITableViewStyle.Grouped);
			TableView.Source = new SessionsTableSource ();
		}

		class SessionsTableSource : UITableViewSource
		{		
			static readonly string sessionCellId = "SessionCell";

			public override int RowsInSection (UITableView tableview, int section)
			{
				return SessionsXmlParser.Instance.Sessions.Count ();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var session = SessionsXmlParser.Instance.Sessions.ElementAt (indexPath.Row);

				new UIAlertView ("Session Selected", session.Title, null, "OK", null).Show ();

				tableView.DeselectRow (indexPath, true);
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (SessionCell) tableView.DequeueReusableCell (sessionCellId);

				var session = SessionsXmlParser.Instance.Sessions.ElementAt (indexPath.Row);
				if (cell == null)
					cell = new SessionCell (sessionCellId);	

				cell.Session = session;
			
				return cell;
			}
		}
	}
}