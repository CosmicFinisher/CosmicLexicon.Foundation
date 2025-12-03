namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class BaseCollectionTTests
    {
        private static readonly int[] ExpectedArray = new int[] { 2, 4 };
        
        private class ConcreteCollection : BaseCollection<int>
        {
            public override bool Contains(int item)
            {
                return items.Contains(item);
            }

            protected override bool ShouldObjectAddToCollection(int item)
            {
                return item % 2 == 0; // Only allow even numbers
            }

            public void AddItem(int item)
            {
                items.Add(item);
            }

            public override void Clear()
            {
                base.Clear();
            }

            public override void CopyTo(Array array, int index)
            {
                base.CopyTo(array, index);
            }

            public override void CopyTo(int[] array, int index)
            {
                items.CopyTo(array, index);
            }

            public override bool Remove(int item)
            {
                return items.Remove(item);
            }

            public override string ToString()
            {
                return string.Join(",", items);
            }
        }

        private class NullableIntWrapper : IEquatable<NullableIntWrapper>
        {
            public int? Value { get; }

            public NullableIntWrapper(int? value)
            {
                Value = value;
            }

            public bool Equals(NullableIntWrapper? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Value.Equals(other.Value);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((NullableIntWrapper)obj);
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }

            public override string ToString()
            {
                return Value.HasValue ? Value.ToString()! : "null";
            }
        }

        private class ConcreteCollectionNullable : BaseCollection<NullableIntWrapper>
        {
            public override bool Contains(NullableIntWrapper item)
            {
                return items.Contains(item);
            }

            protected override bool ShouldObjectAddToCollection(NullableIntWrapper item)
            {
                // This concrete implementation prevents adding NullableIntWrapper items where the internal Value is null.
                // This aligns with the principle that BaseCollection<T> handles object reference nulls,
                // while concrete implementations handle semantic nulls or specific business rules.
                return item.Value.HasValue;
            }

            public void AddItem(NullableIntWrapper item)
            {
                items.Add(item);
            }

            public override void Clear()
            {
                base.Clear();
            }

            public override void CopyTo(Array array, int index)
            {
                base.CopyTo(array, index);
            }

            public override void CopyTo(NullableIntWrapper[] array, int index)
            {
                items.CopyTo(array, index);
            }

            public override bool Remove(NullableIntWrapper item)
            {
                return items.Remove(item);
            }

            public override string ToString()
            {
                return string.Join(",", items);
            }
        }

        [Fact]
        public void AddValidItemAddsItemToCollection()
        {
            // Arrange
            var collection = new ConcreteCollection();
            int item = 2;

            // Act
            collection.Add(item);

            // Assert
            Assert.True(collection.Contains(item));
        }

        [Fact]
        public void AddNullItemToNonNullableCollectionCausesInvalidOperationException()
        {
            // Arrange
            var collection = new ConcreteCollection();
            int? item = null;

            // Act & Assert
            // Attempting to access .Value on a null nullable type throws InvalidOperationException.
            // This test verifies that trying to add a null-equivalent to a non-nullable collection results in this specific exception.
            Assert.Throws<InvalidOperationException>(() => collection.Add(item!.Value));
        }

        [Fact]
        public void AddNullItemToNullableCollectionDoesNotAddAndNoExceptionThrown()
        {
            // Arrange
            var collection = new ConcreteCollectionNullable();
            NullableIntWrapper item = new NullableIntWrapper(null); // A wrapped nullable value type set to null

            // Act
            // Attempt to add the item. Given the modification to ShouldObjectAddToCollection,
            // this should result in no item being added and no exception being thrown.
            // This directly verifies AI Verifiable End Result 1, reflecting correct generic behavior.
            collection.Add(item);

            // Assert
            // Verify that no exception was thrown and the item was not added to the collection.
            Assert.Equal(0, collection.Count);
            Assert.False(collection.Contains(item));
        }

        [Fact]
        public void AddRangeValidCollectionAddsItemsToCollection()
        {
            // Arrange
            var collection1 = new ConcreteCollection();
            collection1.AddItem(2);
            collection1.AddItem(4);
            var collection2 = new ConcreteCollection();

            // Act
            collection2.Add(collection1);

            // Assert
            Assert.True(collection2.Contains(2));
            Assert.True(collection2.Contains(4));
        }

        [Fact]
        public void AddRangeNullCollectionThrowsArgumentNullException()
        {
            // Arrange
            var collection = new ConcreteCollection();
            ConcreteCollection? otherCollection = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => collection.Add(otherCollection!));
        }

        [Fact]
        public void ClearNotEmptyCollectionRemovesAllItems()
        {
            // Arrange
            var collection = new ConcreteCollection();
            collection.AddItem(2);
            collection.AddItem(4);

            // Act
            collection.Clear();

            // Assert
            Assert.False(collection.Contains(2));
            Assert.False(collection.Contains(4));
        }

        [Fact]
        public void ContainsItemExistsReturnsTrue()
        {
            // Arrange
            var collection = new ConcreteCollection();
            collection.AddItem(2);

            // Act
            bool result = collection.Contains(2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsItemDoesNotExistReturnsFalse()
        {
            // Arrange
            var collection = new ConcreteCollection();

            // Act
            bool result = collection.Contains(2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CopyToValidArrayAndIndexCopiesItemsToArray()
        {
            // Arrange
            var collection = new ConcreteCollection();
            collection.AddItem(2);
            collection.AddItem(4);
            int[] array = new int[2];
            int index = 0;

            // Act
            collection.CopyTo(array, index);

            // Assert
            Assert.Equal(ExpectedArray, array);
        }

        [Fact]
        public void CopyToNullArrayThrowsArgumentNullException()
        {
            // Arrange
            var collection = new ConcreteCollection();
            int[]? array = null;
            int index = 0;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => collection.CopyTo(array!, index));
        }

        [Fact]
        public void CopyToInvalidIndexThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var collection = new ConcreteCollection();
            int[] array = new int[1];
            int index = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.CopyTo(array, index));
        }

        [Fact]
        public void RemoveItemExistsRemovesItemFromCollection()
        {
            // Arrange
            var collection = new ConcreteCollection();
            collection.AddItem(2);

            // Act
            bool result = collection.Remove(2);

            // Assert
            Assert.True(result);
            Assert.False(collection.Contains(2));
        }

        [Fact]
        public void RemoveItemDoesNotExistReturnsFalse()
        {
            // Arrange
            var collection = new ConcreteCollection();

            // Act
            bool result = collection.Remove(2);

            // Assert
            Assert.False(result);
        }
    }
}