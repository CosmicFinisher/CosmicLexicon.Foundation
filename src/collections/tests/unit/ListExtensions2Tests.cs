using System.Collections.Generic;
using System.Linq;
using Xunit;
using OpenEchoSystem.Core.xCollections;

namespace OpenEchoSystem.Core.xCollections
{
    public class ListExtensions3Tests
    {
        [Fact]
        public void AddIfNotExists_IEnumerable_AddsUniqueValues()
        {
            // Arrange
            List<int> list = new List<int> { 1, 2, 3 };
            IEnumerable<int> values = new List<int> { 3, 4, 5 };

            // Act
            list.AddIfNotExists(values);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3, 4, 5 }, list);
        }

        [Fact]
        public void AddIfNotExists_IEnumerable_DoesNotAddExistingValues()
        {
            // Arrange
            List<int> list = new List<int> { 1, 2, 3 };
            IEnumerable<int> values = new List<int> { 1, 2, 3 };

            // Act
            list.AddIfNotExists(values);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3 }, list);
        }

        [Fact]
        public void AddIfNotExists_SingleValue_AddsValueIfUnique()
        {
            // Arrange
            List<int> list = new List<int> { 1, 2, 3 };
            int value = 4;

            // Act
            list.AddIfNotExists(value);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3, 4 }, list);
        }

        [Fact]
        public void AddIfNotExists_SingleValue_DoesNotAddValueIfExisting()
        {
            // Arrange
            List<int> list = new List<int> { 1, 2, 3 };
            int value = 3;

            // Act
            list.AddIfNotExists(value);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3 }, list);
        }
    }
}
