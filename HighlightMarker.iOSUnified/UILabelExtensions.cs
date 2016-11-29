#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace HighlightMarker
{
    public static class UILabelExtensions
    {
        public static void HighlightText(this UILabel textView, string searchText, UIColor foregroundColor, UIColor backgroundColor = null)
        {
            var highlightMarker = new HighlightMarker(textView.Text, searchText);

            var textAttributed = new NSMutableAttributedString(textView.Text);
            foreach (var segment in highlightMarker)
            {
                int fromIndex = segment.FromIndex;
                int length = segment.Length;
                bool isHighlighted = segment.IsHighlighted;

                if (isHighlighted)
                {
                    var colourAttribute = new UIStringAttributes
                    {
                        ForegroundColor = foregroundColor,
                        BackgroundColor = backgroundColor
                    };
                    
                    textAttributed.SetAttributes(colourAttribute, new NSRange(fromIndex, length));
                }
            }

            textView.AttributedText = textAttributed;
        }
    }
}