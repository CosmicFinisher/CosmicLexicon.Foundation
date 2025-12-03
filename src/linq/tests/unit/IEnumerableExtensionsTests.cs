using CosmicLexicon.Foundation.Structures.Linq;

namespace CosmicLexicon.Foundation.Structures.Linq.UnitTest
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void EmptyNullArrayReturnsTrue()
        {
            // Arrange
            IEnumerable<int?> array = null;

            // Act
            bool result = array.Empty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyEmptyArrayReturnsTrue()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { };

            // Act
            bool result = array.Empty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyNotEmptyArrayReturnsFalse()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { 1, 2, 3 };

            // Act
            bool result = array.Empty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ToNullSafeArrayNullArrayReturnsEmptyArray()
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
        public void ToNullSafeArrayNotEmptyArrayReturnsOriginalArray()
        {
            // Arrange
            IEnumerable<int?> array = new int?[] { 1, 2, 3 };

            // Act
            int?[] result = array.ToNullSafeArray();

            // Assert
            Assert.Equal(new int?[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void ElementsBetweenValidRangeReturnsCorrectElements()
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
        public void ExceptValidPredicateReturnsCorrectElements()
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
