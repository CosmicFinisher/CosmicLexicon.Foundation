using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using CosmicLexicon.Foundation.xLinq;
using System.Globalization;

namespace CosmicLexicon.Foundation.xLinq
{
    public class IEnumerableHelpersTests
    {        private static readonly string[] expectedStrings = new string[] { "1", "2", "3" };

        [Fact]
        public void ToArrayValidSourceAndSelectorReturnsTransformedArray()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            Func<int, string> selector = x => x.ToString(CultureInfo.InvariantCulture);

            // Act
            string[] result = IEnumerableHelpers.ToArray(source, selector);

            // Assert
            Assert.Equal(expectedStrings, result);
        }

        [Fact]
        public void ToArrayNullSourceThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = null;
            Func<int, string> selector = x => x.ToString();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ToArray(source, selector));
        }

        [Fact]
        public void ToArrayNullSelectorThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            Func<int, string> selector = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ToArray(source, selector));
        }        [Fact]
        public void ToListValidSourceAndSelectorReturnsTransformedList()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            Func<int, string> selector = x => x.ToString();

            // Act
            List<string> result = IEnumerableHelpers.ToList(source, selector);

            // Assert
            Assert.Equal(new string[] { "1", "2", "3" }, result);
        }        [Fact]
        public void ToListNullSourceThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = null;
            Func<int, string> selector = x => x.ToString(System.Globalization.CultureInfo.InvariantCulture);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ToList(source, selector));
        }        [Fact]
        public void ToListNullSelectorThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            Func<int, string> selector = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ToList(source, selector));
        }        [Fact]
        public void NullToEmptyNullSourceReturnsEmptyEnumerable()
        {
            // Arrange
            IEnumerable<int>? source = null;

            // Act
            IEnumerable<int> result = IEnumerableHelpers.NullToEmpty(source);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }        private static readonly int[] testArray = new[] { 1, 2, 3 };

        [Fact]
        public void NullToEmptyNotEmptySourceReturnsOriginalEnumerable()
        {
            // Arrange
            IEnumerable<int> source = new List<int>(testArray);

            // Act
            IEnumerable<int> result = IEnumerableHelpers.NullToEmpty(source);

            // Assert
            Assert.Equal(testArray, result.ToArray());
        }        [Fact]
        public void ToArrayOrEmptyNullSourceReturnsEmptyArray()
        {
            // Arrange
            IEnumerable<int>? source = null;

            // Act
            int[] result = IEnumerableHelpers.ToArrayOrEmpty(source);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }        [Fact]
        public void ToArrayOrEmptyNotEmptySourceReturnsOriginalArray()
        {
            // Arrange
            IEnumerable<int> source = new List<int>(testArray);

            // Act
            int[] result = IEnumerableHelpers.ToArrayOrEmpty(source);

            // Assert
            Assert.Equal(testArray, result);
        }        [Fact]
        public void ToListOrEmptyNullSourceReturnsEmptyList()
        {
            // Arrange
            IEnumerable<int>? source = null;

            // Act
            List<int> result = IEnumerableHelpers.ToListOrEmpty(source);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }        [Fact]
        public void ToListOrEmptyNotEmptySourceReturnsOriginalList()
        {
            // Arrange
            IEnumerable<int> source = new List<int>(testArray);

            // Act
            List<int> result = IEnumerableHelpers.ToListOrEmpty(source);

            // Assert
            Assert.Equal(testArray, result);
        }        [Fact]
        public void ForEachValidSourceAndActionExecutesActionOnAllElements()
        {
            // Arrange
            IEnumerable<int> source = new List<int>(testArray);
            int sum = 0;
            Action<int> action = x => sum += x;

            // Act
            IEnumerableHelpers.ForEach(source, action);

            // Assert
            Assert.Equal(6, sum);
        }        [Fact]
        public void ForEachNullSourceThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int>? source = null;
            Action<int> action = x => { };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ForEach(source, action));
        }        [Fact]
        public void ForEachNullActionThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = new List<int>(testArray);
            Action<int>? action = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => IEnumerableHelpers.ForEach(source, action));
        }
    }
}