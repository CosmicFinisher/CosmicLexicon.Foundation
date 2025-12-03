using System.Collections.ObjectModel;

namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class ListExtensionsTests
    {
        [Fact]
        public void ToReadOnlyReturnsReadOnlyCollection()
        {
            // Arrange
            int item = 10;

            // Act
            var collection = ListExtensions.ToReadOnly(item);

            // Assert
            Assert.IsType<ReadOnlyCollection<int>>(collection);
            Assert.Single(collection);
            Assert.Equal(item, collection[0]);
        }

        [Fact]
        public void IsNullOrEmptyWithNullCollectionReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = null;

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void IsNullOrEmptyWithEmptyCollectionReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int>();

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void IsNullOrEmptyWithNonEmptyCollectionReturnsOriginalCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int> { 1, 2, 3 };

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Equal(collection, result);
        }

        [Fact]
        public void ConcatWithNullCollectionsReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection1 = null;
            IReadOnlyCollection<int> collection2 = null;

            // Act
            var result = ListExtensions.Concat(collection1, collection2);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ToFlatListWithListOfListsReturnsFlattenedList()
        {
            // Arrange
            var listOfLists = new List<List<int>?>
                        {
                            new List<int> { 1, 2 },
                            null,
                            new List<int> { 3, 4 }
                        };

            // Act
            var result = ListExtensions.ToFlatList(listOfLists);

            // Assert
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, result);
        }

        [Fact]
        public void AsFlattenedWithListOfListsReturnsFlattenedEnumerable()
        {
            // Arrange
            var listOfLists = new List<List<int>?>
                        {
                            new List<int> { 1, 2 },
                            null,
                            new List<int> { 3, 4 }
                        };

            // Act
            var result = ListExtensions.AsFlattened(listOfLists);

            // Assert
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, result.ToList());
        }

        [Fact]
        public void ToFlattenedListWithListOfListsReturnsFlattenedListFromEnumerable()
        {
            // Arrange
            var listOfLists = new List<List<int>?>
                        {
                            new List<int> { 1, 2 },
                            null,
                            new List<int> { 3, 4 }
                        };

            // Act
            var result = ListExtensions.ToFlattenedList(listOfLists);

            // Assert
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, result);
        }
        [Fact]
        public void ToReadOnlyValidItemReturnsReadOnlyCollection()
        {
            // Arrange
            int item = 5;

            // Act
            ReadOnlyCollection<int> result = item.ToReadOnly();

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
            IReadOnlyCollection<int> result = collection.NullCheck();

            // Assert
            Assert.Same(collection, result);
        }

        [Fact]
        public void NullCheckNullCollectionReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = null;

            // Act
            IReadOnlyCollection<int> result = collection.NullCheck();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ConcatNonNullCollectionsReturnsConcatenatedCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection1 = new List<int> { 1, 2 };
            IReadOnlyCollection<int> collection2 = new List<int> { 3, 4 };

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3, 4 }, result.ToList());
        }

        [Fact]
        public void ConcatNullCollectionsReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection1 = null;
            IReadOnlyCollection<int> collection2 = null;

            // Act
            IEnumerable<int> result = collection1.Concat(collection2);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ToFlatListNonNullListsReturnsFlatList()
        {
            // Arrange
            List<List<int>?> collection = new List<List<int>?>
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4 }
            };

            // Act
            List<int> result = collection.ToFlatList();

            // Assert
            Assert.Equal(new int[] { 1, 2, 3, 4 }, result);
        }

        [Fact]
        public void ToFlatListNullListsReturnsEmptyList()
        {
            // Arrange
            List<List<int>?> collection = new List<List<int>?> { null, null };

            // Act
            List<int> result = collection.ToFlatList();

            // Assert
            Assert.Empty(result);
        }
    }
}
