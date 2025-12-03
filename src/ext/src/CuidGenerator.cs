namespace CosmicLexicon.Foundation.Extensions;

/// <summary>
/// Generate CUIDs (Custom Unique Identifier (ID))
/// </summary>
public static class CuidGenerator
{
    private static readonly char[] ValidChars;

    static CuidGenerator()
    {
        // Initialize valid characters array once
        var chars = new List<char>(62);
        chars.AddRange(Enumerable.Range('A', 26).Select(x => (char)x)); // Uppercase A-Z
        chars.AddRange(Enumerable.Range('a', 26).Select(x => (char)x)); // Lowercase a-z
        chars.AddRange(Enumerable.Range('0', 10).Select(x => (char)x)); // Numbers 0-9
        ValidChars = [.. chars];
    }

    /// <summary>
    /// Generate CUID with default length
    /// </summary>
    /// <returns>A cryptographically secure random string of 11 characters</returns>
    public static string NewCuid()
    {
        return NewCuid(11);
    }

    /// <summary>
    /// Generate CUID with custom length in string format
    /// </summary>
    /// <param name="length">The Length of CUID. Must be greater than 0.</param>
    /// <returns>A cryptographically secure random string of the specified length</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when length is 0</exception>
    public static string NewCuid(byte length)
    {
        ArgumentOutOfRangeException.ThrowIfZero(length);
        if (length > ValidChars.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length), $"Length must be less than or equal to {ValidChars.Length}");
        }

        var result = new char[length];

        // Generate random bytes (using one byte per character)
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            // Convert random bytes to characters
            for (int i = 0; i < length; i++)
            {
                result[i] = ValidChars[bytes[i] % ValidChars.Length];
            }
        }

        return new string(result);
    }
}