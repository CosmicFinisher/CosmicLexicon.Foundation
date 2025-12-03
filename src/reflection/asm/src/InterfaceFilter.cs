namespace CosmicLexicon.Foundation.xReflection.xAssembly
{
    public enum InterfaceFilter
    {
        /// <summary>
        /// Return All Interfaces Returns from type
        /// </summary>
        None,
        /// <summary>
        /// Filter Out  Interfaces defined by Any interfaces of current type and BaseType, then returns interfaces defined by interfaces.
        /// Note : Filtered out Any Interface defined by interfaces even interface defined by both current type and any base type
        /// </summary>
        NotInheritedFromInterfaces,
        /// <summary>
        /// Filter Out Interfaces from Both Inherited Interfaces and BaseType Ancestors
        /// then Return Interfaces Not Intersect with any BaseType and Inherited Interface Interfaces Ancestors.
        /// Note : Filtered out Any Interface defined by both base types and current type
        /// </summary>
        NotExclusiveInterfaces,
        /// <summary>
        /// Filter Out BaseType and Exclusive Interfaces of current type, then returns interfaces defined by interfaces.
        /// Note : Filtered out Any Interface defined by current type and base types
        /// even interface defined by both type and any interfaces of interfaces
        /// </summary>
        InheritedFromInterface,

    }
}