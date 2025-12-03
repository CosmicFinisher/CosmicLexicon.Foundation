using System.Collections.ObjectModel;

namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class ListExtensions2Tests
    {
        [Fact]
        public void ToReadOnlyValidItemReturnsReadOnlyCollection()
        {
            // Arrange
            int item = 5;

            // Act
            var result = item.ToReadOnly();

            // Assert
            Assert.IsType<ReadOnlyCollection<int>>(result);
            Assert.Single(result);
            Assert.Equal(5, result[0]);
        }

        [Fact]
        public void NullCheckNonNullCollectionReturnsOriginalCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int> { 1, 2, 3 };

            // Act
            var result = collection.NullCheck();

            // Assert
            Assert.Same(collection, result);
        }

        [Fact]
        public void NullCheckNullCollectionReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = null;

            // Act
            var result = collection.NullCheck();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
