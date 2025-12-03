using CosmicLexicon.Foundation.Linq;

namespace CosmicLexicon.Foundation.Linq
{
    /// <summary>
    /// Provides helper methods for IEnumerable types to enhance collection operations.
    /// </summary>
    public static class IEnumerableHelpers
    {
        /// <summary>
        /// Converts an IEnumerable to an array using a selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
        /// <typeparam name="TTarget">The type of elements in the resulting array.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An array containing the transformed elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source or selector is null.</exception>
        public static TTarget[] ToArray<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.Select(selector).ToArray();
        }

        /// <summary>
        /// Converts an IEnumerable to a List using a selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
        /// <typeparam name="TTarget">The type of elements in the resulting list.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>A List containing the transformed elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source or selector is null.</exception>
        public static List<TTarget> ToList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.Select(selector).ToList();
        }

        /// <summary>
        /// Converts null to an empty enumerable, otherwise returns the source enumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="source">The source enumerable, which may be null.</param>
        /// <returns>The source enumerable if not null; otherwise, an empty enumerable.</returns>
        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T>? source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Alias for NullToEmpty. Returns an empty enumerable if the source is null.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="source">The source enumerable, which may be null.</param>
        /// <returns>The source enumerable if not null; otherwise, an empty enumerable.</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) => source.NullToEmpty();

        /// <summary>
        /// Converts an enumerable to an array, returning an empty array if the source is null.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="source">The source enumerable, which may be null.</param>
        /// <returns>An array containing the elements from source, or an empty array if source is null.</returns>
        public static T[] ToArrayOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.ToArray() ?? Array.Empty<T>();
        }

        /// <summary>
        /// Converts an enumerable to a List, returning an empty List if the source is null.
        /// </summary>
        /// <typeparam name="T">The type of elements in the List.</typeparam>
        /// <param name="source">The source enumerable, which may be null.</param>
        /// <returns>A List containing the elements from source, or an empty List if source is null.</returns>
        public static List<T> ToListOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.ToList() ?? new List<T>();
        }

        /// <summary>
        /// Executes an action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="action">The action to execute on each element.</param>
        /// <exception cref="ArgumentNullException">Thrown when source or action is null.</exception>
        /// <returns>The source enumerable to enable method chaining.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(action);

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
}
