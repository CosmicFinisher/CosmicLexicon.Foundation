namespace CosmicLexicon.Foundation.Introspection;

using System;
using System.Collections.Generic;

//
// Summary:
//     Fast activator static class
public static class FastActivator
{
    //
    // Summary:
    //     The constructors
    private static readonly Dictionary<int, ConstructorList> Constructors = [];

    //
    // Summary:
    //     The lock object
    private static readonly object LockObject = new();

    //
    // Summary:
    //     Creates an instance of the class.
    //
    // Parameters:
    //   args:
    //     The arguments.
    //
    // Type parameters:
    //   TClass:
    //     The type of the class.
    //
    // Returns:
    //     The instance created.
    public static TClass? CreateInstance<TClass>(params object[] args)
    {
        var obj = CreateInstance(typeof(TClass), args);
        if (obj is not null)
        {
            return (TClass)obj;
        }

        return default!;
    }

    //
    // Summary:
    //     Creates an instance of the class.
    //
    // Type parameters:
    //   TClass:
    //     The type of the class.
    //
    // Returns:
    //     The instance created.
    public static TClass? CreateInstance<TClass>()
    {
        var obj = CreateInstance(typeof(TClass));
        if (obj is not null)
        {
            return (TClass)obj;
        }

        return default!;
    }

    //
    // Summary:
    //     Creates an instance.
    //
    // Parameters:
    //   type:
    //     The type.
    //
    //   args:
    //     The arguments.
    //
    // Returns:
    //     The object if it can be created, null otherwise.
    public static object? CreateInstance(Type type, params object[] args)
    {
        return GetConstructorList(type)?.CreateInstance(args);
    }

    //
    // Summary:
    //     Creates an instance.
    //
    // Parameters:
    //   type:
    //     The type.
    //
    // Returns:
    //     The object if it can be created, null otherwise.
    public static object? CreateInstance(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        try
        {
            return Activator.CreateInstance(type);
        }
        catch (Exception)
        {
            return null;
        }
    }

    //
    // Summary:
    //     Creates the constructors.
    //
    // Parameters:
    //   type:
    //     The type.
    //
    //   HashCode:
    //     The hash code.
    //
    // Returns:
    //     The constructors
    private static ConstructorList CreateConstructors(Type type, int HashCode)
    {
        lock (LockObject)
        {
            if (Constructors.TryGetValue(HashCode, out ConstructorList? value))
            {
                return value;
            }

            value = new ConstructorList(type, HashCode);
            Constructors.Add(HashCode, value);
            return value;
        }
    }

    //
    // Summary:
    //     Gets the constructor list.
    //
    // Parameters:
    //   type:
    //     The type.
    //
    // Returns:
    //     The constructor list.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     type
    private static ConstructorList? GetConstructorList(Type type)
    {
        int hashCode;
        try
        {
            hashCode = type.GetHashCode();
        }
        catch
        {
            return null;
        }

        if (Constructors.TryGetValue(hashCode, out var value))
        {
            return value;
        }

        return CreateConstructors(type, hashCode);
    }
}