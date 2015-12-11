using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HighlightMarker
{
    public static class SearchTextHighlighting
    {
        #region Static Fields

        public static readonly DependencyProperty FullTextProperty = DependencyProperty.RegisterAttached(
            "FullText",
            typeof(string),
            typeof(SearchTextHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.RegisterAttached(
            "HighlightBrush",
            typeof(Brush),
            typeof(SearchTextHighlighting),
            new PropertyMetadata(null, OnHighlightBrushChangedCallback));

        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.RegisterAttached(
            "HighlightedText",
            typeof(string),
            typeof(SearchTextHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get full text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetFullText(TextBlock element)
        {
            return (string)element.GetValue(FullTextProperty);
        }

        /// <summary>
        ///     The get highlight brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="Brush" />.</returns>
        public static Brush GetHighlightBrush(TextBlock element)
        {
            return (Brush)element.GetValue(HighlightBrushProperty);
        }

        /// <summary>
        ///     The get highlighted text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetHighlightedText(TextBlock element)
        {
            return (string)element.GetValue(HighlightedTextProperty);
        }

        /// <summary>
        ///     The set full text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetFullText(TextBlock element, string value)
        {
            element.SetValue(FullTextProperty, value);
        }

        /// <summary>
        ///     The set highlight brush.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHighlightBrush(TextBlock element, Brush value)
        {
            element.SetValue(HighlightBrushProperty, value);
        }

        /// <summary>
        ///     The set highlighted text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHighlightedText(TextBlock element, string value)
        {
            element.SetValue(HighlightedTextProperty, value);
        }

        #endregion

        #region Methods

        private static void OnHighlightBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO GATH: Test this code! Forward call to OnTextChangedCallback might work better than this...

            var textBlock = d as TextBlock;
            if (textBlock == null)
            {
                return;
            }

            Brush brush = GetHighlightBrush(textBlock);

            for (int i = 0; i < textBlock.Inlines.Count; i += 2)
            {
                textBlock.Inlines[i].Foreground = brush;
            }
        }

        private static void OnTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBlock = d as TextBlock;
            if (textBlock == null)
            {
                return;
            }
            string fulltext = GetFullText(textBlock);
            string highlightedText = GetHighlightedText(textBlock);

            if (string.IsNullOrEmpty(fulltext) || string.IsNullOrEmpty(highlightedText))
            {
                // Clear search text highlighting
                textBlock.Inlines.Clear();
                textBlock.Inlines.Add(new Run { Text = fulltext });
                return;
            }

            Brush brush = GetHighlightBrush(textBlock) ?? new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);

            textBlock.Inlines.Clear();

            var highlightMarker = new HighlightMarker(fulltext, highlightedText);

            using (var enumerator = highlightMarker.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int fromIndex = enumerator.Current.FromIndex;
                    int length = enumerator.Current.Length;
                    bool isHighlighted = enumerator.Current.IsHighlighted;

                    if (isHighlighted)
                    {
                        textBlock.Inlines.Add(new Run { Text = fulltext.Substring(fromIndex, length), Foreground = brush });
                    }
                    else
                    {
                        textBlock.Inlines.Add(new Run { Text = fulltext.Substring(fromIndex, length) });
                    }
                }
            }
        }

        #endregion
    }
}