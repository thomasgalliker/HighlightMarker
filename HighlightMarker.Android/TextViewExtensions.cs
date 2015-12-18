using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Widget;

namespace HighlightMarker
{
    public static class TextViewExtensions
    {
        public static void HighlightText(this TextView textView, string searchText, Color foregroundColor, Color? backgroundColor = null)
        {
            var highlightMarker = new HighlightMarker(textView.Text, searchText);
            var spannableStringBuilder = new SpannableStringBuilder(textView.Text);

            foreach (var current in highlightMarker)
            {
                int fromIndex = current.FromIndex;
                int endIndex = fromIndex + current.Length;
                bool isHighlighted = current.IsHighlighted;

                if (isHighlighted)
                {
                    spannableStringBuilder.SetSpan(new ForegroundColorSpan(foregroundColor), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);

                    if (backgroundColor.HasValue)
                    {
                        spannableStringBuilder.SetSpan(new BackgroundColorSpan(backgroundColor.Value), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);
                    }
                }
            }

            textView.TextFormatted = spannableStringBuilder;
        }
    }
}