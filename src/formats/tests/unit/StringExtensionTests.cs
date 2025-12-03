namespace CosmicLexicon.Foundation.Formats.UnitTest
{
    public class StringExtensionTests
    {
        [Fact]
        public void TruncateWithEllipsisNullStringReturnsNull()
        {
            string? value = null;
            int maxLength = 10;
            Assert.Null(value.TruncateWithEllipsis(maxLength)); // CS8604
        }

        [Fact]
        public void TruncateWithEllipsisEmptyStringReturnsEmptyString()
        {
            string value = "";
            int maxLength = 10;
            Assert.Equal("", value.TruncateWithEllipsis(maxLength));
        }

        [Fact]
        public void TruncateWithEllipsisStringShorterThanMaxLengthReturnsOriginalString()
        {
            string value = "short";
            int maxLength = 10;
            Assert.Equal("short", value.TruncateWithEllipsis(maxLength));
        }

        [Fact]
        public void TruncateWithEllipsisStringEqualToMaxLengthReturnsOriginalString()
        {
            string value = "exactlyten";
            int maxLength = 10;
            Assert.Equal("exactlyten", value.TruncateWithEllipsis(maxLength));
        }

        [Fact]
        public void TruncateWithEllipsisStringLongerThanMaxLengthReturnsTruncatedStringWithEllipsis()
        {
            string value = "thisstringislongerthanfifteenchars";
            int maxLength = 15;
            Assert.Equal("thisstringis...", value.TruncateWithEllipsis(maxLength));
        }

        [Fact]
        public void TruncateWithEllipsisMaxLengthIsOneReturnsDot()
        {
            string value = "any string";
            int maxLength = 1;
            Assert.Equal(".", value.TruncateWithEllipsis(maxLength));
        }

        [Fact]
        public void TruncateWithEllipsisMaxLengthIsTwoReturnsTwoDots()
        {
            string value = "any string";
            int maxLength = 2;
            Assert.Equal("..", value.TruncateWithEllipsis(maxLength));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void TruncateWithEllipsisMaxLengthIsZeroOrNegativeThrowsArgumentOutOfRangeException(int maxLength)
        {
            string value = "any string";
            Assert.Throws<ArgumentOutOfRangeException>(() => value.TruncateWithEllipsis(maxLength));
        }
    }

    public class StringCasingExtensionTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("A", "a")]
        [InlineData("HelloWorld", "helloWorld")]
        [InlineData("helloWorld", "helloWorld")]
        public void MakeCamelCaseConvertsFirstCharToLower(string? input, string? expected)
        {
            Assert.Equal(expected, StringExtensions.MakeCamelCase(input!)); // CS8604
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("helloWorld", "HelloWorld")]
        [InlineData("HelloWorld", "HelloWorld")]
        public void MakeFirstCharUpperConvertsFirstCharToUpper(string? input, string? expected)
        {
            Assert.Equal(expected, StringExtensions.MakeFirstCharUpper(input!)); // CS8604
        }

        [Fact]
        public void MakeCamelCaseWithTurkishCultureBehavesConsistently()
        {
            // The Turkish 'i' (U+0069) has a dotted and dotless form.
            // In Turkish culture, 'I' (U+0049) converts to 'ı' (U+0131) (dotless i) when lowercased.
            // In invariant culture, 'I' (U+0049) converts to 'i' (U+0069) (dotted i) when lowercased.
            // This test verifies the default culture-sensitive behavior.
            var originalCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
                Assert.Equal("istanbul", StringExtensions.MakeCamelCase("Istanbul"));
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }

        [Fact]
        public void MakeFirstCharUpperWithTurkishCultureBehavesConsistently()
        {
            // The Turkish 'i' (U+0069) has a dotted and dotless form.
            // In Turkish culture, 'i' (U+0069) converts to 'İ' (U+0130) (dotted I) when uppercased.
            // In invariant culture, 'i' (U+0069) converts to 'I' (U+0049) (dotless I) when uppercased.
            // This test verifies the default culture-sensitive behavior.
            var originalCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
                Assert.Equal("Izmir", StringExtensions.MakeFirstCharUpper("izmir"));
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }
    }
}