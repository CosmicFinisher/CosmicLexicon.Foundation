using CosmicLexicon.Foundation.xText.xString;
using System.Reflection.Metadata;
using Xunit;

namespace CosmicLexicon.Foundation.xText
{
    public sealed class StringConcatExtensionTests
    {
        static readonly char extraSpace = ' ';

        // ForEach<T> was not found in LinqExtension.cs, so related tests are kept commented.
        // Region ForEach<T> was commented out in original file, keeping as is.
        // #region ForEach<T>
        // #region TheArrayNumbers*MustMultiplyByAction*
        // [Fact]
        // public void TheArrayNumbersMustMultiplyByAction()
        // {
        //     var array = new[] { 1, 2, 3, 4, 5 };
        //     var actualResult = 1;
        //     var resultOfMultiply = 1;

        //     // array.ForEach((int number) => // Assuming ForEach was an extension method
        //     // {
        //     //     resultOfMultiply *= number;
        //     // });
        //     Assert.Equal(resultOfMultiply, actualResult);
        // }
        // #endregion
        // #endregion
        readonly string string1 = " ";
        readonly string string2 = "Successful";
        readonly string string3 = "Concatenation";
        readonly string string4 = "Strings";
        readonly string string5 = "!";

        [Fact]
        public void ConcatenatedStringsMustEqualToActualString()
        {
            var @strings = GetStrings();

            // Calling the Concat extension method from CosmicLexicon.Net.Core.xLinqExtensions
            var actualString = StringHelpers.Concat(@strings).ToString();

            var expected = GetExpectedConcatenatedStringSimple();

            Assert.Equal(expected, actualString);
        }
        [Fact]
        public void CustomConcatenatedStringsMustEqualToActualStringWithSpaceToken()
        {
            var @strings = GetStringsForSpacedConcat();

            // Calling the Concat extension method with token and whitespace
            var actualString = StringHelpers.Concat(@strings, ' ', false); // token ' ', includeWhitespace false (token is the space)

            var expected = GetExpectedConcatenatedStringWithSpaces();

            Assert.Equal(expected, actualString);
        }

    
        private static string GetExpectedConcatenatedStringSimple()
        {
            // string1 is " ", string2="Successful", etc.
            // Empty strings are skipped by Concat(). Whitespace strings are included.
            return " SuccessfulConcatenationStrings!";
        }

        private static string GetExpectedConcatenatedStringWithSpaces()
        {
            // Test with GetStringsForSpacedConcat which doesn't have leading/trailing space strings by default
            return "Successful Concatenation Strings !";
        }

        private  string[] GetStrings() => new string[5] {
                string1, string2, string3, string4, string5
        };

        private static string[] GetStringsForSpacedConcat() => new string[4] {
                "Successful", "Concatenation", "Strings", "!"
        };

        [Fact]
        public void CustomConcatenatedStringsUsingFuncMustEqualToActualString()
        {
            var @strings = GetStringsForSpacedConcat();
            var expected = "Successful Concatenation Strings !";

            var actualString = StringHelpers.Concat(@strings, new Func<string, string>((inputString) =>
            {
                return inputString + extraSpace; // Adds an extra space after each non-empty part
            }));

            Assert.Equal(expected, actualString.TrimEnd()); // TrimEnd for trailing space from last item processing
        }
        [Fact]
        public void ConcatWithEmptyAdditionsReturnsOriginalEnumerable()
        {
            // Arrange
            IEnumerable<int> enumerable1 = new int[] { 1, 2, 3 };
            IEnumerable<int>[] additions = Array.Empty<IEnumerable<int>>(); // Fixed CA1825

            // Act
            IEnumerable<int> result = StringHelpers.Concat(enumerable1, additions);

            // Assert
            Assert.Equal(enumerable1, result);
        }
    }
}
