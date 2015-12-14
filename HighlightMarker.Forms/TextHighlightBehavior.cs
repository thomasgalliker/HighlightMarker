using System;

using Xamarin.Forms;

namespace HighlightMarker.Forms
{
    public class TextHighlightBehavior
    {
        public static readonly BindableProperty FullTextProperty = BindableProperty.CreateAttached<TextHighlightBehavior, string>(
               staticgetter: bindable => TextHighlightBehavior.GetFullText(bindable),
               defaultValue: null,
               defaultBindingMode: BindingMode.OneWay,
               validateValue: null,
               propertyChanged: (b, o, n) => TextHighlightBehavior.OnFullTextPropertyChanged(b, o, n),
               propertyChanging: null,
               coerceValue: null);

        public static readonly BindableProperty HighlightedTextProperty = BindableProperty.CreateAttached<TextHighlightBehavior, string>(
              staticgetter: bindable => TextHighlightBehavior.GetHighlightedText(bindable),
              defaultValue: null,
              defaultBindingMode: BindingMode.OneWay,
              validateValue: null,
              propertyChanged: (b, o, n) => TextHighlightBehavior.OnHighlightedTextPropertyChanged(b, o, n),
              propertyChanging: null,
              coerceValue: null);

        public static string GetFullText(BindableObject bo)
        {
            return (string)bo.GetValue(TextHighlightBehavior.FullTextProperty);
        }

        public static void SetFullText(BindableObject bo, string value)
        {
            bo.SetValue(TextHighlightBehavior.FullTextProperty, value);
        }

        public static string GetHighlightedText(BindableObject bo)
        {
            return (string)bo.GetValue(TextHighlightBehavior.HighlightedTextProperty);
        }

        public static void SetHighlightedText(BindableObject bo, string value)
        {
            bo.SetValue(TextHighlightBehavior.HighlightedTextProperty, value);
        }

        private static void OnFullTextPropertyChanged(BindableObject bindableObject, string oldValue, string newValue)
        {
            UpdateHighlightedText(bindableObject);
        }
        private static void OnHighlightedTextPropertyChanged(BindableObject bindableObject, string oldValue, string newValue)
        {
            UpdateHighlightedText(bindableObject);
        }

        private static void UpdateHighlightedText(BindableObject bindableObject)
        {
            var label = bindableObject as Label;
            if (label == null)
            {
                return;
            }
            string fulltext = GetFullText(label);
            string highlightedText = GetHighlightedText(label);

            if (string.IsNullOrEmpty(fulltext) || string.IsNullOrEmpty(highlightedText))
            {
                // Clear search text highlighting
                label.Text = fulltext;
                return;
            }

            Color foregroundColor = Color.Accent; //GetHighlightBrush(textBlock) ?? new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);

            label.FormattedText = string.Empty;

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
                        label.FormattedText.Spans.Add(new Span { Text = fulltext.Substring(fromIndex, length), ForegroundColor = foregroundColor });
                    }
                    else
                    {
                        label.FormattedText.Spans.Add(new Span { Text = fulltext.Substring(fromIndex, length) });
                    }
                }
            }
        }
    }
}
