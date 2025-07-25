using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace OpenEchoSystem.Core.xReflection.xAssembly
{

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> QueryTypes(this Assembly assembly, AssemblyTypeCollectionQuery loadingCondition)
        {
            return loadingCondition switch
            {
                AssemblyTypeCollectionQuery.All => assembly.DefinedTypes,
                AssemblyTypeCollectionQuery.Exported => assembly.ExportedTypes,
                _ => throw new NotImplementedException(),
            };
        }
        public static IEnumerable<Type> GetTypes(this Assembly assembly, AssemblyTypeQuery loadingCondition)
        {
            return loadingCondition switch
            {
                AssemblyTypeQuery.All => assembly.GetTypes(),
                AssemblyTypeQuery.Exported => assembly.GetExportedTypes(),
                AssemblyTypeQuery.Forvarded => assembly.GetForwardedTypes(),
                _ => throw new NotImplementedException(),
            };
        }
        public static IEnumerable<Type> FilterTypes(this Assembly assembly,
                                                    Type type,
                                                    AssemblyTypeQuery loadingCondition,
                                                    InterfaceFilter interfaceFilter,
                                                    bool genericEqual = false)
        {
            if (type.IsInterface)
                return assembly.FilterTypesByInterface(type, loadingCondition, [],
                interfaceFilter, genericEqual);
            else if (type.IsClass)
                return assembly.FilterTypesByBaseClass(type, loadingCondition, [AssemblyTypeFilter.FilterInterfaces], false, genericEqual);
            else
                throw new NotImplementedException(nameof(AssemblyExtensions) + ":" + nameof(FilterTypes));
        }
        public static bool CompareTypeDefenition(this Type type, Type other)
        {
            return other.GenericTypeArguments.Length < 1
                    ?
                                    type.GetTypeDefenitionIfItIsGeneric() == other.GetTypeDefenitionIfItIsGeneric()
                                :
                                      type == other;

        }

        public static Type GetTypeDefenitionIfItIsGeneric(this Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericTypeDefinition();
            return type;
        }
  
        public static IEnumerable<Type> FilterTypesByInterface<TInterface>(this Assembly assembly,
                                                
                                                AssemblyTypeQuery loadingCondition = AssemblyTypeQuery.All,
                                           InterfaceFilter interfaceFilter = InterfaceFilter.None,
                                             bool? genericEqual = null)
        {
            return assembly.FilterTypesByInterface(typeof(TInterface), loadingCondition, interfaceFilter, genericEqual ?? typeof(TInterface).ShouldGenericEquals());
        }
        public static IEnumerable<Type> FilterTypesByInterface(this Assembly assembly,
                                                       Type type,
                                                       AssemblyTypeQuery loadingCondition = AssemblyTypeQuery.All,
                                                  InterfaceFilter interfaceFilter = InterfaceFilter.None,
                                                    bool? genericEqual = null)
            => assembly.FilterTypesByInterface(type, loadingCondition, [],
                interfaceFilter,
                genericEqual ?? type.ShouldGenericEquals());

        public static IEnumerable<Type> FilterTypesByBaseType(this Assembly assembly,
                                                       Type type,
                                                       AssemblyTypeQuery loadingCondition,
                                                            bool justDirectInheritedTypes = false,
                                                    bool? genericEqual = null)
            => assembly.FilterTypesByBaseClass(type, loadingCondition, [AssemblyTypeFilter.FilterInterfaces],
                justDirectInheritedTypes,
                genericEqual ?? type.ShouldGenericEquals());
        public static IEnumerable<Type> FilterTypesByInterface(this Assembly assembly,
                                                         Type type,
                                                         AssemblyTypeQuery loadingCondition,
                                                         AssemblyTypeFilter[] filters,
                                                    InterfaceFilter interfaceFilter,
                                                    bool? genericEqual = null)
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(filters);

            if (!type.IsInterface)
                throw new ArgumentException($"Type <{type.Name}> is not an interface");

            var filteredTypes = assembly.GetTypes(loadingCondition)
                .Where(o => !o.IsAnonymousType() && !o.IsMicrosoftProduct());
            foreach (var filter in filters)
            {

                switch (filter)
                {
                    case AssemblyTypeFilter.FilterInterfaces:
                        filteredTypes = filteredTypes.Where(o => !o.IsInterface);
                        break;
                    case AssemblyTypeFilter.FilterClasses:
                        filteredTypes = filteredTypes.Where(o => !o.IsClass);
                        break;
                }

            }

            return filteredTypes
            .Where(t => t.HasInterface(type, interfaceFilter, genericEqual));
  
        }


        public static IEnumerable<Type> FilterTypesByBaseClass(this Assembly assembly,
                                                       Type baseClass,
                                                       AssemblyTypeQuery loadingCondition,
                                                       AssemblyTypeFilter[] filters,
                                                            bool justDirectInheritedType = false,
                                                    bool genericEqual = false)
        {
            ArgumentNullException.ThrowIfNull(baseClass);

            ArgumentNullException.ThrowIfNull(filters);

            if (!baseClass.IsClass)
                throw new ArgumentException($"Type <{baseClass.Name}> is not a class");


            if (baseClass.IsSealed)
                throw new ArgumentException($"Can not filter types by a sealed class<{baseClass.Name}>.");

            //#if NETSTANDARD2_1
            //            .Where(o => o.AssemblyQualifiedName == assembly.GetName().Name && o.IsTypeDefinition)
            //#endif
            //#if NETSTANDARD2_0
            //            .Where(o => o.AssemblyQualifiedName == assembly.GetName().Name && !o.IsInterface && !o.IsEnum && !o.IsGenericParameter)
            //#endif
            //#if NETSTANDARD2_1
            //#endif
            //#if NETSTANDARD2_0
            //                            ?.Where(o =>  !o.IsInterface && !o.IsEnum && !o.IsGenericParameter)
            //#endif
            var filteredTypes = assembly.GetTypes(loadingCondition)
                .Where(o => !o.IsAnonymousType() && !o.IsMicrosoftProduct());
            for (int i = 0; i < filters.Length; i++)
            {


                if (filters[i] == AssemblyTypeFilter.FilterInterfaces)
                    filteredTypes = filteredTypes.Where(o => !o.IsInterface)
                        .Where(x => !x.IsEnum && !x.IsPointer && !x.IsPrimitive && !x.IsValueType && !x.IsGenericTypeParameter);

                else if (filters[i] == AssemblyTypeFilter.FilterClasses)
                    filteredTypes = filteredTypes.Where(o => !o.IsClass)
                        .Where(x => !x.IsEnum && !x.IsPointer && !x.IsPrimitive && !x.IsValueType && !x.IsGenericTypeParameter);

            }


            //return filteredTypes?
            //.Where(o => !o.IsAnonymousType() && (!o.Namespace?.StartsWith(microsoft) ?? true)
            //    && (!o.Namespace?.StartsWith(system) ?? true))
            //?.Where(t => (
            //t.BaseType is { }
            //&&
            //justDirectInheritedType ?
            //new[] { t.BaseType }
            //:
            //t.GetBaseClasses())
            //.Any(i =>
            //type.IsGenericType
            //    ? i.IsGenericType && i.GetGenericTypeDefinition().GuidEqual(type.GUID)
            //    : !i.IsGenericType && i.GuidEqual(type.GUID))) ?? [];


            //Refactored From Above Not Tested

            return filteredTypes
            .Where(x => x.IsClass)
            .Where(t =>
            {
                if (IsLookupForDirectInherited(justDirectInheritedType, t))
                    return LookupForDirectInherited(baseClass, t, genericEqual);
                else
                    return LookupInBaseClasses(baseClass, t, genericEqual);

            });


        }
        static bool IsLookupForDirectInherited(bool justDirectInheritedType, Type t)
            => t.HasBaseType() && justDirectInheritedType;

        static bool LookupForDirectInherited(Type baseClass, Type type, bool genericEqual)
        {
            // dont remove (type.BaseType != typeof(object)) Cause StackOverFlow
            if (type.BaseType is null)
                return false;
            

            return type.GenericEquals(baseClass, genericEqual);
        }
        private static bool LookupInBaseClasses(Type baseClass, Type type, bool genericEqual)
        {

            return type.GetBaseClasses()
                            .Any(i => i.GenericEquals(baseClass, genericEqual));
        }
    }
}
