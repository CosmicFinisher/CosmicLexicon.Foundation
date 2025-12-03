namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class ListExtensions3Tests
    {
        [Fact]
        public void AddIfNotExistsIEnumerableAddsUniqueValues()
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
        public void AddIfNotExistsIEnumerableDoesNotAddExistingValues()
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
        public void AddIfNotExistsSingleValueAddsValueIfUnique()
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
        public void AddIfNotExistsSingleValueDoesNotAddValueIfExisting()
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
