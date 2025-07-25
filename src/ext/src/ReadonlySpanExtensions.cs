
namespace OpenEchoSystem.Core.xExtensions
{
    public static class ReadonlySpanExtensions
    {
        public static T Parse<T>(this ReadOnlySpan<char> input, IFormatProvider? formatProvider = null)
            where T : ISpanParsable<T>
        {
            return T.Parse(input, formatProvider);
        }
    }
} 