using System.Reflection;

namespace OpenEchoSystem.Core.xReflection;

using System;
using System.Linq;
using System.Linq.Expressions;


//
// Summary:
//     Constructor
public sealed class Constructor
{
    //
    // Summary:
    //     Gets the size of the parameter.
    //
    // Value:
    //     The size of the parameter.
    public int ParameterLength { get; }

    //
    // Summary:
    //     Gets the constructor information.
    //
    // Value:
    //     The constructor information.
    internal ConstructorDelegate ConstructorDelegate { get; } // This type needs to be in this namespace

    //
    // Summary:
    //     Gets the parameter nullable.
    //
    // Value:
    //     The parameter nullable.
    private bool[] ParameterNullable { get; }

    //
    // Summary:
    //     Gets the parameters.
    //
    // Value:
    //     The parameters.
    private Type[] Parameters { get; }

    //
    // Summary:
    //     Initializes a new instance of the Fast.Activator.Utils.Constructor class.
    //
    // Parameters:
    //   constructor:
    //     The constructor.
    //
    //   parameters:
    //     The parameters.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     constructor
    public Constructor(ConstructorInfo constructor, ParameterInfo[] parameters)
    {
        if (constructor is null)
        {
            throw new ArgumentNullException(nameof(constructor));
        }

        ParameterInfo[] array = parameters ?? [];
        Parameters = array.Select(x => x.ParameterType).ToArray();
        ParameterLength = Parameters.Length;
        ParameterNullable = Parameters.Select(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(Nullable<>)).ToArray();
        ConstructorDelegate = CreateConstructor(constructor, array);
    }

    //
    // Summary:
    //     Initializes a new instance of the Fast.Activator.Utils.Constructor class.
    //
    // Parameters:
    //   delegate:
    //     The delegate.
    public Constructor(ConstructorDelegate @delegate)
    {
        ConstructorDelegate = @delegate;
        ParameterLength = 0;
        Parameters = [];
        ParameterNullable = [];
    }

    //
    // Summary:
    //     Create an instance.
    //
    // Parameters:
    //   args:
    //     The arguments.
    //
    // Returns:
    //     The new object
    public object? CreateInstance(object[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        if (!IsMatch(args))
        {
            return null; // Return null if arguments don't match
        }
        return ConstructorDelegate(args);
    }

    //
    // Summary:
    //     Determines whether the specified arguments is a match.
    //
    // Parameters:
    //   args:
    //     The arguments.
    //
    // Returns:
    //     true if the specified arguments is a match; otherwise, false.
    public bool IsMatch(object[]? args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        if (args.Length != ParameterLength)
        {
            return false;
        }

        for (int i = 0; i < ParameterLength; i++)
        {
            object? obj = args[i];
            Type type = Parameters[i];
            if (obj == null)
            {
                if (!ParameterNullable[i])
                {
                    return false;
                }

                continue;
            }

            Type type2 = obj.GetType();
            if (!type.IsAssignableFrom(type2))
            {
                return false;
            }
        }

        return true;
    }

    //
    // Summary:
    //     Creates the argument expression.
    //
    // Parameters:
    //   parameterExpression:
    //     The parameter expression.
    //
    //   parameterInfo:
    //     The parameter information.
    //
    //   index:
    //     The index.
    //
    // Returns:
    //     The argument expression
    private static Expression CreateArgumentExpression(ParameterExpression parameterExpression, ParameterInfo parameterInfo, int index)
    {
        return Expression.Convert(Expression.ArrayIndex(parameterExpression, Expression.Constant(index)), parameterInfo.ParameterType);
    }

    //
    // Summary:
    //     Creates the constructor.
    //
    // Parameters:
    //   constructor:
    //     The constructor.
    //
    //   parameters:
    //     The parameters.
    //
    // Returns:
    //     The constructor
    private static ConstructorDelegate CreateConstructor(ConstructorInfo constructor, ParameterInfo[] parameters)
    {
        ParameterExpression ParameterExpression = Expression.Parameter(typeof(object[]), "args");
        Expression[] arguments = [.. parameters.Select((info, index) => CreateArgumentExpression(ParameterExpression, info, index))];
        Expression expression = Expression.New(constructor, arguments);
        if (constructor.DeclaringType is not null && constructor.DeclaringType.IsValueType)
        {
            expression = Expression.Convert(expression, typeof(object));
        }

        return Expression.Lambda(typeof(ConstructorDelegate), expression, ParameterExpression).Compile() as ConstructorDelegate;
    }
}