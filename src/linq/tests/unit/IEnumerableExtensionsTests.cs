using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using OpenEchoSystem.Core.xLinq;

namespace OpenEchoSystem.Core.xLinq
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void Empty_NullArray_ReturnsTrue()
        {
            // Arrange
            IEnumerable<int?> array = null;

            // Act
            bool result = array.Empty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Empty_EmptyArray_ReturnsTrue()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { };

            // Act
            bool result = array.Empty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Empty_NotEmptyArray_ReturnsFalse()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { 1, 2, 3 };

            // Act
            bool result = array.Empty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ToNullSafeArray_NullArray_ReturnsEmptyArray()
        {
            // Arrange
            IEnumerable<int?> array = null;

            // Act
            int?[] result = array.ToNullSafeArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void ToNullSafeArray_NotEmptyArray_ReturnsOriginalArray()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { 1, 2, 3 };

            // Act
            int?[] result = array.ToNullSafeArray();

            // Assert
            Assert.Equal(new int?[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void ElementsBetween_ValidRange_ReturnsCorrectElements()
        {
            // Arrange
            IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };
            int start = 1;
            int end = 4;

            // Act
            IEnumerable<int> result = list.ElementsBetween(start, end);

            // Assert
            Assert.Equal(new int[] { 2, 3, 4 }, result.ToList());
        }

        [Fact]
        public void Except_ValidPredicate_ReturnsCorrectElements()
        {
            // Arrange
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            Func<int, bool> predicate = x => x % 2 == 0;

            // Act
            IEnumerable<int> result = value.Except(predicate);

            // Assert
            Assert.Equal(new int[] { 1, 3, 5 }, result.ToList());
        }
    }
}
