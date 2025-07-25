namespace OpenEchoSystem.Core.xReflection;

using System.Reflection;
using System;

//
// Summary:
//     Type cache info
//
// Type parameters:
//   T:
//     Type to cache.
public static class TypeCacheFor<T>
{
    //
    // Summary:
    //     The constructors
    public static readonly ConstructorInfo[] Constructors = typeof(T).GetConstructors();

    //
    // Summary:
    //     The fields
    public static readonly FieldInfo[] Fields = typeof(T).GetFields();

    //
    // Summary:
    //     The interfaces
    public static readonly Type[] Interfaces = typeof(T).GetInterfaces();

    //
    // Summary:
    //     The methods
    public static readonly MethodInfo[] Methods = typeof(T).GetMethods();

    //
    // Summary:
    //     The properties
    public static readonly PropertyInfo[] Properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

    //
    // Summary:
    //     The type
    public static readonly Type Type = typeof(T);
} 