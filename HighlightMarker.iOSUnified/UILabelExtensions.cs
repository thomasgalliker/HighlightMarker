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
            using (var enumerator = highlightMarker.GetEnumerator())
            {
                var textAttributed = new NSMutableAttributedString(textView.Text);
                while (enumerator.MoveNext())
                {
                    int fromIndex = enumerator.Current.FromIndex;
                    int length = enumerator.Current.Length;
                    bool isHighlighted = enumerator.Current.IsHighlighted;

                    if (isHighlighted)
                    {
                        var colourAttribute = new UIStringAttributes
                        {
                            ForegroundColor = foregroundColor,
                        };
                        textAttributed.SetAttributes(colourAttribute, new NSRange(fromIndex, length));

                        ////spannableStringBuilder.SetSpan(new ForegroundColorSpan(foregroundColor), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);
                        
                        ////if (backgroundColor.HasValue)
                        ////{
                        ////    spannableStringBuilder.SetSpan(new BackgroundColorSpan(backgroundColor.Value), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);
                        ////}
                    }
                }

                textView.AttributedText = textAttributed;
            }
        }
    }
}