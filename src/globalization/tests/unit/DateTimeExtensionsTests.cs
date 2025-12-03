using System.Globalization;

namespace CosmicLexicon.Foundation.Globalization.UnitTest
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void AddWeeksAddsWeeksToDate()
        {
            // Arrange
            DateTime date = new(2025, 1, 1);
            int numberOfWeeks = 2;

            // Act
            DateTime result = date.AddWeeks(numberOfWeeks);

            // Assert
            Assert.Equal(new DateTime(2025, 1, 15), result);
        }

        [Fact]
        public void AgeCalculatesAgeCorrectly()
        {
            // Arrange
            DateTime birthDate = new(1990, 5, 30);
            DateTime calculateFrom = new(2025, 5, 30);

            // Act
            int age = birthDate.Age(calculateFrom);

            // Assert
            Assert.Equal(35, age);
        }

        [Fact]
        public void AgeCalculatesAgeCorrectlyWhenCalculateFromIsDefault()
        {
            // Arrange
            DateTime birthDate = DateTime.Now.AddYears(-25); // 25 years ago
            
            // Act
            int age = birthDate.Age(); // Should use DateTime.Now as default

            // Assert
            Assert.Equal(25, age);
        }

        [Fact]
        public void AgeCalculatesAgeCorrectlyWhenBirthDateIsAfterCalculateFrom()
        {
            // Arrange
            DateTime birthDate = new(1990, 12, 31);
            DateTime calculateFrom = new(1991, 1, 1); // Only a few days after birthDate in the next year

            // Act
            int age = birthDate.Age(calculateFrom);

            // Assert
            Assert.Equal(0, age); // Should be 0 as not a full year yet
        }

        [Fact]
        public void AgeCalculatesAgeCorrectlyWhenBirthMonthDayIsAfterCalculateMonthDay()
        {
            // Arrange
            DateTime birthDate = new(1990, 6, 15);
            DateTime calculateFrom = new(2025, 5, 10); // CalculateFrom is before birth day/month in the current year

            // Act
            int age = birthDate.Age(calculateFrom);

            // Assert
            Assert.Equal(34, age); // Should be 34, not 35
        }

        [Fact]
        public void BeginningOfDayReturnsBeginningOfDay()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Day);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 0, 0, 0), result);
        }

        [Fact]
        public void BeginningOfWeekReturnsBeginningOfWeek()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0); // Friday, assuming Sunday as FirstDayOfWeek

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Week);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 25, 0, 0, 0), result); // Sunday (default for many cultures)
        }

        [Fact]
        public void BeginningOfWeekReturnsBeginningOfWeekForMondayCulture()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0); // Friday
            CultureInfo culture = new("de-DE"); // Culture where Monday is FirstDayOfWeek

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Week, culture);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 26, 0, 0, 0), result); // Monday
        }

        [Fact]
        public void BeginningOfMonthReturnsBeginningOfMonth()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Month);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 1, 0, 0, 0), result);
        }

        [Fact]
        public void BeginningOfQuarterReturnsBeginningOfQuarter()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Quarter);

            // Assert
            Assert.Equal(new DateTime(2025, 4, 1, 0, 0, 0), result);
        }

        [Fact]
        public void BeginningOfYearReturnsBeginningOfYear()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Year);

            // Assert
            Assert.Equal(new DateTime(2025, 1, 1, 0, 0, 0), result);
        }

        [Fact]
        public void BeginningOfMinuteReturnsBeginningOfMinute()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 45);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Minute);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 10, 30, 0), result);
        }

        [Fact]
        public void BeginningOfHourReturnsBeginningOfHour()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 45);

            // Act
            DateTime result = date.BeginningOf(TimeFrame.Hour);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 10, 0, 0), result);
        }

        [Theory]
        [InlineData(2024, 2, 15, TimeFrame.Month, 29)] // Leap year February
        [InlineData(2025, 2, 15, TimeFrame.Month, 28)] // Non-leap year February
        [InlineData(2025, 1, 15, TimeFrame.Month, 31)] // January
        [InlineData(2025, 4, 15, TimeFrame.Month, 30)] // April
        [InlineData(2025, 5, 15, TimeFrame.Month, 31)] // May
        public void DaysInMonthTimeFrameReturnsCorrectDays(int year, int month, int day, TimeFrame timeFrame, int expectedDays)
        {
            // Arrange
            DateTime date = new(year, month, day);

            // Act
            int result = date.DaysIn(timeFrame);

            // Assert
            Assert.Equal(expectedDays, result);
        }

        [Fact]
        public void DaysInYearTimeFrameReturnsCorrectDaysWhenCultureIsNotSpecified()
        {
            // Arrange
            DateTime date = new(2025, 6, 15);

            // Act
            int result = date.DaysIn(TimeFrame.Year);

            // Assert
            Assert.Equal(365, result);
        }

        [Fact]
        public void DaysInInvalidTimeFrameThrowsArgumentException()
        {
            // Arrange
            DateTime date = new(2025, 1, 1);

            // Act & Assert
            Assert.Throws<System.ComponentModel.InvalidEnumArgumentException>(() => date.DaysIn((TimeFrame)999));
        }

        [Fact]
        public void DaysInDayTimeFrameReturnsOne()
        {
            // Arrange
            DateTime date = new(2025, 1, 15);

            // Act
            int result = date.DaysIn(TimeFrame.Day);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void DaysInWeekTimeFrameReturnsSeven()
        {
            // Arrange
            DateTime date = new(2025, 1, 15);

            // Act
            int result = date.DaysIn(TimeFrame.Week);

            // Assert
            Assert.Equal(7, result);
        }

        [Theory]
        [InlineData(2025, 1, 1, 90)] // Q1: Jan 1 - Mar 31 (31+28+31 = 90 days)
        [InlineData(2025, 4, 1, 91)] // Q2: Apr 1 - Jun 30 (30+31+30 = 91 days)
        [InlineData(2025, 7, 1, 92)] // Q3: Jul 1 - Sep 30 (31+31+30 = 92 days)
        [InlineData(2025, 10, 1, 92)] // Q4: Oct 1 - Dec 31 (31+30+31 = 92 days)
        [InlineData(2024, 1, 1, 91)] // Leap year Q1: Jan 1 - Mar 31 (31+29+31 = 91 days)
        public void DaysInQuarterTimeFrameReturnsCorrectDays(int year, int month, int day, int expectedDays)
        {
            // Arrange
            DateTime date = new(year, month, day);
            DateTime startOfQuarter1 = new(year, 1, 1);

            // Act
            int result = date.DaysIn(TimeFrame.Quarter, startOfQuarter1);

            // Assert
            Assert.Equal(expectedDays, result);
        }

        [Theory]
        [InlineData(2025, 365)] // Non-leap year
        [InlineData(2024, 366)] // Leap year
        public void DaysInYearTimeFrameReturnsCorrectDays(int year, int expectedDays)
        {
            // Arrange
            DateTime date = new(year, 6, 15);

            // Act
            int result = date.DaysIn(TimeFrame.Year);

            // Assert
            Assert.Equal(expectedDays, result);
        }

        [Theory]
        [InlineData(2025, 1, 15, 2025, 1, 1)] // Jan -> Jan 1
        [InlineData(2025, 2, 15, 2025, 1, 1)] // Feb -> Jan 1
        [InlineData(2025, 3, 15, 2025, 1, 1)] // Mar -> Jan 1
        [InlineData(2025, 4, 15, 2025, 4, 1)] // Apr -> Apr 1
        [InlineData(2025, 5, 15, 2025, 4, 1)] // May -> Apr 1
        [InlineData(2025, 6, 15, 2025, 4, 1)] // Jun -> Apr 1
        [InlineData(2025, 7, 15, 2025, 7, 1)] // Jul -> Jul 1
        [InlineData(2025, 8, 15, 2025, 7, 1)] // Aug -> Jul 1
        [InlineData(2025, 9, 15, 2025, 7, 1)] // Sep -> Jul 1
        [InlineData(2025, 10, 15, 2025, 10, 1)] // Oct -> Oct 1
        [InlineData(2025, 11, 15, 2025, 10, 1)] // Nov -> Oct 1
        [InlineData(2025, 12, 15, 2025, 10, 1)] // Dec -> Oct 1
        public void BeginningOfQuarterReturnsCorrectDate(int year, int month, int day, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            DateTime date = new(year, month, day);

            // Act
            DateTime result = date.BeginningOfQuarter();

            // Assert
            Assert.Equal(new DateTime(expectedYear, expectedMonth, expectedDay, 0, 0, 0), result);
        }

        [Fact]
        public void EndOfDayTimeFrameReturnsEndOfDay()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0);

            // Act
            DateTime result = date.EndOf(TimeFrame.Day);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 23, 59, 59).AddTicks(9999999), result);
        }

        [Fact]
        public void EndOfMinuteReturnsEndOfMinute()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 45, 123);

            // Act
            DateTime result = date.EndOf(TimeFrame.Minute);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 10, 30, 59, 999), result);
        }

        [Fact]
        public void EndOfHourReturnsEndOfHour()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 45, 123);

            // Act
            DateTime result = date.EndOf(TimeFrame.Hour);

            // Assert
            Assert.Equal(new DateTime(2025, 5, 30, 10, 59, 59, 999), result);
        }

        [Fact]
        public void EndOfInvalidTimeFrameThrowsArgumentException()
        {
            // Arrange
            DateTime date = new(2025, 1, 1);

            // Act & Assert
            Assert.Throws<System.ComponentModel.InvalidEnumArgumentException>(() => date.EndOf((TimeFrame)999));
        }

        [Fact]
        public void EndOfWeekTimeFrameReturnsEndOfWeek()
        {
            // Arrange
            DateTime date = new(2025, 5, 30, 10, 30, 0); // Friday, assuming Sunday as FirstDayOfWeek

            // Act
            DateTime result = date.EndOf(TimeFrame.Week);

            // Assert
            // End of week for a Friday (May 30) assuming Sunday is start of week is Saturday (May 31) 23:59:59.999
            Assert.Equal(new DateTime(2025, 5, 31, 23, 59, 59).AddTicks(9999999), result);
        }

        [Theory]
        [InlineData(2025, 1, 31)] // January has 31 days
        [InlineData(2025, 2, 28)] // February (non-leap) has 28 days
        [InlineData(2024, 2, 29)] // February (leap) has 29 days
        [InlineData(2025, 4, 30)] // April has 30 days
        public void EndOfMonthTimeFrameReturnsEndOfMonth(int year, int month, int expectedDay)
        {
            // Arrange
            DateTime date = new(year, month, 15, 10, 30, 0);

            // Act
            DateTime result = date.EndOf(TimeFrame.Month);

            // Assert
            Assert.Equal(new DateTime(year, month, expectedDay, 23, 59, 59, 999), result);
        }

        [Theory]
        [InlineData(2025, 1, 15, 2025, 3, 31)] // Q1
        [InlineData(2025, 4, 15, 2025, 6, 30)] // Q2
        [InlineData(2025, 7, 15, 2025, 9, 30)] // Q3
        [InlineData(2025, 10, 15, 2025, 12, 31)] // Q4
        [InlineData(2024, 1, 15, 2024, 3, 31)] // Leap year Q1
        public void EndOfQuarterTimeFrameReturnsEndOfQuarter(int year, int month, int day, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            DateTime date = new(year, month, day, 10, 30, 0);

            // Act
            DateTime result = date.EndOf(TimeFrame.Quarter);

            // Assert
            Assert.Equal(new DateTime(expectedYear, expectedMonth, expectedDay, 23, 59, 59).AddTicks(9999999), result);
        }

        [Theory]
        [InlineData(2025, 2025, 12, 31)] // Non-leap year
        [InlineData(2024, 2024, 12, 31)] // Leap year
        public void EndOfYearTimeFrameReturnsEndOfYear(int year, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            DateTime date = new(year, 6, 15, 10, 30, 0);

            // Act
            DateTime result = date.EndOf(TimeFrame.Year);

            // Assert
            Assert.Equal(new DateTime(expectedYear, expectedMonth, expectedDay, 23, 59, 59, 999), result);
        }
    }
}