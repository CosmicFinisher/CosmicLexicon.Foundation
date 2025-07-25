using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Loader;
using System.Xml.Linq;

namespace OpenEchoSystem.Core.xReflection.xAssembly
{
    //Intelisense
    //[EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeExtensions
    {
        private const string MICROSOFT = "Microsoft";
        private const string SYSTEM = "System";
        private const string ANONYMOUS = "AnonymousType";

        public static bool IsNullable(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);
            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
           }
        public static Type[] GetAggregatedConstructorParamTypes(this Type type)
        {
            var list = new List<Type>();
            using var constructorEnumerator = type.GetConstructors().AsEnumerable().GetEnumerator();

            while (constructorEnumerator.MoveNext())
            {
                using var parameterEnumerator = constructorEnumerator.Current.GetParameters().AsEnumerable().GetEnumerator();

                while (parameterEnumerator.MoveNext())
                {
                    if (list.Any(o => o
                                .GenericEquals(parameterEnumerator.Current.ParameterType, false)))
                        continue;
                    list.Add(parameterEnumerator.Current.ParameterType);
                }
            }

            return [.. list];
        }
        public static bool ShouldGenericEquals(this Type type)
        {
            //return type.ContainsGenericParameters;
            return type.GenericTypeArguments.Length < 1;
        }
        public static bool GuidEqual(this Type type, Guid otherTypeGuid)
             => type.GUID.CompareTo(otherTypeGuid) == 0;
        public static bool EqualOrHasInterfaces<TInterface>(this Type type)
            => type.EqualOrHasInterfaces<TInterface>(InterfaceFilter.None, typeof(TInterface).ShouldGenericEquals());
        public static bool EqualOrHasInterfaces<TInterface>(this Type type, InterfaceFilter interfaceFilter = InterfaceFilter.None, bool? checkGenricDefenitionEqual = null)
          => type.EqualOrHasInterfaces(typeof(TInterface), interfaceFilter, checkGenricDefenitionEqual);
        public static bool HasInterface<T>(this Type type, InterfaceFilter interfaceFilter = InterfaceFilter.None, bool? checkGenricDefenitionEqual = null)
    where T : class
=> type.HasInterface(typeof(T), interfaceFilter, checkGenricDefenitionEqual);
        public static bool HasInterface<T>(this Type type)
            where T : class
    => type.HasInterface<T>(InterfaceFilter.None, typeof(T).ShouldGenericEquals());
        public static bool EqualOrHasInterfaces(this Type type, Type iface, InterfaceFilter interfaceFilter = InterfaceFilter.None, bool? checkGenricDefenitionEqual = null)
        {
            if (type.IsInterface && type.GenericEquals(iface, checkGenricDefenitionEqual ?? iface.ShouldGenericEquals()))
                return true;
            return type.HasInterface(iface, interfaceFilter, checkGenricDefenitionEqual);
        }
        public static bool IsMicrosoftProduct(this Type type)
        {
            if (string.IsNullOrEmpty(type?.Namespace))
            {
                //IsNested= true, IsNestedPrivate= true, ____PrivateArray, IExplicitLayout = true
                //In Maui.dll
                return type.Module.Name.StartsWith(MICROSOFT, StringComparison.InvariantCulture)
                 || type.Module.Name.StartsWith(SYSTEM, StringComparison.InvariantCulture);
            }
            return type.Namespace.StartsWith(MICROSOFT, StringComparison.InvariantCulture)
         || type.Namespace.StartsWith(SYSTEM, StringComparison.InvariantCulture);
        }
        public static bool TryGetPublicParameterlessConstuctor(this Type type, [NotNullWhen(true)] out ConstructorInfo? publicParameterlessConstuctor)
        {
            publicParameterlessConstuctor = type.GetConstructors()
            .SingleOrDefault(o => o.IsConstructor && o.IsPublic && o.GetParameters().Length == 0);
            return publicParameterlessConstuctor is { };
        }
        public static bool HasPublicParameterlessConstuctor(this Type type)
        {
            return type.TryGetPublicParameterlessConstuctor(out _);
        }

        public static bool HasInterface(this Type type, Type iface, InterfaceFilter interfaceFilter, bool? checkGenricDefenitionEqual = null)
        {
            return type.GetInterfaces(interfaceFilter).Any(x => x.GenericEquals(iface, checkGenricDefenitionEqual ?? iface.ShouldGenericEquals()));
        }
        public static bool GuidEqual(this Type type, Type otherType)
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(otherType);

            return type.GuidEqual(otherType.GUID);
        }
        public static bool HasBaseType(this Type type)
        {
            return type.BaseType is { } && type.BaseType != typeof(object);
        }

        public static bool EqualOrHasBaseType(this Type type, Type baseType, bool checkGenricDefenitionEqual)
        {
            if (type.GenericEquals(baseType, checkGenricDefenitionEqual))
                return true;
            return type.HasBaseType(baseType, checkGenricDefenitionEqual);
        }
        public static bool HasBaseType(this Type type, Type baseType, bool checkGenricDefenitionEquale)
        {

            if (type.BaseType is null)
                return false;
            if (type.BaseType.GenericEquals(baseType, checkGenricDefenitionEquale))
                return true;

            return type.BaseType.HasBaseType(baseType, checkGenricDefenitionEquale);
        }
        public static bool TryGetAttributeByInterface<TAttribute>(Type type, string interfaceName, [NotNullWhen(true)] out IEnumerable<TAttribute> attributes)
     where TAttribute : Attribute
        {
            attributes = [];

            if (!type.TryGetCustomAttribute(out attributes))
                return false;

            attributes = attributes
                .Where(o => o.GetType()?.GetInterface(interfaceName) is { });

            return attributes.Any();
        }

        public static bool GenericEquals(this Type type, Type other, bool checkGenricDefenitionEqual)
        {
            return checkGenricDefenitionEqual ? type.GetTypeDefenitionIfItIsGeneric() == other.GetTypeDefenitionIfItIsGeneric()
                : type == other;

        }
        public static bool GenericEqualsOrHasBaseType<TType, TBaseType>(this Type type, bool checkGenricDefenitionEqual)
    where TType : Type
        {
            return typeof(TType).GenericEqualsOrHasBaseType(typeof(TBaseType), checkGenricDefenitionEqual);
        }
        public static bool GenericEqualsOrHasBaseType(this Type type, Type baseType, bool checkGenricDefenitionEqual)
        {
            return type.GenericEquals(baseType, checkGenricDefenitionEqual)
                    || type.HasBaseType(baseType, checkGenricDefenitionEqual);

        }
        public static bool GenericEqualsOrHasInterface<TInterfaceType>(this Type type, InterfaceFilter interfaceFilter = InterfaceFilter.None, bool? checkGenricDefenitionEqual = null)
            where TInterfaceType : Type
        {
            return type.GenericEqualsOrHasInterface(typeof(TInterfaceType), interfaceFilter, checkGenricDefenitionEqual);
        }
        public static bool GenericEqualsOrHasInterface(this Type type, Type iface, InterfaceFilter interfaceFilter = InterfaceFilter.None, bool? checkGenricDefenitionEqual = null)
        {
            return type.GenericEquals(iface, checkGenricDefenitionEqual ?? iface.ShouldGenericEquals())
                    || type.HasInterface(iface, interfaceFilter, checkGenricDefenitionEqual);

        }
        public static bool TryGetCustomAttribute<T>(this Type type, out IEnumerable<T> attributes)
    where T : Attribute
        {
            ArgumentNullException.ThrowIfNull(type);

            return (attributes = type.GetCustomAttributes<T>(true)).Any();
        }
        // public static bool TryGetTypeByInterface<TInterface>(Type type, out Type @interface)
        //where TInterface : interface
        // {
        //     @interface = type.GetInterface(nameof(TInterface));

        //     if(@interface is null)
        //         return false;


        //     return true;
        // }
        public static bool TryGetParent(this Type type, Type targetInterface, [NotNullWhen(true)] out Type? parent)
        {
            parent = type.BaseType;

            while (true)
            {
                if (parent is null)
                    break;
                if (parent.GuidEqual(targetInterface))
                    break;
                parent = parent.BaseType;
            }
            return parent?.GuidEqual(targetInterface) ?? false;
        }


        public static Type? GetPrimaryInterface(this Type type)
        {
            var ifaces = type.GetInterfaces();
            if (ifaces.Length == 0) return null;
            if (ifaces.Length == 1) return ifaces[0];
            Dictionary<Type, int> typeScores = GetInterfaceScores(ifaces);
            Type? winner = null;
            int bestScore = -1;
            foreach (var pair in typeScores)
                if (pair.Value > bestScore)
                {
                    bestScore = pair.Value;
                    winner = pair.Key;
                }
            return winner;
        }

        private static Dictionary<Type, int> GetInterfaceScores(Type[] ifaces)
        {
            Dictionary<Type, int> typeScores = [];

            foreach (var iface in ifaces)
                typeScores.Add(iface, 0);

            foreach (var iface in ifaces)
                foreach (var iface1 in ifaces)
                    if (iface.IsAssignableFrom(iface1))
                        typeScores[iface1]++;
            return typeScores;
        }

        public static Type[] GetInterfaces(this Type type, InterfaceFilter interfaceFilter, Type? targetInterface = null)
        {
            var info = (TypeInfo)type;
            ArgumentNullException.ThrowIfNull(type);
            if (targetInterface is { } && !targetInterface.IsInterface)
                throw new ArgumentException(nameof(targetInterface) + " is not an interface.");
            var allInterfaces = type.GetInterfaces()?.Where(x => x.IsInterface)?.ToArray() ?? [];
            IEnumerable<Type> filter = [];
            switch (interfaceFilter)
            {
                case InterfaceFilter.None:
                    filter = allInterfaces;
                break;
                case InterfaceFilter.NotInheritedFromInterfaces:
                    filter = FilterInterfacesDefinedByInterfaces(allInterfaces);
                break;
                case InterfaceFilter.NotExclusiveInterfaces:
                    filter = FilterInterfacesDefinedByInterfaces(allInterfaces);
                    filter = FilterInterfacesDefinedByBaseType(type, filter);
                break;

                case InterfaceFilter.InheritedFromInterface:
                    filter = GetInterfacesDefinedInterfaces(allInterfaces);
                break;
                default:
                    throw new NotImplementedException(nameof(interfaceFilter));
            }


            if (targetInterface is { })
                filter = filter.Where(o => o.GenericEquals(targetInterface, targetInterface.ShouldGenericEquals()));

            return [.. filter.Distinct()];


            static IEnumerable<Type> FilterInterfacesDefinedByInterfaces(Type[] ifaces)
            {
                return ifaces.Except(GetInterfacesDefinedInterfaces(ifaces));

            }
            static IEnumerable<Type> GetInterfacesDefinedInterfaces(Type[] ifaces)
            {
                return ifaces.SelectMany(x => x.GetInterfaces().Where(x => x.IsInterface));
            }
            static IEnumerable<Type> FilterInterfacesDefinedByBaseType(Type type, IEnumerable<Type> ifaces)
            {
                if (type.BaseType is null)
                    return ifaces;
                return ifaces.Except(type.BaseType.GetInterfaces().Where(x => x.IsInterface));
            }
        }




        //public static Type[] GetItSelfInterfaces(this Type type)
        //{
        //    if (type is null)
        //    {
        //        throw new ArgumentNullException(nameof(type));
        //    }
        //    var allInterfaces = type.GetInterfaces();
        //    var nonInheritedInterfaces = new HashSet<Type>(allInterfaces);
        //    if (type.BaseType == null)
        //        return allInterfaces;
        //    foreach (var baseInterface in type.BaseType.GetInterfaces())
        //    {
        //        RemoveInheritedInterfaces(baseInterface, nonInheritedInterfaces);

        //    }
        //    foreach (var iface in allInterfaces)
        //    {
        //        RemoveInheritedInterfaces(iface, nonInheritedInterfaces);
        //    }
        //    return nonInheritedInterfaces.ToArray();
        //}

        //public static void RemoveInheritedInterfaces(Type iface, HashSet<Type> ifaces)
        //{
        //    foreach (var inheritedIface in iface.GetInterfaces())
        //    {
        //        ifaces.Remove(inheritedIface);
        //        RemoveInheritedInterfaces(inheritedIface, ifaces);
        //    }
        //}

        public static IReadOnlyList<Type> GetBaseClasses(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);

            if (!type.IsClass)
                throw new ArgumentException(nameof(type) + " is not a class.");

            var list = new List<Type>();
            // dont remove (type.BaseType != typeof(object)). Cause StackOverFlow
            while (type.BaseType is { } && type.BaseType != typeof(object))
            {
                list.Add(type.BaseType ?? throw new InvalidProgramException(nameof(GetBaseClasses)));
                type = type.BaseType;
            }
            return list;
        }




        //private void ApplyAdditionalAttributes(object instance, Type type, PropertyInfo[] props)
        //{
        //    PropertyOverridingTypeDescriptor ctd = new PropertyOverridingTypeDescriptor(TypeDescriptor.GetProvider(instance).GetTypeDescriptor(instance));
        //    foreach (var prop in props)
        //    {
        //        var attrs = prop.GetCustomAttributes<FileBrowserVMAttribute>(true).ToList()[0].GetAdditionalAttributes();
        //        PropertyDescriptor pd2 =
        //            TypeDescriptor.CreateProperty(
        //                type, // or just _settings, if it's already a type
        //                TypeDescriptor.GetProperties(instance)[prop.Name],
        //                attrs.ToArray()
        //         );
        //        ctd.OverrideProperty(pd2);
        //    }
        //    TypeDescriptor.AddProvider(new TypeDescriptorOverridingProvider(ctd), instance);
        //    var props2 = TypeDescriptor.GetProperties(instance, true);

        //}
        //private void ApplyAdditionalAttributes(object instance, Type type)
        //{
        //    PropertyOverridingTypeDescriptor ctd = new PropertyOverridingTypeDescriptor(TypeDescriptor.GetProvider(instance).GetTypeDescriptor(instance));
        //    foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(instance))
        //    {
        //        var prop = type.GetProperties().FirstOrDefault(o => o.Name == pd.Name);
        //        var attrs = prop.GetCustomAttributes<BaseFormControlViewModelAttribute>(true).ToList();

        //        if (!attrs.Any())
        //            continue;

        //        var additionalAttributes = new List<Attribute>();
        //        for (int i = 0; i < attrs.Count; i++)
        //            additionalAttributes.AddRange(attrs[i].GetAdditionalAttributes());

        //        PropertyDescriptor pd2 =
        //            TypeDescriptor.CreateProperty(
        //                type, // or just _settings, if it's already a type
        //                pd,
        //                additionalAttributes.ToArray()
        //         );
        //        ctd.OverrideProperty(pd2);
        //    }
        //    TypeDescriptor.AddProvider(new TypeDescriptorOverridingProvider(ctd), instance);
        //    var props2 = TypeDescriptor.GetProperties(instance, true);

        //}
        public static bool IsAnonymousType(this Type? type)
        {
            ArgumentNullException.ThrowIfNull(type);
            return type.TryGetCustomAttribute<CompilerGeneratedAttribute>(out _)
                && (type.FullName?.Contains(ANONYMOUS) ?? false);
        }
        public static TypeCode GetTypeCode(this Type? type, bool throwIfUnknown = false)
        {
            if (type is null)
                return TypeCode.Empty;
            else if (type.GuidEqual(typeof(DBNull).GUID))
                return TypeCode.DBNull;
            else if (type.GuidEqual(typeof(bool).GUID))
                return TypeCode.Boolean;
            else if (type.GuidEqual(typeof(char).GUID))
                return TypeCode.Char;
            else if (type.GuidEqual(typeof(sbyte).GUID))
                return TypeCode.SByte;
            else if (type.GuidEqual(typeof(byte).GUID))
                return TypeCode.Byte;
            else if (type.GuidEqual(typeof(short).GUID))
                return TypeCode.Int16;
            else if (type.GuidEqual(typeof(ushort).GUID))
                return TypeCode.UInt16;
            else if (type.GuidEqual(typeof(int).GUID))
                return TypeCode.Int32;
            else if (type.GuidEqual(typeof(uint).GUID))
                return TypeCode.UInt32;
            else if (type.GuidEqual(typeof(long).GUID))
                return TypeCode.Int64;
            else if (type.GuidEqual(typeof(ulong).GUID))
                return TypeCode.UInt64;
            else if (type.GuidEqual(typeof(float).GUID))
                return TypeCode.Single;
            else if (type.GuidEqual(typeof(double).GUID))
                return TypeCode.Double;
            else if (type.GuidEqual(typeof(DateTime).GUID))
                return TypeCode.DateTime;
            else if (type.GuidEqual(typeof(string).GUID))
                return TypeCode.String;
            else if (type.GuidEqual(typeof(decimal).GUID))
                return TypeCode.Decimal;
            if (throwIfUnknown)
                throw new ArgumentOutOfRangeException(type.Name);
            else
                return TypeCode.Object;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class InterfaceDeclarationAttribute : Attribute
    {
        public InterfaceDeclarationAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }

        public Type InterfaceType { get; }
    }
}
