using System.ComponentModel;
using System.Reflection;

namespace CosmicLexicon.Foundation.Introspection.Modules
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PropertyInfoExtensions
    {
        public static bool HasNullableOperator(this PropertyInfo? property)
        {
            if (property is null) return false;
            var getAccessor = property.GetGetMethod();
            if (getAccessor is null) return false;

            var retType = getAccessor.ReturnType;
            return retType.GenericEquals(typeof(Nullable<>), checkGenricDefenitionEqual: true);
        }

        public static bool HasAnInaccessibleSetter(this PropertyInfo? property)
        {
        	if (property is null) return false;
        	ArgumentNullException.ThrowIfNull(property);
        	var setMethod = property.GetSetMethod(nonPublic: true);
        	if (setMethod is null || setMethod.IsPrivate) return true;
      
        	return !(setMethod.IsPublic || setMethod.IsFamily || setMethod.IsFamilyOrAssembly || setMethod.IsAssembly);
        }

        public static bool IsNullable(this PropertyInfo property)
        {
            ArgumentNullException.ThrowIfNull(property);
            return property.PropertyType.IsNullable();
        }

        public static bool TryGetCustomAttribute<T>(this PropertyInfo propType, out IEnumerable<T> attributes) where T : Attribute
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

        public static bool TryGetAttributeByInterface<TAttribute>(this PropertyInfo propType, string interfaceName, out IEnumerable<TAttribute> attributes) where TAttribute : Attribute
        {
            ArgumentNullException.ThrowIfNull(propType);
            ArgumentNullException.ThrowIfNull(interfaceName);
            
            var attrs = propType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().ToList();
            var filteredAttrs = attrs.Where(o => o.GetType()?.GetInterface(interfaceName) is not null).ToList();
            attributes = filteredAttrs;
            return ((List<TAttribute>)filteredAttrs).Count > 0;
        }

        public static bool IsAccessibleFromSetter(this PropertyInfo? property)
        {
        	if (property is null) return false;
        	ArgumentNullException.ThrowIfNull(property);
        	var setMethod = property.GetSetMethod(nonPublic: true);
        	return setMethod is { } && !setMethod.IsPrivate && (setMethod.IsPublic || setMethod.IsFamily || setMethod.IsAssembly || setMethod.IsFamilyAndAssembly)
        		&& property.CanWrite;
        }

        public static bool AnySetter(this PropertyInfo property)
        {
            ArgumentNullException.ThrowIfNull(property);
            var setMethod = property.GetSetMethod(nonPublic: true);
            return setMethod is not null && property.CanWrite;
        }
    }
}
