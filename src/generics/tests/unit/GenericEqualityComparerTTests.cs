using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenEchoSystem.Core.xGenerics;

namespace OpenEchoSystem.Core.xCollections.Comparison
{
    public class GenericEqualityComparerTTests
    {
        [Fact]
        public void EqualsBothObjectsNullReturnsTrue()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<object>();

            // Act
            bool result = comparer.Equals(null!, null!);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOneObjectNullReturnsFalse()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<object>();

            // Act
            bool result = comparer.Equals(null!, new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsBothObjectsEqualReturnsTrue()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<int>();

            // Act
            bool result = comparer.Equals(1, 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsBothObjectsNotEqualReturnsFalse()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<int>();

            // Act
            bool result = comparer.Equals(1, 2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsEnumerablesEqualReturnsTrue()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<IEnumerable<int>>();
            IEnumerable<int> list1 = new List<int> { 1, 2, 3 };
            IEnumerable<int> list2 = new List<int> { 1, 2, 3 };

            // Act
            bool result = comparer.Equals(list1, list2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsEnumerablesNotEqualReturnsFalse()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<IEnumerable<int>>();
            IEnumerable<int> list1 = new List<int> { 1, 2, 3 };
            IEnumerable<int> list2 = new List<int> { 1, 2, 4 };

            // Act
            bool result = comparer.Equals(list1, list2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCodeObjectNullReturnsNegativeOne()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<object>();

            // Act
            int result = comparer.GetHashCode(null!);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void GetHashCodeObjectNotNullReturnsObjectHashCode()
        {
            // Arrange
            var comparer = new GenericEqualityComparer<string>();
            string obj = "test";

            // Act
            int result = comparer.GetHashCode(obj);

            // Assert
            Assert.Equal(obj.GetHashCode(), result);
        }
    }
}