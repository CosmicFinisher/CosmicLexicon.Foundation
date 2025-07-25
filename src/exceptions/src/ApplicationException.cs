using System.Runtime.Serialization;

namespace OpenEchoSystem.Core.xExceptions
{
    /// <summary>
    /// Represents an application-specific exception that occurs during execution.
    /// </summary>
    [Serializable]
    public class ApplicationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        public ApplicationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ApplicationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets or sets the application-specific integer error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the application-specific custom string error code.
        /// </summary>
        public string CustomErrorCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message
        /// and an application-specific integer error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The application-specific integer error code.</param>
        public ApplicationException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message,
        /// an application-specific integer error code, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The application-specific integer error code.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ApplicationException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message
        /// and an application-specific custom string error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="customErrorCode">The application-specific custom string error code.</param>
        public ApplicationException(string message, string customErrorCode)
            : base(message)
        {
            CustomErrorCode = customErrorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with a specified error message,
        /// an application-specific custom string error code, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="customErrorCode">The application-specific custom string error code.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ApplicationException(string message, string customErrorCode, Exception innerException)
            : base(message, innerException)
        {
            CustomErrorCode = customErrorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info.GetInt32(nameof(ErrorCode));
            CustomErrorCode = info.GetString(nameof(CustomErrorCode));
        }
    }
}