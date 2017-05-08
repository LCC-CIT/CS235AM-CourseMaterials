using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class CallHistoryController : UITableViewController
	{
		public List<String> PhoneNumbers { get; set; }

		static NSString callHistoryCellId = new NSString ("CallHistoryCell");

		public CallHistoryController (IntPtr handle) : base (handle)
		{
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), callHistoryCellId);
			TableView.Source = new CallHistoryDataSource (this);
			PhoneNumbers = new List<string>();
		}

		class CallHistoryDataSource : UITableViewSource
		{
			CallHistoryController controller;
			CallAlertDelegate alertDelegate;

			public CallHistoryDataSource (CallHistoryController controller)
			{
				this.controller = controller;
				alertDelegate = new CallAlertDelegate ();
			}

			public override int RowsInSection (UITableView tableView, int section)
			{
				return controller.PhoneNumbers.Count;
			}
		
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CallHistoryController.callHistoryCellId);
				
				int row = indexPath.Row;

				cell.TextLabel.Text = controller.PhoneNumbers [row];
			
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, false);

				alertDelegate.PhoneNumber = controller.PhoneNumbers [indexPath.Row];

				var alert = new UIAlertView ("Dial Number"
				                         	 , "Would you like to call this number?"
				                         	 , alertDelegate
				                         	 , "No"
				                         	 , "Yes");

				alert.Show ();
			}

			class CallAlertDelegate : UIAlertViewDelegate
			{
				public string PhoneNumber { get; set; }

				public override void Clicked (UIAlertView alertview, int buttonIndex)
				{
					if (buttonIndex == 1) {
						PhoneUtility.Dial (PhoneNumber);
					}
				}
			}
		}
	}
}
