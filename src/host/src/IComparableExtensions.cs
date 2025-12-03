using System.ComponentModel;

namespace CosmicLexicon.Foundation.Host
{
    //
    // Summary:
    //     IComparable extensions
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IComparableExtensions
    {
        extension<T>(T value) where T : IComparable
        {
            //
            // Summary:
            //     Checks if an item is between two values
            //
            // Parameters:
            //   value:
            //     Value to check
            //
            //   min:
            //     Minimum value
            //
            //   max:
            //     Maximum value
            //
            //   comparer:
            //     Comparer used to compare the values (defaults to GenericComparer)"
            //
            // Type parameters:
            //   T:
            //     Type of the value
            //
            // Returns:
            //     True if it is between the values, false otherwise
            public bool Between(T min, T max, IComparer<T>? comparer = null)
            {
                comparer ??= GenericComparer<T>.Comparer;

                if (comparer.Compare(max, value) >= 0)
                {
                    return comparer.Compare(value, min) >= 0;
                }

                return false;
            }

            //
            // Summary:
            //     Clamps a value between two values
            //
            // Parameters:
            //   value:
            //     Value sent in
            //
            //   max:
            //     Max value it can be (inclusive)
            //
            //   min:
            //     Min value it can be (inclusive)
            //
            //   comparer:
            //     Comparer to use (defaults to GenericComparer)
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     The value set between Min and Max
            public T Clamp(T max, T min, IComparer<T>? comparer = null)
            {
                comparer ??= GenericComparer<T>.Comparer;

                // Ensure min is always less than or equal to max
                if (comparer.Compare(min, max) > 0)
                {
                    (min, max) = (max, min); // Swap min and max
                }

                if (comparer.Compare(value, min) < 0)
                {
                    return min;
                }

                if (comparer.Compare(value, max) > 0)
                {
                    return max;
                }

                return value;
            }
        }

        extension<T>(T inputA) where T : IComparable
        {
            //
            // Summary:
            //     Returns the maximum value between the two
            //
            // Parameters:
            //   inputA:
            //     Input A
            //
            //   inputB:
            //     Input B
            //
            //   comparer:
            //     Comparer to use (defaults to GenericComparer)
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     The maximum value
            public T Max(T inputB, IComparer<T>? comparer = null)
            {
                comparer ??= GenericComparer<T>.Comparer;

                if (comparer.Compare(inputA, inputB) >= 0)
                {
                    return inputA;
                }

                return inputB;
            }

            //
            // Summary:
            //     Returns the minimum value between the two
            //
            // Parameters:
            //   inputA:
            //     Input A
            //
            //   inputB:
            //     Input B
            //
            //   comparer:
            //     Comparer to use (defaults to GenericComparer)
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     The minimum value
            public T Min(T inputB, IComparer<T>? comparer = null)
            {
                comparer ??= GenericComparer<T>.Comparer;

                if (comparer.Compare(inputA, inputB) <= 0)
                {
                    return inputA;
                }

                return inputB;
            }
        }
    }
}
