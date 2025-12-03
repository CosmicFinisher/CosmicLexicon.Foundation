using System;
using CosmicLexicon.Foundation.Host;
using Xunit;

namespace CosmicLexicon.Foundation.Host;

public class GenericComparerTTests
{
    [Fact]
    public void Compare_DifferentValues_ReturnsCorrectComparison()
    {
        // Arrange
        var comparer = GenericComparer<int>.Comparer;
        int x = 1;
        int y = 2;

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void Compare_NonNullValues_ReturnsCorrectComparison()
    {
        // Arrange
        var comparer = GenericComparer<string>.Comparer;
        string x = "test1";
        string y = "test2";

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void Compare_EqualValues_ReturnsZero()
    {
        // Arrange
        var comparer = GenericComparer<int>.Comparer;
        int x = 1;
        int y = 1;

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Compare_NullValues_ReturnsCorrectComparison()
    {
        // Arrange
        var comparer = GenericComparer<string>.Comparer;
        string x = null;
        string y = "test";

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.True(result < 0);
    }
}