namespace CosmicLexicon.Foundation.Generics.UnitTest
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void IsNullOrDbNullNullObjectReturnsTrue()
        {
            // Arrange
            object obj = null;

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrDbNullDbNullObjectReturnsTrue()
        {
            // Arrange
            object obj = DBNull.Value;

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrDbNullNotNullObjectReturnsFalse()
        {
            // Arrange
            object obj = new();

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.False(result);
        }
    }
}
