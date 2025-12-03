using System.ComponentModel;

namespace CosmicLexicon.Foundation.Structures.Linq
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EnumerableHelpers
    {
        public static bool CanBeSucceeded<T>(IEnumerable<T> collection, Func<T, bool> func)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(func);

            using var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var member = enumerator.Current;
                if (!func(member)) return false;
            }
            return true;
        }

        public static bool CanBeIncluded<T>(IEnumerable<T> collection, Func<T, bool> func)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(func);

            using var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var member = enumerator.Current;
                if (func(member)) return true;
            }
            return false;
        }
    }
}
