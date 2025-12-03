namespace CosmicLexicon.Foundation.Formats
{
    //
    // Summary:
    //     String formatter
    public interface IStringFormatter
    {
        //
        // Summary:
        //     Formats the string based on the pattern
        //
        // Parameters:
        //   input:
        //     Input string
        //
        //   formatPattern:
        //     Format pattern
        //
        // Returns:
        //     The formatted string
        string Format(string? input, string formatPattern);
    }
} 