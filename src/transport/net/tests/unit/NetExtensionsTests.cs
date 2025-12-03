namespace CosmicLexicon.Foundation.Transport.Net.UnitTest
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