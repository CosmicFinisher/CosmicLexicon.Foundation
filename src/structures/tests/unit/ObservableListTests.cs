namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class ObservableListCollectionTests
    {
        [Fact]
        public void AddItemAddsItemToBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int>();
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Add(1);

            // Assert
            Assert.Equal(1, list.Count);
            Assert.Equal(1, list[0]);
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void AddObjectAddsItemToBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int>();
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Add((object)1);

            // Assert
            Assert.Equal(1, list.Count);
            Assert.Equal(1, list[0]);
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void AddObjectThrowsArgumentExceptionWhenObjectTypeIsInvalid()
        {
            // Arrange
            var list = new ObservableListCollection<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => list.Add("string"));
        }

        [Fact]
        public void AddRangeCollectionAddsItemsToBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int>();
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.AddRange(new int[] { 1, 2, 3 });

            // Assert
            Assert.Equal(3, list.Count);
            Assert.Equal(new int[] { 1, 2, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void ClearRemovesAllItemsFromBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Clear();

            // Assert
            Assert.Equal(0, list.Count);
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }
        [Fact]
        public void ClearDelegatesRemovesAllDelegatesFromCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int>();
            int collectionChangedInvocations = 0;
            int propertyChangedInvocations = 0;
            list.CollectionChanged += (sender, e) => collectionChangedInvocations++;
            list.PropertyChanged += (sender, e) => propertyChangedInvocations++;

            // Act
            list.ClearDelegates();
            list.Add(1);

            // Assert
            Assert.Equal(0, collectionChangedInvocations);
            Assert.Equal(0, propertyChangedInvocations);
        }

        [Fact]
        public void ContainsItemReturnsTrueIfItemIsInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            bool result = list.Contains(2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsItemReturnsFalseIfItemIsNotInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            bool result = list.Contains(4);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainsObjectReturnsTrueIfItemIsInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            bool result = list.Contains((object)2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsObjectReturnsFalseIfItemIsNotInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            bool result = list.Contains((object)4);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CopyToArrayAndIndexCopiesItemsFromBaseListToArray()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            int[] array = new int[5];

            // Act
            list.CopyTo(array, 1);

            // Assert
            Assert.Equal(new int[] { 0, 1, 2, 3, 0 }, array);
        }

        [Fact]
        public void GetEnumeratorReturnsEnumeratorForBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            var expected = new[] { 1, 2, 3 };

            // Act
            var result = list.ToArray();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IndexOfItemReturnsIndexOfItemInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            int result = list.IndexOf(2);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void IndexOfObjectReturnsIndexOfItemInBaseList()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            int result = list.IndexOf((object)2);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void InsertIndexAndItemInsertsItemIntoBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Insert(1, 4);

            // Assert
            Assert.Equal(4, list.Count);
            Assert.Equal(new int[] { 1, 4, 2, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void InsertIndexAndObjectInsertsItemIntoBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Insert(1, (object)4);

            // Assert
            Assert.Equal(4, list.Count);
            Assert.Equal(new int[] { 1, 4, 2, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void InsertIndexAndObjectThrowsArgumentExceptionWhenObjectTypeIsInvalid()
        {
            // Arrange
            var list = new ObservableListCollection<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => list.Insert(1, "string"));
        }

        [Fact]
        public void InsertRangeIndexAndCollectionInsertsItemsIntoBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.InsertRange(1, new int[] { 4, 5 });

            // Assert
            Assert.Equal(5, list.Count);
            Assert.Equal(new int[] { 1, 4, 5, 2, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void RemoveItemRemovesItemFromBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Remove(2);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Equal(new int[] { 1, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void RemoveObjectRemovesItemFromBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.Remove((object)2);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Equal(new int[] { 1, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void RemoveAtIndexRemovesItemAtIndexFromBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.RemoveAt(1);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Equal(new int[] { 1, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void RemoveRangeIndexAndCountRemovesItemsFromBaseListAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3, 4, 5 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list.RemoveRange(1, 2);

            // Assert
            Assert.Equal(3, list.Count);
            Assert.Equal(new int[] { 1, 4, 5 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void IndexerReturnsItemAtIndex()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };

            // Act
            int result = list[1];

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void IndexerSetsItemAtIndexAndRaisesCollectionChangedAndPropertyChanged()
        {
            // Arrange
            var list = new ObservableListCollection<int> { 1, 2, 3 };
            bool collectionChangedRaised = false;
            bool propertyChangedRaised = false;
            list.CollectionChanged += (sender, e) => collectionChangedRaised = true;
            list.PropertyChanged += (sender, e) => propertyChangedRaised = true;

            // Act
            list[1] = 4;

            // Assert
            Assert.Equal(4, list[1]);
            Assert.Equal(new int[] { 1, 4, 3 }, list.ToArray());
            Assert.True(collectionChangedRaised);
            Assert.True(propertyChangedRaised);
        }
    }
}