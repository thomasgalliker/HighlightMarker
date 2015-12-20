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

        public CustomTableViewCell(NSString cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this.ContentView.BackgroundColor = UIColor.White;
            this.headingLabel = new UILabel
            {
                Font = UIFont.FromName("AppleSDGothicNeo-Medium", 22f), 
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            this.subheadingLabel = new UILabel
            {
                Font = UIFont.FromName("AppleSDGothicNeo-Medium", 14f),
                TextColor = UIColor.DarkGray,
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear
            };
            this.ContentView.AddSubviews(this.headingLabel, this.subheadingLabel);
        }

        public void UpdateCell(string caption, string subtitle, string searchText)
        {
            this.headingLabel.Text = caption;
            this.headingLabel.HighlightText(searchText, this.TintColor);
            this.subheadingLabel.Text = subtitle;
            this.subheadingLabel.HighlightText(searchText, this.TintColor);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this.headingLabel.Frame = new CGRect(5, 0, this.ContentView.Bounds.Width - 10, 25);
            this.subheadingLabel.Frame = new CGRect(5, 22, this.ContentView.Bounds.Width - 10, 16);
        }
    }
}