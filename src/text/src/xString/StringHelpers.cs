using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenEchoSystem.Core.xText.xString
{
    //
    // Summary:
    //     String helpers
    public static partial class StringHelpers
    {
        
        /// <summary>
        /// Formats a string with the specified format and objects.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="objects">An array of objects to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException">format is null.</exception>
        public static string FormatString([StringSyntax("CompositeFormat")] string format, params object?[] objects)
        {
            ArgumentNullException.ThrowIfNull(format);
            return string.Format(CultureInfo.CurrentCulture, format, objects);
        }
        //
        // Summary:
        //     The strip HTML regex
        private static readonly Regex STRIP_HTML_REGEX =   new(@"<[^>]*>", RegexOptions.Compiled);

        //
        // Summary:
        //     Gets the is unicode.
        //
        // Value:
        //     The is unicode.
        private static Regex IsUnicode { get; } = new(@"[^\u0000-\u007F]", RegexOptions.Compiled);
                        

        //
        // Summary:
        //     Adds spaces to a string before capital letters. Acronyms are respected and left
        //     alone.
        //
        // Parameters:
        //   input:
        //     The input.
        //
        // Returns:
        //     The resulting string.
        public static string AddSpaces(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new (input!.Length * 2);
            stringBuilder.Append(input![0]);
            int i = 1;
            for (int length = input!.Length; i < length; i++)
            {
                if (char.IsUpper(input![i]) && (input![i - 1] != ' ' && !char.IsUpper(input![i - 1]) || char.IsUpper(input![i - 1]) && i < input!.Length - 1 && !char.IsUpper(input![i + 1]) && input![i + 1] != ' '))
                {
                    stringBuilder.Append(' ');
                }

                stringBuilder.Append(input![i]);
            }

            return stringBuilder.ToString();
        }

        //
        // Summary:
        //     Centers the input string (if it's longer than the length) and pads it using the
        //     padding string
        //
        // Parameters:
        //   input:
        //
        //   length:
        //
        //   padding:
        //
        // Returns:
        //     The centered string
        /// <summary>
        /// Centers the input string within a specified length using a padding character.
        /// </summary>
        /// <param name="input">The string to center.</param>
        /// <param name="length">The total length of the resulting string.</param>
        /// <param name="padding">The string to use for padding. Defaults to a single space.</param>
        /// <returns>The centered string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">length is less than 0.</exception>
        public static string Center(string? input, int length, string padding = " ")
        {
            ArgumentOutOfRangeException.ThrowIfNegative(length);
            
            input ??= string.Empty;
            padding ??= " ";
            
            if (padding.Length == 0 || input.Length >= length)
                return input;

            var leftPadding = (length - input.Length) / 2;
            var rightPadding = length - input.Length - leftPadding;

            StringBuilder sb = new(length);
            sb.Append(padding[0], leftPadding);
            sb.Append(input);
            sb.Append(padding[0], rightPadding);
            return sb.ToString();
        }

        //
        // Summary:
        //     Is this value of the specified type
        //
        // Parameters:
        //   value:
        //     Value to compare
        //
        //   comparisonType:
        //     Comparison type
        //
        // Returns:
        //     True if it is of the type specified, false otherwise
        public static bool Is(string? value, StringCompare comparisonType)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (comparisonType)
            {
                case StringCompare.CreditCard:
                    {
                        long num = 0L;
                        value = string.Join("", value!.Replace("-", "", StringComparison.Ordinal).Reverse());
                        for (int i = 0; i < value!.Length; i++)
                        {
                            if (!char.IsDigit(value![i])) 
                            {
                                return false;
                            }

                            for (int num2 = (value![i] - 48) * (i % 2 != 1 ? 1 : 2); num2 > 0; num2 /= 10)
                            {
                                num += num2 % 10;
                            }
                        }

                        return num % 10 == 0;
                    }
                case StringCompare.Anagram:
                    return Is(value, "", StringCompare.Anagram);
                case StringCompare.Unicode:
                    if (!string.IsNullOrEmpty(value))
                    {
                        return IsUnicode.Replace(value, "") != value;
                    }
                    return true;
                default:
                    return false;
            }
        }
        //
        // Summary:
        //     Is this value of the specified type
        //
        // Parameters:
        //   value1:
        //     Value 1 to compare
        //
        //   value2:
        //     Value 2 to compare
        //
        //   comparisonType:
        //     Comparison type
        //
        // Returns:
        //     True if it is of the type specified, false otherwise
        public static bool Is(string? value1, string value2, StringCompare comparisonType)
        {
            if (comparisonType == StringCompare.Anagram)
            {
                return new string((from x in value1?.ToCharArray()
                                   orderby x
                                   select x).ToArray()) == new string((from x in value2?.ToCharArray()
                                                                       orderby x
                                                                       select x).ToArray());
            }

            return StringHelpers.Is(value1, comparisonType); // Adjusted
        }

        //
        // Summary:
        //     Removes everything that is not in the filter text from the input.
        //
        // Parameters:
        //   input:
        //     Input text
        //
        //   filter:
        //     Regex expression of text to keep
        //
        // Returns:
        //     The input text minus everything not in the filter text.
        public static string Keep(string? input, string filter)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(filter))
            {
                return string.Empty;
            }

            Regex regex = new Regex(filter);
            MatchCollection matchCollection = regex.Matches(input);
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            for (int count = matchCollection.Count; i < count; i++)
            {
                Match match = matchCollection[i];
                stringBuilder.Append(match.Value);
            }

            return stringBuilder.ToString();
        }

        public static string Left(string? input, int length)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            if (length <= 0)
            {
                return string.Empty;
            }

            if (length >= input.Length)
            {
                return input;
            }
            return input.Substring(0, length);
        }

        public static int LevenshteinDistance(string? value1, string value2)
        {
            ArgumentNullException.ThrowIfNull(value1);
            ArgumentNullException.ThrowIfNull(value2);

            if (value1.Length == 0) return value2.Length;
            if (value2.Length == 0) return value1.Length;

            int[,] d = new int[value1.Length + 1, value2.Length + 1];

            for (int i = 0; i <= value1.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= value2.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= value2.Length; j++)
            {
                for (int i = 1; i <= value1.Length; i++)
                {
                    int cost = value1[i - 1] == value2[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[value1.Length, value2.Length];
        }

        public static string MaskLeft(string? input, int endPosition = 4, char mask = '#')
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            endPosition = Math.Min(endPosition, input.Length);
            char[] maskedChars = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                if (i < endPosition)
                    maskedChars[i] = input[i];
                else
                    maskedChars[i] = mask;
            }
            return new string(maskedChars);
        }

        public static string MaskRight(string? input, int startPosition = 4, char mask = '#')
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            startPosition = Math.Max(0, input.Length - startPosition);
             char[] maskedChars = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                if (i < startPosition)
                    maskedChars[i] = mask;
                else
                    maskedChars[i] = input[i];
            }
            return new string(maskedChars);
        }

        public static string Minify(string? Input, MinificationType Type = MinificationType.HTML)
        {
            return Type switch
            {
                MinificationType.HTML => HTMLMinify(Input ?? string.Empty),
                MinificationType.CSS => CSSMinify(Input ?? string.Empty),
                MinificationType.JavaScript => JavaScriptMinify(Input ?? string.Empty),
                _ => Input ?? string.Empty,
            };
        }

        public static int NumberTimesOccurs(string? input, string match)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(match))
            {
                return 0;
            }
            int count = 0;
            int i = 0;
            while ((i = input.IndexOf(match, i, StringComparison.Ordinal)) != -1)
            {
                i += match.Length;
                count++;
            }
            return count;
        }

        public static string? Remove(string? input, string filter)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(filter))
                return input;
            return new Regex(filter).Replace(input, "");
        }

        public static string? Remove(string? input, StringFilter filter)
        {
            return Replace(input, filter, ""); // Adjusted
        }

        public static string RemoveDiacritics(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder(normalizedString.Length);

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Replace(string? input, StringFilter filter, string value = "")
        {
             if (input == null) return string.Empty;
            return new Regex(BuildFilter(filter)).Replace(input, value);
        }

        public static string ReplaceAll(string? input, IEnumerable<KeyValuePair<string, string>>? replacements)
        {
            if (string.IsNullOrEmpty(input) || replacements == null || !replacements.Any())
                return input ?? string.Empty;

            StringBuilder sb = new StringBuilder(input);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }
            return sb.ToString();
        }

        public static string ReplaceAll(string? value, char[] oldChars, char newChar)
        {
            if (string.IsNullOrEmpty(value) || oldChars == null || oldChars.Length == 0)
                return value ?? string.Empty;
            
            StringBuilder sb = new StringBuilder(value.Length);
            foreach (char c in value)
            {
                sb.Append(oldChars.Contains(c) ? newChar : c);
            }
            return sb.ToString();
        }

        public static string Reverse(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string Right(string? input, int length)
        {
             if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            if (length <= 0)
            {
                return string.Empty;
            }

            if (length >= input.Length)
            {
                return input;
            }
            return input.Substring(input.Length - length);
        }

        public static string StripHTML(string? html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            return STRIP_HTML_REGEX.Replace(html, string.Empty);
        }

        public static string StripIllegalXML(string? content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char item in content)
            {
                // XML 1.0 legal characters:
                // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD]
                // Surrogate pairs are handled automatically by .NET
                if (item == '\u0009' || item == '\u000A' || item == '\u000D' ||
                    item >= '\u0020' && item <= '\uD7FF' ||
                    item >= '\uE000' && item <= '\uFFFD')
                {
                    stringBuilder.Append(item);
                }
                // Note: Characters above U+FFFF are handled automatically by .NET's UTF-16 encoding
                // There's no need to explicitly check for them as they're represented as surrogate pairs
            }
            return stringBuilder.ToString();
        }

        public static string? StripLeft(string? input, string characters = " ")
        {
             if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(characters))
                return input;
            
            int i = 0;
            while(i < input.Length && characters.Contains(input[i]))
            {
                i++;
            }
            return input.Substring(i);
        }

        public static string? StripRight(string? input, string characters = " ")
        {
             if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(characters))
                return input;
            
            int i = input.Length -1;
            while(i >= 0 && characters.Contains(input[i]))
            {
                i--;
            }
            return input.Substring(0, i + 1);
        }

        public static string ToBase64String(string? input, Encoding? originalEncodingUsing = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            originalEncodingUsing ??= Encoding.UTF8;
            byte[] bytes = originalEncodingUsing.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static byte[] ToByteArray(string? input, Encoding? encodingUsing = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Array.Empty<byte>();
            }
            encodingUsing ??= Encoding.UTF8;
            return encodingUsing.GetBytes(input);
        }

        public static string ToString(string? input, StringCase caseOfString, CultureInfo? provider = null)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            provider ??= CultureInfo.CurrentCulture;

            return caseOfString switch
            {
                StringCase.Lower => input.ToLower(provider),
                StringCase.Upper => input.ToUpper(provider),
                StringCase.Title => CapitalizeSentence(input, provider.TextInfo),
                StringCase.Invert => string.Join("", input.Select(c => 
                    char.IsUpper(c) ? char.ToLower(c, provider) : char.ToUpper(c, provider))),
                _ => input,
            };
        }

        private static string CapitalizeSentence(string input, TextInfo textInfo)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var sentences = input.Split(SENTENCE_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", sentences.Select(s => textInfo.ToTitleCase(s)));
        }

        public static string ToString(string? input, string? format = null)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            try
            {
                return string.Format(CultureInfo.CurrentCulture, format!, input);
            }
            catch (FormatException)
            {
                return input;
            }
        }

        public static string ToString(string? input, IFormatProvider? provider)
        {
            return input ?? string.Empty;
        }

        public static string ToString(string? input, string format, IStringFormatter? formatter)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            return formatter?.Format(input, format) ?? string.Format(CultureInfo.CurrentCulture, format, input);
        }

        public static bool GetIsUnicode(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return input.Any(c => c > 127);
        }

        private static readonly string[] SENTENCE_SEPARATORS = new[] { ". ", "! ", "? " };

        public static string ToString(string? input, string format, IFormatProvider? provider = null)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            try
            {
                return string.Format(provider ?? CultureInfo.CurrentCulture, format, input);
            }
            catch (FormatException)
            {
                return input;
            }
        }

        public static string? ToString(string? input, object? inputObject, string startSeperator = "{", string endSeperator = "}")
        {
            if (input == null || inputObject == null)
                return string.Empty;

            ArgumentNullException.ThrowIfNull(startSeperator);
            ArgumentNullException.ThrowIfNull(endSeperator);

            Type type = inputObject.GetType();
            List<PropertyInfo> props = new(type.GetProperties());

            string result = input;
            foreach (PropertyInfo prop in props.Where(p => p.CanRead))
            {
                string name = prop.Name;
                object? value = prop.GetValue(inputObject, null);
                string oldValue = startSeperator + name + endSeperator;
                if (value != null)
                    result = result.Replace(oldValue, value.ToString(), StringComparison.Ordinal);
            }

            return result;
        }

        public static string ToString(string? input, string format, string outputFormat, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            try
            {
                var regex = new Regex(format, options | RegexOptions.CultureInvariant);
                return regex.Replace(input, outputFormat);
            }
            catch (ArgumentException)
            {
                // Invalid regex pattern
                return input;
            }
        }

        public static string? ToString(string? input, params KeyValuePair<string, string>[] pairs)
        {
            if (input == null)
                return string.Empty;

            string result = input;
            foreach (KeyValuePair<string, string> pair in pairs)
            {
                result = result.Replace(pair.Key, pair.Value, StringComparison.Ordinal);
            }

            return result;
        }

        private static string BuildFilter(StringFilter filter)
        {
            return filter switch
            {
                StringFilter.Numbers => "[0-9]",
                StringFilter.Letters => "[a-zA-Z]",
                StringFilter.Alphanumeric => "[a-zA-Z0-9]",
                StringFilter.NonAlphanumeric => "[^a-zA-Z0-9]",
                StringFilter.Whitespace => "\\s",
                StringFilter.NonWhitespace => "\\S",
                StringFilter.Punctuation => "\\p{P}",
                StringFilter.NonPunctuation => "[^\\p{P}]",
                StringFilter.Symbols => "\\p{S}",
                StringFilter.NonSymbols => "[^\\p{S}]",
                StringFilter.Hexadecimal => "[0-9a-fA-F]",
                StringFilter.NonHexadecimal => "[^0-9a-fA-F]",
                StringFilter.Binary => "[01]",
                StringFilter.NonBinary => "[^01]",
                _ => string.Empty,
            };
        }

        private static string CSSMinify(string input)
        {
            string pattern = "(/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/)|(^\\s*//.*$)|(^\\s*/\\*.*\\*/$)";
            Regex commentRegex = new Regex(pattern, RegexOptions.Multiline);
            input = commentRegex.Replace(input, string.Empty);
            Regex whitespaceRegex = new Regex("\\s+");
            input = whitespaceRegex.Replace(input, " ");
            input = input.Replace("} ", "}");
            input = input.Replace("{ ", "{");
            input = input.Replace(": ", ":");
            input = input.Replace("; ", ";");
            input = input.Replace(", ", ",");
            input = input.Replace("} ", "}");
            input = input.Replace(" {", "{");
            input = input.Replace(" :", ":");
            input = input.Replace(" ;", ";");
            input = input.Replace(" ,", ",");
            return input.Trim();
        }

        private static string Evaluate(Match matcher)
        {
            return string.Empty;
        }

        private static string HTMLMinify(string input)
        {
            input = Regex.Replace(input, @"\s+", " ");
            input = Regex.Replace(input, @"<!--.*?-->", string.Empty, RegexOptions.Singleline);
            input = Regex.Replace(input, @"(?<=[>])\s+(?=[<])", string.Empty);
            return input.Trim();
        }

        private static string JavaScriptMinify(string input)
        {
            // Remove comments
            input = Regex.Replace(input, @"/\*[\s\S]*?\*/", string.Empty);
            input = Regex.Replace(input, @"//[^\r\n]*", string.Empty);

            // Remove whitespace
            input = Regex.Replace(input, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            input = Regex.Replace(input, @"\t", " ");
            input = Regex.Replace(input, @"\r", string.Empty);
            input = Regex.Replace(input, @"\n", string.Empty);
            input = Regex.Replace(input, @"\s+", " ");

            // Remove unnecessary characters
            input = Regex.Replace(input, @"\s?([;:{},=()])\s?", "$1");

            return input.Trim();
        }

        public static T? Parse<T>(string? input, CultureInfo? formatProvider = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default;
            }

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                {
                    return (T?)converter.ConvertFromString(input);
                }
            }
            catch (Exception)
            {
                // Conversion failed
                return default;
            }
            return default;
        }

        [Flags]
        public enum StringFilter
        {
            None = 0,
            Numbers = 1,
            Letters = 2,
            Alphanumeric = 4,
            NonAlphanumeric = 8,
            Whitespace = 16,
            NonWhitespace = 32,
            Punctuation = 64,
            NonPunctuation = 128,
            Symbols = 256,
            NonSymbols = 512,
            Hexadecimal = 1024,
            NonHexadecimal = 2048,
            Binary = 4096,
            NonBinary = 8192
        }

        public static string GetCamelCase(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = input.Trim();
            var words = Regex.Split(input, @"[^A-Za-z0-9]+", RegexOptions.Compiled | RegexOptions.CultureInvariant)
                            .Where(w => !string.IsNullOrEmpty(w))
                            .ToArray();

            if (words.Length == 0)
                return string.Empty;

            var sb = new StringBuilder(input.Length);
            sb.Append(words[0].ToLowerInvariant());

            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    sb.Append(char.ToUpperInvariant(words[i][0]));
                    if (words[i].Length > 1)
                        sb.Append(words[i][1..].ToLowerInvariant());
                }
            }

            return sb.ToString();
        }

        public static string FromLowerCaseToPascalCase(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLowerInvariant());
        }

        // From https://stackoverflow.com/a/9865388
        // Used to calculate line numbers for error messages
        public static int[] GetLineLengths(string text)
        {
            List<int> lineLengths = new List<int>();

            if (text != null)
            {
                using (StringReader reader = new StringReader(text))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineLengths.Add(line.Length);
                    }
                }
            }
            return lineLengths.ToArray();
        }

        private enum ChunkContentType // Moved from TextUtilities
        {
            Text,
            OpenBrace,
            CloseBrace
        }

        public static IEnumerable<string> SplitSemicolonSeparatedList(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                yield break;

            int start = 0;
            bool inQuote = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '"')
                    inQuote = !inQuote;

                if (!inQuote && text[i] == ';')
                {
                    yield return text.Substring(start, i - start).Trim();
                    start = i + 1;
                }
            }

            yield return text.Substring(start).Trim();
        }

        public static string StripQuotes(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (text.StartsWith('"') && text.EndsWith('"'))
            {
                // Remove the first and last character
                return text.Substring(1, text.Length - 2);
            }
            return text;
        }

        public static string Concat(IEnumerable<string> enumeration, char token, bool includeWhitespace) // Refactored
        {
            if (enumeration == null || !enumeration.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (string item in enumeration)
            {
                if (!includeWhitespace && string.IsNullOrWhiteSpace(item))
                    continue;

                sb.Append(item);
                sb.Append(token);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1); // Remove trailing token

            return sb.ToString();
        }

        public static string Concat(IEnumerable<string> enumeration) // Refactored
        {
            if (enumeration == null || !enumeration.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (string item in enumeration)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> enumerable1, params IEnumerable<T>[] additions)
        {
            if (enumerable1 == null)
            {
                enumerable1 = [];
            }


            if (additions == null || additions.Length == 0)
            {
                return enumerable1;
            }


            ArrayPool<IEnumerable<T>> shared = ArrayPool<IEnumerable<T>>.Shared;
            IEnumerable<T>[] array = shared.Rent(additions.Length + 1);
            array[0] = enumerable1;
            Array.Copy(additions, 0, array, 1, additions.Length);
            T[] result = array.Where((x) => x != null).SelectMany((x) => x).ToArray();
            shared.Return(array);
            return result;
        }

        public static string Concat(IEnumerable<string> enumeration, Func<string, string> action) // Refactored
        {
            if (enumeration == null || !enumeration.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (string item in enumeration)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;

                sb.Append(action(item));
            }

            return sb.ToString();
        }

        public static string Format(string format, params object?[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        public static string Format(string format, object arg0, object arg1, object arg2, IFormatProvider? formatProvider = null)
        {
            formatProvider ??= CultureInfo.CurrentCulture;
            return string.Format(formatProvider, format, arg0, arg1, arg2);
        }

        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            return Format(format, arg0, arg1, arg2, CultureInfo.CurrentCulture);
        }

        public static string ToString(string? input, string format, bool useFormatter = false)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            var formatter = useFormatter ? GetDefaultFormatter() : null;
            return formatter?.Format(input, format) ?? string.Format(CultureInfo.CurrentCulture, format, input);
        }

        private static IStringFormatter? GetDefaultFormatter()
        {
            // Return your default formatter implementation here
            return null;  
        }

        /// <summary>
        /// Creates a string representation with custom formatting.
        /// </summary>
        public static string FormatWith(string? input, string format, IStringFormatter? formatter = null)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            return formatter?.Format(input, format) ?? string.Format(CultureInfo.CurrentCulture, format, input);
        }

        /// <summary>
        /// Formats a string using object properties as named parameters.
        /// </summary>
        public static string FormatWithObject(string? input, object inputObject, string startSeperator = "{", string endSeperator = "}")
        {
            if (input == null || inputObject == null)
                return string.Empty;

            ArgumentNullException.ThrowIfNull(startSeperator);
            ArgumentNullException.ThrowIfNull(endSeperator);

            Type type = inputObject.GetType();
            List<PropertyInfo> props = new(type.GetProperties());

            string result = input;
            foreach (PropertyInfo prop in props.Where(p => p.CanRead))
            {
                string name = prop.Name;
                object? value = prop.GetValue(inputObject, null);
                string oldValue = startSeperator + name + endSeperator;
                if (value != null)
                    result = result.Replace(oldValue, value.ToString(), StringComparison.Ordinal);
            }

            return result;
        }

        [return: NotNullIfNotNull("format")]
        public static string? Format(string? format, object? arg0)
        {
            if (format == null) return null;
            return string.Format(CultureInfo.CurrentCulture, format, arg0);
        }

        public static string FormatWithFormatter(string? input, string format, IStringFormatter? formatter)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return input;

            if (formatter == null)
                return ToString(input, format);

            return formatter.Format(input, format) ?? input;
        }
    }
}