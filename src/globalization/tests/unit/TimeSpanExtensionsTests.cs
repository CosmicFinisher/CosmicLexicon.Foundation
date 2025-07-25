using Xunit;
using OpenEchoSystem.Core.xGlobalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEchoSystem.Core.xGlobalization
{
    public class TimeSpanExtensionsTests
    {
        [Fact]
        public void AverageWithValidTimeSpanListReturnsCorrectAverage()
        {
            // Arrange
            List<TimeSpan> timeSpans = new List<TimeSpan>()
            {
                TimeSpan.FromHours(1),
                TimeSpan.FromHours(2),
                TimeSpan.FromHours(3)
            };

            // Act
            TimeSpan result = timeSpans.Average();

            // Assert
            Assert.Equal(TimeSpan.FromHours(2), result);
        }

        [Fact]
        public void AverageWithEmptyTimeSpanListReturnsZeroTimeSpan()
        {
            // Arrange
            List<TimeSpan> timeSpans = new List<TimeSpan>();

            // Act
            TimeSpan result = timeSpans.Average();

            // Assert
            Assert.Equal(TimeSpan.Zero, result);
        }

        [Fact]
        public void DaysRemainderWithValidTimeSpanReturnsCorrectDaysRemainder()
        {
            // Arrange
            TimeSpan timeSpan = TimeSpan.FromDays(365 + 31 + 10); // 1 year, 1 month, 10 days

            // Act
            int result = timeSpan.DaysRemainder();

            // Assert
            Assert.Equal(11, result);
        }

        [Fact]
        public void MonthsWithValidTimeSpanReturnsCorrectMonths()
        {
            // Arrange
            TimeSpan timeSpan = TimeSpan.FromDays(365 + 31 + 10); // 1 year, 1 month, 10 days

            // Act
            int result = timeSpan.Months();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void YearsWithValidTimeSpanReturnsCorrectYears()
        {
            // Arrange
            TimeSpan timeSpan = TimeSpan.FromDays(365.2425 * 2 + 10); // 2 years, 10 days

            // Act
            int result = timeSpan.Years();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void ToStringFullWithValidTimeSpanReturnsCorrectString()
        {
            // Arrange
            TimeSpan timeSpan = TimeSpan.FromDays(365 + 31 + 10) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(3); // 1 year, 1 month, 10 days, 1 hour, 2 minutes, 3 seconds

            // Act
            string result = timeSpan.ToStringFull();

            // Assert
            Assert.Equal("1 year, 1 month, 11 days, 1 hour, 2 minutes, 3 seconds", result);
        }
    }
}