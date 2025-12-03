namespace CosmicLexicon.Foundation.Globalization;

using CosmicLexicon.Foundation.Globalization;

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
    extension(IEnumerable<TimeSpan> list)
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
        public TimeSpan Average()
        {
            ArgumentNullException.ThrowIfNull(list);

            if (!list.Any())
            {
                return TimeSpan.Zero;
            }

            return new TimeSpan((long)list.Average(x => x.Ticks));
        }
    }

    extension(TimeSpan span)
    {
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
        public int DaysRemainder() => span.Days % 365 % 30;

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
        public int Months() => (int)(span.TotalDays % 365.2425 / 30);

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
        public int Years() => (int)(span.TotalDays / 365.2425);
    }

    extension(TimeSpan input)
    {
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
        public string ToStringFull()
        {
            StringBuilder text = new();
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
    }
}