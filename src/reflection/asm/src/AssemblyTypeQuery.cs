using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CosmicLexicon.Foundation.xReflection.xAssembly
{
    public enum AssemblyTypeCollectionQuery
    {
        All = 0,
        Exported = 1,
        NonPublic = 2,
        Public = 3,
        Nested = 4,
    }
    public enum AssemblyTypeQuery
    {
        All = 0,
        Exported = 1,
        Forvarded = 2,
    }
}
