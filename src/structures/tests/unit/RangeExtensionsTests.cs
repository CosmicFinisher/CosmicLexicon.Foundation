using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using CosmicLexicon.Foundation.Structures;

namespace CosmicLexicon.Foundation.Structures
{
    public class RangeExtensionsTests
    {
        [Fact]
        [RequiresPreviewFeatures]
        public void GetEnumeratorValidRangeReturnsEnumerator()
        {
            // Arrange
            Range range = 1..5;

            // Act
            var enumerator = range.GetEnumerator();
            var result = new List<int>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }

            // Assert
            Assert.Equal(new int[] { 1, 2, 3, 4, 5 }, result);
        }

        [Fact]
        [RequiresPreviewFeatures]
        public void GetEnumeratorReversedRangeReturnsReversedEnumerator()
        {
            // Arrange
            Range range = 5..1;

            // Act
            var enumerator = range.GetEnumerator();
            var result = new List<int>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }

            // Assert
            Assert.Equal(new int[] { 5, 4, 3, 2, 1 }, result);
        }

        [Fact]
        [RequiresPreviewFeatures]
        public void CustomRangeEnumeratorFromEndRangeThrowsNotSupportedException()
        {
            // Arrange
            Range range = 1..^1;

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => range.GetEnumerator());
        }
    }
}
