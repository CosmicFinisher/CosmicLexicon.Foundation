using Xunit;
using CosmicLexicon.Foundation.Transport;
using System;
using System.IO;
using CosmicLexicon.Foundation.Transport;

namespace CosmicLexicon.Foundation.Transport
{
    public class PathHelpersTests
    {
        [Fact]
        public void QuoteIfNeededPathWithSpacesAddsQuotes()
        {
            // Arrange
            string path = "C:\\Program Files\\Test";
            
            // Act
            string result = PathHelpers.QuoteIfNeeded(path);
            
            // Assert
            Assert.Equal("\"C:\\Program Files\\Test\"", result);
        }
        
        [Fact]
        public void QuoteIfNeededPathWithoutSpacesReturnsUnchanged()
        {
            // Arrange
            string path = "C:\\ProgramFiles\\Test";
            
            // Act
            string result = PathHelpers.QuoteIfNeeded(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void QuoteIfNeededNullPathReturnsNull()
        {
            // Act
            string? result = PathHelpers.QuoteIfNeeded(null);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TrimSlashPathWithSlashRemovesTrailingSlash()
        {
            // Arrange
            string path = "C:\\Test\\";
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal("C:\\Test", result);
        }
        
        [Fact]
        public void TrimSlashPathWithoutSlashReturnsUnchanged()
        {
            // Arrange
            string path = "C:\\Test";
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void TrimSlashEmptyStringReturnsEmptyString()
        {
            // Arrange
            string path = string.Empty;
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal(string.Empty, result);
        }
        
        [Fact]
        public void TrimSlashNullStringReturnsNull()
        {
            // Arrange
            string? path = null;
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void MustBeAbsoluteAbsolutePathReturnsPath()
        {
            // Arrange
            string path = "C:\\Test";
            
            // Act
            string result = PathHelpers.MustBeAbsolute(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void MustBeAbsoluteRelativePathThrowsArgumentException()
        {
            // Arrange
            string path = "Test\\SubFolder";
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => PathHelpers.MustBeAbsolute(path));
            Assert.Contains("is not absolute", exception.Message);
        }
        
        [Fact]
        public void QuoteIfNeededWithPathWithSpacesReturnsQuotedPath()
        {
            // Arrange
            string path = "path with spaces";

            // Act
            string result = PathHelpers.QuoteIfNeeded(path);

            // Assert
            Assert.Equal("\"path with spaces\"", result);
        }

        [Fact]
        public void QuoteIfNeededWithPathWithoutSpacesReturnsOriginalPath()
        {
            // Arrange
            string path = "path";

            // Act
            string result = PathHelpers.QuoteIfNeeded(path);

            // Assert
            Assert.Equal("path", result);
        }

        [Fact]
        public void QuoteIfNeededWithQuotedPathReturnsOriginalPath()
        {
            // Arrange
            string path = "\"quoted path\"";

            // Act
            string result = PathHelpers.QuoteIfNeeded(path);

            // Assert
            Assert.Equal("\"quoted path\"", result);
        }

        [Fact]
        public void TrimSlashWithTrailingSlashesReturnsTrimmedPath()
        {
            // Arrange
            string path = "path\\/";
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal("path", result);
        }

        [Fact]
        public void TrimSlashWithoutTrailingSlashesReturnsOriginalPath()
        {
            // Arrange
            string path = "path";

            // Act
            string result = PathHelpers.TrimSlash(path);

            // Assert
            Assert.Equal("path", result);
        }

        [Fact]
        public void MustBeAbsoluteWithRelativePathThrowsArgumentException()
        {
            // Arrange
            string path = "path";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => PathHelpers.MustBeAbsolute(path));
        }

        [Fact]
        public void MustBeAbsoluteWithAbsolutePathReturnsOriginalPath()
        {
            // Arrange
            string path = Path.GetFullPath("path");

            // Act
            string result = PathHelpers.MustBeAbsolute(path);

            // Assert
            Assert.Equal(path, result);
        }
    }
}