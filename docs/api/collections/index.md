# ConsmicLexicon.Foundation.Structures API Reference

## Overview

The ConsmicLexicon.Foundation.Structures namespace provides enhanced collection types and utilities designed for high-performance, type-safe operations in .NET applications.

## Key Components

### BaseCollection<TItem>

A thread-safe base collection implementation that serves as the foundation for specialized collections.
public abstract class BaseCollection<TItem> : ICollection<TItem>, IEnumerable<TItem>, 
    IReadOnlyCollection<TItem>, ICollection, IDisposable
    where TItem : IEquatable<TItem>
#### Properties

| Property | Type | Description |
|----------|------|-------------|
| Count | `int` | Number of items in collection |
| IsReadOnly | `bool` | Indicates if collection is read-only |
| IsSynchronized | `bool` | Indicates thread-safety status |
| SyncRoot | `object` | Synchronization object |

#### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| this[int] | `int index` | `TItem` | Indexed access to items |
| Dispose | - | `void` | Releases resources |

### ListExtensions

Extension methods for List<T> and related collection types.

#### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToReadOnly<T> | `T item` | `ReadOnlyCollection<T>` | Converts single item to ReadOnlyCollection |
| IsNullOrEmpty<T> | `IReadOnlyCollection<T> source` | `IReadOnlyCollection<T>` | Null-safe empty check |
| NullCheck<T> | `IReadOnlyCollection<T> source` | `IReadOnlyCollection<T>` | Ensures non-null collection |
| Concat<T> | `IReadOnlyCollection<T> collection, IReadOnlyCollection<T> otherCollection` | `IEnumerable<T>` | Concatenates collections |
| ToFlatList<T> | `List<List<T>?> collection` | `List<T>` | Flattens nested lists |
| AsFlattened<T> | `IEnumerable<List<T>?> collection` | `IEnumerable<T>` | Returns flattened enumerable |

### IEnumerableExtensions

Extension methods for IEnumerable<T> types.

#### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ForEach<T> | `IEnumerable<T> source, Action<T> action` | `void` | Executes action on each element |
| IsNullOrEmpty<T> | `IEnumerable<T> source` | `bool` | Checks if null or empty |
| Concat<T> | `IEnumerable<T> first, IEnumerable<T> second` | `IEnumerable<T>` | Safe concatenation |

## Usage Examples

### BaseCollection Usage
public class CustomCollection<T> : BaseCollection<T> where T : IEquatable<T>
{
    // Implement custom collection behavior
}
### List Extensions
// Convert single item to ReadOnly
var item = 42;
var readOnlyList = item.ToReadOnly();

// Flatten nested lists
var nestedLists = new List<List<int>> 
{
    new() { 1, 2 },
    new() { 3, 4 }
};
var flat = nestedLists.ToFlatList(); // [1, 2, 3, 4]
### Collection Operations
// Null-safe operations
IReadOnlyCollection<int> nullCollection = null;
var safe = nullCollection.IsNullOrEmpty(); // true

// Safe concatenation
var first = new[] { 1, 2 };
var second = new[] { 3, 4 };
var combined = first.Concat(second); // [1, 2, 3, 4]
## Best Practices

1. **Thread Safety**
   - Use BaseCollection<T> for thread-safe requirements
   - Utilize SyncRoot for custom synchronization

2. **Performance**
   - Prefer AsFlattened() for lazy evaluation
   - Use ToFlatList() when materialized list is needed

3. **Null Safety**
   - Use IsNullOrEmpty() for null checks
   - Leverage NullCheck() for null-safe operations

4. **Memory Management**
   - Implement IDisposable when extending BaseCollection<T>
   - Use using statements with disposable collections

## Common Pitfalls

1. **Thread Safety**
   - Don't assume thread safety without IsSynchronized
   - Always check IsReadOnly before modifications

2. **Performance**
   - Avoid unnecessary ToFlatList() calls
   - Consider memory impact of large collections

3. **Null Handling**
   - Don't skip null checks on external inputs
   - Handle null cases in custom implementations

## See Also

- [Collections.Generic Documentation](../collections-generic/index.md)
- [LINQ Extensions](../linq/index.md)
- [Threading Safety](../threading/index.md)