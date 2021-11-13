using Xamarin.Forms;

namespace HighlightMarker.Forms
{
    public class TextHighlightBehavior
    {
        public static readonly BindableProperty FullTextProperty = BindableProperty.CreateAttached(
            propertyName: "FullText",
            returnType: typeof(string),
            declaringType: typeof(TextHighlightBehavior),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => OnTextPropertyChanged(b /*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static readonly BindableProperty HighlightedTextProperty = BindableProperty.CreateAttached(
            propertyName: "HighlightedText",
            returnType: typeof(string),
            declaringType: typeof(TextHighlightBehavior),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => OnTextPropertyChanged(b /*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static readonly BindableProperty ForegroundProperty = BindableProperty.CreateAttached(
            propertyName: "Foreground",
            returnType: typeof(Color),
            declaringType: typeof(TextHighlightBehavior),
            defaultValue: Color.Accent,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => OnTextPropertyChanged(b /*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static readonly BindableProperty BackgroundProperty = BindableProperty.CreateAttached(
            propertyName: "Background",
            returnType: typeof(Color),
            declaringType: typeof(TextHighlightBehavior),
            defaultValue: Color.Transparent,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => OnTextPropertyChanged(b /*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.CreateAttached(
            propertyName: "FontAttributes",
            returnType: typeof(FontAttributes),
            declaringType: typeof(TextHighlightBehavior),
            defaultValue: FontAttributes.None,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: (b, o, n) => OnTextPropertyChanged(b /*, o, n*/),
            propertyChanging: null,
            coerceValue: null);

        public static string GetFullText(BindableObject bo)
        {
            return (string)bo.GetValue(FullTextProperty);
        }

        public static void SetFullText(BindableObject bo, string value)
        {
            bo.SetValue(FullTextProperty, value);
        }

        public static string GetHighlightedText(BindableObject bo)
        {
            return (string)bo.GetValue(HighlightedTextProperty);
        }

        public static void SetHighlightedText(BindableObject bo, string value)
        {
            bo.SetValue(HighlightedTextProperty, value);
        }

        public static Color GetForeground(BindableObject bo)
        {
            return (Color)bo.GetValue(ForegroundProperty);
        }

        public static void SetForeground(BindableObject bo, Color value)
        {
            bo.SetValue(ForegroundProperty, value);
        }

        public static Color GetBackground(BindableObject bo)
        {
            return (Color)bo.GetValue(BackgroundProperty);
        }

        public static string Test { get; set; }

        public static void SetBackground(BindableObject bo, Color value)
        {
            bo.SetValue(BackgroundProperty, value);
        }

        public static FontAttributes GetFontAttributes(BindableObject bo)
        {
            return (FontAttributes)bo.GetValue(FontAttributesProperty);
        }

        public static void SetFontAttributes(BindableObject bo, FontAttributes value)
        {
            bo.SetValue(FontAttributesProperty, value);
        }

        private static void OnTextPropertyChanged(BindableObject bindableObject /*, string oldValue, string newValue*/)
        {
            if (!(bindableObject is Label label))
            {
                return;
            }

            var fulltext = GetFullText(label);
            var highlightedText = GetHighlightedText(label);

            if (string.IsNullOrEmpty(fulltext) || string.IsNullOrEmpty(highlightedText))
            {
                // Clear text highlighting
                label.Text = fulltext;
                return;
            }

            var foregroundColor = GetForeground(label);
            var backgroundColor = GetBackground(label);
            var fontAttributes = GetFontAttributes(label);

            label.FormattedText = string.Empty;

            var highlightMarker = new HighlightMarker(fulltext, highlightedText);

            using (var enumerator = highlightMarker.GetEnumerator())
            {
                var formattedText = new FormattedString();

                while (enumerator.MoveNext())
                {
                    var fromIndex = enumerator.Current.FromIndex;
                    var length = enumerator.Current.Length;
                    var isHighlighted = enumerator.Current.IsHighlighted;

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