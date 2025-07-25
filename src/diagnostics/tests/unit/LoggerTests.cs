using Xunit;
using OpenEchoSystem.Core.xDiagnostics;
using System;
using System.IO;

namespace OpenEchoSystem.Core.xDiagnostics
{
    using Xunit;
    using OpenEchoSystem.Core.xDiagnostics;
    using System;
    using System.IO;

    public class LoggerTests
    {
        [Fact]
        public void Log_LogsMessageToConsole()
        {
            // Arrange
            var logger = new Logger();
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            string message = "Test message";

            // Act
            logger.Log(message);

            // Assert
            Assert.Equal(message + Environment.NewLine, stringWriter.ToString());

            // Restore the original console output
            Console.SetOut(Console.Out);
        }
    }
}
