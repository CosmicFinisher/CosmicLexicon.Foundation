using Xunit;
using OpenEchoSystem.Core.xExtensions;
using System;
using System.Text.RegularExpressions;

namespace OpenEchoSystem.Core.xExtensions.Tests
{
    public class CuidGeneratorTests
    {
        [Fact]
        public void NewCuidWithDefaultLengthReturnsCuidWithDefaultLength()
        {
            // Act
            string cuid = CuidGenerator.NewCuid();

            // Assert
            Assert.Equal(11, cuid.Length);
            Assert.Matches("^[a-zA-Z0-9]+$", cuid);
        }

        [Fact]
        public void NewCuidWithCustomLengthReturnsCuidWithCustomLength()
        {
            // Arrange
            byte length = 15;

            // Act
            string cuid = CuidGenerator.NewCuid(length);

            // Assert
            Assert.Equal(length, cuid.Length);
            Assert.Matches("^[a-zA-Z0-9]+$", cuid);
        }

        [Fact]
        public void NewCuidWithZeroLengthThrowsArgumentOutOfRangeException()
        {
            // Arrange
            byte length = 0;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => CuidGenerator.NewCuid(length));
        }
    }
}