using CosmicLexicon.Foundation.Structures.Linq;
using System.Buffers;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text;


namespace CosmicLexicon.Foundation.Structures.Linq
{
    //
    // Summary:
    //     IEnumerable extensions
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IEnumerableExtensions
    {


        public static bool Empty<T>(this IEnumerable<T?>? array)
        {
            return !array.ToNullSafeArray().Any();
        }
        public static T?[] ToNullSafeArray<T>(this IEnumerable<T?>? array)
        {
            return array?.ToArray() ?? [];
        }
        public static IEnumerable<T?> ToNullSafeEnumerable<T>(this IEnumerable<T?>? array)
        {
            return array?.ToArray() ?? [];
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        //
        // Summary:
        //     Combines multiple IEnumerables together and returns a new IEnumerable containing
        //     all of the values
        //
        // Parameters:
        //   enumerable1:
        //     IEnumerable 1
        //
        //   additions:
        //     IEnumerables to concat onto the first item
        //
        // Type parameters:
        //   T:
        //     Type of the data in the IEnumerable
        //
        // Returns:
        //     A new IEnumerable containing all values


        //
        // Summary:
        //     Returns elements starting at the index and ending at the end index
        //
        // Parameters:
        //   list:
        //     List to search
        //
        //   start:
        //     Start index (inclusive)
        //
        //   end:
        //     End index (exclusive)
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The items between the start and end index
        public static IEnumerable<T> ElementsBetween<T>(this IEnumerable<T> list, int start, int end)
        {
            if (list != null && list.Any())
            {
                if (end > list.Count())
                {
                    end = list.Count();
                }


                if (start < 0)
                {
                    start = 0;
                }


                if (start > end)
                {
                    int num = start;
                    start = end;
                    end = num;
                }


                T[] TempList = [.. list];
                int x = start;
                while (x < end)
                {
                    yield return TempList[x];
                    int num2 = x + 1;
                    x = num2;
                }
            }
        }


        //
        // Summary:
        //     Removes values from a list that meet the criteria set forth by the predicate
        //
        // Parameters:
        //   value:
        //     List to cull items from
        //
        //   predicate:
        //     Predicate that determines what items to remove
        //
        // Type parameters:
        //   T:
        //     Value type
        //
        // Returns:
        //     An IEnumerable with the objects that meet the criteria removed
        public static IEnumerable<T> Except<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            Func<T, bool> predicate2 = predicate;
            if (value == null || !value.Any())
            {
                return [];
            }


            if (predicate2 == null)
            {
                return value;
            }


            return value.Where((x) => !predicate2(x));
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable between the start and end indexes
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   start:
        //     Item to start with (inclusive)
        //
        //   end:
        //     Item to end with (exclusive)
        //
        //   action:
        //     Action to do
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The original list
        public static IEnumerable<T> For<T>(this IEnumerable<T> list, int start, int end, Action<T, int> action)
        {
            if (list == null || !list.Any())
            {
                return [];
            }


            if (action == null)
            {
                return list;
            }


            int num = 0;
            foreach (T item in list.ElementsBetween(start, end))
            {
                action(item, num);
                num++;
            }


            return list;
        }


        //
        // Summary:
        //     Does a function for each item in the IEnumerable between the start and end indexes
        //     and returns an IEnumerable of the results
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   start:
        //     Item to start with
        //
        //   end:
        //     Item to end with
        //
        //   function:
        //     Function to do
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Return type
        //
        // Returns:
        //     The resulting list
        public static IEnumerable<TResult> For<TObject, TResult>(this IEnumerable<TObject> list, int start, int end, Func<TObject, int, TResult> function)
        {
            if (list == null || function == null || !list.Any())
            {
                return [];
            }


            int num = 0;
            TResult[] array = new TResult[end + 1 - start];
            foreach (TObject item in list.ElementsBetween(start, end))
            {
                array[num] = function(item, num);
                num++;
            }


            return array;
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   action:
        //     Action to do
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The original list
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T>? list, Action<T>? action)
        {
            if (list == null || !list.Any())
            {
                return [];
            }


            if (action == null)
            {
                return list;
            }


            foreach (T item in list)
            {
                action!(item);
            }


            return list;
        }


        //
        // Summary:
        //     Does a function for each item in the IEnumerable, returning a list of the results
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   function:
        //     Function to do
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Return type
        //
        // Returns:
        //     The resulting list
        public static IEnumerable<TResult> ForEach<TObject, TResult>(this IEnumerable<TObject> list, Func<TObject, TResult> function)
        {
            Func<TObject, TResult> function2 = function;
            if (list == null || function2 == null || !list.Any())
            {
                return [];
            }


            return list.Select((x) => function2(x)).ToArray();
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   action:
        //     Action to do
        //
        //   catchAction:
        //     Action that occurs if an exception occurs
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The original list
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action, Action<T, Exception> catchAction)
        {
            if (list == null || !list.Any())
            {
                return [];
            }


            if (action == null || catchAction == null)
            {
                return list;
            }


            foreach (T item in list)
            {
                try
                {
                    action(item);
                }
                catch (Exception arg)
                {
                    catchAction(item, arg);
                }
            }


            return list;
        }


        //
        // Summary:
        //     Does a function for each item in the IEnumerable, returning a list of the results
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   function:
        //     Function to do
        //
        //   catchAction:
        //     Action that occurs if an exception occurs
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Return type
        //
        // Returns:
        //     The resulting list
        public static IEnumerable<TResult> ForEach<TObject, TResult>(this IEnumerable<TObject> list, Func<TObject, TResult> function, Action<TObject, Exception> catchAction)
        {
            Func<TObject, TResult> function2 = function;
            if (list == null || function2 == null || catchAction == null || !list.Any())
            {
                return [];
            }


            TResult[] array = new TResult[list.Count()];
            int num = 0;
            foreach (TObject item in list)
            {
                try
                {
                    array[num] = function(item);
                }
                catch (Exception arg)
                {
                    catchAction(item, arg);
                }


                num++;
            }


            return array;
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable in parallel
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   action:
        //     Action to do
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The original list
        public static IEnumerable<T> ForEachParallel<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null || !list.Any())
            {
                return [];
            }


            if (action == null)
            {
                return list;
            }


            Parallel.ForEach(list, action);
            return list;
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable in parallel
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   function:
        //     Function to do
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Results type
        //
        // Returns:
        //     The results in an IEnumerable list
        public static IEnumerable<TResult> ForEachParallel<TObject, TResult>(this IEnumerable<TObject> list, Func<TObject, TResult> function)
        {
            Func<TObject, TResult> function2 = function;
            if (list == null || function2 == null || !list.Any())
            {
                return [];
            }


            return list.ForParallel(0, list.Count() - 1, (x, _) => function2(x));


        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   action:
        //     Action to do
        //
        //   catchAction:
        //     Action that occurs if an exception occurs
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Results type
        //
        // Returns:
        //     The results in an IEnumerable list
        public static IEnumerable<TObject> ForEachParallel<TObject>(this IEnumerable<TObject> list, Action<TObject> action, Action<TObject, Exception> catchAction)
        {
            if (list == null || action == null || catchAction == null || !list.Any())
            {
                return [];
            }

            Parallel.ForEach(list, (item) =>
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    catchAction(item, ex);
                }
            });

            return list;
        }


        //
        // Summary:
        //     Does a function for each item in the IEnumerable, returning a list of the results
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   function:
        //     Function to do
        //
        //   catchAction:
        //     Action that occurs if an exception occurs
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Return type
        //
        // Returns:
        //     The resulting list
        public static IEnumerable<TResult> ForEachParallel<TObject, TResult>(this IEnumerable<TObject> list, Func<TObject, TResult> function, Action<TObject, Exception> catchAction)
        {
            if (list == null || function == null || catchAction == null || !list.Any())
            {
                return [];
            }

            ConcurrentBag<TResult> results = new ConcurrentBag<TResult>();
            Parallel.ForEach(list, (item) =>
            {
                try
                {
                    results.Add(function(item));
                }
                catch (Exception ex)
                {
                    catchAction(item, ex);
                }
            });

            return results;
        }


        //
        // Summary:
        //     Does an action for each item in the IEnumerable in parallel
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   start:
        //     Item to start with
        //
        //   end:
        //     Item to end with
        //
        //   action:
        //     Action to do
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     The original list
        public static IEnumerable<T> ForParallel<T>(this IEnumerable<T> list, int start, int end, Action<T, int> action)
        {
            if (list == null || !list.Any())
            {
                return [];
            }

            if (action == null)
            {
                return list;
            }

            T[] TempList = [.. list];
            Parallel.For(start, end + 1, (x) =>
            {
                action(TempList[x], x);
            });

            return list;
        }


        //
        // Summary:
        //     Does a function for each item in the IEnumerable, returning a list of the results
        //
        // Parameters:
        //   list:
        //     IEnumerable to iterate over
        //
        //   start:
        //     Item to start with
        //
        //   end:
        //     Item to end with
        //
        //   function:
        //     Function to do
        //
        // Type parameters:
        //   TObject:
        //     Object type
        //
        //   TResult:
        //     Results type
        //
        // Returns:
        //     The results in an IEnumerable list
        public static IEnumerable<TResult> ForParallel<TObject, TResult>(this IEnumerable<TObject> list, int start, int end, Func<TObject, int, TResult> function)
        {
            if (list == null || function == null || !list.Any())
            {
                return [];
            }

            TObject[] TempList = [.. list];
            TResult[] results = new TResult[list.Count()];
            Parallel.For(start, end + 1, (x) =>
            {
                results[x] = function(TempList[x], x);
            });

            return results;
        }

        //
        // Summary:
        //     Returns only the last X amount of items from the list
        //
        // Parameters:
        //   list:
        //     List to pull items from
        //
        //   count:
        //     Number of items to return
        //
        // Type parameters:
        //   T:
        //     Object type
        //
        // Returns:
        //     An IEnumerable of the last X items
        public static IEnumerable<T> Last<T>(this IEnumerable<T> list, int count)
        {
            if (list == null || !list.Any())
            {
                return [];
            }

            return list.Skip(System.Math.Max(0, list.Count() - count));
        }

        //
        // Summary:
        //     Throws the specified exception if all items meet the predicate
        //
        // Parameters:
        //   list:
        //     List of objects
        //
        //   predicate:
        //     Predicate to check
        //
        //   exception:
        //     Exception to throw if predicate is true
        //
        // Type parameters:
        //   T:
        //     Item type
        //
        // Returns:
        //     the original Item
        public static IEnumerable<T> ThrowIfAll<T>(this IEnumerable<T> list, Func<T, bool> predicate, Func<Exception> exception)
        {
            if (list == null)
            {
                return list;
            }

            if (predicate == null)
            {
                return list;
            }

            if (list.All(predicate))
            {
                throw exception();
            }

            return list;
        }

        //
        // Summary:
        //     Throws the specified exception if all items meet the predicate
        //
        // Parameters:
        //   list:
        //     List of objects
        //
        //   predicate:
        //     Predicate to check
        //
        //   exception:
        //     Exception to throw if predicate is true
        //
        // Type parameters:
        //   T:
        //     Item type
        //
        // Returns:
        //     the original Item
        public static IEnumerable<T> ThrowIfAll<T>(this IEnumerable<T> list, Func<T, bool> predicate, Exception exception)
        {
            if (list == null)
            {
                return list;
            }

            if (predicate == null)
            {
                return list;
            }

            if (list.All(predicate))
            {
                throw exception;
            }

            return list;
        }

        //
        // Summary:
        //     Throws the specified exception if any items meet the predicate
        //
        // Parameters:
        //   list:
        //     List of objects
        //
        //   predicate:
        //     Predicate to check
        //
        //   exception:
        //     Exception to throw if predicate is true
        //
        // Type parameters:
        //   T:
        //     Item type
        //
        // Returns:
        //     the original Item
        public static IEnumerable<T> ThrowIfAny<T>(this IEnumerable<T> list, Func<T, bool> predicate, Func<Exception> exception)
        {
            if (list == null)
            {
                return list;
            }

            if (predicate == null)
            {
                return list;
            }

            if (list.Any(predicate))
            {
                throw exception();
            }

            return list;
        }

        //
        // Summary:
        //     Throws the specified exception if any items meet the predicate
        //
        // Parameters:
        //   list:
        //     List of objects
        //
        //   predicate:
        //     Predicate to check
        //
        //   exception:
        //     Exception to throw if predicate is true
        //
        // Type parameters:
        //   T:
        //     Item type
        //
        // Returns:
        //     the original Item
        public static IEnumerable<T> ThrowIfAny<T>(this IEnumerable<T> list, Func<T, bool> predicate, Exception exception)
        {
            if (list == null)
            {
                return list;
            }

            if (predicate == null)
            {
                return list;
            }

            if (list.Any(predicate))
            {
                throw exception;
            }

            return list;
        }

        //
        // Summary:
        //     Converts the IEnumerable to an array
        //
        // Parameters:
        //   list:
        //     The list to convert
        //
        //   convertingFunction:
        //     The converting function.
        //
        // Type parameters:
        //   TSource:
        //     The type of the source.
        //
        //   TTarget:
        //     The type of the target.
        //
        // Returns:
        //     The array version of the original list
        public static TTarget[] ToArray<TSource, TTarget>(this IEnumerable<TSource> list, Func<TSource, TTarget> convertingFunction)
        {
            if (list == null)
            {
                return [];
            }

            if (convertingFunction == null)
            {
                return [];
            }

            return list.Select(convertingFunction).ToArray();
        }

        //
        // Summary:
        //     Converts the IEnumerable to a list
        //
        // Parameters:
        //   list:
        //     The list to convert
        //
        //   convertingFunction:
        //     The converting function.
        //
        // Type parameters:
        //   TSource:
        //     The type of the source.
        //
        //   TTarget:
        //     The type of the target.
        //
        // Returns:
        //     The list version of the original list
        public static List<TTarget> ToList<TSource, TTarget>(this IEnumerable<TSource> list, Func<TSource, TTarget> convertingFunction)
        {
            if (list == null)
            {
                return [];
            }

            if (convertingFunction == null)
            {
                return [];
            }

            return list.Select(convertingFunction).ToList();
        }

        //
        // Summary:
        //     Converts the IEnumerable to a string
        //
        // Parameters:
        //   list:
        //     The list to convert
        //
        //   itemOutput:
        //     The converting function.
        //
        //   seperator:
        //     Seperator that is between each item
        //
        // Type parameters:
        //   T:
        //     The type of the source.
        //
        // Returns:
        //     A string
        public static string ToString<T>(this IEnumerable<T> list, Func<T, string>? itemOutput = null, string seperator = ",")
        {
            if (list == null)
            {
                return "";
            }

            if (!list.Any())
            {
                return "";
            }

            StringBuilder builder = new StringBuilder();
            if (itemOutput == null)
            {
                foreach (T Item in list)
                {
                    builder.Append(Item);
                    builder.Append(seperator);
                }
            }
            else
            {
                foreach (T Item in list)
                {
                    builder.Append(itemOutput(Item));
                    builder.Append(seperator);
                }
            }

            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - seperator.Length, seperator.Length);
            }

            return builder.ToString();
        }

        //
        // Summary:
        //     Transverses the specified collection.
        //
        // Parameters:
        //   collection:
        //     The collection.
        //
        //   property:
        //     The property.
        //
        // Type parameters:
        //   T:
        //     The collection's type.
        //
        // Returns:
        //     An IEnumerable of the specified type
        public static IEnumerable<T> Transverse<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> property)
        {
            if (collection == null)
            {
                yield break;
            }

            foreach (T item in collection)
            {
                yield return item;
                foreach (T child in item.Transverse(property))
                {
                    yield return child;
                }
            }
        }

        //
        // Summary:
        //     Transverses the specified item.
        //
        // Parameters:
        //   item:
        //     The item.
        //
        //   property:
        //     The property.
        //
        // Type parameters:
        //   T:
        //     The item's type.
        //
        // Returns:
        //     An IEnumerable of the specified type
        public static IEnumerable<T> Transverse<T>(this T item, Func<T, IEnumerable<T>> property)
        {
            if (item == null)
            {
                yield break;
            }

            IEnumerable<T> children = property(item);
            if (children == null)
            {
                yield break;
            }

            foreach (T child in children)
            {
                yield return child;
                foreach (T subChild in child.Transverse(property))
                {
                    yield return subChild;
                }
            }
        }
    }
}
