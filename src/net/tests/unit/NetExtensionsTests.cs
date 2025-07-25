using Xunit;
using OpenEchoSystem.Core.xNet;
using System.Threading.Tasks;

namespace OpenEchoSystem.Core.xNet
{
    public class NetExtensionsTests
    {
        [Fact]
        public async Task HasInternetConnectionAsync_ReturnsTrue()
        {
            // Arrange
            string host = "google.com";

            // Act
            bool result = await NetExtensions.HasInternetConnectionAsync(host);

            // Assert
            Assert.True(result);
        }
    }
}