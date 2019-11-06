using System.Windows;
using System.Windows.Media;

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush GetDefaultForegroundBrush()
        {
            return SystemColors.HighlightBrush;
        }

        public static Brush GetDefaultBackgroundBrush()
        {
            return Brushes.Transparent;
        }
    }
}