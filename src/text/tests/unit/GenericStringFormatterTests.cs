using System;
using Xunit;
using OpenEchoSystem.Core.xText.xString;

namespace OpenEchoSystem.Core.xText
{
    public class GenericStringFormatterTests
    {
        [Fact]
        public void Constructor_Default_SetsDefaultChars()
        {
            // AI Verifiable End Result: Verify default constructor sets correct default characters.
            var formatter = new GenericStringFormatter();
            Assert.Equal('#', formatter.DigitChar);
            Assert.Equal('@', formatter.AlphaChar);
            Assert.Equal('\\', formatter.EscapeChar);
        }

        [Fact]
        public void Constructor_Custom_SetsCustomChars()
        {
            // AI Verifiable End Result: Verify custom constructor sets provided characters.
            var formatter = new GenericStringFormatter('0', 'A', '/');
            Assert.Equal('0', formatter.DigitChar);
            Assert.Equal('A', formatter.AlphaChar);
            Assert.Equal('/', formatter.EscapeChar);
        }

        [Theory]
        [InlineData("12345", "#####", "12345")]
        [InlineData("abcde", "@@@@@", "abcde")]
        [InlineData("1a2b3", "#@#@#", "1a2b3")]
        [InlineData("123", "#####", "123")] // Input shorter than pattern
        [InlineData("1234567", "#####", "12345")] // Input longer than pattern
        [InlineData("", "#####", "")] // Empty input
        [InlineData(null, "#####", "")] // Null input
        [InlineData("123", "##\\#", "12#")] // Escaped char in pattern
        [InlineData("abc", "@\\@", "a@")] // Escaped char in pattern
        [InlineData("123", "###-###", "123-")] // Pattern with literal and digits
        [InlineData("123", "\\#", "#")] // Escaped format char
        [InlineData("A", "\\@", "@")] // Escaped alpha char
        [InlineData("Test", "T\\e#s@t", "Test")]
        public void FormatStringPatternReturnsFormattedString(string? input, string formatPattern, string expected)
        {
            // AI Verifiable End Result: Verify Format(string, string) with various valid patterns and inputs.
            var formatter = new GenericStringFormatter();
            Assert.Equal(expected, formatter.Format(input, formatPattern));
        }

        [Fact]
        public void FormatStringPatternWithInvalidPatternThrowsArgumentException()
        {
            // AI Verifiable End Result: Verify Format(string, string) throws ArgumentException for invalid patterns.
            var formatter = new GenericStringFormatter();
            Assert.Throws<ArgumentException>(() => formatter.Format("123", "abc\\")); // Invalid pattern: trailing escape character
        }

        [Fact]
        public void FormatStringPatternWithNullFormatPatternThrowsArgumentException()
        {
            // AI Verifiable End Result: Verify Format(string, string) throws ArgumentException for null format pattern.
            var formatter = new GenericStringFormatter();
            Assert.Throws<ArgumentException>(() => formatter.Format("123", null));
        }

        [Fact]
        public void FormatObjectIFormatProviderReturnsFormattedString()
        {
            // AI Verifiable End Result: Verify Format(object, string, IFormatProvider) delegates correctly.
            var formatter = new GenericStringFormatter();
            var testObject = 12345;
            string formatString = "#####";
            Assert.Equal("12345", formatter.Format(formatString, testObject, formatter));
        }

        [Fact]
        public void FormatObjectIFormatProviderWithNullArgReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify Format(object, string, IFormatProvider) handles null arg.
            var formatter = new GenericStringFormatter();
            string formatString = "#####";
            Assert.Equal(string.Empty, formatter.Format(formatString, null, formatter));
        }

        [Fact]
        public void GetFormatWithICustomFormatterTypeReturnsThis()
        {
            // AI Verifiable End Result: Verify GetFormat returns itself for ICustomFormatter type.
            var formatter = new GenericStringFormatter();
            Assert.Same(formatter, formatter.GetFormat(typeof(ICustomFormatter)));
        }

        [Fact]
        public void GetFormatWithOtherTypeReturnsNull()
        {
            // AI Verifiable End Result: Verify GetFormat returns null for other types.
            var formatter = new GenericStringFormatter();
            Assert.Null(formatter.GetFormat(typeof(string)));
            Assert.Null(formatter.GetFormat(typeof(int)));
        }
    }
}