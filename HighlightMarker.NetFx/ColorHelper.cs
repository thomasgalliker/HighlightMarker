
#if WPF
using System.Windows;
using System.Windows.Media;
#elif WINDOWS_UWP
using Windows.UI;
using Windows.UI.Xaml.Media;
#endif

namespace HighlightMarker
{
    internal static class ColorHelper
    {
        internal static Brush DefaultForegroundBrush { get; } =
#if WINDOWS_UWP
            new SolidColorBrush(Colors.Black);
#else
            SystemColors.HighlightBrush;
#endif

        public static Brush DefaultBackgroundBrush { get; } = new SolidColorBrush(Colors.Transparent);
    }
}
