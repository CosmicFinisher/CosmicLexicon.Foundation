using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using OpenEchoSystem.Core.xLinq;
using OpenEchoSystem.Core.xLinq;
using OpenEchoSystem.Core.xGenerics;

namespace OpenEchoSystem.Core.xCollections.Extensions
{
    /// <summary>
    /// IEnumerable extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this ICollection<T> collection, ICollection<T>[] otherCollection)
        {
            if (collection == null || collection.Count == 0)
            {
                if (otherCollection == null || otherCollection.Length == 0)
                {
                    return Enumerable.Empty<T>();
                }
                return otherCollection.Where(c => c != null && c.Count > 0)
                                    .SelectMany(c => c);
            }

            if (otherCollection == null || otherCollection.Length == 0)
            {
                return collection;
            }

            return otherCollection.Where(c => c != null && c.Count > 0)
                                 .Aggregate(collection as IEnumerable<T>, 
                                          (current, next) => current.Concat(next));
        }

        public static IEnumerable<T> Concat<T>(this ICollection<T> collection, ICollection<T> otherCollection)
        {
            if (collection == null || collection.Count == 0)
            {
                if (otherCollection == null || otherCollection.Count == 0)
                {
                    return Enumerable.Empty<T>();
                }
                return otherCollection;
            }

            if (otherCollection == null || otherCollection.Count == 0)
            {
                return collection;
            }

            return Enumerable.Concat(collection, otherCollection);
        }
        //
        // Summary:
        //     Does a left join on the two lists
        //
        // Parameters:
        //   outer:
        //     The outer list.
        //
        //   inner:
        //     The inner list.
        //
        //   outerKeySelector:
        //     The outer key selector.
        //
        //   innerKeySelector:
        //     The inner key selector.
        //
        //   resultSelector:
        //     The result selector.
        //
        //   comparer:
        //     The comparer (if null, a generic comparer is used).
        //
        // Type parameters:
        //   TObject1:
        //     The type of outer list.
        //
        //   TObject2:
        //     The type of inner list.
        //
        //   TKey:
        //     The type of the key.
        //
        //   TResult:
        //     The return type
        //
        // Returns:
        //     Returns a left join of the two lists
        public static IEnumerable<TResult> LeftJoin<TObject1, TObject2, TKey, TResult>(this IEnumerable<TObject1> outer, IEnumerable<TObject2> inner, Func<TObject1, TKey> outerKeySelector, Func<TObject2, TKey> innerKeySelector, Func<TObject1, TObject2, TResult> resultSelector, IEqualityComparer<TKey>? comparer = null)
        {
            if (inner is null)
            {
                throw new ArgumentNullException(nameof(inner));
            }

            if (outerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(outerKeySelector));
            }

            if (innerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(innerKeySelector));
            }

            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            comparer ??= new GenericEqualityComparer<TKey>();

            return outer.ForEach((x) => new
            {
                left = x,
                right = inner.FirstOrDefault((y) => comparer.Equals(innerKeySelector(y), outerKeySelector(x)))
            }).ForEach(x => resultSelector(x.left, x.right));
        }

        //
        // Summary:
        //     Does an outer join on the two lists
        //
        // Parameters:
        //   outer:
        //     The outer list.
        //
        //   inner:
        //     The inner list.
        //
        //   outerKeySelector:
        //     The outer key selector.
        //
        //   innerKeySelector:
        //     The inner key selector.
        //
        //   resultSelector:
        //     The result selector.
        //
        //   comparer:
        //     The comparer (if null, a generic comparer is used).
        //
        // Type parameters:
        //   T1:
        //     The type of outer list.
        //
        //   T2:
        //     The type of inner list.
        //
        //   TKey:
        //     The type of the key.
        //
        //   TResult:
        //     The return type
        //
        // Returns:
        //     Returns an outer join of the two lists
        public static IEnumerable<TResult> OuterJoin<T1, T2, TKey, TResult>(this IEnumerable<T1> outer, IEnumerable<T2> inner, Func<T1, TKey> outerKeySelector, Func<T2, TKey> innerKeySelector, Func<T1, T2, TResult> resultSelector, IEqualityComparer<TKey>? comparer = null)
        {
            if (inner is null)
            {
                throw new ArgumentNullException(nameof(inner));
            }

            if (outer is null)
            {
                throw new ArgumentNullException(nameof(outer));
            }

            if (outerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(outerKeySelector));
            }

            if (innerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(innerKeySelector));
            }

            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            IEnumerable<TResult> first = outer.LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
            IEnumerable<TResult> second = outer.RightJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
            return first.Union(second);
        }

        //
        // Summary:
        //     Determines the position of an object if it is present, otherwise it returns -1
        //
        // Parameters:
        //   list:
        //     List of objects to search
        //
        //   item:
        //     Object to find the position of
        //
        //   equalityComparer:
        //     Equality comparer used to determine if the object is present
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The position of the object if it is present, otherwise -1
        public static int PositionOf<T>(this IEnumerable<T> list, T item, IEqualityComparer<T>? equalityComparer = null)
        {
            if (list == null || !list.Any())
            {
                return -1;
            }

            if (equalityComparer == null)
            {
                equalityComparer = new GenericEqualityComparer<T>();
            }

            int num = 0;
            foreach (T item2 in list)
            {
                if (equalityComparer!.Equals(item, item2))
                {
                    return num;
                }

                num++;
            }

            return -1;
        }

        //
        // Summary:
        //     Does a right join on the two lists
        //
        // Parameters:
        //   outer:
        //     The outer list.
        //
        //   inner:
        //     The inner list.
        //
        //   outerKeySelector:
        //     The outer key selector.
        //
        //   innerKeySelector:
        //     The inner key selector.
        //
        //   resultSelector:
        //     The result selector.
        //
        //   comparer:
        //     The comparer (if null, a generic comparer is used).
        //
        // Type parameters:
        //   T1:
        //     The type of outer list.
        //
        //   T2:
        //     The type of inner list.
        //
        //   TKey:
        //     The type of the key.
        //
        //   TResult:
        //     The return type
        //
        // Returns:
        //     Returns a right join of the two lists
        public static IEnumerable<TResult> RightJoin<T1, T2, TKey, TResult>(this IEnumerable<T1> outer, IEnumerable<T2> inner, Func<T1, TKey> outerKeySelector, Func<T2, TKey> innerKeySelector, Func<T1, T2, TResult> resultSelector, IEqualityComparer<TKey>? comparer = null)
        {
            IEnumerable<T1> outer2 = outer;
            IEqualityComparer<TKey> comparer2 = comparer;
            Func<T2, TKey> innerKeySelector2 = innerKeySelector;
            Func<T1, TKey> outerKeySelector2 = outerKeySelector;
            Func<T1, T2, TResult> resultSelector2 = resultSelector;
            if (outer2 == null || outerKeySelector2 == null || innerKeySelector2 == null || resultSelector2 == null)
            {
                return [];
            }

            if (comparer2 == null)
            {
                comparer2 = new GenericEqualityComparer<TKey>();
            }

            return inner.ForEach((x) => new
            {
                left = outer2.FirstOrDefault((y) => comparer2.Equals(innerKeySelector2(x), outerKeySelector2(y))),
                right = x
            }).ForEach(x => resultSelector2(x.left, x.right));
        }
        //
        // Summary:
        //     Returns only distinct items from the IEnumerable based on the predicate
        //
        // Parameters:
        //   enumerable:
        //     List of objects
        //
        //   predicate:
        //     Predicate that is used to determine if two objects are equal. True if they are
        //     the same, false otherwise
        //
        // Type parameters:
        //   T:
        //     Object type within the list
        //
        // Returns:
        //     An IEnumerable of only the distinct items
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> enumerable, Func<T, T, bool> predicate)
        {
            if (enumerable == null || !enumerable.Any())
            {
                yield break;
            }

            predicate ??= new GenericEqualityComparer<T>().Equals;

            HashSet<T> distinctItems = new HashSet<T>();
            foreach (T item in enumerable)
            {
                if (distinctItems.Add(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Converts the IEnumerable to an observable list collection with type conversion
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <param name="convertingFunction">The function to convert items from source to target type.</param>
        /// <returns>The observable list collection version of the original list</returns>
        public static ObservableListCollection<TTarget> ToObservableList<TSource, TTarget>(this IEnumerable<TSource>? list, Func<TSource, TTarget> convertingFunction)
        {
            ArgumentNullException.ThrowIfNull(convertingFunction);

            if (list is null)
            {
                return new ObservableListCollection<TTarget>();
            }

            return new ObservableListCollection<TTarget>(list.Select(convertingFunction));
        }

    }
}

