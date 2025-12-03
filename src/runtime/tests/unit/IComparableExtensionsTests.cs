using System;
using CosmicLexicon.Foundation.xRuntime;
using Xunit;

namespace CosmicLexicon.Foundation.xRuntime;

public class IComparableExtensionsTests
{
    [Fact]
    public void Between_ValueBetweenMinAndMax_ReturnsTrue()
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
    public void Between_ValueLessThanMin_ReturnsFalse()
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
    public void Between_ValueGreaterThanMax_ReturnsFalse()
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
    public void Clamp_ValueBetweenMinAndMax_ReturnsValue()
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
    public void Clamp_ValueLessThanMin_ReturnsMin()
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
    public void Clamp_ValueGreaterThanMax_ReturnsMax()
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
    public void Max_InputAGreaterThanInputB_ReturnsInputA()
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
    public void Max_InputBGreaterThanInputA_ReturnsInputB()
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
    public void Min_InputALessThanInputB_ReturnsInputA()
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
    public void Min_InputBLessThanInputA_ReturnsInputB()
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