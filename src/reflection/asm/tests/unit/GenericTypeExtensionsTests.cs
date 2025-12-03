using Xunit;
using CosmicLexicon.Foundation.xReflection.xAssembly;
using CosmicLexicon.Foundation.xReflection.xAssembly;
using System;
using System.Collections.Generic;

namespace CosmicLexicon.Foundation.xReflection.xAssembly.Tests
{
    public class GenericTypeExtensionsTests
    {
        [Fact]
        public void IsDefaultWithDefaultValueReturnsTrue()
        {
            // Arrange
            int value = 0;

            // Act
            bool result = value.IsDefault();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDefaultWithNonDefaultValueReturnsFalse()
        {
            // Arrange
            int value = 5;

            // Act
            bool result = value.IsDefault();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsDefaultWithNullValueReturnsTrue()
        {
            // Arrange
            string? value = null;

            // Act
            bool result = value.IsDefault();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDefaultWithNonNullValueReturnsFalse()
        {
            // Arrange
            string value = "test";

            // Act
            bool result = value.IsDefault();

            // Assert
            Assert.False(result);
        }
    }
}