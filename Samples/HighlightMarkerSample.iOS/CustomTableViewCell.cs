using CoreGraphics;
using Foundation;
using HighlightMarker;
using UIKit;

namespace HighlightMarkerSample.iOS
{
    public class CustomTableViewCell : UITableViewCell
    {
        private readonly UILabel headingLabel;
        private readonly UILabel subheadingLabel;
        private readonly UIImageView imageView;

        public CustomTableViewCell(NSString cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this.ContentView.BackgroundColor = UIColor.FromRGB(218, 255, 127);
            this.imageView = new UIImageView();
            this.headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Arial", 22f), 
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };
            this.subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear
            };
            this.ContentView.AddSubviews(this.headingLabel, this.subheadingLabel, this.imageView);
        }

        public void UpdateCell(string caption, string subtitle, UIImage image, string searchText)
        {
            this.imageView.Image = image;
            this.headingLabel.Text = caption;
            this.headingLabel.HighlightText(searchText, UIColor.Blue);
            this.subheadingLabel.Text = subtitle;
            this.subheadingLabel.HighlightText(searchText, UIColor.Blue);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this.imageView.Frame = new CGRect(this.ContentView.Bounds.Width - 63, 5, 33, 33);
            this.headingLabel.Frame = new CGRect(5, 4, this.ContentView.Bounds.Width - 63, 25);
            this.subheadingLabel.Frame = new CGRect(100, 18, 100, 20);
        }
    }
}