namespace CosmicLexicon.Foundation.xGlobalization;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

//
// Summary:
//     TimeSpan extension methods
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TimeSpanExtensions
{
    //
    // Summary:
    //     Averages a list of TimeSpans
    //
    // Parameters:
    //   list:
    //     List of TimeSpans
    //
    // Returns:
    //     The average value
    public static TimeSpan Average(this IEnumerable<TimeSpan> list)
    {
        if (list is null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (!list.Any())
        {
            return TimeSpan.Zero;
        }

        return new TimeSpan((long)list.Average(x => x.Ticks));
    }

    //
    // Summary:
    //     Days in the TimeSpan minus the months and years
    //
    // Parameters:
    //   span:
    //     TimeSpan to get the days from
    //
    // Returns:
    //     The number of days minus the months and years that the TimeSpan has
    public static int DaysRemainder(this TimeSpan span)
    {
        return span.Days % 365 % 30;
    }

    //
    // Summary:
    //     Months in the TimeSpan
    //
    // Parameters:
    //   span:
        //     TimeSpan to get the months from
        //
        // Returns:
        //     The number of months that the TimeSpan has
        public static int Months(this TimeSpan span)
        {
            return (int)(span.TotalDays % 365.2425 / 30);
        }

        //
        // Summary:
        //     Converts the input to a string in this format: (Years) years, (Months) months,
        //     (DaysRemainder) days, (Hours) hours, (Minutes) minutes, (Seconds) seconds
        //
        // Parameters:
        //   input:
        //     input TimeSpan
        //
        // Returns:
        //     The TimeSpan as a string
        public static string ToStringFull(this TimeSpan input)
        {
            StringBuilder text = new StringBuilder();
            string text2 = "";
            if (input.Years() > 0)
            {
                text.Append(input.Years());
                text.Append(" year");
                text.Append(input.Years() > 1 ? "s" : "");
                text2 = ", ";
            }

            if (input.Months() > 0)
            {
                text.Append(text2);
                text.Append(input.Months());
                text.Append(" month");
                text.Append(input.Months() > 1 ? "s" : "");
                text2 = ", ";
            }

            if (input.DaysRemainder() > 0)
            {
                text.Append(text2);
                text.Append(input.DaysRemainder());
                text.Append(" day");
                text.Append(input.DaysRemainder() > 1 ? "s" : "");
                text2 = ", ";
            }

            if (input.Hours > 0)
            {
                text.Append(text2);
                text.Append(input.Hours);
                text.Append(" hour");
                text.Append(input.Hours > 1 ? "s" : "");
                text2 = ", ";
            }

            if (input.Minutes > 0)
            {
                text.Append(text2);
                text.Append(input.Minutes);
                text.Append(" minute");
                text.Append(input.Minutes > 1 ? "s" : "");
                text2 = ", ";
            }

            if (input.Seconds > 0)
            {
                text.Append(text2);
                text.Append(input.Seconds);
                text.Append(" second");
                text.Append(input.Seconds > 1 ? "s" : "");
            }

            return text.ToString();
        }

        //
        // Summary:
        //     Years in the TimeSpan
        //
        // Parameters:
        //   span:
        //     TimeSpan to get the years from
        //
        // Returns:
        //     The number of years that the TimeSpan has
        public static int Years(this TimeSpan span)
        {
            return (int)(span.TotalDays / 365.2425);
        }
}