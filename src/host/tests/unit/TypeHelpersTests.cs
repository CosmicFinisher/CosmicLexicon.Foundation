using System;
using CosmicLexicon.Foundation.Host;
using Xunit;

namespace CosmicLexicon.Foundation.Host;

public class TypeHelpersTests
{
    [Fact]
    public void ParseToObjectValidIntStringReturnsInt()
    {
        // Arrange
        string value = "123";
        Type propertyType = typeof(int);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.IsType<int>(result);
        Assert.Equal(123, result);
    }

    [Fact]
    public void ParseToObjectValidGuidStringReturnsGuid()
    {
        // Arrange
        string value = "a7e7b3b0-5b5a-4b4a-8a1a-2b2b2b2b2b2b";
        Type propertyType = typeof(Guid);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.Equal(Guid.Parse(value), result);
    }

    [Fact]
    public void ParseToObjectInvalidGuidStringThrowsFormatException()
    {
        // Arrange
        string value = "invalid-guid";
        Type propertyType = typeof(Guid);

        // Act & Assert
        Assert.Throws<FormatException>(() => TypeHelpers.ParseToObject(propertyType, value));
    }

    [Fact]
    public void ParseToObjectValidTimeSpanStringReturnsTimeSpan()
    {
        // Arrange
        string value = "00:01:00";
        Type propertyType = typeof(TimeSpan);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.IsType<TimeSpan>(result);
        Assert.Equal(TimeSpan.Parse(value), result);
    }

    [Fact]
    public void ParseToObjectInvalidTimeSpanStringThrowsFormatException()
    {
        // Arrange
        string value = "invalid-timespan";
        Type propertyType = typeof(TimeSpan);

        // Act & Assert
        Assert.Throws<FormatException>(() => TypeHelpers.ParseToObject(propertyType, value));
    }

    [Fact]
    public void ParseToObjectValidEnumStringReturnsEnum()
    {
        // Arrange
        string value = "Value1";
        Type propertyType = typeof(TestEnum);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.IsType<TestEnum>(result);
        Assert.Equal(TestEnum.Value1, result);
    }

    [Fact]
    public void ParseToObjectInvalidEnumStringThrowsFormatException()
    {
        // Arrange
        string value = "InvalidValue";
        Type propertyType = typeof(TestEnum);

        // Act & Assert
        Assert.Throws<FormatException>(() => TypeHelpers.ParseToObject(propertyType, value));
    }

    [Fact]
    public void ParseToObjectEmptyStringAndNullableTypeReturnsNull()
    {
        // Arrange
        string value = "";
        Type propertyType = typeof(int?);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ParseToObjectStringValueReturnsString()
    {
        // Arrange
        string value = "test";
        Type propertyType = typeof(string);

        // Act
        object result = TypeHelpers.ParseToObject(propertyType, value);

        // Assert
        Assert.IsType<string>(result);
        Assert.Equal("test", result);
    }

    public enum TestEnum
    {
        Value1,
        Value2
    }
}