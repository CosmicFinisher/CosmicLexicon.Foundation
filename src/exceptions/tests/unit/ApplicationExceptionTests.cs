using CosmicLexicon.Foundation.xExceptions;
using System;
using Xunit;

namespace CosmicLexicon.Foundation.xExceptions
{
    public class ApplicationExceptionTests
    {
        [Fact]
        public void ConstructorDefaultCreatesInstance()
        {
            // Act
            var exception = new CosmicLexicon.Foundation.xExceptions.ApplicationException();
            
            // Assert
            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
            Assert.Equal(0, exception.ErrorCode);
        }
        
        [Fact]
        public void ConstructorWithMessageSetsMessage()
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
        public void ConstructorWithMessageAndInnerExceptionSetsMessageAndInnerException()
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
        public void ConstructorWithMessageAndErrorCodeSetsMessageAndErrorCode()
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
        public void ConstructorWithMessageErrorCodeAndInnerExceptionSetsAllProperties()
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
        public void ConstructorWithMessageAndNonZeroErrorCodeSetsMessageAndErrorCode()
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
        public void ErrorCodeCanBeSet()
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
        public void ConstructorWithMessageAndCustomStringErrorCodeSetsMessageAndCustomStringErrorCode()
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
        public void ConstructorWithMessageCustomStringErrorCodeAndInnerExceptionSetsAllProperties()
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
        public void CustomStringErrorCodeCanBeSet()
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
