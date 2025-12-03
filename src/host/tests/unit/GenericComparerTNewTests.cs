namespace CosmicLexicon.Foundation.Host.UnitTest
{
    public class GenericComparerTNewTests
    {
        // Custom class for testing IComparable and IComparable<T>
        private class CustomComparable : IComparable, IComparable<CustomComparable>
        {
            public int Value { get; }
            public CustomComparable(int value) => Value = value;

            public int CompareTo(object? obj)
            {
                if (obj == null) return 1;
                if (obj is CustomComparable other)
                {
                    return Value.CompareTo(other.Value);
                }
                throw new ArgumentException("Object is not a CustomComparable");
            }

            public int CompareTo(CustomComparable? other)
            {
                if (other == null) return 1;
                return Value.CompareTo(other.Value);
            }
        }

        [Fact]
        public void CompareCustomComparableTypesReturnsCorrectResult()
        {
            // Arrange
            CustomComparable x = new CustomComparable(10);
            CustomComparable y = new CustomComparable(5);
            CustomComparable z = new CustomComparable(10);

            // Act
            int resultXY = GenericComparer<CustomComparable>.Comparer.Compare(x, y);
            int resultYX = GenericComparer<CustomComparable>.Comparer.Compare(y, x);
            int resultXZ = GenericComparer<CustomComparable>.Comparer.Compare(x, z);

            // Assert
            Assert.True(resultXY > 0); // 10 > 5
            Assert.True(resultYX < 0); // 5 < 10
            Assert.Equal(0, resultXZ); // 10 == 10
        }

        [Fact]
        public void CompareReferenceTypeFirstIsNullReturnsNegativeOne()
        {
            // Arrange
            string? x = null;
            string y = "test";

            // Act
            int result = GenericComparer<string>.Comparer.Compare(x, (string?)y);

            // Assert
            Assert.True(result < 0); // null is considered less than any non-null object
        }

        [Fact]
        public void CompareReferenceTypeSecondIsNullReturnsOne()
        {
            // Arrange
            string x = "test";
            string? y = null;

            // Act
            int result = GenericComparer<string>.Comparer.Compare((string?)x, y);

            // Assert
            Assert.True(result > 0); // non-null is considered greater than null
        }

        [Fact]
        public void CompareReferenceTypeBothAreNullReturnsZero()
        {
            // Arrange
            string? x = null;
            string? y = null;

            // Act
            int result = GenericComparer<string>.Comparer.Compare((string?)x, (string?)y);

            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(5, 10, -1)]
        [InlineData(10, 5, 1)]
        [InlineData(5, 5, 0)]
        public void CompareDiverseNumericTypesInt(int x, int y, int expectedSign)
        {
            // Act
            int result = GenericComparer<int>.Comparer.Compare(x, y);

            // Assert
            Assert.Equal(expectedSign, Math.Sign(result));
        }

        [Theory]
        [InlineData(5.5f, 10.5f, -1)]
        [InlineData(10.5f, 5.5f, 1)]
        [InlineData(5.5f, 5.5f, 0)]
        public void CompareDiverseNumericTypesFloat(float x, float y, int expectedSign)
        {
            // Act
            int result = GenericComparer<float>.Comparer.Compare(x, y);

            // Assert
            Assert.Equal(expectedSign, Math.Sign(result));
        }

        [Theory]
        [InlineData("apple", "banana", -1)]
        [InlineData("banana", "apple", 1)]
        [InlineData("apple", "apple", 0)]
        [InlineData("Apple", "apple", 1)] // Default string comparison is case-sensitive, 'A' is less than 'a' in ordinal, but Compare returns 1 for A > a
        public void CompareStringComparisonsReturnsCorrectResult(string x, string y, int expectedSign)
        {
            // Act
            int result = GenericComparer<string>.Comparer.Compare(x, y);

            // Assert
            Assert.Equal(expectedSign, Math.Sign(result));
        }
    }
}