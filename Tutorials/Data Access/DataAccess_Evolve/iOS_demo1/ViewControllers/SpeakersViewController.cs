using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite {
	/// <summary>
	/// Speaker list with index
	/// </summary>
	public class SpeakersViewController : UITableViewController {
		List<Speaker> speakers;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			Refresh ();
		}
		public void Refresh () {
			//TODO: DemoiOS1 : 
//			speakers = AppDelegate.Database.GetSpeakers ();	
//			TableView.Source = new SpeakersTableSource (speakers);
			TableView.ReloadData ();
		}

		class SpeakersTableSource : UITableViewSource
		{		
			static readonly string speakerCellId = "SpeakerCell";

			List<Speaker> speakers;

			public SpeakersTableSource (List<Speaker> speakers)
			{
				this.speakers = speakers;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return SpeakerIndicies ().Count ();
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return SpeakersGrouped [section].Count ();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var speakerGroup = SpeakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);
				
				new UIAlertView ("Speaker Selected", speaker.Name, null, "OK", null).Show ();
				
				tableView.DeselectRow (indexPath, true);
			}

			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return SpeakerIndicies ();
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (speakerCellId);

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, speakerCellId);

				var speakerGroup = SpeakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);
				
				cell.TextLabel.Text = speaker.Name;
				cell.DetailTextLabel.Text = speaker.Company;
//				cell.ImageView.Image = speaker.Image;
				
				return cell;
			}


			#region Grouping and Indicies for UITableView Index

			IGrouping<char, Speaker>[] speakersGrouped;

			public IGrouping<char, Speaker>[] SpeakersGrouped
			{
				get {
					if (speakersGrouped == null) {
						speakersGrouped = GetSpeakersGrouped();
					}
					return speakersGrouped;
				}
			}
			
			public string[] SpeakerIndicies()
			{
				var indicies = (from s in speakers
				                orderby s.Name ascending
				                group s by s.Name[0]
				                into g
				                select g.Key.ToString()).ToArray();
				
				return indicies;
			}
			
			IGrouping<char, Speaker>[] GetSpeakersGrouped()
			{
				var speakersGrouped = (from s in speakers
				                       orderby s.Name ascending
				                       group s by s.Name[0]
				                       into g
				                       select g).ToArray();
				
				return speakersGrouped;
			}
			#endregion
		}
	}
}