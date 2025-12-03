using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CosmicLexicon.Foundation.xLinq.Tests
{
    public class EnumerableHelpersTests
    {
        [Fact]
        public void CanBeSucceededAllElementsPassPredicateReturnsTrue()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 2, 4, 6, 8 };
            Func<int, bool> func = x => x % 2 == 0;

            // Act
            bool result =CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeSucceeded(collection, func);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeSucceededOneElementFailsPredicateReturnsFalse()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 2, 4, 5, 8 };
            Func<int, bool> func = x => x % 2 == 0;

            // Act
            bool result =CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeSucceeded(collection, func);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeSucceededNullCollectionThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int>? collection = null;
            Func<int, bool> func = x => x % 2 == 0;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
               CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeSucceeded(collection!, func));
        }

        [Fact]
        public void CanBeSucceededNullFuncThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 2, 4, 6, 8 };
            Func<int, bool>? func = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
               CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeSucceeded(collection, func!));
        }

        [Fact]
        public void CanBeIncludedOneElementPassesPredicateReturnsTrue()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 1, 3, 5, 6 };
            Func<int, bool> func = x => x % 2 == 0;

            // Act
            bool result =CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeIncluded(collection, func);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeIncludedNoElementPassesPredicateReturnsFalse()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 1, 3, 5, 7 };
            Func<int, bool> func = x => x % 2 == 0;

            // Act
            bool result =CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeIncluded(collection, func);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeIncludedNullCollectionThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int>? collection = null;
            Func<int, bool> func = x => x % 2 == 0;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeIncluded(collection!, func));
        }

        [Fact]
        public void CanBeIncludedNullFuncThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> collection = new List<int> { 1, 3, 5, 6 };
            Func<int, bool>? func = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
               CosmicLexicon.Foundation.xLinq.EnumerableHelpers.CanBeIncluded(collection, func!));
        }
    }
}