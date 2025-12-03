using System.Collections;

namespace CosmicLexicon.Foundation.Generics
{
    //
    // Summary:
    //     Generic equality comparer
    //
    // Type parameters:
    //   TData:
    //     Data type
    public sealed class GenericEqualityComparer<TData> : IEqualityComparer<TData>
    {
        //
        // Summary:
        //     Gets the comparer.
        //
        // Value:
        //     The comparer.
        // The static Comparer property is removed to comply with CA1000.
        // Instead, callers should instantiate GenericEqualityComparer<TData> or use EqualityComparer<TData>.Default.

        //
        // Summary:
        //     Determines if the two items are equal
        //
        // Parameters:
        //   x:
        //     Object 1
        //
        //   y:
        //     Object 2
        //
        // Returns:
        //     True if they are, false otherwise
        public bool Equals(TData? x, TData? y) // Changed signature to match IEqualityComparer<TData>
        {
            Type typeFromHandle = typeof(TData);
            if (Nullable.GetUnderlyingType(typeFromHandle) != null)
            {
                if (x is null)
                {
                    return y is null;
                }

                if (y is null)
                {
                    return false;
                }
            }

            if (x is IEnumerable enumerableX && y is IEnumerable enumerableY)
            {
                IEnumerator enumeratorX = enumerableX.GetEnumerator();
                IEnumerator enumeratorY = enumerableY.GetEnumerator();

                while (true)
                {
                    bool hasNextX = enumeratorX.MoveNext();
                    bool hasNextY = enumeratorY.MoveNext();

                    if (!hasNextX || !hasNextY)
                    {
                        return hasNextX == hasNextY;
                    }

                    if (!Equals(enumeratorX.Current, enumeratorY.Current))
                    {
                        return false;
                    }
                }
            }

            if (x is IEqualityComparer<TData> equalityComparer)
            {
                return equalityComparer.Equals(x, y);
            }

            if (x is IComparable<TData> comparable)
            {
                return comparable.CompareTo(y) == 0;
            }

            if (x is IComparable comparable2)
            {
                return comparable2.CompareTo(y) == 0;
            }

            return EqualityComparer<TData>.Default.Equals(x, y);
        }

        //
        // Summary:
        //     Get hash code
        //
        // Parameters:
        //   obj:
        //     Object to get the hash code of
        //
        // Returns:
        //     The object's hash code
        public int GetHashCode(TData obj) => obj?.GetHashCode() ?? -1;
    }
}
