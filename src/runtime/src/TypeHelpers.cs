using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEchoSystem.Core.xRuntime
{
    public static class TypeHelpers
    {
        public static object ParseToObject(Type propertyType, string value)
        {
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            var type = underlyingType ?? propertyType;

            if (string.IsNullOrEmpty(value) && underlyingType != null)
            {
                return null; // For nullable types, empty string means null
            }

            // Handle specific types that Convert.ChangeType might not handle directly or robustly enough
            if (type == typeof(Guid))
            {
                if (Guid.TryParse(value, out var guidResult))
                {
                    return guidResult;
                }
                throw new FormatException($"Cannot convert string '{value}' to type {type.FullName}. Invalid GUID format.");
            }
            else if (type == typeof(TimeSpan))
            {
                if (TimeSpan.TryParse(value, System.Globalization.CultureInfo.InvariantCulture, out var timeSpanResult))
                {
                    return timeSpanResult;
                }
                throw new FormatException($"Cannot convert string '{value}' to type {type.FullName}. Invalid TimeSpan format.");
            }
            else if (type.IsEnum)
            {
                try
                {
                    return Enum.Parse(type, value, ignoreCase: true);
                }
                catch (ArgumentException ex)
                {
                    throw new FormatException($"Cannot convert string '{value}' to enum type {type.FullName}.", ex);
                }
            }

            try
            {
                return Convert.ChangeType(value, type, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Cannot convert string '{value}' to type {type.FullName}.", ex);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Value '{value}' is out of range for type {type.FullName}.", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException($"Cannot convert string '{value}' to type {type.FullName}.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while converting string '{value}' to type {type.FullName}.", ex);
            }
        }
    }
}
