using System;
using System.Collections.Generic;
using CosmicLexicon.Foundation.xRuntime;
using Xunit;
using System.Globalization;

namespace CosmicLexicon.Foundation.xRuntime
{
    public class TypeHelpersNewTests
    {
        // --- Success Cases (Comprehensive) ---

        [Theory]
        [InlineData(typeof(sbyte), "10", (sbyte)10)]
        [InlineData(typeof(byte), "100", (byte)100)]
        [InlineData(typeof(short), "1000", (short)1000)]
        [InlineData(typeof(ushort), "2000", (ushort)2000)]
        [InlineData(typeof(int), "12345", 12345)]
        [InlineData(typeof(uint), "54321", (uint)54321)]
        [InlineData(typeof(long), "9876543210", 9876543210L)]
        [InlineData(typeof(ulong), "123456789012345", (ulong)123456789012345)]
        [InlineData(typeof(float), "1.23", 1.23f)]
        [InlineData(typeof(double), "123.456", 123.456)]
        [InlineData(typeof(decimal), "789.012", 789.012)]
        [InlineData(typeof(char), "A", 'A')]
        [InlineData(typeof(string), "Hello World", "Hello World")]
        [InlineData(typeof(bool), "true", true)]
        [InlineData(typeof(bool), "false", false)]
        [InlineData(typeof(Guid), "a1a2a3a4-b1b2-c1c2-d1d2-e1e2e3e4e5e6", "a1a2a3a4-b1b2-c1c2-d1d2-e1e2e3e4e5e6")]
        [InlineData(typeof(TimeSpan), "01:00:00", "01:00:00")]
        public void ParseToObject_PrimitiveAndCommonTypes_ReturnsParsedObject(Type propertyType, string value, object expected)
        {
            // Act
            object result = TypeHelpers.ParseToObject(propertyType, value);

            // Assert
            if (propertyType == typeof(Guid))
            {
                Assert.Equal(Guid.Parse(expected.ToString()), result);
            }
            else if (propertyType == typeof(TimeSpan))
            {
                Assert.Equal(TimeSpan.Parse(expected.ToString(), CultureInfo.InvariantCulture), result);
            }
            else if (propertyType == typeof(float))
            {
                Assert.Equal(Convert.ToSingle(expected, CultureInfo.InvariantCulture), Convert.ToSingle(result, CultureInfo.InvariantCulture));
            }
            else if (propertyType == typeof(double))
            {
                Assert.Equal(Convert.ToDouble(expected, CultureInfo.InvariantCulture), Convert.ToDouble(result, CultureInfo.InvariantCulture));
            }
            else if (propertyType == typeof(decimal))
            {
                Assert.Equal(Convert.ToDecimal(expected, CultureInfo.InvariantCulture), Convert.ToDecimal(result, CultureInfo.InvariantCulture));
            }
            else
            {
                Assert.Equal(expected, result);
            }
        }

        public enum TestEnum { Value1, Value2, Value3 }

        [Theory]
        [InlineData(typeof(TestEnum), "Value1", TestEnum.Value1)]
        [InlineData(typeof(TestEnum), "Value2", TestEnum.Value2)]
        public void ParseToObject_EnumTypes_ReturnsParsedObject(Type propertyType, string value, object expected)
        {
            // Act
            object result = TypeHelpers.ParseToObject(propertyType, value);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(typeof(int?), "123", 123)]
        [InlineData(typeof(int?), "", null)]
        [InlineData(typeof(bool?), "true", true)]
        [InlineData(typeof(bool?), "", null)]
        [InlineData(typeof(DateTime?), "2023-01-01", "2023-01-01T00:00:00.0000000")]
        [InlineData(typeof(Guid?), "a1a2a3a4-b1b2-c1c2-d1d2-e1e2e3e4e5e6", "a1a2a3a4-b1b2-c1c2-d1d2-e1e2e3e4e5e6")]
        public void ParseToObject_NullableTypes_ReturnsParsedObject(Type propertyType, string value, object expected)
        {
            // Act
            object result = TypeHelpers.ParseToObject(propertyType, value);

            // Assert
            if (propertyType == typeof(DateTime?))
            {
                Assert.Equal(expected == null ? null : (DateTime?)DateTime.Parse(expected.ToString(), CultureInfo.InvariantCulture), result);
            }
            else if (propertyType == typeof(Guid?))
            {
                Assert.Equal(expected == null ? null : (Guid?)Guid.Parse(expected.ToString()), result);
            }
            else
            {
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void ParseToObject_EmptyStringForNonNullableType_ThrowsFormatException()
        {
            // Arrange
            Type propertyType = typeof(int);
            string value = "";

            // Act & Assert
            Assert.Throws<FormatException>(() => TypeHelpers.ParseToObject(propertyType, value));
        }

        [Fact]
        public void ParseToObject_WhitespaceStringForNonNullableType_ThrowsFormatException()
        {
            // Arrange
            Type propertyType = typeof(int);
            string value = "   ";

            // Act & Assert
            Assert.Throws<FormatException>(() => TypeHelpers.ParseToObject(propertyType, value));
        }

        // --- Failure Cases (Exception Handling) ---

        public class CustomClass { }

        [Fact]
        public void ParseToObject_InvalidCast_ThrowsInvalidCastException()
        {
            // Arrange
            Type propertyType = typeof(CustomClass);
            string value = "some string";
            // Act & Assert
            Assert.Throws<InvalidCastException>(() => TypeHelpers.ParseToObject(propertyType, value));
        }

        [Theory]
        [InlineData("3.402824E+38")] // Value slightly above float.MaxValue
        [InlineData("-3.402824E+38")] // Value slightly below float.MinValue
        public void ParseToObject_FloatingPointOverflow_ClampsToFloatMinMax(string value)
        {
            // Arrange
            Type propertyType = typeof(float);

            // Act
            object result = TypeHelpers.ParseToObject(propertyType, value);
          
            // Assert
            float floatResult = (float)result;
            Assert.True(floatResult >= float.MaxValue || floatResult <= float.MinValue, $"Expected infinity, but got {floatResult}");
        }

        [Theory]
        [InlineData("1.7976931348623158E+308")] // Value slightly above double.MaxValue
        [InlineData("-1.7976931348623158E+308")] // Value slightly below double.MinValue
        public void ParseToObject_FloatingPointOverflow_ClampsToDoubleMinMax(string value)
        {
            // Arrange
            Type propertyType = typeof(double);

            // Act
            object result = TypeHelpers.ParseToObject(propertyType, value);

            // Assert
            double doubleResult = (double)result;
            Assert.True(doubleResult >= double.MaxValue || doubleResult <= double.MinValue, $"Expected infinity, but got {doubleResult}");
        }

        [Fact]
        public void ParseToObject_UnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            // Example of a type that Convert.ChangeType cannot handle without a custom TypeConverter
            Type propertyType = typeof(System.IO.FileInfo);
            string value = "some string";

            // Act & Assert
            Assert.Throws<InvalidCastException>(() => TypeHelpers.ParseToObject(propertyType, value));
        }
    }
}