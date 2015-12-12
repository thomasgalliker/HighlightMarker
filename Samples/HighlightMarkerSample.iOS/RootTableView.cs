using Foundation;
using System;
using UIKit;

namespace HighlightMarkerSample.iOS
{
    public class RootTableSource : UITableViewSource
    {
        private readonly ListItem[] tableItems;
        private readonly NSString cellIdentifier = new NSString("taskcell");

        public RootTableSource(ListItem[] items)
        {
            this.tableItems = items;
        }

        public string SearchText { get; set; }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this.tableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.cellIdentifier) as CustomTableViewCell;
            if (cell == null)
            {
                cell = new CustomTableViewCell(this.cellIdentifier);
            }

            cell.UpdateCell(this.tableItems[indexPath.Row].Title, this.tableItems[indexPath.Row].Subtitle, null, this.SearchText);
            return cell;
        }
    }
}