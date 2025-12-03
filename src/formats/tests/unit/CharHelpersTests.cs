namespace CosmicLexicon.Foundation.Formats.UnitTest
{
    public class CharHelpersTests
    {
        // Test Case 5.4.1: Valid Line Break Characters
        [Theory]
        [InlineData('\n', true)] // Newline
        [InlineData('\r', true)] // Carriage return
        // You might add other Unicode line break characters if CharHelpers.IsLineBreakChar is designed to handle them.
        // For example:
        // [InlineData('\u0085', true)] // Next Line (NEL)
        // [InlineData('\u2028', true)] // Line Separator
        // [InlineData('\u2029', true)] // Paragraph Separator
        public void IsLineBreakCharValidLineBreakCharactersReturnsTrue(char c, bool expected)
        {
            // Act
            bool result = CharHelpers.IsLineBreakChar(c);

            // Assert - Precise Observable Outcome (AI Verifiable)
            Assert.Equal(expected, result);
        }

        // Test Case 5.4.2: Non-Line Break Characters
        [Theory]
        [InlineData('A', false)]
        [InlineData(' ', false)] // Space
        [InlineData('\t', false)] // Tab
        [InlineData('1', false)] // Digit
        [InlineData('@', false)] // Symbol
        public void IsLineBreakCharNonLineBreakCharactersReturnsFalse(char c, bool expected)
        {
            // Act
            bool result = CharHelpers.IsLineBreakChar(c);

            // Assert - Precise Observable Outcome (AI Verifiable)
            Assert.Equal(expected, result);
        }
    }
}