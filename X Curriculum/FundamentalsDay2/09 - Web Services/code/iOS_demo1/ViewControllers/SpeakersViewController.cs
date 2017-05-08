using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite
{
	public class SpeakersViewController : UITableViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.Source = new SpeakersTableSource ();
		}

		class SpeakersTableSource : UITableViewSource
		{		
			static readonly string speakerCellId = "SpeakerCell";
			
			public SpeakersTableSource ()
			{
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return SpeakersJsonParser.Instance.SpeakerIndicies ().Count ();
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return SpeakersJsonParser.Instance.SpeakersGrouped [section].Count ();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var speakerGroup = SpeakersJsonParser.Instance.SpeakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);
				
				new UIAlertView ("Speaker Selected", speaker.Name, null, "OK", null).Show ();
				
				tableView.DeselectRow (indexPath, true);
			}

			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return SpeakersJsonParser.Instance.SpeakerIndicies ();
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (speakerCellId);

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, speakerCellId);

				var speakerGroup = SpeakersJsonParser.Instance.SpeakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);
				
				cell.TextLabel.Text = speaker.Name;
				cell.DetailTextLabel.Text = speaker.Company;
//				cell.ImageView.Image = speaker.Image;
				
				return cell;
			}
		}
	}
}