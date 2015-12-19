using System.Windows;
using System.Windows.Media;

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush GetDefaultHighlightBrush()
        {
            return SystemColors.HighlightBrush;
        }
    }
}