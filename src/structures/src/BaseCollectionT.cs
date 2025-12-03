using System.Collections;

namespace CosmicLexicon.Foundation.Structures
{
    public abstract class BaseCollection<TItem> : ICollection<TItem>, IEnumerable<TItem>, IEnumerable, IReadOnlyCollection<TItem>, ICollection, IDisposable
        where TItem : IEquatable<TItem>
    {
        protected BaseCollection()
        {
            items = [];
            SyncRoot = new object();
            IsSynchronized = false;
        }
        protected readonly List<TItem> items;
        private bool disposedValue;

        //
        // Summary:
        //     Gets the element at the specified index.
        //
        //
        // Parameters:/
        //   index:
        //     The zero-based index of the System.Net.TItem to be found.
        //
        // Returns:
        //     A System.Net.TItem with a specific index from a System.Net.TItemCollection.
        //
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0 or index is greater than or equal to System.Net.TItemCollection.Count.
        public TItem this[int index] => items[index];


        //
        // Summary:
        //     Gets the number of items contained in a System.Net.TItemCollection.
        //
        // Returns:
        //     The number of items contained in a System.Net.TItemCollection.
        public int Count => items.Count;
        //
        // Summary:
        //     Gets a value that indicates whether a System.Net.TItemCollection is read-only.
        //
        //
        // Returns:
        //     true if this is a read-only System.Net.TItemCollection; otherwise, false. The
        //     default is true.
        public bool IsReadOnly { get; init; } = true;
        //
        // Summary:
        //     Gets a value that indicates whether access to a System.Net.TItemCollection is
        //     thread safe.
        //
        // Returns:
        //     true if access to the System.Net.TItemCollection is thread safe; otherwise,
        //     false. The default is false.
        public bool IsSynchronized { get; protected set; }
        //
        // Summary:
        //     Gets an object to synchronize access to the System.Net.TItemCollection.
        //
        // Returns:
        //     An object to synchronize access to the System.Net.TItemCollection.
        public object SyncRoot { get; private set; }

        //
        // Summary:
        //     Adds a System.Net.TItem to a System.Net.TItemCollection.
        //
        // Parameters:
        //   item:
        //     The System.Net.TItem to be added to a System.Net.TItemCollection.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     item is null.
        public virtual void Add(TItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (SyncRoot)
            {
                if (!ShouldObjectAddToCollection(item))
                {
                    return;
                }
                items.Add(item);
            }
        }

        protected abstract bool ShouldObjectAddToCollection(TItem item);
        //protected abstract bool ShouldObjectAddToCollection(BaseCollection<TItem> item);

        //
        // Summary:
        //     Adds the contents of a System.Net.TItemCollection to the current instance.
        //
        // Parameters:
        //   items:
        //     The System.Net.TItemCollection to be added.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     items is null.
        public virtual void Add(ICollection<TItem> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (var item in collection)
            {
                lock (SyncRoot)
                {
                    if (ShouldObjectAddToCollection(item))
                    {
                        items.Add(item);
                    }
                }
            }
        }
        //
        // Summary:
        //     Removes all elements from the System.Net.TItemCollection object.
        public virtual void Clear()
        {
            lock (SyncRoot)
            {
                items.Clear();
            }
        }
        //
        // Summary:
        //     Determines whether the specified item is in the System.Net.TItemCollection.
        //
        //
        // Parameters:
        //   item:
        //     The item to locate in the System.Net.TItemCollection.
        //
        // Returns:
        //     true if the specified item is found in the System.Net.TItemCollection; otherwise,
        //     false.
        public abstract bool Contains(TItem item);
        //
        // Summary:
        //     Copies the elements of a System.Net.TItemCollection to the specified array,
        //     starting at a particular index.
        //
        // Parameters:
        //   array:
        //     The target array to which the System.Net.TItemCollection will be copied.
        //
        //   index:
        //     The zero-based index in the target array where copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   T:System.ArgumentException:
        //     array is multidimensional. -or- The number of elements in this System.Net.TItemCollection
        //     is greater than the available space from index to the end of the destination
        //     array.
        //
        //   T:System.InvalidCastException:
        //     The elements in this System.Net.TItemCollection cannot be cast automatically
        //     to the type of the destination array.
        public virtual void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            lock (SyncRoot)
            {
                items.CopyTo(array as TItem[], index);
            }
        }

        //
        // Summary:
        //     Copies the elements of this System.Net.TItemCollection to a TItem
        //     array starting at the specified index of the target array.
        //
        // Parameters:
        //   array:
        //     The target System.Net.TItem array to which the System.Net.TItemCollection will
        //     be copied.
        //
        //   index:
        //     The zero-based index in the target array where copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   T:System.ArgumentException:
        //     array is multidimensional. -or- The number of elements in this System.Net.TItemCollection
        //     is greater than the available space from index to the end of the destination
        //     array.
        //
        //   T:System.InvalidCastException:
        //     The elements in this System.Net.TItemCollection cannot be cast automatically
        //     to the type of the destination array.
        public virtual void CopyTo(TItem[] array, int index)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            lock (SyncRoot)
            {
                items.CopyTo(array, index);
            }
        }

        //
        // Summary:
        //     Gets an enumerator that can iterate through a TItemCollection.
        //
        // Returns:
        //     An System.Collections.IEnumerator for this collection.
        public IEnumerator GetEnumerator() => items.GetEnumerator();
        //
        // Summary:
        //     Removes the specified item from the System.Net.TItemCollection.
        //
        // Parameters:
        //   item:
        //     The item to remove from the System.Net.TItemCollection.
        //
        // Returns:
        //     true if item was successfully removed from the System.Net.TItemCollection;
        //     otherwise, false. This method also returns false if item is not found in the
        //     original collection.
        public virtual bool Remove(TItem item)
        {
            lock (SyncRoot)
            {
                return items.Remove(item);
            }
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            lock (SyncRoot)
            {
                return items.GetEnumerator();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    items.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
