using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite
{
	public class FavoritesViewController : UITableViewController
	{
		List<Session> model;
		
		public FavoritesViewController ()
		{
			model = new List<Session>{
				new Session{Title = "Keynote 1"}, new Session{Title = "Keynote 2"},
				new Session{Title = "Keynote 3"}, new Session{Title = "Keynote 4"}
			};
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "Favorites";
			
			NavigationItem.RightBarButtonItem = this.EditButtonItem;
			TableView.Source = new FavoritesTableViewSource (model);
		}
		
		class FavoritesTableViewSource : UITableViewSource
		{
			static readonly string favoriteSessionCellId = "FavoriteSessionCell";

			List<Session> sessions;
			
			public FavoritesTableViewSource (List<Session> sessions)
			{
				this.sessions = sessions;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return sessions.Count;
			}
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (favoriteSessionCellId);
				
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Default, favoriteSessionCellId);



				cell.TextLabel.Text = sessions [indexPath.Row].Title;
				return cell;
			}

			//Adjust the model when you edit the list
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					sessions.RemoveAt (indexPath.Row);
					tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Middle);
				}
				if (editingStyle == UITableViewCellEditingStyle.Insert) {
					//When we want to adjust the model when an insert has happened.

					//How??
					//tableView.InsertRows(...);
				}
			}


			//Please sir, Can I Move the Row?
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
//				Console.WriteLine ("Can I move Row: {}?", indexPath.Row);
				if (indexPath.Row == 1) {
					return false;
				}
				return true;
			}


			//This method is called by TableView
			//When the row is moved in editing mode.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{ 
				Session s = sessions [sourceIndexPath.Row];
				sessions.RemoveAt (sourceIndexPath.Row);
				sessions.Insert (destinationIndexPath.Row, s);
			}
		}
		
	}
}
