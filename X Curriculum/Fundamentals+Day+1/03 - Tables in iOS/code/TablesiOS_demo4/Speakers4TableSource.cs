using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite {
	public class SpeakersTableSource : UITableViewSource {
		static readonly string speakerCellId = "SpeakerCell";
		List<Speaker> data;
		
		public SpeakersTableSource (List<Speaker> speakers)
		{
			data = speakers;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return data.Count;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var speaker = data [indexPath.Row];
			
			new UIAlertView ("Speaker Selected", speaker.Name, null, "OK", null).Show ();
			
			tableView.DeselectRow (indexPath, true);
		}

		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (speakerCellId);

			//TODO: Demo4: uncomment one of the build-in UITableViewCellStyles
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, speakerCellId);
//				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, speakerCellId);
//				cell = new UITableViewCell (UITableViewCellStyle.Value1, speakerCellId);
//				cell = new UITableViewCell (UITableViewCellStyle.Value2, speakerCellId);
			}
			var speaker = data [indexPath.Row];

			//TODO: Demo4: select which built-in layout elements to set
			cell.TextLabel.Text = speaker.Name;
			cell.DetailTextLabel.Text = speaker.Company;
//			cell.ImageView.Image = UIImage.FromBundle(speaker.HeadshotUrl);

			//TODO: Demo4: OPTIONALLY show one of the Accessories
//			cell.Accessory = UITableViewCellAccessory.Checkmark;
//			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
//			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton; // allows you to wire up another 'tap' event
			
			return cell;
		}

		//TODO: Demo4: uncomment when you are using DetailDisclosureButton
//		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
//		{
//			var speaker = data.ElementAt (indexPath.Row);
//			
//			new UIAlertView ("Session Accessory Tapped", speaker.Name, null, "OK"
//			                 , null).Show ();
//		}
	}
}