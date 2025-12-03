using Xunit;
using CosmicLexicon.Foundation.xNet;
using System.Threading.Tasks;

namespace CosmicLexicon.Foundation.xNet
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