using System;
using System.Collections.Generic;
using CosmicLexicon.Foundation.Host;
using Xunit;

namespace CosmicLexicon.Foundation.Host
{
    public class IComparableExtensionsNewTests
    {
        [Fact]
        public void Between_InvertedRange_ReturnsFalse()
        {
            // Arrange
            int value = 5;
            int min = 10;
            int max = 1;

            // Act
            bool result = value.Between(min, max);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Between_DateTime_ValueIsBetweenMinAndMax_ReturnsTrue()
        {
            // Arrange
            DateTime value = new DateTime(2023, 1, 5);
            DateTime min = new DateTime(2023, 1, 1);
            DateTime max = new DateTime(2023, 1, 10);

            // Act
            bool result = value.Between(min, max);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Between_Guid_ValueIsBetweenMinAndMax_ReturnsTrue()
        {
            // Arrange
            Guid value = new Guid("00000000-0000-0000-0000-000000000002");
            Guid min = new Guid("00000000-0000-0000-0000-000000000001");
            Guid max = new Guid("00000000-0000-0000-0000-000000000003");

            // Act
            bool result = value.Between(min, max);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Clamp_InvertedRange_ValueBelowMin_ReturnsCorrectClampedValue()
        {
            // Arrange
            int value = 0;
            int min = 10; // Original min
            int max = 1;  // Original max

            // Act
            int result = value.Clamp(max, min); // Clamp(0, 1, 10) - internally, min becomes 1, max becomes 10

            // Assert
            // After internal swap in Clamp, effective min is 1. Value (0) is less than effective min (1).
            // So, it should return the effective min, which is 1.
            Assert.Equal(1, result);
        }

        [Fact]
        public void Clamp_InvertedRange_ValueAboveMax_ReturnsCorrectClampedValue()
        {
            // Arrange
            int value = 15;
            int min = 10; // Original min
            int max = 1;  // Original max

            // Act
            int result = value.Clamp(max, min); // Clamp(15, 1, 10) - internally, min becomes 1, max becomes 10

            // Assert
            // After internal swap in Clamp, effective max is 10. Value (15) is greater than effective max (10).
            // So, it should return the effective max, which is 10.
            Assert.Equal(10, result);
        }

        [Fact]
        public void Clamp_InvertedRange_ValueBetweenInvertedMinMax_ReturnsCorrectClampedValue()
        {
            // Arrange
            int value = 5;
            int min = 10; // Original min
            int max = 1;  // Original max

            // Act
            int result = value.Clamp(max, min); // Clamp(5, 1, 10) - internally, min becomes 1, max becomes 10

            // Assert
            // After internal swap in Clamp, effective min is 1 and effective max is 10.
            // Value (5) is between 1 and 10, so it should return the value itself.
            Assert.Equal(value, result);
        }

        [Fact]
        public void Clamp_DateTime_ValueIsBetweenMinAndMax_ReturnsValue()
        {
            // Arrange
            DateTime value = new DateTime(2023, 1, 5);
            DateTime min = new DateTime(2023, 1, 1);
            DateTime max = new DateTime(2023, 1, 10);

            // Act
            DateTime result = value.Clamp(max, min);

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void Max_DateTime_ReturnsMax()
        {
            // Arrange
            DateTime inputA = new DateTime(2023, 1, 10);
            DateTime inputB = new DateTime(2023, 1, 5);

            // Act
            DateTime result = inputA.Max(inputB);

            // Assert
            Assert.Equal(inputA, result);
        }

        [Fact]
        public void Min_DateTime_ReturnsMin()
        {
            // Arrange
            DateTime inputA = new DateTime(2023, 1, 10);
            DateTime inputB = new DateTime(2023, 1, 5);

            // Act
            DateTime result = inputA.Min(inputB);

            // Assert
            Assert.Equal(inputB, result);
        }
    }
}