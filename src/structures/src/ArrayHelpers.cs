using System.ComponentModel;

namespace CosmicLexicon.Foundation.Structures
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ArrayHelpers
    {
        public static TArray[] FillArrayElements<TArray>(TArray[] array, TArray value, int startIndex = 0) // Refactored
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length == 0)
                return array;
            if (startIndex < 0 || startIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            for (var i = startIndex; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }

        public static bool FillArrayElement<T>(T[] array, Predicate<T> finder, Func<T, T> producer, int startIndex = 0) // Refactored
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length == 0) return false; // Moved this check up

            // If array is empty, the previous check (line 27) should handle it.
            // This check is for non-empty arrays where startIndex is out of bounds.
            if (startIndex < 0 || (array.Length > 0 && startIndex >= array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            ArgumentNullException.ThrowIfNull(producer);
            ArgumentNullException.ThrowIfNull(finder);

            var index = Array.FindIndex(array, startIndex, finder);

            if (index < 0) 
                return false;

            array[index] = producer(array[index]);
            return true;
        }
    }
}