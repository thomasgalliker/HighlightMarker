#if WINDOWS_PHONE || WPF
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

#elif WINDOWS_PHONE_APP || WINDOWS_APP || WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
#endif

namespace HighlightMarker
{
    public class TextBlockHighlighting : DependencyObject
    {
        /// <summary>
        ///     FullText property is used to bind to the content which is normally bound to TextBlock.Text.
        /// </summary>
        public static readonly DependencyProperty FullTextProperty = DependencyProperty.RegisterAttached(
            "FullText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(string.Empty, OnTextChangedCallback));

        /// <summary>
        ///     The foreground color used for text highlighting.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
            "Foreground",
            typeof(Brush),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        /// <summary>
        ///     The background color used for text highlighting.
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
            "Background",
            typeof(Brush),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        /// <summary>
        ///     HighlightedText is usually bound to the highlighter source, which may be the Text property of a search box.
        /// </summary>
        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.RegisterAttached(
            "HighlightedText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(
                string.Empty
#if !WINDOWS_PHONE_APP
                ,
                OnTextChangedCallback // BUG: VERY STRANGE: Windows Phone 8.1 (w/o SL) crashes when this line is active
#endif
                ));

        public static readonly DependencyProperty HighlightProcessorProperty = DependencyProperty.RegisterAttached(
            "HighlightProcessor",
            typeof(IHighlightProcessor),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null));

        public static string GetFullText(TextBlock element)
        {
            return (string)element.GetValue(FullTextProperty);
        }

        public static void SetFullText(TextBlock element, string value)
        {
            element.SetValue(FullTextProperty, value);
        }

        public static Brush GetForeground(TextBlock element)
        {
            return (Brush)element.GetValue(ForegroundProperty);
        }

        public static void SetForeground(TextBlock element, Brush value)
        {
            element.SetValue(ForegroundProperty, value);
        }

        public static Brush GetBackground(TextBlock element)
        {
            return (Brush)element.GetValue(BackgroundProperty);
        }

        public static void SetBackground(TextBlock element, Brush value)
        {
            element.SetValue(BackgroundProperty, value);
        }

        public static string GetHighlightedText(DependencyObject d)
        {
            return (string)d.GetValue(HighlightedTextProperty);
        }

        public static void SetHighlightedText(DependencyObject d, string value)
        {
            d.SetValue(HighlightedTextProperty, value);
        }

        public static IHighlightProcessor GetHighlightProcessor(DependencyObject d)
        {
            return (IHighlightProcessor)d.GetValue(HighlightProcessorProperty);
        }

        public static void SetHighlightProcessor(DependencyObject d, IHighlightProcessor value)
        {
            d.SetValue(HighlightProcessorProperty, value);
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

            var foregroundBrush = GetForeground(textBlock) ?? ColorHelper.GetDefaultForegroundBrush();
#if !(WINDOWS_APP || WINDOWS_PHONE || WINDOWS_PHONE_APP || WINDOWS_UWP)
            var backgroundBrush = GetBackground(textBlock) ?? ColorHelper.GetDefaultBackgroundBrush();
#endif
            var highlightProcessor = GetHighlightProcessor(textBlock);

            var highlightMarker = new HighlightMarker(fulltext, highlightedText, highlightProcessor);

            foreach (var current in highlightMarker)
            {
                int fromIndex = current.FromIndex;
                int length = current.Length;
                bool isHighlighted = current.IsHighlighted;

                var inlineRun = new Run { Text = fulltext.Substring(fromIndex, length) };
                if (isHighlighted)
                {
                    inlineRun.Foreground = foregroundBrush;
#if !(WINDOWS_APP || WINDOWS_PHONE || WINDOWS_PHONE_APP || WINDOWS_UWP)
                    inlineRun.Background = backgroundBrush;
#endif
                }
                textBlock.Inlines.Add(inlineRun);
            }
        }
    }
}