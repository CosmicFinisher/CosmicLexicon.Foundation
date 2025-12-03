using System.Text;

namespace CosmicLexicon.Foundation.Formats
{
    public sealed class GenericStringFormatter : IFormatProvider, ICustomFormatter, IStringFormatter
    {
        //
        // Summary:
        //     Represents alpha characters (defaults to @)
        public char AlphaChar { get; private set; }

        //
        // Summary:
        //     Represents digits (defaults to #)
        public char DigitChar { get; private set; }

        //
        // Summary:
        //     Represents the escape character (defaults to \\)
        public char EscapeChar { get; private set; }

        //
        // Summary:
        //     Constructor
        public GenericStringFormatter()
            : this('#', '@', '\\')
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the BigBook.Formatters.GenericStringFormatter class.
        //
        // Parameters:
        //   digitChar:
        //     The digit character.
        //
        //   alphaChar:
        //     The alpha character.
        //
        //   escapeChar:
        //     The escape character.
        public GenericStringFormatter(char digitChar, char alphaChar, char escapeChar)
        {
            DigitChar = digitChar;
            AlphaChar = alphaChar;
            EscapeChar = escapeChar;
        }

        //
        // Summary:
        //     Formats the string
        //
        // Parameters:
        //   format:
        //     Format to use
        //
        //   arg:
        //     Argument object to use
        //
        //   formatProvider:
        //     Format provider to use
        //
        // Returns:
        //     The formatted string
        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            return Format(arg?.ToString() ?? string.Empty, format ?? string.Empty);
        }

        public string Format(string? input, string formatPattern)
        {
            if (!IsValid(formatPattern))
            {
                throw new ArgumentException("Format pattern is not valid.", nameof(formatPattern));
            }

            StringBuilder stringBuilder = new StringBuilder();
            int inputIndex = 0;

            for (int i = 0; i < formatPattern.Length; i++)
            {
                char patternChar = formatPattern[i];

                if (patternChar == EscapeChar)
                {
                    i++;
                    if (i < formatPattern.Length)
                    {
                        stringBuilder.Append(formatPattern[i]);
                    }
                }
                else if (patternChar == DigitChar)
                {
                    while (inputIndex < input?.Length && !char.IsDigit(input[inputIndex]))
                    {
                        inputIndex++;
                    }
                    if (inputIndex < input?.Length)
                    {
                        stringBuilder.Append(input[inputIndex]);
                        inputIndex++;
                    }
                }
                else if (patternChar == AlphaChar)
                {
                    while (inputIndex < input?.Length && !char.IsLetter(input[inputIndex]))
                    {
                        inputIndex++;
                    }
                    if (inputIndex < input?.Length)
                    {
                        stringBuilder.Append(input[inputIndex]);
                        inputIndex++;
                    }
                }
                else
                {
                    stringBuilder.Append(patternChar);
                }
            }

            return stringBuilder.ToString();
        }

        public object? GetFormat(Type? formatType)
        {
            if (formatType != typeof(ICustomFormatter))
            {
                return null;
            }

            return this;
        }

        private bool IsValid(string? formatPattern)
        {
            if (string.IsNullOrEmpty(formatPattern))
            {
                return false;
            }

            bool previousWasEscape = false;
            for (int i = 0; i < formatPattern.Length; i++)
            {
                if (previousWasEscape)
                {
                    previousWasEscape = false;
                }
                else if (formatPattern[i] == EscapeChar)
                {
                    previousWasEscape = true;
                }
            }

            return !previousWasEscape;
        }
    }
}