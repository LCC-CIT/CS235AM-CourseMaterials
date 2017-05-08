using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace EvolveLite {
	public class SpeakersViewController : UITableViewController {
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string[] items = new string[] {"Miguel de Icaza", "Nat Friedman", "Bart Decrem", "Scott Hanselman"};
			// TODO: Demo1: implement and uncomment the UITableViewSource
//			TableView.Source = new SpeakersTableSource (items);
		}
	}
}