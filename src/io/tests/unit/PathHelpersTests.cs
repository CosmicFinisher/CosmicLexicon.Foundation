using Xunit;
using OpenEchoSystem.Core.xIO;
using System;
using System.IO;
using OpenEchoSystem.Core.xIO;

namespace OpenEchoSystem.Core.xIO
{
    public class PathHelpersTests
    {
        [Fact]
        public void QuoteIfNeeded_PathWithSpaces_AddsQuotes()
        {
            // Arrange
            string path = "C:\\Program Files\\Test";
            
            // Act
            string result = PathHelpers.QuoteIfNeeded(path);
            
            // Assert
            Assert.Equal("\"C:\\Program Files\\Test\"", result);
        }
        
        [Fact]
        public void QuoteIfNeeded_PathWithoutSpaces_ReturnsUnchanged()
        {
            // Arrange
            string path = "C:\\ProgramFiles\\Test";
            
            // Act
            string result = PathHelpers.QuoteIfNeeded(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void QuoteIfNeeded_NullPath_ReturnsNull()
        {
            // Act
            string? result = PathHelpers.QuoteIfNeeded(null);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TrimSlash_PathWithSlash_RemovesTrailingSlash()
        {
            // Arrange
            string path = "C:\\Test\\";
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal("C:\\Test", result);
        }
        
        [Fact]
        public void TrimSlash_PathWithoutSlash_ReturnsUnchanged()
        {
            // Arrange
            string path = "C:\\Test";
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void TrimSlash_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            string path = string.Empty;
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Equal(string.Empty, result);
        }
        
        [Fact]
        public void TrimSlash_NullString_ReturnsNull()
        {
            // Arrange
            string? path = null;
            
            // Act
            string result = PathHelpers.TrimSlash(path);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void MustBeAbsolute_AbsolutePath_ReturnsPath()
        {
            // Arrange
            string path = "C:\\Test";
            
            // Act
            string result = PathHelpers.MustBeAbsolute(path);
            
            // Assert
            Assert.Equal(path, result);
        }
        
        [Fact]
        public void MustBeAbsolute_RelativePath_ThrowsArgumentException()
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