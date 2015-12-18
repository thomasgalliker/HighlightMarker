using System.Windows;
using System.Windows.Media;

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush GetDefaultHighlightBrush()
        {
            return new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);
        }
    }
}