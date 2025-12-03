using System.ComponentModel;
using System.Drawing;

namespace CosmicLexicon.Foundation.xText.xString
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ColorHelpers
    {
        public static bool IsColor(string colorName)
        {
            var color = Color.FromName(colorName);
            return !color.IsEmpty;
        }
    }
} 