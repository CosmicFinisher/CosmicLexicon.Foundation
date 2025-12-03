using System.Collections.ObjectModel;
using System.ComponentModel;
using CosmicLexicon.Foundation.Structures;

namespace CosmicLexicon.Foundation.Structures
{
    [EditorBrowsable(EditorBrowsableState.Never),]

    public static class ListExtensions
    {

        extension<T>(T item)
        {
            public ReadOnlyCollection<T> ToReadOnly() => new([item]);
        }

        extension<T>(IReadOnlyCollection<T> source)
        {
            public IReadOnlyCollection<T> IsNullOrEmpty() => source ?? [];
            public IReadOnlyCollection<T> NullCheck() => source ?? [];
        }

        extension<T>(IReadOnlyCollection<T> collection)
        {
            public IEnumerable<T> Concat(IReadOnlyCollection<T> otherCollection) => (collection ?? Enumerable.Empty<T>()).Concat(otherCollection ?? Enumerable.Empty<T>());
        }

        extension<T>(List<List<T>?> collection)
        {
            public List<T> ToFlatList()
            {
                ArgumentNullException.ThrowIfNull(collection);
                return [.. collection.SelectMany(list => list ?? Enumerable.Empty<T>())];
            }
        }

        extension<T>(IEnumerable<List<T>?> collection)
        {
            public IEnumerable<T> AsFlattened()
            {
                ArgumentNullException.ThrowIfNull(collection);

                return collection.SelectMany(list => list ?? Enumerable.Empty<T>());
            }
            public List<T> ToFlattenedList() => [.. collection.AsFlattened()];
        }
    }
}
