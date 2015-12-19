using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush GetDefaultHighlightBrush()
        {
            var brush = Application.Current.Resources["PhoneAccentBrush"];
            return brush as Brush;
        }
    }
}