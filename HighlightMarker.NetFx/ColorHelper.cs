
#if NETFX || NETWPF
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
#if NETFX || NETWPF
            SystemColors.HighlightBrush;
#elif WINDOWS_UWP
            new SolidColorBrush(Colors.Black);
#endif

        public static Brush DefaultBackgroundBrush { get; } =
#if NETFX || NETWPF
            Brushes.Transparent;
#elif WINDOWS_UWP            
            new SolidColorBrush(Colors.Transparent);
#endif
    }
}
