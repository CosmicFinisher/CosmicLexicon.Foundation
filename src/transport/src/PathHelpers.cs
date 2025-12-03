using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLexicon.Foundation.Transport
{
    /// <summary>
    /// Provides helper methods for working with file paths.
    /// </summary>
    public static class PathHelpers
    {

        /// <summary>
        /// Quotes a path if it contains spaces and is not already quoted.
        /// </summary>
        /// <param name="path">The path to quote.</param>
        /// <returns>The quoted path, or the original path if it doesn't need quoting.</returns>
        public static string? QuoteIfNeeded(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (path.Contains(' ') && !path.StartsWith("\"") && !path.EndsWith("\""))
            {
                path = "\"" + path + "\"";
            }

            return path;
        }

        /// <summary>
        /// Trims trailing backslashes and forward slashes from a path.
        /// </summary>
        /// <param name="path">The path to trim.</param>
        /// <returns>The trimmed path.</returns>
        public static string TrimSlash(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            return path.TrimEnd('\\', '/');
        }

        /// <summary>
        /// Checks if a path is absolute and throws an exception if it is not.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>The original path if it is absolute.</returns>
        /// <exception cref="ArgumentException">Thrown when the path is not absolute.</exception>
        public static string MustBeAbsolute(string path)
        {
            if (string.IsNullOrEmpty(path) || !Path.IsPathRooted(path))
            {
                throw new ArgumentException($"Path '{path}' is not absolute.", nameof(path));
            }

            return path;
        }
    }
}
