namespace CosmicLexicon.Foundation.Host.UnitTest;

public class IComparableExtensionsTests
{
    [Fact]
    public void BetweenValueBetweenMinAndMaxReturnsTrue()
    {
        // Arrange
        int value = 5;
        int min = 1;
        int max = 10;

        // Act
        bool result = value.Between(min, max);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void BetweenValueLessThanMinReturnsFalse()
    {
        // Arrange
        int value = 0;
        int min = 1;
        int max = 10;

        // Act
        bool result = value.Between(min, max);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void BetweenValueGreaterThanMaxReturnsFalse()
    {
        // Arrange
        int value = 11;
        int min = 1;
        int max = 10;

        // Act
        bool result = value.Between(min, max);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ClampValueBetweenMinAndMaxReturnsValue()
    {
        // Arrange
        int value = 5;
        int min = 1;
        int max = 10;

        // Act
        int result = value.Clamp(min, max);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void ClampValueLessThanMinReturnsMin()
    {
        // Arrange
        int value = 0;
        int min = 1;
        int max = 10;

        // Act
        int result = value.Clamp(min, max);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void ClampValueGreaterThanMaxReturnsMax()
    {
        // Arrange
        int value = 11;
        int min = 1;
        int max = 10;

        // Act
        int result = value.Clamp(min, max);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void MaxInputAGreaterThanInputBReturnsInputA()
    {
        // Arrange
        int inputA = 10;
        int inputB = 5;

        // Act
        int result = inputA.Max(inputB);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void MaxInputBGreaterThanInputAReturnsInputB()
    {
        // Arrange
        int inputA = 5;
        int inputB = 10;

        // Act
        int result = inputA.Max(inputB);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void MinInputALessThanInputBReturnsInputA()
    {
        // Arrange
        int inputA = 5;
        int inputB = 10;

        // Act
        int result = inputA.Min(inputB);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void MinInputBLessThanInputAReturnsInputB()
    {
        // Arrange
        int inputA = 10;
        int inputB = 5;

        // Act
        int result = inputA.Min(inputB);

        // Assert
        Assert.Equal(5, result);
    }
}