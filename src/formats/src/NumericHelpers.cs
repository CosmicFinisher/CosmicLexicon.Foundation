using System.ComponentModel;

namespace CosmicLexicon.Foundation.Formats // Adjusted namespace
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NumericHelpers
    {
        private static string WithThousandSeparators(object value) => string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:#,0}", value);
        public static string WithThousandSeparators(long number) => WithThousandSeparators((object)number);
        public static string WithThousandSeparators(int number) => WithThousandSeparators((object)number);
        public static string WithThousandSeparators(short number) => WithThousandSeparators((object)number);
        public static string WithThousandSeparators(byte number) => WithThousandSeparators((object)number);
    }
} 