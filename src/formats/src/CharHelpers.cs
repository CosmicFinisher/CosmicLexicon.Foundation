using System.ComponentModel;

namespace CosmicLexicon.Foundation.Formats
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CharHelpers
    {
        public static bool Is(char value, CharIs characterType) => characterType switch
        {
            CharIs.WhiteSpace => char.IsWhiteSpace(value),
            CharIs.Upper => char.IsUpper(value),
            CharIs.Symbol => char.IsSymbol(value),
            CharIs.Surrogate => char.IsSurrogate(value),
            CharIs.Punctuation => char.IsPunctuation(value),
            CharIs.Number => char.IsNumber(value),
            CharIs.LowSurrogate => char.IsLowSurrogate(value),
            CharIs.Lower => char.IsLower(value),
            CharIs.LetterOrDigit => char.IsLetterOrDigit(value),
            CharIs.Letter => char.IsLetter(value),
            CharIs.HighSurrogate => char.IsHighSurrogate(value),
            CharIs.Digit => char.IsDigit(value),
            CharIs.Control => char.IsControl(value),
            _ => false,
        };

        public static bool IsLineBreakChar(char c) => c == '\r' || c == '\n'; // Moved from TextUtilities
    }
} 