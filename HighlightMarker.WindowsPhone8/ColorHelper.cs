#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Media;
#elif WINDOWS_PHONE_APP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#endif

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