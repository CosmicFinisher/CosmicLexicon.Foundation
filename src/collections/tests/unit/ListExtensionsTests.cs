using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;
using OpenEchoSystem.Core.xCollections;

namespace OpenEchoSystem.Core.xCollections
{
    public class ListExtensionsTests
    {
        [Fact]
        public void ToReadOnly_ReturnsReadOnlyCollection()
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
        public void IsNullOrEmpty_WithNullCollection_ReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = null;

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void IsNullOrEmpty_WithEmptyCollection_ReturnsEmptyCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int>();

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void IsNullOrEmpty_WithNonEmptyCollection_ReturnsOriginalCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int> { 1, 2, 3 };

            // Act
            var result = ListExtensions.IsNullOrEmpty(collection);

            // Assert
            Assert.Equal(collection, result);
        }

        [Fact]
        public void Concat_WithNullCollections_ReturnsEmptyCollection()
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
        public void ToFlatList_WithListOfLists_ReturnsFlattenedList()
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
        public void AsFlattened_WithListOfLists_ReturnsFlattenedEnumerable()
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
        public void ToFlattenedList_WithListOfLists_ReturnsFlattenedListFromEnumerable()
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
        public void ToReadOnly_ValidItem_ReturnsReadOnlyCollection()
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
        public void NullCheck_NonNullCollection_ReturnsOriginalCollection()
        {
            // Arrange
            IReadOnlyCollection<int> collection = new List<int> { 1, 2, 3 };

            // Act
            IReadOnlyCollection<int> result = collection.NullCheck();

            // Assert
            Assert.Same(collection, result);
        }

        [Fact]
        public void NullCheck_NullCollection_ReturnsEmptyCollection()
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
        public void Concat_NonNullCollections_ReturnsConcatenatedCollection()
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
        public void Concat_NullCollections_ReturnsEmptyCollection()
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
        public void ToFlatList_NonNullLists_ReturnsFlatList()
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
        public void ToFlatList_NullLists_ReturnsEmptyList()
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
