using System.ComponentModel;
using System.Reflection;

namespace CosmicLexicon.Foundation.Introspection.Modules
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PropertyInfoExtensions
    {
        extension(PropertyInfo? property)
        {
            public bool HasNullableOperator()
            {
                if (property is null) return false;
                var getAccessor = property.GetGetMethod();
                if (getAccessor is null) return false;

                var retType = getAccessor.ReturnType;
                return retType.GenericEquals(typeof(Nullable<>), checkGenricDefenitionEqual: true);
            }

            public bool HasAnInaccessibleSetter()
            {
                if (property is null) return false;
                ArgumentNullException.ThrowIfNull(property);
                var setMethod = property.GetSetMethod(nonPublic: true);
                if (setMethod is null || setMethod.IsPrivate) return true;

                return !(setMethod.IsPublic || setMethod.IsFamily || setMethod.IsFamilyOrAssembly || setMethod.IsAssembly);
            }

            public bool IsAccessibleFromSetter()
            {
                if (property is null) return false;
                ArgumentNullException.ThrowIfNull(property);
                var setMethod = property.GetSetMethod(nonPublic: true);
                return setMethod is { } && !setMethod.IsPrivate && (setMethod.IsPublic || setMethod.IsFamily || setMethod.IsAssembly || setMethod.IsFamilyAndAssembly)
                    && property.CanWrite;
            }
        }

        extension(PropertyInfo property)
        {
            public bool IsNullable()
            {
                ArgumentNullException.ThrowIfNull(property);
                return property.PropertyType.IsNullable();
            }

            public bool AnySetter()
            {
                ArgumentNullException.ThrowIfNull(property);
                var setMethod = property.GetSetMethod(nonPublic: true);
                return setMethod is not null && property.CanWrite;
            }
        }

        extension(PropertyInfo propType)
        {
            public bool TryGetCustomAttribute<T>(out IEnumerable<T> attributes) where T : Attribute
            {
                ArgumentNullException.ThrowIfNull(propType);

                var attrs = propType.GetCustomAttributes(typeof(T), true).Cast<T>().ToList();
                if (((List<T>)attrs).Count == 0)
                {
                    attributes = null!;
                    return false;
                }
                attributes = attrs;
                return true;
            }

            public bool TryGetAttributeByInterface<TAttribute>(string interfaceName, out IEnumerable<TAttribute> attributes) where TAttribute : Attribute
            {
                ArgumentNullException.ThrowIfNull(propType);
                ArgumentNullException.ThrowIfNull(interfaceName);

                var attrs = propType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().ToList();
                var filteredAttrs = attrs.Where(o => o.GetType()?.GetInterface(interfaceName) is not null).ToList();
                attributes = filteredAttrs;
                return ((List<TAttribute>)filteredAttrs).Count > 0;
            }
        }
    }
}
