using Xunit;
using CosmicLexicon.Foundation.xReflection;

namespace CosmicLexicon.Foundation.xReflection.Tests
{
    public class VersionInfoTests
    {
        [Fact]
        public void VersionInfoShortVersionHasCorrectValue()
        {
            // Arrange

            // Act
            VersionInfo versionInfo = VersionInfo.ShortVersion;

            // Assert
            Assert.Equal(0x0, (int)versionInfo);
        }

        [Fact]
        public void VersionInfoLongVersionHasCorrectValue()
        {
            // Arrange

            // Act
            VersionInfo versionInfo = VersionInfo.LongVersion;

            // Assert
            Assert.Equal(0x1, (int)versionInfo);
        }

        [Fact]
        public void VersionInfoCombinationHasCorrectValue()
        {
            // Arrange
            VersionInfo combinedVersion = VersionInfo.ShortVersion | VersionInfo.LongVersion;

            // Act & Assert
            Assert.Equal(0x1, (int)combinedVersion);
            Assert.True(combinedVersion.HasFlag(VersionInfo.LongVersion));
            Assert.True(combinedVersion.HasFlag(VersionInfo.ShortVersion)); // 0x0 is always true for HasFlag
        }
    }
}