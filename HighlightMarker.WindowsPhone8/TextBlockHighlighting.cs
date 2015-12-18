using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HighlightMarker
{
    public static class TextBlockHighlighting
    {
        public static readonly DependencyProperty FullTextProperty = DependencyProperty.RegisterAttached(
            "FullText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.RegisterAttached(
            "HighlightBrush",
            typeof(Brush),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnHighlightBrushChangedCallback));

        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.RegisterAttached(
            "HighlightedText",
            typeof(string),
            typeof(TextBlockHighlighting),
            new PropertyMetadata(null, OnTextChangedCallback));

        public static string GetFullText(TextBlock element)
        {
            return (string)element.GetValue(FullTextProperty);
        }

        public static Brush GetHighlightBrush(TextBlock element)
        {
            return (Brush)element.GetValue(HighlightBrushProperty);
        }

        public static string GetHighlightedText(TextBlock element)
        {
            return (string)element.GetValue(HighlightedTextProperty);
        }

        public static void SetFullText(TextBlock element, string value)
        {
            element.SetValue(FullTextProperty, value);
        }

        public static void SetHighlightBrush(TextBlock element, Brush value)
        {
            element.SetValue(HighlightBrushProperty, value);
        }

        public static void SetHighlightedText(TextBlock element, string value)
        {
            element.SetValue(HighlightedTextProperty, value);
        }

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

            textBlock.Inlines.Clear();

            if (string.IsNullOrEmpty(fulltext) || string.IsNullOrEmpty(highlightedText))
            {
                textBlock.Inlines.Add(new Run { Text = fulltext });
                return;
            }

            var brush = GetHighlightBrush(textBlock) ?? new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);

            var highlightMarker = new HighlightMarker(fulltext, highlightedText);

            foreach (var current in highlightMarker)
            {
                int fromIndex = current.FromIndex;
                int length = current.Length;
                bool isHighlighted = current.IsHighlighted;

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
}