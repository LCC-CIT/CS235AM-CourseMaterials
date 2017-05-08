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
		// a list of speakers
		List<Speaker> speakers;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// populate our speaker data
			this.PopulateSpeakerData ();

			// create a new table source and assign it to our table
//			TableView.Source = new SimpleSpeakersTableSource (this.speakers);
			TableView.Source = new SpeakersTableSource ( this.speakers );
		}

		public class SimpleSpeakersTableSource : UITableViewSource
		{		
			static readonly string speakerCellId = "SpeakerCell";
			List<Speaker> speakers;

			public SimpleSpeakersTableSource (List<Speaker> speakers)
			{
				this.speakers = speakers;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return speakers.Count;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				new UIAlertView ("Speaker Selected", speakers [indexPath.Row].Name, null, "OK", null).Show ();
				
				tableView.DeselectRow (indexPath, true);
			}
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				// request a recycled cell to save memory and performance
				UITableViewCell cell = tableView.DequeueReusableCell (speakerCellId);
				
				// if there isn't one, create a new one
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Default, speakerCellId);
				
				cell.TextLabel.Text = speakers [indexPath.Row].Name;
				
				return cell;
			}
			
			public void UpdateSpeakers (List<Speaker> speakers)
			{
				this.speakers = speakers;
			}
			
		}

		public class SpeakersTableSource : UITableViewSource
		{	
			// list of speakers
			protected List<Speaker> speakers;
			// speakers grouped by their first letter
			protected IGrouping<char,Speaker>[] speakersGrouped;
			// an index of all first letters
			protected string[] speakerIndices;

			// cell identifier (used for cell reuse)
			static readonly string speakerCellId = "SpeakerCell";
			
			public SpeakersTableSource (List<Speaker> speakers)
			{
				this.speakers = speakers;

				// creates a grouping of speakers based on their first letter
				this.speakersGrouped = (from s in speakers 
				                        orderby s.Name ascending
				                        group s by s.Name [0] into g 
				                        select g).ToArray ();
				
				// creates a string array of unique first letters from speaker names
				this.speakerIndices = (from s in speakers 
				                       orderby s.Name ascending
				                       group s by s.Name [0] into g 
				                       select g.Key.ToString ()).ToArray ();
			}

			// called when the user selected the row
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				// get a reference to the section group and the speaker
				var speakerGroup = this.speakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);

				// show an alert with the speaker name
				new UIAlertView ("Speaker Selected", speaker.Name, null, "OK", null).Show ();

				// unselect the row
				tableView.DeselectRow (indexPath, true);
			}

			// called to create the actual cells
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				// try to grab a cell object that can be reused
				var cell = tableView.DequeueReusableCell (speakerCellId);

				// if there wasn't one to grab, create a new one
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, speakerCellId);

				// get a reference to the speaker for this section and row
				var speakerGroup = this.speakersGrouped [indexPath.Section];
				var speaker = speakerGroup.ElementAt (indexPath.Row);

				// configure our cell
				cell.TextLabel.Text = speaker.Name;
				cell.DetailTextLabel.Text = speaker.Company;
				cell.ImageView.Image = speaker.Image;


				// return the configured cell
				return cell;
			}

			// Index methods:

			// how many sections are there?
			public override int NumberOfSections (UITableView tableView)
			{
				return this.speakerIndices.Count ();
			}

			// how many rows are in the passed section?
			public override int RowsInSection (UITableView tableview, int section)
			{
				return this.speakersGrouped [section].Count ();
			}

			// what are the what are the index titles?
			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return this.speakerIndices;
			}

		}

		// helper method to populate our speaker data
		protected void PopulateSpeakerData()
		{
			this.speakers = new List<Speaker> () {
				new Speaker { Name = "John Zablocki", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/john_zablocki.jpg") },
				new Speaker { Name = "Miguel de Icaza", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/miguel.jpg") },
				new Speaker { Name = "Aaron Bockover", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/aaron.jpg") },
				new Speaker { Name = "John Bubriski", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/john_bubriski.jpg") },
				new Speaker { Name = "Paul Betts", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/paul.jpg") },
				new Speaker { Name = "Louis DeJardin", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/louis.jpg") },
				new Speaker { Name = "Scott Olson", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/scott.jpg") },
				new Speaker { Name = "Igor Moochnick", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/igor.jpg") },
				new Speaker { Name = "Jérémie Laval", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/jeremie.jpg") },
				new Speaker { Name = "Ananth Balasubramaniam", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/ananth.jpg") },
				new Speaker { Name = "Bob Familiar", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/bob.jpg") },
				new Speaker { Name = "Michael Hutchinson", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/michael.jpg") },
				new Speaker { Name = "Jonathan Chambers", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/jonathan.jpg") },
				new Speaker { Name = "Steve Millar", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/steve.jpg") },
				new Speaker { Name = "Somya Jain", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/somya.jpg") },
				new Speaker { Name = "Sam Lippert", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/sam.jpg") },
				new Speaker { Name = "Don Syme", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/don.jpg") },
				new Speaker { Name = "Dean Ellis", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/dean.jpg") },
				new Speaker { Name = "Jb Evain", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/jb.jpg") },
				new Speaker { Name = "Chris Hardy", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/chris.jpg") },
				new Speaker { Name = "Demis Bellot", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/demis.jpg") },
				new Speaker { Name = "Frank Krueger", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/frank.jpg") },
				new Speaker { Name = "Greg Shackles", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/greg.jpg") },
				new Speaker { Name = "Phil Haack", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/phil.jpg") },
				new Speaker { Name = "David Fowler", Company = "Xamarin", Image = UIImage.FromFile ("images/speakers/david.jpg") },
			};  		

		}

	}
}