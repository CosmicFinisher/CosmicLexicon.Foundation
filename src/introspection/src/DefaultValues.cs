namespace CosmicLexicon.Foundation.Introspection;

using System;
using System.Collections.Generic;

//
// Summary:
//     Default values
internal static class DefaultValues
{
    //
    // Summary:
    //     The values
    public static Dictionary<Type, object?> Values;

    static DefaultValues()
    {
        Values = new()
        {
            [typeof(byte)] = (byte)0,
            [typeof(sbyte)] = (sbyte)0,
            [typeof(short)] = (short)0,
            [typeof(int)] = 0,
            [typeof(long)] = 0L,
            [typeof(ushort)] = (ushort)0,
            [typeof(uint)] = 0u,
            [typeof(ulong)] = 0uL,
            [typeof(double)] = 0.0,
            [typeof(float)] = 0f,
            [typeof(decimal)] = 0m,
            [typeof(bool)] = false,
            [typeof(char)] = '\0',
            [typeof(string)] = null,
            [typeof(Guid)] = default(Guid),
            [typeof(DateTime)] = default(DateTime),
            [typeof(DateTimeOffset)] = default(DateTimeOffset),
            [typeof(TimeSpan)] = default(TimeSpan),
            [typeof(byte?)] = null,
            [typeof(sbyte?)] = null,
            [typeof(short?)] = null,
            [typeof(int?)] = null,
            [typeof(long?)] = null,
            [typeof(ushort?)] = null,
            [typeof(uint?)] = null,
            [typeof(ulong?)] = null,
            [typeof(double?)] = null,
            [typeof(float?)] = null,
            [typeof(decimal?)] = null,
            [typeof(bool?)] = null,
            [typeof(char?)] = null,
            [typeof(Guid?)] = null,
            [typeof(DateTime?)] = null,
            [typeof(DateTimeOffset?)] = null,
            [typeof(TimeSpan?)] = null,
            [typeof(byte[])] = null,
            [typeof(CustomStruct)] = default(CustomStruct),
            [typeof(CustomClass)] = null
        };
    }

}