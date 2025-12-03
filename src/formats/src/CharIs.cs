namespace CosmicLexicon.Foundation.Formats
{
    //
    // Summary:
    //     What type of character is this
    [Flags]
    public enum CharIs
    {
        //
        // Summary:
        //     The none
        None = 0x0,
        //
        // Summary:
        //     White space
        WhiteSpace = 0x1,
        //
        // Summary:
        //     Upper case
        Upper = 0x2,
        //
        // Summary:
        //     Symbol
        Symbol = 0x4,
        //
        // Summary:
        //     Surrogate
        Surrogate = 0x8,
        //
        // Summary:
        //     Punctuation
        Punctuation = 0x10,
        //
        // Summary:
        //     Number
        Number = 0x20,
        //
        // Summary:
        //     Low surrogate
        LowSurrogate = 0x40,
        //
        // Summary:
        //     Lower
        Lower = 0x80,
        //
        // Summary:
        //     letter or digit
        LetterOrDigit = 0x100,
        //
        // Summary:
        //     Letter
        Letter = 0x200,
        //
        // Summary:
        //     High surrogate
        HighSurrogate = 0x400,
        //
        // Summary:
        //     Digit
        Digit = 0x800,
        //
        // Summary:
        //     Control
        Control = 0x1000
    }
} 