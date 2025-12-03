using System.ComponentModel;
using System.Text;

namespace CosmicLexicon.Foundation.Formats
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringExtensions
    {
        extension(string value)
        {
            public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(value);
        }

        public static bool Is(string value1, string value2, StringCompare compareType)
        {
            if (value1 == null || value2 == null)
                return false;

            switch (compareType)
            {
                case StringCompare.Anagram:
                    if (value1.Length != value2.Length)
                        return false;

                    var chars1 = value1.ToLowerInvariant().ToCharArray();
                    var chars2 = value2.ToLowerInvariant().ToCharArray();
                    Array.Sort(chars1);
                    Array.Sort(chars2);
                    return chars1.SequenceEqual(chars2);

                case StringCompare.CreditCard:
                case StringCompare.Unicode:
                default:
                    return false;
            }
        }

        public static string MakeCamelCase(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(value[0]));
            sb.Append(value.AsSpan(1));
            return sb.ToString();
        }
        
        public static string MakeFirstCharUpper(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var sb = new StringBuilder();
            sb.Append(char.ToUpperInvariant(value[0]));
            sb.Append(value.AsSpan(1));
            return sb.ToString();
        }

        extension(string str)
        {
            /// <summary>
            /// Truncates a string to a specified maximum length and appends an ellipsis if truncated.
            /// </summary>
            /// <param name="str">The string to truncate.</param>
            /// <param name="maxLength">The maximum length of the returned string, including the ellipsis.</param>
            /// <returns>The truncated string with an ellipsis, or the original string if truncation is not needed.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maxLength"/> is less than or equal to 0.</exception>
            public string TruncateWithEllipsis(int maxLength)
            {
                if (str.IsNullOrWhiteSpace())
                {
                    return str;
                }

                if (maxLength <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(maxLength), "Maximum length must be greater than 0.");
                }

                if (str.Length <= maxLength)
                {
                    return str;
                }

                if (maxLength == 1)
                {
                    return ".";
                }

                if (maxLength == 2)
                {
                    return "..";
                }

                return str.AsSpan(0, maxLength - 3).ToString() + "...";
            }
        }
    }
}