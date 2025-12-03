using System.Collections.Generic;
using System.ComponentModel;

namespace CosmicLexicon.Foundation.xLinq
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EnumerableHelpers
    {
        public static bool CanBeSucceeded<T>(IEnumerable<T> collection, Func<T, bool> func)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (func == null) throw new ArgumentNullException(nameof(func));

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
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (func == null) throw new ArgumentNullException(nameof(func));

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
