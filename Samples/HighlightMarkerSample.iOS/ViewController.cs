using System;
using System.Collections.Generic;

using UIKit;

namespace HighlightMarkerSample.iOS
{
    public partial class ViewController : UIViewController
    {
        private readonly List<ListItem> dataList;

        public ViewController(IntPtr handle)
            : base(handle)
        {
            this.Title = "ChoreBoard";

            this.dataList = new List<ListItem>
            {
                new ListItem { Title = "Groceries", Subtitle = "Buy bread, cheese, apples"},
                new ListItem { Title = "Devices", Subtitle = "Buy Nexus, Galaxy, Droid"},
                new ListItem { Title = "Toys", Subtitle = "Buy Lego"}
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // bind every time, to reflect deletion in the Detail view
            this.TableView.Source = new RootTableSource(this.dataList.ToArray());
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            this.SearchBar.Placeholder = "Enter Search Text";
            this.SearchBar.SizeToFit();
            this.SearchBar.AutocorrectionType = UITextAutocorrectionType.No;
            this.SearchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
            this.SearchBar.EnablesReturnKeyAutomatically = true;
            this.SearchBar.TextChanged += (sender, e) =>
                {
                    this.Search();
                };
        }

        private void Search()
        {
            ((RootTableSource)this.TableView.Source).SearchText = this.SearchBar.Text;
            this.TableView.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}