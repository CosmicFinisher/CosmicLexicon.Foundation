namespace CosmicLexicon.Foundation.Formats.UnitTest
{
    public class StringExtensionTruncateWithEllipsisTests
    {
        // Test Case 5.3.1: String Shorter Than Max Length
        [Theory]
        [InlineData("Short string", 20, "Short string")]
        [InlineData("Exact", 5, "Exact")]
        // Add test for string of length 1, maxLength 1, expecting original string
        [InlineData("A", 1, "A")] // Added: Test for string length equal to maxLength 1
        [InlineData("AB", 2, "AB")] // Added: Test for string length equal to maxLength 2
        public void TruncateWithEllipsisStringShorterThanOrEqualToMaxLengthReturnsOriginalString(string input, int maxLength, string expected)
        {
            // Act
            string result = input.TruncateWithEllipsis(maxLength);

            // Assert - Precise Observable Outcome (AI Verifiable)
            Assert.Equal(expected, result);
        }

        // Test Case 5.3.2: String Longer Than Max Length
        [Theory]
        [InlineData("This is a very long string that needs to be truncated.", 10, "This is...")]
        [InlineData("Another long string example", 7, "Anot...")] // Modified: Expected "Anot..." based on maxLength - 3 logic
        public void TruncateWithEllipsisStringLongerThanMaxLengthTruncatesAndAppendsEllipsis(string input, int maxLength, string expected)
        {
            // Act
            string result = input.TruncateWithEllipsis(maxLength);

            // Assert - Precise Observable Outcome (AI Verifiable)
            Assert.Equal(expected, result);
        }

        // Test Case 5.3.3: Max Length Too Small (Edge Case) - only for strings that ARE truncated
        [Theory]
        // This test case will now only include scenarios where the string *must* be truncated
        // because its length is greater than maxLength, ensuring the ellipsis logic for 1 or 2 maxLength is hit.
        [InlineData("Hello", 2, "..")] // String longer than 2, maxLength is 2, should return ".."
        [InlineData("Long", 1, ".")] // String longer than 1, maxLength is 1, should return "."
        public void TruncateWithEllipsisStringNeedsTruncationAndMaxLengthIsOneOrTwoReturnsCorrectEllipsis(string input, int maxLength, string expected)
        {
            // Act
            string result = input.TruncateWithEllipsis(maxLength);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TruncateWithEllipsisMaxLengthIsZeroOrLessThrowsArgumentOutOfRangeException()
        {
            // Test Data
            string input = "Hello";
            int maxLength = 0; // Or any negative number

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => input.TruncateWithEllipsis(maxLength));
            Assert.Contains("Maximum length must be greater than 0.", exception.Message);
        }
    }
}