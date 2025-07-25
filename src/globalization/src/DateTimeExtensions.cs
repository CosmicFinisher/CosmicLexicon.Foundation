namespace OpenEchoSystem.Core.xGlobalization;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

public static class DateTimeExtensions
{


    //     Adds the number of weeks to the date
    //
    // Parameters:
    //   date:
    //     Date input
    //
    //   numberOfWeeks:
    //     Number of weeks to add
    //
    // Returns:
    //     The date after the number of weeks are added
    public static DateTime AddWeeks(this DateTime date, int numberOfWeeks)
    {
        return date.AddDays(numberOfWeeks * 7);
    }

    //
    // Summary:
    //     Calculates age based on date supplied
    //
    // Parameters:
    //   date:
    //     Birth date
    //
    //   calculateFrom:
    //     Date to calculate from
    //
    // Returns:
    //     The total age in years
    public static int Age(this DateTime date, DateTime calculateFrom = default)
    {
        if (calculateFrom == default)
        {
            calculateFrom = DateTime.Now;
        }

        int age = calculateFrom.Year - date.Year;
        if (date > calculateFrom.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    //
    // Summary:
    //     Beginning of a specific time frame
    //
    // Parameters:
    //   date:
    //     Date to base off of
    //
    //   timeFrame:
    //     Time frame to use
    //
    //   culture:
    //     Culture to use for calculating (defaults to the current culture)
    //
    // Returns:
    //     The beginning of a specific time frame
    public static DateTime BeginningOf(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;

        return timeFrame switch
        {
            TimeFrame.Minute => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0),
            TimeFrame.Hour => new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0),
            TimeFrame.Day => date.Date,
            TimeFrame.Week => date.AddDays(culture.DateTimeFormat.FirstDayOfWeek - date.DayOfWeek).Date,
            TimeFrame.Month => new DateTime(date.Year, date.Month, 1),
            TimeFrame.Quarter => date.BeginningOf(TimeFrame.Quarter, date.BeginningOf(TimeFrame.Year, culture), culture),
            TimeFrame.Year => new DateTime(date.Year, 1, 1),
            _ => throw new InvalidEnumArgumentException(nameof(timeFrame), (int)timeFrame, typeof(TimeFrame)),
        };
    }

    //
    // Summary:
    //     Beginning of a specific time frame
    //
    // Parameters:
    //   date:
    //     Date to base off of
    //
    //   timeFrame:
    //     Time frame to use
    //
    //   startOfQuarter1:
    //     Start of the first quarter
    //
    //   culture:
    //     Culture to use for calculating (defaults to the current culture)
    //
    // Returns:
    //     The beginning of a specific time frame
    public static DateTime BeginningOf(this DateTime date, TimeFrame timeFrame, DateTime startOfQuarter1, CultureInfo? culture = null)
    {
        if (timeFrame != TimeFrame.Quarter)
        {
            return date.BeginningOf(timeFrame, culture);
        }

        culture ??= CultureInfo.CurrentCulture;

        return date switch
        {
            _ when date >= startOfQuarter1 && date <= startOfQuarter1.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, culture) => startOfQuarter1.Date,
            _ when date >= startOfQuarter1.AddMonths(3) && date <= startOfQuarter1.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, culture) => startOfQuarter1.AddMonths(3).Date,
            _ when date >= startOfQuarter1.AddMonths(6) && date <= startOfQuarter1.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, culture) => startOfQuarter1.AddMonths(6).Date,
            _ => startOfQuarter1.AddMonths(9).Date,
        };
    }

    //
    // Summary:
    //     Gets the number of days in the time frame specified based on the date
    //
    // Parameters:
    //   date:
    //     Date
    //
    //   timeFrame:
    //     Time frame to calculate the number of days from
    //
    //   culture:
    //     Culture to use for calculating (defaults to the current culture)
    //
    // Returns:
    //     The number of days in the time frame
    public static int DaysIn(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;

        return timeFrame switch
        {
            TimeFrame.Minute => 1,
            TimeFrame.Hour => 1,
            TimeFrame.Day => 1,
            TimeFrame.Week => 7,
            TimeFrame.Month => culture.Calendar.GetDaysInMonth(date.Year, date.Month),
            TimeFrame.Quarter => (date.EndOf(TimeFrame.Quarter, culture) - date.BeginningOf(TimeFrame.Quarter, culture)).Days + 1,
            TimeFrame.Year => culture.Calendar.GetDaysInYear(date.Year),
            _ => throw new InvalidEnumArgumentException(nameof(timeFrame), (int)timeFrame, typeof(TimeFrame)),
        };
    }

    //
    // Summary:
    //     Gets the number of days in the time frame specified based on the date
    //
    // Parameters:
    //   date:
    //     Date
    //
    //   timeFrame:
    //     Time frame to calculate the number of days from
    //
    //   startOfQuarter1:
    //     Start of the first quarter
    //
    //   culture:
    //     Culture to use for calculating (defaults to the current culture)
    //
    // Returns:
    //     The number of days in the time frame
    public static int DaysIn(this DateTime date, TimeFrame timeFrame, DateTime startOfQuarter1, CultureInfo? culture = null)
    {
        if (timeFrame != TimeFrame.Quarter)
        {
            return date.DaysIn(timeFrame, culture);
        }

        culture ??= CultureInfo.CurrentCulture;

        return date switch
        {
            _ when date >= startOfQuarter1 && date <= startOfQuarter1.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, culture) => (startOfQuarter1.AddMonths(3).AddDays(-1) - startOfQuarter1).Days + 1,
            _ when date >= startOfQuarter1.AddMonths(3) && date <= startOfQuarter1.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, culture) => (startOfQuarter1.AddMonths(6).AddDays(-1) - startOfQuarter1.AddMonths(3)).Days + 1,
            _ when date >= startOfQuarter1.AddMonths(6) && date <= startOfQuarter1.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, culture) => (startOfQuarter1.AddMonths(9).AddDays(-1) - startOfQuarter1.AddMonths(6)).Days + 1,
            _ => (startOfQuarter1.AddMonths(12).AddDays(-1) - startOfQuarter1.AddMonths(9)).Days + 1,
        };
    }

    /// <summary>
    /// Gets the beginning of the quarter for the given date
    /// </summary>
    /// <param name="date">The date to get the beginning of the quarter for</param>
    /// <returns>The beginning of the quarter</returns>
    public static DateTime BeginningOfQuarter(this DateTime date)
    {
        var month = ((date.Month - 1) / 3) * 3 + 1;
        return new DateTime(date.Year, month, 1);
    }

    /// <summary>
    /// Gets the end of the specified time frame for the given date
    /// </summary>
    /// <param name="date">The date to get the end of the time frame for</param>
    /// <param name="timeFrame">The time frame to get the end of</param>
    /// <param name="culture">The culture to use for calculations (defaults to current culture)</param>
    /// <returns>The end of the specified time frame</returns>
    public static DateTime EndOf(this DateTime date, TimeFrame timeFrame, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;

        return timeFrame switch
        {
            TimeFrame.Minute => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999),
            TimeFrame.Hour => new DateTime(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999),
            TimeFrame.Day => date.Date.AddDays(1).AddTicks(-1),
            TimeFrame.Week => date.BeginningOf(TimeFrame.Week, culture).AddDays(6).EndOf(TimeFrame.Day, culture),
            TimeFrame.Month => new DateTime(date.Year, date.Month, culture.Calendar.GetDaysInMonth(date.Year, date.Month), 23, 59, 59, 999),
            TimeFrame.Quarter => date.BeginningOfQuarter().AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, culture),
            TimeFrame.Year => new DateTime(date.Year, 12, 31, 23, 59, 59, 999),
            _ => throw new InvalidEnumArgumentException(nameof(timeFrame), (int)timeFrame, typeof(TimeFrame)),
        };
    }
}