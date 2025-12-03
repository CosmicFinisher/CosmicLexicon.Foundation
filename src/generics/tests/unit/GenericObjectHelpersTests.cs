namespace CosmicLexicon.Foundation.Generics.UnitTest
{
    public class GenericObjectHelpersTests
    {
        [Fact]
        public void MakeShallowCopyNullObjectReturnsDefault()
        {
            // Arrange
            object inputObject = null;

            // Act
            object result = GenericObjectHelpers.MakeShallowCopy<object>(inputObject);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void MakeShallowCopyPrimitiveTypeReturnsSameValue()
        {
            // Arrange
            int inputObject = 5;

            // Act
            int? result = GenericObjectHelpers.MakeShallowCopy(inputObject);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void MakeShallowCopyStringTypeReturnsSameValue()
        {
            // Arrange
            string inputObject = "test";

            // Act
            string result = GenericObjectHelpers.MakeShallowCopy(inputObject);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void MakeShallowCopySimpleObjectReturnsShallowCopy()
        {
            // Arrange
            var inputObject = new SimpleObject { Value = 10 };

            // Act
            var result = GenericObjectHelpers.MakeShallowCopy(inputObject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inputObject.Value, result.Value);
            Assert.NotSame(inputObject, result);
        }

        [Fact]
        public void MakeShallowCopySimpleObjectSimpleTypesOnlyReturnsDefault()
        {
            // Arrange
            var inputObject = new SimpleObject { Value = 10 };

            // Act
            var result = GenericObjectHelpers.MakeShallowCopy(inputObject, true);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void MakeShallowCopyArrayReturnsShallowCopy()
        {
            // Arrange
            int[] inputArray = [1, 2, 3];

            // Act
            int[] resultArray = GenericObjectHelpers.MakeShallowCopy(inputArray);

            // Assert
            Assert.Equal(inputArray, resultArray);
            Assert.NotSame(inputArray, resultArray);
        }

        public class SimpleObject
        {
            public int Value { get; set; }
        }
    }
}
