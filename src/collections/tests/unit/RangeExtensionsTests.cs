using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using OpenEchoSystem.Core.xCollections;

namespace OpenEchoSystem.Core.xCollections
{
    public class RangeExtensionsTests
    {
        [Fact]
        [RequiresPreviewFeatures]
        public void GetEnumerator_ValidRange_ReturnsEnumerator()
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
        public void GetEnumerator_ReversedRange_ReturnsReversedEnumerator()
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
        public void CustomRangeEnumerator_FromEndRange_ThrowsNotSupportedException()
        {
            // Arrange
            Range range = 1..^1;

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => range.GetEnumerator());
        }
    }
}
