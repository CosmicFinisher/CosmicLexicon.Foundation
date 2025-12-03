using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CosmicLexicon.Foundation.Structures
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //
    // Summary:
    //     Maps a key to a list of data
    //
    // Type parameters:
    //   T1:
    //     Key value
    //
    //   T2:
    //     Type that the list should contain
    public sealed class ListMapping<T1, T2> : IDictionary<T1, IEnumerable<T2>>, ICollection<KeyValuePair<T1, IEnumerable<T2>>>, IEnumerable<KeyValuePair<T1, IEnumerable<T2>>>, IEnumerable where T1 : notnull
    {
        //
        // Summary:
        //     The lock object
        private readonly object LockObject = new object();

        //
        // Summary:
        //     To string
        private string? _ToString;

        //
        // Summary:
        //     The number of items in the listing
        public int Count => Items.Count;

        //
        // Summary:
        //     Not read only
        public bool IsReadOnly { get; } = false;

        //
        // Summary:
        //     The list of keys within the mapping
        public ICollection<T1> Keys => Items.Keys;

        //
        // Summary:
        //     List that contains the list of values
        public ICollection<IEnumerable<T2>> Values => Items.Values.Select(x => (IEnumerable<T2>)x).ToList();

        //
        // Summary:
        //     Container holding the data
        protected Dictionary<T1, List<T2>> Items { get; } = [];

        private static readonly IEnumerable<T2> Empty = Enumerable.Empty<T2>();

        //
        // Summary:
        //     Gets a list of values associated with a key
        //
        // Parameters:
        //   key:
        //     Key to look for
        //
        // Returns:
        //     The list of values
        public IEnumerable<T2> this[T1 key]
        {
            get
            {
                if (key == null) return Empty; // Handle null key gracefully

                lock (LockObject)
                {
                    Items.TryGetValue(key, out var value);
                    return value ?? Empty;
                }
            }
            set
            {
                ArgumentNullException.ThrowIfNull(key);
                lock (LockObject)
                {
                    if (Items.TryGetValue(key, out var list))
                    {
                        list.Clear();
                    }
                    AddValues(key, value);
                }
            }
        }

        //
        // Summary:
        //     Adds an item to the mapping
        //
        // Parameters:
        //   key:
        //     Key value
        //
        //   value:
        //     The value to add
        public void Add(T1 key, T2 value)
        {
            AddValues(key, value);
        }

        //
        // Summary:
        //     Adds a key value pair
        //
        // Parameters:
        //   item:
        //     Key value pair to add
        public void Add(KeyValuePair<T1, IEnumerable<T2>> item)
        {
            Add(item.Key, item.Value);
        }

        //
        // Summary:
        //     Adds a list of items to the mapping
        //
        // Parameters:
        //   key:
        //     Key value
        //
        //   value:
        //     The values to add
        public void Add(T1 key, IEnumerable<T2> value)
        {
            AddValues(key, value);
        }

        //
        // Summary:
        //     Clears all items from the listing
        public void Clear()
        {
            _ToString = null;
            lock (LockObject)
            {
                Items.Clear();
            }
        }

        //
        // Summary:
        //     Does this contain the key value pairs?
        //
        // Parameters:
        //   item:
        //     Key value pair to check
        //
        // Returns:
        //     True if it exists, false otherwise
        public bool Contains(KeyValuePair<T1, IEnumerable<T2>> item)
        {
            return ContainsKey(item.Key) && this[item.Key].SequenceEqual(item.Value);
        }

        //
        // Summary:
        //     Does the list mapping contain the key value pairs?
        //
        // Parameters:
        //   key:
        //     Key value
        //
        //   values:
        //     Value
        //
        // Returns:
        //     True if it exists, false otherwise
        public bool Contains(T1 key, IEnumerable<T2> values)
        {
            if (key == null)
            {
                return false;
            }

            lock (LockObject)
            {
                if (!Items.TryGetValue(key, out var tempValues))
                {
                    return false;
                }

                HashSet<T2> valueSet = new HashSet<T2>(tempValues);
                return values.All(valueSet.Contains);
            }
        }

        //
        // Summary:
        //     Does the list mapping contain the key value pair?
        //
        // Parameters:
        //   key:
        //     Key
        //
        //   value:
        //     Value
        //
        // Returns:
        //     True if it exists, false otherwise
        public bool Contains(T1 key, T2 value)
        {
            if (key == null)
            {
                return false;
            }

            lock (LockObject)
            {
                List<T2> value2;
                return Items.TryGetValue(key, out value2) && value2.Contains(value);
            }
        }

        //
        // Summary:
        //     Determines if a key exists
        //
        // Parameters:
        //   key:
        //     Key to check on
        //
        // Returns:
        //     True if it exists, false otherwise
        public bool ContainsKey(T1 key)
        {
            if (key == null) return false; // Handle null key gracefully

            lock (LockObject)
            {
                return Items.ContainsKey(key);
            }
        }

        //
        // Summary:
        //     Not implemented
        //
        // Parameters:
        //   array:
        //     Array to copy to
        //
        //   arrayIndex:
        //     array index
        public void CopyTo(KeyValuePair<T1, IEnumerable<T2>>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Not enough space in the destination array.");
            }

            int i = arrayIndex;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        //
        // Summary:
        //     Gets the enumerator
        //
        // Returns:
        //     The enumerator for this object
        public IEnumerator<KeyValuePair<T1, IEnumerable<T2>>> GetEnumerator()
        {
            foreach (T1 key in Items.Keys.ToList())
            {
                yield return new KeyValuePair<T1, IEnumerable<T2>>(key, this[key]);
            }
        }

        //
        // Summary:
        //     Gets the enumerator
        //
        // Returns:
        //     The enumerator for this object
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Items)
            {
                yield return new KeyValuePair<T1, IEnumerable<T2>>(item.Key, item.Value);
            }
        }

        //
        // Summary:
        //     Remove a list of items associated with a key
        //
        // Parameters:
        //   key:
        //     Key to use
        //
        // Returns:
        //     True if the key is found, false otherwise
        public bool Remove(T1 key)
        {
            if (key == null)
            {
                return false;
            }

            _ToString = null;
            lock (LockObject)
            {
                return Items.Remove(key);
            }
        }

        //
        // Summary:
        //     Removes a key value pair from the list mapping
        //
        // Parameters:
        //   item:
        //     items to remove
        //
        // Returns:
        //     True if it is removed, false otherwise
        public bool Remove(KeyValuePair<T1, IEnumerable<T2>> item)
        {
            return Remove(item.Key);
        }

        //
        // Summary:
        //     Removes a key value pair from the list mapping
        //
        // Parameters:
        //   key:
        //     Key to remove
        //
        //   value:
        //     Value to remove
        //
        // Returns:
        //     True if it is removed, false otherwise
        public bool Remove(T1 key, T2 value)
        {
            if (key == null)
            {
                return false;
            }

            _ToString = null;
            lock (LockObject)
            {
                if (!Items.TryGetValue(key, out var value2))
                {
                    return false;
                }

                bool result = value2.Remove(value);
                if (value2.Count == 0)
                {
                    Items.Remove(key);
                }

                return result;
            }
        }

        //
        // Summary:
        //     Returns a System.String that represents this instance.
        //
        //     Returns:
        //     A System.String that represents this instance.
        public override string ToString()
        {
            string? temp = _ToString;
            if (temp != null)
            {
                return temp;
            }

            StringBuilder stringBuilder = new StringBuilder();
            lock (LockObject)
            {
                foreach (var item in Items)
                {
                    stringBuilder.AppendLine($"{item.Key}:{{{string.Join(", ", item.Value)}}}");
                }

                temp = stringBuilder.ToString();
            }
            _ToString = temp;
            return temp;
        }

        //
        // Summary:
        //     Tries to get the value associated with the key
        //
        // Parameters:
        //   key:
        //     Key value
        //
        //   value:
        //     The values getting
        //
        // Returns:
        //     True if it was able to get the value, false otherwise
        public bool TryGetValue(T1 key, out IEnumerable<T2> value)
        {
            if (key is null)
            {
                value = Enumerable.Empty<T2>();
                return false;
            }

            lock (LockObject)
            {
                if (Items.TryGetValue(key, out var value2))
                {
                    value = value2;
                    return true;
                }

                value = Enumerable.Empty<T2>();
                return false;
            }
        }

        //
        // Summary:
        //     Adds the values.
        //
        // Parameters:
        //   key:
        //     The key.
        //
        //   value:
        //     The value.
        private void AddValues(T1 key, IEnumerable<T2> values)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(values);

            _ToString = null;
            lock (LockObject)
            {
                if (!Items.TryGetValue(key, out var value))
                {
                    value = [];
                    Items.Add(key, value);
                }

                value.AddRange(values);
            }
        }

        private void AddValues(T1 key, T2 value)
        {
            AddValues(key, Enumerable.Repeat(value, 1));
        }
    }
}
