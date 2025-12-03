using CosmicLexicon.Foundation.xExceptions;
using System;
using Xunit;

namespace CosmicLexicon.Foundation.xExceptions
{
    public class ApplicationExceptionTests
    {
        [Fact]
        public void Constructor_Default_CreatesInstance()
        {
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException();
            
            // Assert
            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
            Assert.Equal(0, exception.ErrorCode);
        }
        
        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            // Arrange
            string message = "Test exception message";
            
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message);
            
            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(0, exception.ErrorCode);
        }
        
        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerException()
        {
            // Arrange
            string message = "Test exception message";
            var innerException = new InvalidOperationException("Inner exception");
            
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, innerException);
            
            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
            Assert.Equal(0, exception.ErrorCode);
        }
        
        [Fact]
        public void Constructor_WithMessageAndErrorCode_SetsMessageAndErrorCode()
        {
            // Arrange
            string message = "Test exception message";
            int errorCode = 42;
            
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, errorCode);
            
            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(errorCode, exception.ErrorCode);
        }
        
        [Fact]
        public void Constructor_WithMessageErrorCodeAndInnerException_SetsAllProperties()
        {
            // Arrange
            string message = "Test exception message";
            int errorCode = 42;
            var innerException = new InvalidOperationException("Inner exception");
            
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, errorCode, innerException);
            
            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
            Assert.Equal(errorCode, exception.ErrorCode);
        }

        [Fact]
        public void Constructor_WithMessageAndNonZeroErrorCode_SetsMessageAndErrorCode()
        {
            // Arrange
            string message = "Another test exception message";
            int errorCode = 100; // Use a non-zero error code

            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, errorCode);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(errorCode, exception.ErrorCode);
        }

        [Fact]
        public void ErrorCode_CanBeSet()
        {
            // Arrange
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException();
            int errorCode = 42;

            // Act
            exception.ErrorCode = errorCode;

            // Assert
            Assert.Equal(errorCode, exception.ErrorCode);
        }

        [Fact]
        public void Constructor_WithMessageAndCustomStringErrorCode_SetsMessageAndCustomStringErrorCode()
        {
            // Arrange
            string message = "Test exception with custom string error code";
            string customErrorCode = "CUSTOM_ERROR_STRING_001";

            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, customErrorCode);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(customErrorCode, exception.CustomErrorCode);
            Assert.Equal(0, exception.ErrorCode);
        }

        [Fact]
        public void Constructor_WithMessageCustomStringErrorCodeAndInnerException_SetsAllProperties()
        {
            // Arrange
            string message = "Test exception with custom string error code and inner exception";
            string customErrorCode = "CUSTOM_ERROR_STRING_002";
            var innerException = new InvalidOperationException("Inner exception for string error code");

            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException(message, customErrorCode, innerException);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
            Assert.Equal(customErrorCode, exception.CustomErrorCode);
            Assert.Equal(0, exception.ErrorCode);
        }

        [Fact]
        public void CustomStringErrorCode_CanBeSet()
        {
            // Arrange
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException();
            string customErrorCode = "ANOTHER_CUSTOM_ERROR_STRING";
            
            // Act
            exception.CustomErrorCode = customErrorCode;

            // Assert
            Assert.Equal(customErrorCode, exception.CustomErrorCode);
            Assert.Equal(0, exception.ErrorCode);
        }
    }
}
