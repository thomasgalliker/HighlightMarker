
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
               propertyChanged: (b, o, n) => TextHighlightBehavior.OnTextPropertyChanged(b/*, o, n*/),
               propertyChanging: null,
               coerceValue: null);

        public static readonly BindableProperty HighlightedTextProperty = BindableProperty.CreateAttached<TextHighlightBehavior, string>(
              staticgetter: bindable => TextHighlightBehavior.GetHighlightedText(bindable),
              defaultValue: null,
              defaultBindingMode: BindingMode.OneWay,
              validateValue: null,
              propertyChanged: (b, o, n) => TextHighlightBehavior.OnTextPropertyChanged(b/*, o, n*/),
              propertyChanging: null,
              coerceValue: null);

        public static readonly BindableProperty ForegroundProperty = BindableProperty.CreateAttached<TextHighlightBehavior, Color>(
            staticgetter: bindable => TextHighlightBehavior.GetForeground(bindable),
            defaultValue: Color.Accent,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => TextHighlightBehavior.OnTextPropertyChanged(b/*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static readonly BindableProperty BackgroundProperty = BindableProperty.CreateAttached<TextHighlightBehavior, Color>(
             staticgetter: bindable => TextHighlightBehavior.GetBackground(bindable),
             defaultValue: Color.Transparent,
             defaultBindingMode: BindingMode.OneWay,
             validateValue: null,
             propertyChanged: (b, o, n) => TextHighlightBehavior.OnTextPropertyChanged(b/*, o, n*/),
             propertyChanging: null,
             coerceValue: null);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.CreateAttached<TextHighlightBehavior, FontAttributes>(
           staticgetter: bindable => TextHighlightBehavior.GetFontAttributes(bindable),
           defaultValue: FontAttributes.None,
           defaultBindingMode: BindingMode.OneWay,
           validateValue: null,
           propertyChanged: (b, o, n) => TextHighlightBehavior.OnTextPropertyChanged(b/*, o, n*/),
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

        public static Color GetForeground(BindableObject bo)
        {
            return (Color)bo.GetValue(TextHighlightBehavior.ForegroundProperty);
        }

        public static void SetForeground(BindableObject bo, Color value)
        {
            bo.SetValue(TextHighlightBehavior.ForegroundProperty, value);
        }

        public static Color GetBackground(BindableObject bo)
        {
            return (Color)bo.GetValue(TextHighlightBehavior.BackgroundProperty);
        }

        public static void SetBackground(BindableObject bo, Color value)
        {
            bo.SetValue(TextHighlightBehavior.BackgroundProperty, value);
        }

        public static FontAttributes GetFontAttributes(BindableObject bo)
        {
            return (FontAttributes)bo.GetValue(TextHighlightBehavior.FontAttributesProperty);
        }

        public static void SetFontAttributes(BindableObject bo, FontAttributes value)
        {
            bo.SetValue(TextHighlightBehavior.FontAttributesProperty, value);
        }

        private static void OnTextPropertyChanged(BindableObject bindableObject/*, string oldValue, string newValue*/)
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
                // Clear text highlighting
                label.Text = fulltext;
                return;
            }

            Color foregroundColor = GetForeground(label);
            Color backgroundColor = GetBackground(label);
            FontAttributes fontAttributes = GetFontAttributes(label);

            label.FormattedText = string.Empty;

            var highlightMarker = new HighlightMarker(fulltext, highlightedText);

            using (var enumerator = highlightMarker.GetEnumerator())
            {
                var formattedText = new FormattedString();

                while (enumerator.MoveNext())
                {
                    int fromIndex = enumerator.Current.FromIndex;
                    int length = enumerator.Current.Length;
                    bool isHighlighted = enumerator.Current.IsHighlighted;

                    var span = new Span { Text = fulltext.Substring(fromIndex, length) };
                    if (isHighlighted)
                    {
                        span.ForegroundColor = foregroundColor;
                        span.BackgroundColor = backgroundColor;
                        span.FontAttributes = fontAttributes;
                    }

                    formattedText.Spans.Add(span);
                }

                label.FormattedText = formattedText;
            }
        }
    }
}
