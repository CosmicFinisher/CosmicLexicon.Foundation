using Xunit;
using CosmicLexicon.Foundation.xDiagnostics;
using System;
using System.IO;

namespace CosmicLexicon.Foundation.xDiagnostics
{
    using Xunit;
    using CosmicLexicon.Foundation.xDiagnostics;
    using System;
    using System.IO;

    public class LoggerTests
    {
        [Fact]
        public void LogLogsMessageToConsole()
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
