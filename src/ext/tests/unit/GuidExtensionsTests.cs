namespace CosmicLexicon.Foundation.Extensions.UnitTest
{
    public class GuidExtensionsTests
    {
        [Fact]
        public void NewIfNullOrEmptyWithNullGuidReturnsNewGuid()
        {
            // Arrange
            Guid? value = null;

            // Act
            Guid result = value.NewIfNullOrEmpty();

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void NewIfNullOrEmptyWithEmptyGuidReturnsNewGuid()
        {
            // Arrange
            Guid? value = Guid.Empty;

            // Act
            Guid result = value.NewIfNullOrEmpty();

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void NewIfNullOrEmptyWithValidGuidReturnsOriginalGuid()
        {
            // Arrange
            Guid originalGuid = Guid.NewGuid();
            Guid? value = originalGuid;

            // Act
            Guid result = value.NewIfNullOrEmpty();

            // Assert
            Assert.Equal(originalGuid, result);
        }

        [Fact]
        public void GuidEqualWithEqualGuidsReturnsTrue()
        {
            // Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = guid1;

            // Act
            bool result = guid1.Equals(guid2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GuidEqualWithDifferentGuidsReturnsFalse()
        {
            // Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            // Act
            bool result = guid1.Equals(guid2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEmptyWithEmptyGuidReturnsTrue()
        {
            // Arrange
            Guid guid = Guid.Empty;

            // Act
            bool result = guid.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmptyWithNonEmptyGuidReturnsFalse()
        {
            // Arrange
            Guid guid = Guid.NewGuid();

            // Act
            bool result = guid.IsEmpty();

            // Assert
            Assert.False(result);
        }
    }
}