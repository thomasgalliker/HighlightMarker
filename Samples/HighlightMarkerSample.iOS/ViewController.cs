using System;

using Foundation;

using HighlightMarkerSample.Data.Model;

using ObservableView;

using UIKit;

namespace HighlightMarkerSample.iOS
{
    public partial class ViewController : UIViewController
    {
        private readonly ObservableViewTableViewController<Mall> ListController;

        public ViewController(IntPtr handle)
            : base(handle)
        {
            this.Title = "Malls";

            var malls = MallManager.GetMalls();
            this.ListController = new ObservableViewTableViewController<Mall>();
            this.ListController.DataSource = new ObservableView<Mall>(malls);
            this.ListController.DataSource.AddSearchSpecification(x => x.Title);
            this.ListController.DataSource.AddSearchSpecification(x => x.Subtitle);
            this.ListController.CreateCellDelegate = this.CreateCell;
            this.ListController.BindCellDelegate = this.BindCell;

            this.ListController.TableView = this.ListController.TableView; // BUG: TableView is not created
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.TableView.Source = this.ListController.TableSource;
            this.Add(this.TableView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.SearchBar.Placeholder = "Enter Search Text";
            this.SearchBar.SizeToFit();
            this.SearchBar.AutocorrectionType = UITextAutocorrectionType.No;
            this.SearchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
            this.SearchBar.EnablesReturnKeyAutomatically = true;
            this.SearchBar.TextChanged += (sender, e) => { this.Search(); };
        }

        private void Search()
        {
            this.ListController.DataSource.Search(this.SearchBar.Text);
            this.TableView.ReloadData();
        }

        private UITableViewCell CreateCell(NSString reuseId)
        {
            return new CustomTableViewCell(reuseId);
        }

        private void BindCell(UITableViewCell cell, Mall mall, NSIndexPath path)
        {
            var customTableViewCell = cell as CustomTableViewCell;
            customTableViewCell.UpdateCell(mall.Title, mall.Subtitle, this.SearchBar.Text);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}