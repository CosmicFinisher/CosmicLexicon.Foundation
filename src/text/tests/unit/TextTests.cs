using Xunit;

namespace OpenEchoSystem.Core.xText
{
    
    using OpenEchoSystem.Core.xText.xString;

    public class TextTests
    {
        [Fact]
        public void IsNullOrWhiteSpaceReturnsTrueForNullOrWhiteSpace()
        {
            Assert.True(((string)null).IsNullOrWhiteSpace());
            Assert.True("".IsNullOrWhiteSpace());
            Assert.True(" ".IsNullOrWhiteSpace());
        }

        [Fact]
        public void IsNullOrWhiteSpaceReturnsFalseForNonWhiteSpace()
        {
            Assert.False("a".IsNullOrWhiteSpace());
            Assert.False(" a ".IsNullOrWhiteSpace());
        }
[Fact]
        public void IsAnagramReturnsTrueForAnagrams()
        {
            // Arrange
            string value1 = "listen";
            string value2 = "silent";

            // Act
            bool result = StringExtensions.Is(value1, value2, StringCompare.Anagram);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void GetLineNumberNullLineLengthsThrowsArgumentNullException()
        {
            // Arrange
            int start = 0;
            int[] lineLengths = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => TextUtilities.GetLineNumber(start, lineLengths));
        }
    }
}
