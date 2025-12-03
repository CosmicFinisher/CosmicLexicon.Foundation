using Xunit;
using CosmicLexicon.Foundation.xGlobalization;

namespace CosmicLexicon.Foundation.xGlobalization
{
    public class TimeFrameTests
    {
        [Fact]
        public void TimeFrameDayHasCorrectValue()
        {
            // Arrange

            // Act
            TimeFrame timeFrame = TimeFrame.Day;

            // Assert
            Assert.Equal(0, (int)timeFrame);
        }

        [Fact]
        public void TimeFrameWeekHasCorrectValue()
        {
            // Arrange

            // Act
            TimeFrame timeFrame = TimeFrame.Week;

            // Assert
            Assert.Equal(3, (int)timeFrame);
        }

        [Fact]
        public void TimeFrameMonthHasCorrectValue()
        {
            // Arrange

            // Act
            TimeFrame timeFrame = TimeFrame.Month;

            // Assert
            Assert.Equal(4, (int)timeFrame);
        }

        [Fact]
        public void TimeFrameQuarterHasCorrectValue()
        {
            // Arrange

            // Act
            TimeFrame timeFrame = TimeFrame.Quarter;

            // Assert
            Assert.Equal(5, (int)timeFrame);
        }

        [Fact]
        public void TimeFrameYearHasCorrectValue()
        {
            // Arrange

            // Act
            TimeFrame timeFrame = TimeFrame.Year;

            // Assert
            Assert.Equal(6, (int)timeFrame);
        }
    }
}