using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.WebServicesAsync.Rest.iOS.ViewControllers
{
	public class RestTableViewControllerSource : UITableViewSource
	{

		private const string cellKey = "ConceptPropertiesCell";

		public List<Model.ConceptProperty> ConceptProperties {get; private set;}

		public RestTableViewControllerSource (IEnumerable<Model.ConceptProperty> conceptProperties)
		{
			this.ConceptProperties = conceptProperties.ToList();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return ConceptProperties.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (cellKey) as UITableViewCell;

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellKey);

			var conceptPropety = ConceptProperties [indexPath.Row];

			// TODO: populate the cell with the appropriate data based on the indexPath
			cell.TextLabel.Text = conceptPropety.Synonym;
			cell.DetailTextLabel.Text = conceptPropety.Name;
			
			return cell;
		}
	}
}

