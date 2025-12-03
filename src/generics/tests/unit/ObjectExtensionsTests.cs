using CosmicLexicon.Foundation.xGenerics;
using System;
using Xunit;

namespace CosmicLexicon.Foundation.xGenerics
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
            object obj = new object();

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.False(result);
        }
    }
}
