using System.ComponentModel;
using System.Globalization;

namespace CosmicLexicon.Foundation.Generics
{
    //
    // Summary:
    //     Generic extensions dealing with objects
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GenericObjectExtensions
    {
        extension<T>(T inputObject)
        {
            //
            // Summary:
            //     Checks to see if the object meets all the criteria. If it does, it returns the
            //     object. If it does not, it returns the default object
            //
            // Parameters:
            //   inputObject:
            //     Object to check
            //
            //   predicate:
            //     Predicate to check the object against
            //
            //   defaultValue:
            //     The default value to return
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The default object if it fails the criteria, the object otherwise
            public T Check(Predicate<T> predicate, T defaultValue = default!)
            {
                ArgumentNullException.ThrowIfNull(predicate);

                if (!predicate(inputObject))
                {
                    return defaultValue;
                }

                return inputObject;
            }

            //
            // Summary:
            //     Checks to see if the object meets all the criteria. If it does, it returns the
            //     object. If it does not, it returns the default object
            //
            // Parameters:
            //   inputObject:
            //     Object to check
            //
            //   predicate:
            //     Predicate to check the object against
            //
            //   defaultValue:
            //     The default value to return
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The default object if it fails the criteria, the object otherwise
            public T Check(Predicate<T> predicate, Func<T> defaultValue)
            {
                ArgumentNullException.ThrowIfNull(predicate);
                ArgumentNullException.ThrowIfNull(defaultValue);

                if (!predicate(inputObject))
                {
                    return defaultValue();
                }

                return inputObject;
            }

            //
            // Summary:
            //     Checks to see if the object is null. If it is, it returns the default object,
            //     otherwise the object is returned.
            //
            // Parameters:
            //   inputObject:
            //     Object to check
            //
            //   defaultValue:
            //     The default value to return
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The default object if it is null, the object otherwise
            public T Check(T defaultValue = default!) => inputObject.Check((Predicate<T>)((x) => !EqualityComparer<T>.Default.Equals(x, default!)), defaultValue);

            //
            // Summary:
            //     Checks to see if the object is null. If it is, it returns the default object,
            //     otherwise the object is returned.
            //
            // Parameters:
            //   inputObject:
            //     Object to check
            //
            //   defaultValue:
            //     The default value to return
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The default object if it is null, the object otherwise
            public T Check(Func<T> defaultValue)
            {
                ArgumentNullException.ThrowIfNull(defaultValue);

                return inputObject.Check((Predicate<T>)((x) => !EqualityComparer<T>.Default.Equals(x, default!)), defaultValue);
            }

            //
            // Summary:
            //     Determines if the object passes the predicate passed in
            //
            // Parameters:
            //   inputObject:
            //     Object to test
            //
            //   predicate:
            //     Predicate to test
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     True if the object passes the predicate, false otherwise
            public bool Is(Predicate<T> predicate) => predicate?.Invoke(inputObject) ?? false;

            //
            // Summary:
            //     Checks if two objects are equal using a custom comparer.
            //
            // Parameters:
            //   inputObject:
            //     The first object to compare.
            //
            //   comparisonObject:
            //     The second object to compare.
            //
            //   comparer:
            //     The comparer to use.
            //
            // Type parameters:
            //   T:
            //     The type of the objects to compare.
            //
            // Returns:
            //     True if the objects are equal, false otherwise.
            public bool Is(T comparisonObject, IEqualityComparer<T>? comparer = null)
            {
                comparer ??= EqualityComparer<T>.Default;
                return comparer.Equals(inputObject, comparisonObject);
            }
        }

        extension<T>(Func<T> function)
        {
            //
            // Summary:
            //     Executes a function, repeating it a number of times in case it fails
            //
            // Parameters:
            //   function:
            //     Function to run
            //
            //   attempts:
            //     Number of times to attempt it
            //
            //   retryDelay:
            //     The amount of milliseconds to wait between tries
            //
            //   timeOut:
            //     Max amount of time to wait for the function to run (waits for the current attempt
            //     to finish before checking)
            //
            // Type parameters:
            //   T:
            //     Return type
            //
            // Returns:
            //     The returned value from the function
            public T Execute(int attempts = 3, int retryDelay = 0, int timeOut = int.MaxValue)
            {
                if (function is null)
                {
                    return default!;
                }

                Exception? ex = null;
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                while (attempts > 0)
                {
                    try
                    {
                        return function();
                    }
                    catch (Exception ex2)
                    {
                        ex = ex2;
                    }

                    if (stopwatch.ElapsedMilliseconds > timeOut)
                    {
                        break;
                    }

                    Thread.Sleep(retryDelay);
                    attempts--;
                }

                if (ex != null)
                {
                    throw ex;
                }

                return default!;
            }
        }

        extension(Action action)
        {
            //
            // Summary:
            //     Executes an action, repeating it a number of times in case it fails
            //
            // Parameters:
            //   action:
            //     Action to run
            //
            //   attempts:
            //     Number of times to attempt it
            //
            //   retryDelay:
            //     The amount of milliseconds to wait between tries
            //
            //   timeOut:
            //     Max amount of time to wait for the function to run (waits for the current attempt
            //     to finish before checking)
            //
            // Returns:
            //     True if it is executed successfully, false otherwise
            public bool Execute(int attempts = 3, int retryDelay = 0, int timeOut = int.MaxValue)
            {
                if (action is null)
                {
                    return false;
                }

                Exception? ex = null;
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                while (attempts > 0)
                {
                    try
                    {
                        action();
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        ex = ex2;
                    }

                    if (stopwatch.ElapsedMilliseconds > timeOut)
                    {
                        break;
                    }

                    Thread.Sleep(retryDelay);
                    attempts--;
                }

                if (ex != null)
                {
                    throw ex;
                }

                return false;
            }
        }

        extension(object? input)
        {
            //
            // Summary:
            //     Calls the object's ToString function passing in the formatting
            //
            // Parameters:
            //   input:
            //     Input object
            //
            //   format:
            //     Format of the output string
            //
            // Returns:
            //     The formatted string
            public string FormatToString(string format)
            {
                if (input is null)
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(format))
                {
                    return input.ToString() ?? string.Empty;
                }

                if (input is IFormattable formattable)
                {
                    return formattable.ToString(format, null) ?? string.Empty;
                }

                return input.ToString() ?? string.Empty;
            }

#nullable disable
            /// <summary>
            /// Attempts to convert an object to a specific type.
            /// </summary>
            /// <typeparam name="T">The type to convert to.</typeparam>
            /// <param name="input">The object to convert.</param>
            /// <returns>The converted object if successful, otherwise the default value of T.</returns>
            public T To<T>()
            {
                var targetType = typeof(T);
                var underlyingType = Nullable.GetUnderlyingType(targetType);

                if (underlyingType != null)
                {
                    // It's a nullable value type
                    if (input == null)
                    {
                        return default(T)!; // Or return null if T is nullable
                    }
                    object? convertedValue = Convert.ChangeType(input, underlyingType, CultureInfo.InvariantCulture);
                    if (convertedValue == null)
                    {
                        return default(T)!;
                    }

                    if (convertedValue is T result)
                    {
                        return result;
                    }
                    return default(T)!;
                }

                return input == null ? (typeof(T).IsValueType ? default(T)! : default(T)!) : GenericObjectHelpers.ConvertTo<T>(input);
            }
        }

        extension<T>(T item)
        {
            //
            // Summary:
            //     Throws the specified exception if the predicate is true for the item
            //
            // Parameters:
            //   item:
            //     The item
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
            public T ThrowIf(Predicate<T> predicate, Func<Exception> exception)
            {
                if (predicate is null)
                {
                    return item;
                }

                if (predicate(item))
                {
                    throw exception();
                }

                return item;
            }

            //
            // Summary:
            //     Throws the specified exception if the predicate is true for the item
            //
            // Parameters:
            //   item:
            //     The item
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
            public T ThrowIf(Predicate<T> predicate, Exception exception)
            {
                if (predicate is null)
                {
                    return item;
                }

                if (predicate(item))
                {
                    throw exception;
                }

                return item;
            }

            //
            // Summary:
            //     Throws the specified exception if the predicate is false for the item
            //
            // Parameters:
            //   item:
            //     The item
            //
            //   predicate:
            //     Predicate to check
            //
            //   exception:
            //     Exception to throw if predicate is false
            //
            // Type parameters:
            //   T:
            //     Item type
            //
            // Returns:
            //     the original Item
            public T ThrowIfNot(Predicate<T> predicate, Exception exception)
            {
                Predicate<T> predicate2 = predicate;
                return item.ThrowIf((x) => !predicate2(x), exception);
            }

            //
            // Summary:
            //     Throws an exception if the item is its default value.
            //
            // Parameters:
            //   item:
            //     The item to check.
            //
            //   exception:
            //     The exception to throw.
            //
            //   equalityComparer:
            //     The equality comparer to use (defaults to default comparer).
            //
            // Type parameters:
            //   T:
            //     The type of the item.
            //
            // Returns:
            //     The original item if it is not its default value.
            public T ThrowIfDefault(Exception exception, IEqualityComparer<T>? equalityComparer = null)
            {
                equalityComparer ??= EqualityComparer<T>.Default;

                if (equalityComparer.Equals(item, default!))
                {
                    throw exception;
                }
                return item;
            }

            //
            // Summary:
            //     Throws an ArgumentNullException if the item is null, or an ArgumentException if it's a non-null default value.
            //
            // Parameters:
            //   item:
            //     The item to check.
            //
            //   name:
            //     Name of the argument.
            //
            //   equalityComparer:
            //     The equality comparer to use (defaults to default comparer).
            //
            // Type parameters:
            //   T:
            //     The type of the item.
            //
            // Returns:
            //     The original item if it is not its default value.
            public T ThrowIfDefault(string name, IEqualityComparer<T>? equalityComparer = null)
            {
                if (item is null && default(T) is null) // Check if item is null and T is a nullable type
                {
                    throw new ArgumentNullException(name);
                }

                // If not null, or if T is a non-nullable value type, check against default value
                equalityComparer ??= EqualityComparer<T>.Default;
                if (equalityComparer.Equals(item, default!))
                {
                    throw new ArgumentException($"Parameter '{name}' cannot be its default value.", name);
                }
                return item;
            }

            //
            // Summary:
            //     Throws an exception if the item is not its default value.
            //
            // Parameters:
            //   item:
            //     The item to check.
            //
            //   name:
            //     Name of the argument.
            //
            //   equalityComparer:
            //     The equality comparer to use (defaults to default comparer).
            //
            // Type parameters:
            //   T:
            //     The type of the item.
            //
            // Returns:
            //     The original item if it is its default value.
            public T? ThrowIfNotDefault(string name, IEqualityComparer<T>? equalityComparer = null)
            {
                equalityComparer ??= EqualityComparer<T>.Default;
                if (!equalityComparer.Equals(item, default!))
                {
                    throw new ArgumentException($"Parameter '{name}' must be its default value.", name);
                }
                return item;
            }

            //
            // Summary:
            //     Throws an exception if the item is not its default value.
            //
            // Parameters:
            //   item:
            //     The item to check.
            //
            //   exception:
            //     The exception to throw.
            //
            //   equalityComparer:
            //     The equality comparer to use (defaults to default comparer).
            //
            // Type parameters:
            //   T:
            //     The type of the item.
            //
            // Returns:
            //     The original item if it is its default value.
            public T? ThrowIfNotDefault(Exception exception, IEqualityComparer<T>? equalityComparer = null)
            {
                equalityComparer ??= EqualityComparer<T>.Default;
                if (!equalityComparer.Equals(item, default!))
                {
                    throw exception;
                }
                return item;
            }
        }

        extension<T>(T item) where T : class
        {
            //
            // Summary:
            //     Determines if the object is not null and throws an ArgumentException if it is
            //
            // Parameters:
            //   item:
            //     The object to check
            //
            //   name:
            //     Name of the argument
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     Returns Item
            public T? ThrowIfNotNull(string name)
            {
                if (item != null)
                {
                    throw new ArgumentNullException(name);
                }
                return item;
            }

            //
            // Summary:
            //     Determines if the object is not null and throws the exception passed in if it is
            //
            // Parameters:
            //   item:
            //     The object to check
            //
            //   exception:
            //     Exception to throw
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     Returns Item
            public T? ThrowIfNotNull(Exception exception)
            {
                if (item != null)
                {
                    throw exception;
                }
                return item;
            }
        }

        extension<T>(IEnumerable<T> item)
        {
            //
            // Summary:
            //     Determines if the IEnumerable is not null or empty and throws an ArgumentException
            //     if it is
            //
            // Parameters:
            //   item:
            //     The object to check
            //
            //   name:
            //     Name of the argument
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     Returns Item
            public IEnumerable<T>? ThrowIfNotNullOrEmpty(string name) => item.ThrowIfNotNullOrEmpty(new ArgumentException(name));

            //
            // Summary:
            //     Determines if the IEnumerable is not null or empty and throws the exception passed
            //     in if it is
            //
            // Parameters:
            //   item:
            //     The object to check
            //
            //   exception:
            //     Exception to throw
            //
            // Type parameters:
            //   T:
            //
            // Returns:
            //     Returns Item
            public IEnumerable<T>? ThrowIfNotNullOrEmpty(Exception exception)
            {
                if (item is null)
                {
                    return item;
                }
                if (!item.Any()) // If not null and empty, do not throw
                {
                    return item;
                }
                // If not null and not empty, throw
                throw exception;
            }
        }

        extension<T>(T obj)
        {
#nullable restore

            //
            // Summary:
            //     Runs a function based on a condition
            //
            // Parameters:
            //   obj:
            //     Object to use
            //
            //   predicate:
            //     Predicate to check
            //
            //   method:
            //     Method to run if predicate is true
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The original object
            public void When(Predicate<T> predicate, Action<T> method)
            {
                if (predicate is null)
                {
                    return;
                }

                if (predicate(obj))
                {
                    method?.Invoke(obj);
                }
            }

            //
            // Summary:
            //     Runs a function based on a condition
            //
            // Parameters:
            //   obj:
            //     Object to use
            //
            //   condition:
            //     Condition to check
            //
            //   function:
            //     Function to run if condition is true
            //
            // Type parameters:
            //   T:
            //     Object type
            //
            // Returns:
            //     The result of the function if the condition is true, otherwise the original object
            public T When(bool condition, Func<T, T> function)
            {
                if (condition)
                {
                    return function(obj);
                }
                return obj;
            }
        }
    }
}
