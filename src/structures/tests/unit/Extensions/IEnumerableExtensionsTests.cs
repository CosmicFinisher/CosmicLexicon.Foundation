using CosmicLexicon.Foundation.Linq;
using CosmicLexicon.Foundation.Structures;
using CosmicLexicon.Foundation.Structures.Extensions;

namespace CosmicLexicon.Foundation.Structures.UnitTest.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void ConcatNonNullCollectionsReturnsConcatenated()
        {
            // Arrange
            ICollection<int> collection1 = new List<int> { 1, 2 };
            ICollection<int> collection2 = new List<int> { 3, 4 };

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Equal(new[] { 1, 2, 3, 4 }, result.ToArray());
        }

        [Fact]
        public void ConcatNullCollectionsReturnsEmpty()
        {
            // Arrange
            ICollection<int>? collection1 = null;
            ICollection<int>? collection2 = null;

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ConcatFirstNullCollectionReturnsConcatenated()
        {
            // Arrange
            ICollection<int>? collection1 = null;
            ICollection<int> collection2 = new List<int> { 1, 2 };

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Equal(collection2, result);
        }

        [Fact]
        public void ConcatSecondNullCollectionReturnsConcatenated()
        {
            // Arrange
            ICollection<int> collection1 = new List<int> { 1, 2 };
            ICollection<int>? collection2 = null;

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Equal(collection1, result);
        }

        [Fact]
        public void IsNullOrEmptyWithNullReturnsTrue()
        {
            // Arrange
            IEnumerable<int>? collection = null;

            // Act
            bool result = collection.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyWithEmptyReturnsTrue()
        {
            // Arrange
            IEnumerable<int> collection = Enumerable.Empty<int>();

            // Act
            bool result = collection.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyWithNonEmptyReturnsFalse()
        {
            // Arrange
            IEnumerable<int> collection = new[] { 1 };

            // Act
            bool result = collection.IsNullOrEmpty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ForEachPerformsActionOnEachElement()
        {
            // Arrange
            var collection = new[] { 1, 2, 3 };
            var sum = 0;

            // Act
            collection.ForEach(x => sum += x);

            // Assert
            Assert.Equal(6, sum);
        }

        [Fact]
        public void ForEachWithNullCollectionDoesNothing()
        {
            // Arrange
            IEnumerable<int>? collection = null;
            var sum = 0;

            // Act
            collection.ForEach(x => sum += x);

            // Assert
            Assert.Equal(0, sum);
        }
    }
}