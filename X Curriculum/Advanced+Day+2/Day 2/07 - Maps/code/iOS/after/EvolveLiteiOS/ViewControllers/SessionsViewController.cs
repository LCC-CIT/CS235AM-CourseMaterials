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
		List<Session> sessions;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Sanity Initializer.
//			this.TableView = new UITableView ();
			
			// TableViewControllers already have a TableView as their view
			// let's initialize it as a "grouped" style table
			this.TableView = new UITableView (Rectangle.Empty, UITableViewStyle.Grouped);

			// populate our sessions
			this.PopulateSessionData ();

			// create a new table source and assign it to our table
//			this.TableView.Source = new SimpleSessionsTableSource (this.sessions);
			this.TableView.Source = new SessionsTableSource (this.sessions);
		}

		class SimpleSessionsTableSource : UITableViewSource
		{		
			static readonly string sessionCellId = "SessionCell";
			List<Session> sessions;
			
			public SimpleSessionsTableSource (List<Session> sessions)
			{
				this.sessions = sessions;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return sessions.Count;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				new UIAlertView ("Session Selected", sessions [indexPath.Row].Title, null, "OK", null).Show ();
				
				tableView.DeselectRow (indexPath, true);
			}
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (sessionCellId);
				
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, sessionCellId);
				
				cell.TextLabel.Text = sessions [indexPath.Row].Title;
				cell.DetailTextLabel.Text = String.Format ("{0}; {1}", sessions[indexPath.Row].Begins.ToString (), sessions [indexPath.Row].Location ?? "");
				
				return cell;
			}
		}
		
		public class SessionsTableSource : UITableViewSource
		{	
			// list of sessions
			List<Session> sessions;
			// sessions by group
			IGrouping<int,Session>[] sessionsGrouped;
			// cell identifier
			static readonly string sessionCellId = "SessionCell";

			public SessionsTableSource ( List<Session> sessions )
			{
				this.sessions = sessions;

				// group our sessions by date with a little LINQ magic
				this.sessionsGrouped = (from s in sessions
					group s by s.Begins.Day into g
					select g).ToArray ();
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return this.sessionsGrouped[section].Count ();
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return this.sessionsGrouped.Count ();
			}

			public override string TitleForHeader (UITableView tableView, int section)
			{
				return this.sessionsGrouped [section].ElementAt (0).Begins.Date.ToShortDateString ();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var sessionGroup = this.sessionsGrouped [indexPath.Section];
				var session = sessionGroup.ElementAt (indexPath.Row);

				new UIAlertView ("Session Selected", session.Title, null, "OK", null).Show ();

				tableView.DeselectRow (indexPath, true);
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (SessionCell) tableView.DequeueReusableCell (sessionCellId);
			
				var sessionGroup = this.sessionsGrouped [indexPath.Section];
				var session = sessionGroup.ElementAt (indexPath.Row);

				if (cell == null)
					cell = new SessionCell (sessionCellId);	

				cell.Session = session;
			
				return cell;
			}
		}
		
		// helper method to populate our session data
		protected void PopulateSessionData()
		{
			this.sessions = new List<Session> () {
				new Session { Title = "Simple, Fast, Elastic NoSQL with Couchbase Server and Mono", Begins = DateTime.Parse("10/17/2012 7:15:00 AM"), Ends = DateTime.Parse("10/17/2012 8:15:00 AM"), Location = "Sampson" },
				new Session { Title = "Cross Platform Mobile NoSQL with TouchDB-Mono", Begins = DateTime.Parse("10/18/2012 6:00:00 AM"), Ends = DateTime.Parse("10/18/2012 7:00:00 AM"), Location = "Mann" },
				new Session { Title = "What is new with Mono", Begins = DateTime.Parse("10/17/2012 6:00:00 AM"), Ends = DateTime.Parse("10/17/2012 7:00:00 AM"), Location = "Mann" },
				new Session { Title = "Vernacular: modern localization for iOS, Android, Windows Phone, and more", Begins = DateTime.Parse("10/17/2012 12:00:00 PM"), Ends = DateTime.Parse("10/17/2012 1:00:00 PM"), Location = "Mann" },
				new Session { Title = "Alternatives to Entity Framework: Build a Massively Dapper but Simple Data layer", Begins = DateTime.Parse("10/18/2012 8:30:00 AM"), Ends = DateTime.Parse("10/18/2012 9:30:00 AM"), Location = "Adams" },
				new Session { Title = "A Call To Arms - The Future of Async IO in .NET Open Source", Begins = DateTime.Parse("10/18/2012 10:45:00 AM"), Ends = DateTime.Parse("10/18/2012 11:45:00 AM"), Location = "Sampson" },
				new Session { Title = "OWIN - Run your C# for the web anywhere", Begins = DateTime.Parse("10/18/2012 12:00:00 PM"), Ends = DateTime.Parse("10/18/2012 1:00:00 PM"), Location = "Sampson" },
				new Session { Title = "Using View Abstraction and Mixed Views with MonoCross", Begins = DateTime.Parse("10/18/2012 7:15:00 AM"), Ends = DateTime.Parse("10/18/2012 8:15:00 AM"), Location = "Sampson" },
				new Session { Title = "Facelift your JavaScript with TypeScript. The Big Bang theory.", Begins = DateTime.Parse("10/19/2012 6:00:00 AM"), Ends = DateTime.Parse("10/19/2012 7:00:00 AM"), Location = "Sampson" },
				new Session { Title = "Xwt, a cross-desktop UI library", Begins = DateTime.Parse("10/18/2012 8:30:00 AM"), Ends = DateTime.Parse("10/18/2012 9:30:00 AM"), Location = "Sampson" },
				new Session { Title = "Graffiti: A different approach to cross-platform game development", Begins = DateTime.Parse("10/17/2012 12:00:00 PM"), Ends = DateTime.Parse("10/17/2012 1:00:00 PM"), Location = "Sampson" },
				new Session { Title = "Windows 8, XNA and MonoGame – How to get your game into the Windows 8 Store", Begins = DateTime.Parse("10/19/2012 7:15:00 AM"), Ends = DateTime.Parse("10/19/2012 8:15:00 AM"), Location = "Mann" },
				new Session { Title = "Effective MonoDevelop: Tips, Tricks and Workflows", Begins = DateTime.Parse("10/18/2012 10:45:00 AM"), Ends = DateTime.Parse("10/18/2012 11:45:00 AM"), Location = "Mann" },
				new Session { Title = "Embedding the Mono Runtime", Begins = DateTime.Parse("10/18/2012 6:00:00 AM"), Ends = DateTime.Parse("10/18/2012 7:00:00 AM"), Location = "Sampson" },
				new Session { Title = "A Cross-Platform Sensor Framework Based on .NET using the Mono and Microsoft Development Stacks", Begins = DateTime.Parse("10/17/2012 8:30:00 AM"), Ends = DateTime.Parse("10/17/2012 9:30:00 AM"), Location = "Adams" },
				new Session { Title = "Going Mobile", Begins = DateTime.Parse("10/17/2012 7:15:00 AM"), Ends = DateTime.Parse("10/17/2012 8:15:00 AM"), Location = "Mann" },
				new Session { Title = "Sharing iOS and Android Views in MonoCross", Begins = DateTime.Parse("10/19/2012 7:15:00 AM"), Ends = DateTime.Parse("10/19/2012 8:15:00 AM"), Location = "Sampson" },
				new Session { Title = "F#, Type Providers and the Future of Data-Intensive Programming", Begins = DateTime.Parse("10/18/2012 12:00:00 PM"), Ends = DateTime.Parse("10/18/2012 1:00:00 PM"), Location = "Mann" },
				new Session { Title = "MonoGame - Where we are and where we're going", Begins = DateTime.Parse("10/17/2012 8:30:00 AM"), Ends = DateTime.Parse("10/17/2012 9:30:00 AM"), Location = "Mann" },
				new Session { Title = "The Epic Battle of the Managed Assembly Readers/Writers", Begins = DateTime.Parse("10/17/2012 12:00:00 PM"), Ends = DateTime.Parse("10/17/2012 1:00:00 PM"), Location = "Adams" },
				new Session { Title = "A head start with MonoTouch 6", Begins = DateTime.Parse("10/17/2012 10:45:00 AM"), Ends = DateTime.Parse("10/17/2012 11:45:00 AM"), Location = "Mann" },
				new Session { Title = "Expanding ServiceStack's reach with Mono", Begins = DateTime.Parse("10/17/2012 8:30:00 AM"), Ends = DateTime.Parse("10/17/2012 9:30:00 AM"), Location = "Sampson" },
				new Session { Title = "A Return to Rapid Application Development", Begins = DateTime.Parse("10/19/2012 6:00:00 AM"), Ends = DateTime.Parse("10/19/2012 7:00:00 AM"), Location = "Mann" },
				new Session { Title = "Cross-Platform Mobile Development with C#", Begins = DateTime.Parse("10/18/2012 8:30:00 AM"), Ends = DateTime.Parse("10/18/2012 9:30:00 AM"), Location = "Mann" },
				new Session { Title = "Jazz up your open source project with GitHub", Begins = DateTime.Parse("10/17/2012 10:45:00 AM"), Ends = DateTime.Parse("10/17/2012 11:45:00 AM"), Location = "Sampson" },
				new Session { Title = "Registration", Begins = DateTime.Parse("10/17/2012 5:00:00 AM"), Ends = DateTime.Parse("10/17/2012 6:00:00 AM"), Location = "Main Hall" },
				new Session { Title = "Open Space Proposals", Begins = DateTime.Parse("10/18/2012 1:15:00 PM"), Ends = DateTime.Parse("10/18/2012 2:15:00 PM"), Location = "Mann" },
				new Session { Title = "Attendee Party", Begins = DateTime.Parse("10/18/2012 4:00:00 PM"), Ends = DateTime.Parse("10/18/2012 8:59:00 PM"), Location = "The Meadhall" },
				new Session { Title = "Open Space", Begins = DateTime.Parse("10/19/2012 10:45:00 AM"), Ends = DateTime.Parse("10/19/2012 2:15:00 PM"), Location = "All Rooms" },
				new Session { Title = "DNR Road Trip", Begins = DateTime.Parse("10/17/2012 4:30:00 PM"), Ends = DateTime.Parse("10/17/2012 7:00:00 PM"), Location = "Mann" },
				new Session { Title = "SignalR: Websockets, Magic & Awesome, in Real-Time", Begins = DateTime.Parse("10/18/2012 7:15:00 AM"), Ends = DateTime.Parse("10/18/2012 8:15:00 AM"), Location = "Mann" },
			};			
		}
	}
}