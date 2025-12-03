namespace CosmicLexicon.Foundation.Globalization;

using System;

// Summary:
//     Date comparison type
[Flags]
public enum DateCompare
{
    //
    // Summary:
    //     The none
    None = 0x0,
    //
    // Summary:
    //     In the future
    InFuture = 0x1,
    //
    // Summary:
    //     In the past
    InPast = 0x2,
    //
    // Summary:
    //     Today
    Today = 0x4,
    //
    // Summary:
    //     Weekday
    WeekDay = 0x8,
    //
    // Summary:
    //     Weekend
    WeekEnd = 0x10
} 