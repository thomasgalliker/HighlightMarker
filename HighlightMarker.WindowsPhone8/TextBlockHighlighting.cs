#if WINDOWS_PHONE || WPF
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
#elif WINDOWS_PHONE_APP || WINDOWS_APP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
#endif

namespace HighlightMarker
{
    public class TextBlockHighlighting : DependencyObject
    {
        public static readonly DependencyProperty FullTextProperty = DependencyProperty.RegisterAttached(
            "FullText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(string.Empty, OnTextChangedCallback));

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.RegisterAttached(
            "HighlightBrush",
            typeof(Brush),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.RegisterAttached(
            "HighlightedText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(string.Empty
#if !WINDOWS_PHONE_APP
                , OnTextChangedCallback // VERY STRANGE: Windows Phone 8.1 (w/o SL) crashes when this line is active
#endif
            ));

        public static string GetFullText(TextBlock element)
        {
            return (string)element.GetValue(FullTextProperty);
        }

        public static Brush GetHighlightBrush(TextBlock element)
        {
            return (Brush)element.GetValue(HighlightBrushProperty);
        }

        public static void SetFullText(TextBlock element, string value)
        {
            element.SetValue(FullTextProperty, value);
        }

        public static void SetHighlightBrush(TextBlock element, Brush value)
        {
            element.SetValue(HighlightBrushProperty, value);
        }

        public static string GetHighlightedText(DependencyObject d)
        {
            return (string)d.GetValue(HighlightedTextProperty);
        }

        public static void SetHighlightedText(DependencyObject d, string value)
        {
            d.SetValue(HighlightedTextProperty, value);
        }

        private static void OnTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBlock = d as TextBlock;
            if (textBlock == null)
            {
                return;
            }

            string fulltext = GetFullText(textBlock) ?? string.Empty;
            string highlightedText = GetHighlightedText(textBlock) ?? string.Empty;

            textBlock.Inlines.Clear();

            if (string.IsNullOrEmpty(fulltext) || string.IsNullOrEmpty(highlightedText))
            {
                textBlock.Inlines.Add(new Run { Text = fulltext });
                return;
            }

            var foregroundBrush = GetHighlightBrush(textBlock) ?? ColorHelper.GetDefaultHighlightBrush();

            var highlightMarker = new HighlightMarker(fulltext, highlightedText);

            foreach (var current in highlightMarker)
            {
                int fromIndex = current.FromIndex;
                int length = current.Length;
                bool isHighlighted = current.IsHighlighted;

                if (isHighlighted)
                {
                    textBlock.Inlines.Add(new Run { Text = fulltext.Substring(fromIndex, length), Foreground = foregroundBrush });
                }
                else
                {
                    textBlock.Inlines.Add(new Run { Text = fulltext.Substring(fromIndex, length) });
                }
            }
        }
    }
}