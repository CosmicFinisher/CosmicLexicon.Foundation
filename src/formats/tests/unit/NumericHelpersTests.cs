namespace CosmicLexicon.Foundation.Formats.UnitTest
{
    public class NumericHelpersTests
    {
        [Theory]
        [InlineData(1234, "1,234")]
        [InlineData(1234567, "1,234,567")]
        [InlineData(100, "100")]
        [InlineData(0, "0")]
        [InlineData(-1234, "-1,234")]
        public void WithThousandSeparatorsIntReturnsFormattedStringInvariant(int number, string expected) =>
            // Test with InvariantCulture to ensure consistent behavior
            Assert.Equal(expected, NumericHelpers.WithThousandSeparators(number));

        [Theory]
        [InlineData(1234L, "1,234")]
        [InlineData(1234567890123L, "1,234,567,890,123")]
        [InlineData(0L, "0")]
        [InlineData(-9876543210L, "-9,876,543,210")]
        public void WithThousandSeparatorsLongReturnsFormattedStringInvariant(long number, string expected) =>
            // Test with InvariantCulture to ensure consistent behavior
            Assert.Equal(expected, NumericHelpers.WithThousandSeparators(number));

        [Theory]
        [InlineData(123, "123")]
        [InlineData(12345, "12,345")]
        [InlineData(0, "0")]
        [InlineData(-5432, "-5,432")]
        public void WithThousandSeparatorsShortReturnsFormattedStringInvariant(short number, string expected) =>
            // Test with InvariantCulture to ensure consistent behavior
            Assert.Equal(expected, NumericHelpers.WithThousandSeparators(number));

        [Theory]
        [InlineData(123, "123")]
        [InlineData(0, "0")]
        public void WithThousandSeparatorsByteReturnsFormattedStringInvariant(byte number, string expected) =>
            // Test with InvariantCulture to ensure consistent behavior
            Assert.Equal(expected, NumericHelpers.WithThousandSeparators(number));
    }
}