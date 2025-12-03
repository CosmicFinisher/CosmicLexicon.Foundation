using System.Globalization;

namespace CosmicLexicon.Foundation.Extensions.UnitTest
{
    public class ReadonlySpanExtensionsTests
    {
        [Fact]
        public void ParseValidInputReturnsParsedValue()
        {
            // Arrange
            ReadOnlySpan<char> input = "123".AsSpan();

            // Act
            int result = input.Parse<int>();

            // Assert
            Assert.Equal(123, result);
        }

        [Fact]
        public void ParseValidInputWithFormatProviderReturnsParsedValue()
        {
            // Arrange
            ReadOnlySpan<char> input = "123.45".AsSpan();
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            // Act
            decimal result = input.Parse<decimal>(formatProvider);

            // Assert
            Assert.Equal(123.45m, result);
        }

        [Fact]
        public void ParseInvalidInputThrowsFormatException()
        {
            // Since ReadOnlySpan<char> can't be captured in a lambda, we'll use a string
            string testInput = "abc";

            // Act & Assert
            Assert.Throws<FormatException>(() => testInput.AsSpan().Parse<int>());
        }
    }
}