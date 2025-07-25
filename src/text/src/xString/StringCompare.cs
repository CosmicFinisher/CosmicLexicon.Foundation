using System.Collections.Generic;

namespace OpenEchoSystem.Core.xText.xString // Updated namespace
{
    //
    // Summary:
    //     What type of string comparison are we doing?
    public enum StringCompare
    {
        //
        // Summary:
        //     Is this a credit card number?
        CreditCard,
        //
        // Summary:
        //     Is this an anagram?
        Anagram,
        //
        // Summary:
        //     Is this Unicode
        Unicode
    }
} 