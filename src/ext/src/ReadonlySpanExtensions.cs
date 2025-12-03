namespace CosmicLexicon.Foundation.Extensions
{
    public static class ReadonlySpanExtensions
    {
        extension(ReadOnlySpan<char> input)
        {
            public T Parse<T>(IFormatProvider? formatProvider = null)
            where T : ISpanParsable<T> => T.Parse(input, formatProvider);
        }
    }
} 