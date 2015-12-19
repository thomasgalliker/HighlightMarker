using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush GetDefaultHighlightBrush()
        {
            // Metro default brushes found here:
            // http://metro.excastle.com/xaml-system-brushes
            var brush = Application.Current.Resources["ToggleSwitchCurtainPressedBackgroundThemeBrush"];
            return brush as Brush;
        }
    }
}