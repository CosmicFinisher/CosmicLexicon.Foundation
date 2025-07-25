using System.ComponentModel;
using System.Text;

namespace OpenEchoSystem.Core.xRuntime
{
    //
    // Summary:
    //     IComparable extensions
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IComparableExtensions
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
        public static bool Between<T>(this T value, T min, T max, IComparer<T>? comparer = null) where T : IComparable
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
        public static T Clamp<T>(this T value, T max, T min, IComparer<T>? comparer = null) where T : IComparable
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
        public static T Max<T>(this T inputA, T inputB, IComparer<T>? comparer = null) where T : IComparable
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
        public static T Min<T>(this T inputA, T inputB, IComparer<T>? comparer = null) where T : IComparable
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
