using Xunit;
using CosmicLexicon.Foundation.Transport.Net;
using System.Threading.Tasks;

namespace CosmicLexicon.Foundation.Transport.Net
{
    public class NetExtensionsTests
    {
        [Fact]
        public async Task HasInternetConnectionAsyncReturnsTrue()
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