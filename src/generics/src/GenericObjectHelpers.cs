using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization; // Added for CultureInfo
using System.Reflection;

namespace CosmicLexicon.Foundation.Generics
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GenericObjectHelpers
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> CachedProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static T? MakeShallowCopy<T>(T inputObject, bool simpleTypesOnly = false)
        {
            if (inputObject == null) return default!;
            Type type = inputObject.GetType();

            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type.IsEnum)
                return inputObject;

            // Clarified logic: if simpleTypesOnly is true, and the type is not a value type and not a string, return default.
            // This means only primitives, decimals, DateTimes, enums, and strings (handled above)
            // or other value types should be copied if simpleTypesOnly is true.
            if (simpleTypesOnly && !(type.IsValueType || type == typeof(string)))
                return default!;

            if (type.IsArray)
            {
                var array = inputObject as Array;
                if (array == null) return default!;
                var newArray = (Array)array.Clone();
                return (T)(object)newArray;
            }

            var newObj = (T?)Activator.CreateInstance(typeof(T), true);
            if (newObj is null)
            {
                throw new InvalidOperationException($"Could not create instance of type {typeof(T)}");
            }

            PropertyInfo[] properties = CachedProperties.GetOrAdd(type, t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance));

            foreach (var property in properties)
            {
                if (property.CanWrite && property.CanRead)
                {
                    // Removed empty catch block. Exceptions during property setting should be handled if they are expected,
                    // otherwise, they should be allowed to propagate. For a shallow copy, direct assignment is typically safe.
                    property.SetValue(newObj, property.GetValue(inputObject, null), null);
                }
            }
            return newObj;
        }

        /// <summary>
        /// Converts an object to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="input">The object to convert.</param>
        /// <returns>The converted object if successful, otherwise the default value of T.</returns>
        public static T? ConvertTo<T>(object? input)
        {
            if (input is null)
            {
                return default;
            }

            if (input is T result)
            {
                return result;
            }

            try
            {
                return (T)Convert.ChangeType(input, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default;
            }
        }
    }
}
