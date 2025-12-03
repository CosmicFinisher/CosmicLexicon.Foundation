using System.ComponentModel;

namespace CosmicLexicon.Foundation.Formats // Adjusted namespace
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NumericHelpers
    {
        private static string WithThousandSeparators(object value)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:#,0}", value);
        }
        public static string WithThousandSeparators(long number)
        {
            return WithThousandSeparators((object)number); 
        }
        public static string WithThousandSeparators(int number)
        {
            return WithThousandSeparators((object)number);
        }
        public static string WithThousandSeparators(short number)
        {
            return WithThousandSeparators((object)number);
        }
        public static string WithThousandSeparators(byte number)
        {
            return WithThousandSeparators((object)number);
        }
    }
} 