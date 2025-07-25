using System;
using System.Collections.Generic;
using System.Linq;
using OpenEchoSystem.Core.xCollections;
using Xunit;

namespace OpenEchoSystem.Core.xCollections
{
    public class ListMappingTTTests
    {
        private static readonly int[] SingleValueArray = { 1 };
        private static readonly int[] TwoValuesArray = { 1, 2 };
        private static readonly string[] TwoKeysArray = { "key1", "key2" };
        private static readonly int[] SingleValueArray2 = { 2 };

        [Fact]
        public void AddKeyValuePairAddsValueToList()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act
            mapping.Add("key1", 1);

            // Assert
            Assert.True(mapping.ContainsKey("key1"));
            Assert.Equal(SingleValueArray, mapping["key1"]);
        }

        [Fact]
        public void AddKeyValueAddsValueToList()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            var item = new KeyValuePair<string, IEnumerable<int>>("key1", SingleValueArray);

            // Act
            mapping.Add(item);

            // Assert
            Assert.True(mapping.ContainsKey("key1"));
            Assert.Equal(item.Value, mapping["key1"]);
        }

        [Fact]
        public void AddNullKeyThrowsArgumentNullException()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => mapping.Add(null!, 1));
        }

        [Fact]
        public void ClearRemovesAllKeyValuePairs()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);
            mapping.Add("key2", 2);

            // Act
            mapping.Clear();

            // Assert
            Assert.Empty(mapping);
        }

        [Fact]
        public void ContainsKeyReturnsTrueForExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);

            // Act & Assert
            Assert.True(mapping.ContainsKey("key1"));
        }

        [Fact]
        public void ContainsKeyReturnsFalseForNonExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act & Assert
            Assert.False(mapping.ContainsKey("key1"));
        }

        [Fact]
        public void ContainsKeyWithNullKeyReturnsFalse()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act & Assert
            Assert.False(mapping.ContainsKey(null!));
        }

        [Fact]
        public void IndexerGetsListForExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);
            mapping.Add("key1", 2);

            // Act
            var result = mapping["key1"];

            // Assert
            Assert.Equal(TwoValuesArray, result);
        }

        [Fact]
        public void IndexerWithNonExistingKeyReturnsEmptyList()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act
            var result = mapping["key1"];

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void IndexerWithNullKeyReturnsEmptyList()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act
            var result = mapping[null!];

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void TryGetValueReturnsTrueForExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);

            // Act
            bool result = mapping.TryGetValue("key1", out var values);

            // Assert
            Assert.True(result);
            Assert.Equal(SingleValueArray, values);
        }

        [Fact]
        public void TryGetValueReturnsFalseForNonExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act
            bool result = mapping.TryGetValue("key1", out var values);

            // Assert
            Assert.False(result);
            Assert.Empty(values);
        }

        [Fact]
        public void RemoveKeyReturnsTrueForExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);

            // Act
            bool result = mapping.Remove("key1");

            // Assert
            Assert.True(result);
            Assert.False(mapping.ContainsKey("key1"));
        }

        [Fact]
        public void RemoveKeyReturnsFalseForNonExistingKey()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();

            // Act
            bool result = mapping.Remove("key1");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void KeysContainsAllKeys()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);
            mapping.Add("key2", 2);

            // Act
            var keys = mapping.Keys;

            // Assert
            Assert.Equal(TwoKeysArray, keys);
        }

        [Fact]
        public void ValuesContainsAllLists()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);
            mapping.Add("key2", 2);

            // Act
            var values = mapping.Values;

            // Assert
            Assert.Collection(values,
                list => Assert.Equal(SingleValueArray, list),
                list => Assert.Equal(SingleValueArray2, list));
        }

        [Fact]
        public void CountReturnsNumberOfKeys()
        {
            // Arrange
            var mapping = new ListMapping<string, int>();
            mapping.Add("key1", 1);
            mapping.Add("key1", 2);
            mapping.Add("key2", 3);

            // Act & Assert
            Assert.Equal(2, mapping.Count);
        }
    }
}