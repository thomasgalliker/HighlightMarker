using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
