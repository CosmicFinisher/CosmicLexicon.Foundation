namespace OpenEchoSystem.Core.xReflection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// Delegate definition moved into the target namespace
public delegate object ConstructorDelegate(params object[] args);

//
// Summary:
//     Constructor list
public sealed class ConstructorList
{
    //
    // Summary:
    //     The default constructor
    private readonly ConstructorDelegate? DefaultConstructor;

    //
    // Summary:
    //     Gets the constructors.
    //
    // Value:
    //     The constructors.
    private Constructor[] Constructors { get; } // Assumes Constructor class is in this namespace

    //
    // Summary:
    //     Initializes a new instance of the Fast.Activator.Utils.ConstructorList class.
    //
    // Parameters:
    //   type:
    //     The type.
    //
    //   hashCode:
    //     The hash code.
    public ConstructorList(Type type, int hashCode)
    {
        ArgumentNullException.ThrowIfNull(type);

        ConstructorInfo[] array = type.GetConstructors() ?? [];
        List<Constructor> list = [];
        foreach (ConstructorInfo constructorInfo in array)
        {
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            if (!parameters.Any(y => y.ParameterType.IsPointer))
            {
                list.Add(new Constructor(constructorInfo, parameters));
            }
        }

        // Assumes DefaultValues class will be in this namespace
        if (DefaultValues.Values.TryGetValue(type, out var DefaultValue))
        {
            list.Add(new Constructor((_) => DefaultValue));
        }
        else if (type.IsEnum && DefaultValues.Values.TryGetValue(type.GetEnumUnderlyingType(), out DefaultValue))
        {
            list.Add(new Constructor((_) => Enum.Parse(type, DefaultValue?.ToString() ?? string.Empty, ignoreCase: true)));
        }

        Constructors = [.. list.OrderBy(x => x.ParameterLength)];
        if (Constructors.Length != 0 && Constructors[0].ParameterLength == 0)
        {
            DefaultConstructor = Constructors[0].ConstructorDelegate;
        }
    }

    //
    // Summary:
    //     Creates an instance.
    //
    // Parameters:
    //   args:
    //     The arguments.
    //
    // Returns:
    //     The instance created.
    public object? CreateInstance(object[] args)
    {
        for (int i = 0; i < Constructors.Length; i++)
        {
            Constructor constructor = Constructors[i];
            if (constructor.IsMatch(args))
            {
                return constructor.CreateInstance(args);
            }
        }

        return null;
    }

    //
    // Summary:
    //     Creates an instance.
    //
    // Returns:
    //     The instance created.
    public object? CreateInstance()
    {
        if (DefaultConstructor == null)
        {
            return null;
        }

        return DefaultConstructor();
    }
}