using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OpenEchoSystem.Core.xCollections
{
    /// <summary>
    /// Observable List Collection class that implements collection change notifications
    /// </summary>
    /// <typeparam name="T">Object type that the list holds</typeparam>
    public class ObservableListCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, 
                                             INotifyCollectionChanged, INotifyPropertyChanged, IList, ICollection
    {
        private readonly List<T> baseList;
        private NotifyCollectionChangedEventHandler? collectionChanged;
        private PropertyChangedEventHandler? propertyChanged;

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add => collectionChanged = (NotifyCollectionChangedEventHandler?)Delegate.Combine(collectionChanged, value);
            remove => collectionChanged = (NotifyCollectionChangedEventHandler?)Delegate.Remove(collectionChanged, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => propertyChanged = (PropertyChangedEventHandler?)Delegate.Combine(propertyChanged, value);
            remove => propertyChanged = (PropertyChangedEventHandler?)Delegate.Remove(propertyChanged, value);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            collectionChanged?.Invoke(this, e);
        }

        public ObservableListCollection()
        {
            baseList = [];
        }

        public ObservableListCollection(int capacity)
        {
            baseList = new List<T>(capacity);
        }

        public ObservableListCollection(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            baseList = [..collection];
        }

        public void AddRange(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            
            var items = collection.ToList();
            if (items.Count > 0)
            {
                int startingIndex = baseList.Count;
                baseList.AddRange(items);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, startingIndex));
                OnPropertyChanged(nameof(Count));
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var items = collection.ToList();
            if (items.Count > 0)
            {
                baseList.InsertRange(index, items);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, index));
                OnPropertyChanged(nameof(Count));
            }
        }

        public void Add(T item)
        {
            baseList.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(nameof(Count));
        }

        public int Add(object? value)
        {
            if (value is T typedValue)
            {
                baseList.Add(typedValue);
                var index = baseList.Count - 1;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
                OnPropertyChanged(nameof(Count));
                return index;
            }
            throw new ArgumentException("Invalid type", nameof(value));
        }

        public void Clear()
        {
            var oldItems = baseList.ToList();
            baseList.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(nameof(Count));
        }

        public bool Contains(object? value) => value is T typedValue && baseList.Contains(typedValue);

        public bool Contains(T item) => baseList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            baseList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ArgumentNullException.ThrowIfNull(array);
            ((ICollection)baseList).CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator() => baseList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => baseList.GetEnumerator();

        public int IndexOf(T item) => baseList.IndexOf(item);

        public int IndexOf(object? value) => value is T typedValue ? baseList.IndexOf(typedValue) : -1;

        public void Insert(int index, T item)
        {
            baseList.Insert(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            OnPropertyChanged(nameof(Count));
        }

        public void Insert(int index, object? value)
        {
            if (value is T typedValue)
            {
                Insert(index, typedValue);
            }
            else
            {
                throw new ArgumentException("Invalid type", nameof(value));
            }
        }

        public bool Remove(T item)
        {
            var index = baseList.IndexOf(item);
            if (index >= 0)
            {
                baseList.RemoveAt(index);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
                OnPropertyChanged(nameof(Count));
                return true;
            }
            return false;
        }

        public void Remove(object? value)
        {
            if (value is T typedValue)
            {
                Remove(typedValue);
            }
        }

        public void RemoveAt(int index)
        {
            var item = baseList[index];
            baseList.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged(nameof(Count));
        }

        public void RemoveRange(int index, int count)
        {
            var removedItems = baseList.Skip(index).Take(count).ToList();
            baseList.RemoveRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, index));
            OnPropertyChanged(nameof(Count));
        }

        public void ClearDelegates()
        {
            collectionChanged = null;
            propertyChanged = null;
        }

        public int Count => baseList.Count;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot => this;
        public bool IsFixedSize => false;

        public T this[int index]
        {
            get => baseList[index];
            set 
            {
                var oldValue = baseList[index];
                baseList[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldValue, index));
                OnPropertyChanged($"Item");
            }
        }

        object? IList.this[int index]
        {
            get => this[index];
            set
            {
                if (value is T typedValue)
                {
                    this[index] = typedValue;
                }
                else
                {
                    throw new ArgumentException("Invalid type", nameof(value));
                }
            }
        }
    }
}
