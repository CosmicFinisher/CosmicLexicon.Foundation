using System.Text;
using System.ComponentModel;

namespace CosmicLexicon.Foundation.Formats
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ByteArrayHelpers
    {
        //
        // Summary:
        //     Determines if a byte array is unicode
        //
        // Parameters:
        //   input:
        //     Input array
        //
        // Returns:
        //     True if it's unicode, false otherwise
        public static bool IsUnicode(byte[] input)
        {
            // Check for Unicode BOM (UTF-16LE or UTF-16BE)
            return input != null && input.Length >= 2 && (input[0] == 0xFF && input[1] == 0xFE || input[0] == 0xFE && input[1] == 0xFF);
        }

        //
        // Summary:
        //     Converts a byte array into a base 64 string
        //
        // Parameters:
        //   input:
        //     Input array
        //
        //   index:
        //     Index to start at
        //
        //   count:
        //     Number of bytes starting at the index to convert (use -1 for the entire array
        //     starting at the index)
        //
        // Returns:
        //     The equivalent byte array in a base 64 string
        public static string ToString(byte[] input, int index = 0, int count = -1)
        {
            if (input == null)
            {
                return string.Empty;
            }

            if (index < 0) index = 0;
            if (count < 0) count = input.Length - index;
            if (index + count > input.Length) count = input.Length - index;
            if (index >= input.Length || count <= 0) return string.Empty;

            return Convert.ToBase64String(input, index, count);
        }

        //
        // Summary:
        //     Converts a byte array to a string
        //
        // Parameters:
        //   input:
        //     input array
        //
        //   encodingUsing:
        //     The type of encoding the string is using (defaults to UTF8)
        //
        //   index:
        //     Index to start at
        //
        //   count:
        //     Number of bytes starting at the index to convert (use -1 for the entire array
        //     starting at the index)
        //
        // Returns:
        //     string of the byte array
        public static string ToString(byte[] input, Encoding? encodingUsing, int index = 0, int count = -1)
        {
            if (input == null)
            {
                return string.Empty;
            }

            if (index < 0) index = 0;
            if (count < 0) count = input.Length - index;
            if (index + count > input.Length) count = input.Length - index;
            if (index >= input.Length || count <= 0) return string.Empty;

            encodingUsing ??= Encoding.UTF8;

            return encodingUsing.GetString(input, index, count);
        }
    }
} 