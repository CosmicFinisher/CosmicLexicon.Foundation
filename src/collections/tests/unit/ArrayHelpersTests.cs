using Xunit;
using OpenEchoSystem.Core.xCollections;
using System;
using System.Collections.Generic;
using OpenEchoSystem.Core.xCollections.Extensions;

namespace OpenEchoSystem.Core.xCollections
{
    public class ArrayHelpersTests
    {
        [Fact]
        public void FillArrayElements_WithValidArray_FillsArrayWithValue()
        {
            // Arrange
            int[] array = new int[5];
            int value = 42;

            // Act
            ArrayHelpers.FillArrayElements(array, value);

            // Assert
            foreach (var item in array)
            {
                Assert.Equal(value, item);
            }
        }

        [Fact]
        public void FillArrayElements_WithStartIndex_FillsArrayFromStartIndex()
        {
            // Arrange
            int[] array = new int[5] { 1, 2, 3, 4, 5 };
            int value = 42;
            int startIndex = 2;

            // Act
            ArrayHelpers.FillArrayElements(array, value, startIndex);

            // Assert
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(value, array[2]);
            Assert.Equal(value, array[3]);
            Assert.Equal(value, array[4]);
        }

        [Fact]
        public void FillArrayElements_WithNullArray_ThrowsArgumentNullException()
        {
            // Arrange
            int[] array = null;
            int value = 42;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElements(array, value));
        }

        [Fact]
        public void FillArrayElement_WithValidParameters_UpdatesElement()
        {
            // Arrange
            int[] array = new int[5] { 1, 2, 3, 4, 5 };
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;

            // Act
            bool result = ArrayHelpers.FillArrayElement(array, finder, producer);

            // Assert
            Assert.True(result);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(42, array[2]); // index 2 contains value 3
            Assert.Equal(4, array[3]);
            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void FillArrayElement_WithStartIndex_SearchesFromStartIndex()
        {
            // Arrange
            int[] array = new int[5] { 3, 2, 3, 4, 5 };
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;
            int startIndex = 1;

            // Act
            bool result = ArrayHelpers.FillArrayElement(array, finder, producer, startIndex);

            // Assert
            Assert.True(result);
            Assert.Equal(3, array[0]); // Not changed because it's before startIndex
            Assert.Equal(2, array[1]);
            Assert.Equal(42, array[2]); // Changed
            Assert.Equal(4, array[3]);
            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void FillArrayElement_WhenNoElementMatches_ReturnsFalse()
        {
            // Arrange
            int[] array = new int[5] { 1, 2, 3, 4, 5 };
            Predicate<int> finder = i => i == 10; // No element matches
            Func<int, int> producer = index => 42;

            // Act
            bool result = ArrayHelpers.FillArrayElement(array, finder, producer);

            // Assert
            Assert.False(result);
            // Array remains unchanged
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
            Assert.Equal(4, array[3]);
            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void FillArrayElement_WithNullArray_ThrowsArgumentNullException()
        {
            // Arrange
            int[] array = null;
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
        }

        [Fact]
        public void FillArrayElement_WithNullFinder_ThrowsArgumentNullException()
        {
            // Arrange
            int[] array = new int[5];
            Predicate<int> finder = null;
            Func<int, int> producer = index => 42;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
        }

        [Fact]
        public void FillArrayElement_WithNullProducer_ThrowsArgumentNullException()
        {
            // Arrange
            int[] array = new int[5];
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
        }

        [Fact]
        public void FillArrayElement_WithInvalidStartIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int[] array = new int[5];
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;
            int startIndex = -1; // Invalid

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelpers.FillArrayElement(array, finder, producer, startIndex));
        }

        [Fact]
        public void FillArrayElement_WithStartIndexGreaterThanLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int[] array = new int[5];
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;
            int startIndex = 5; // Invalid (equal to length)

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelpers.FillArrayElement(array, finder, producer, startIndex));
        }

        [Fact]
        public void FillArrayElement_EmptyArray_ReturnsFalse()
        {
            // Arrange
            int[] array = new int[0];
            Predicate<int> finder = i => i == 3;
            Func<int, int> producer = index => 42;

            // Act
            bool result = ArrayHelpers.FillArrayElement(array, finder, producer);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void Concat_WithEmptyAdditions_ReturnsOriginalCollection()
        {
            // Arrange
            var arr = new int[] { 1, 2, 3 };
            ICollection<int> enumerable1 = arr;
            ICollection<int>[] additions = new ICollection<int>[] { };

            // Act
            IEnumerable<int> result = IEnumerableExtensions.Concat<int>(arr, additions);

            // Assert
            Assert.Equal(enumerable1, result);
        }

        public class FillArrayElementTests
        {
            [Fact]
            public void AddRange_AddsOnlyItemsThatShouldBeAdded()
            {
                // Arrange
                var collection = new ConcreteCollection();
                var itemsToAdd = new ConcreteCollection();
                itemsToAdd.AddItem(1);
                itemsToAdd.AddItem(2);
                itemsToAdd.AddItem(3);

                // Act
                collection.Add(itemsToAdd);

                // Assert
                Assert.Equal(2, collection.Count);
                Assert.Contains(1, collection);
                Assert.Contains(3, collection);
            }

            [Fact]
            public void CopyTo_CopiesToArrayCorrectly()
            {
                // Arrange
                var collection = new ConcreteCollection();
                collection.AddItem(1);
                collection.AddItem(2);
                collection.AddItem(3);

                var array = new int[5];

                // Act
                collection.CopyTo(array, 1);

                // Assert
                Assert.Equal(0, array[0]);
                Assert.Equal(1, array[1]);
                Assert.Equal(2, array[2]);
                Assert.Equal(3, array[3]);
                Assert.Equal(0, array[4]);
            }

            private class ConcreteCollection : BaseCollection<int>
            {
                public ConcreteCollection()
                {
                    IsReadOnly = false;
                }

                public override bool Contains(int item)
                {
                    return items.Contains(item);
                }

                protected override bool ShouldObjectAddToCollection(int item)
                {
                    return item % 2 != 0;
                }

                public void AddItem(int item)
                {
                    items.Add(item);
                }

                public override void CopyTo(Array array, int index)
                {
                    if (array is int[] intArray)
                    {
                        items.CopyTo(intArray, index);
                    }
                }
            }
            [Fact]
            public void FillArrayElements_ValidInput_ReturnsFilledArray()
            {
                // Arrange
                int[] array = new int[3];
                int value = 5;

                // Act
                int[] result = ArrayHelpers.FillArrayElements(array, value);

                // Assert
                Assert.Equal(new int[] { 5, 5, 5 }, result);
            }

            [Fact]
            public void FillArrayElements_StartIndex_ReturnsPartiallyFilledArray()
            {
                // Arrange
                int[] array = new int[3];
                int value = 5;
                int startIndex = 1;

                // Act
                int[] result = ArrayHelpers.FillArrayElements(array, value, startIndex);

                // Assert
                Assert.Equal(new int[] { 0, 5, 5 }, result);
            }

            [Fact]
            public void FillArrayElements_NullArray_ThrowsArgumentNullException()
            {
                // Arrange
                int[] array = null;
                int value = 5;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElements(array, value));
            }

            [Fact]
            public void FillArrayElements_EmptyArray_ReturnsEmptyArray()
            {
                // Arrange
                int[] array = new int[0];
                int value = 5;

                // Act
                int[] result = ArrayHelpers.FillArrayElements(array, value);

                // Assert
                Assert.Equal(new int[0], result);
            }

            [Fact]
            public void FillArrayElement_ElementFound_ReturnsTrueAndUpdatesArray()
            {
                // Arrange
                int[] array = new int[] { 1, 2, 3 };
                Predicate<int> finder = x => x == 2;
                Func<int, int> producer = x => x * 2; // This producer expects an int (index)
                // The FillArrayElement method now expects Func<T, T> (value, value)
                // So, we need to adjust the producer to work with the value, not the index.
                // For this test, we want to double the value found.
                Func<int, int> valueProducer = x => x * 2;

                // Act
                bool result = ArrayHelpers.FillArrayElement(array, finder, valueProducer);
                int expectedValue = valueProducer(2);

                // Assert
                Assert.True(result);
                Assert.Equal(1, array[0]);
                Assert.Equal(expectedValue, array[1]);
                Assert.Equal(3, array[2]);
            }

            [Fact]
            public void FillArrayElement_ElementNotFound_ReturnsFalseAndDoesNotUpdateArray()
            {
                // Arrange
                int[] array = new int[] { 1, 2, 3 };
                Predicate<int> finder = x => x == 4;
                Func<int, int> producer = x => x * 2; // Original producer, now unused
                Func<int, int> valueProducer = x => x * 2; // New producer that takes value

                // Act
                bool result = ArrayHelpers.FillArrayElement(array, finder, producer);

                // Assert
                Assert.False(result);
                Assert.Equal(new int[] { 1, 2, 3 }, array);
            }

            [Fact]
            public void FillArrayElement_NullArray_ThrowsArgumentNullException()
            {
                // Arrange
                int[] array = null;
                Predicate<int> finder = x => x == 2;
                Func<int, int> producer = x => x * 2;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
            }

            [Fact]
            public void FillArrayElement_StartIndexOutOfRange_ThrowsArgumentOutOfRangeException()
            {
                // Arrange
                int[] array = new int[] { 1, 2, 3 };
                Predicate<int> finder = x => x == 2;
                Func<int, int> producer = x => x * 2;
                int startIndex = 5;

                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelpers.FillArrayElement(array, finder, producer, startIndex));
            }

            [Fact]
            public void FillArrayElement_NullProducer_ThrowsArgumentNullException()
            {
                // Arrange
                int[] array = new int[] { 1, 2, 3 };
                Predicate<int> finder = x => x == 2;
                Func<int, int> producer = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
            }

            [Fact]
            public void FillArrayElement_NullFinder_ThrowsArgumentNullException()
            {
                // Arrange
                int[] array = new int[] { 1, 2, 3 };
                Predicate<int> finder = null;
                Func<int, int> producer = x => x * 2;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => ArrayHelpers.FillArrayElement(array, finder, producer));
            }

            [Fact]
            public void FillArrayElement_EmptyArray_ReturnsFalse()
            {
                // Arrange
                int[] array = new int[0];
                Predicate<int> finder = x => x == 2;
                Func<int, int> producer = x => x * 2;

                // Act & Assert
                bool result = ArrayHelpers.FillArrayElement(array, finder, producer);
                Assert.False(result);
            }
        }
    }
}
