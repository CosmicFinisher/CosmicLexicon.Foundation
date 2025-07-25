using OpenEchoSystem.Core.xGenerics;
using System;
using Xunit;

namespace OpenEchoSystem.Core.xGenerics
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void IsNullOrDbNull_NullObject_ReturnsTrue()
        {
            // Arrange
            object obj = null;

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrDbNull_DbNullObject_ReturnsTrue()
        {
            // Arrange
            object obj = DBNull.Value;

            // Act
            bool result = ObjectExtensions.IsNullOrDbNull(obj);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrDbNull_NotNullObject_ReturnsFalse()
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
