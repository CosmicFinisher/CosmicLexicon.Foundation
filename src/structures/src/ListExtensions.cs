using System.Collections.ObjectModel;
using System.ComponentModel;
using CosmicLexicon.Foundation.Structures;

namespace CosmicLexicon.Foundation.Structures
{
    [EditorBrowsable(EditorBrowsableState.Never),]

    public static class ListExtensions
    {

        public static ReadOnlyCollection<T> ToReadOnly<T>(this T item) => new ReadOnlyCollection<T>(new T[] { item });
        public static IReadOnlyCollection<T> IsNullOrEmpty<T>(this IReadOnlyCollection<T> source) => source ?? [];
        public static IReadOnlyCollection<T> NullCheck<T>(this IReadOnlyCollection<T> source) => source ?? [];
        public static IEnumerable<T> Concat<T>(this IReadOnlyCollection<T> collection, IReadOnlyCollection<T> otherCollection)
        {
            return (collection ?? Enumerable.Empty<T>()).Concat(otherCollection ?? Enumerable.Empty<T>());
        }

        public static List<T> ToFlatList<T>(this List<List<T>?> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            return collection.SelectMany(list => list ?? Enumerable.Empty<T>()).ToList();
        }

        public static IEnumerable<T> AsFlattened<T>(this IEnumerable<List<T>?> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            return collection.SelectMany(list => list ?? Enumerable.Empty<T>());
        }
        public static List<T> ToFlattenedList<T>(this IEnumerable<List<T>?> collection)
        {
            return collection.AsFlattened().ToList();
        }
    }
}
