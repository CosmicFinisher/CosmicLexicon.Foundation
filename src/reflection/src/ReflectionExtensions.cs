using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using CosmicLexicon.Foundation.xGenerics;

namespace CosmicLexicon.Foundation.xReflection
{
    //
    // Summary:
    //     Reflection oriented extensions
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ReflectionHelpers
    {
        //
        // Summary:
        //     Gets the object cartographer to.
        //
        // Value:
        //     The object cartographer to.
        private static MethodInfo ObjectCartographerTo { get; } = typeof(GenericObjectExtensions).GetMethod("To", BindingFlags.Public | BindingFlags.Static) ?? throw new InvalidOperationException("GenericObjectExtensions.To method not found");

        //
        // Summary:
        //     Gets the attribute from the item
        //
        // Parameters:
        //   provider:
        //     Attribute provider
        //
        //   inherit:
        //     When true, it looks up the heirarchy chain for the inherited custom attributes
        //
        // Type parameters:
        //   T:
        //     Attribute type
        //
        // Returns:
        //     Attribute specified if it exists
        [return: MaybeNull]
        public static T Attribute<T>(this MemberInfo provider, bool inherit = true) where T : Attribute
        {
            T[] array = provider.Attributes<T>(inherit);
            if (array.Length == 0)
            {
                return null;
            }

            return array[0];
        }
        //
        // Summary:
        //     Gets the attributes from the item
        //
        // Parameters:
        //   provider:
        //     Attribute provider
        //
        //   inherit:
        //     When true, it looks up the heirarchy chain for the inherited custom attributes
        //
        // Type parameters:
        //   T:
        //     Attribute type
        //
        // Returns:
        //     Array of attributes
        public static T[] Attributes<T>(this MemberInfo provider, bool inherit = true) where T : Attribute
        {
            return provider?.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray() ?? Array.Empty<T>();
        }

        //
        // Summary:
        //     Calls a method on an object
        //
        // Parameters:
        //   inputObject:
        //     Object to call the method on
        //
        //   methodName:
        //     Method name
        //
        //   inputVariables:
        //     (Optional)input variables for the method
        //
        // Type parameters:
        //   TReturnType:
        //     Return type expected
        //
        // Returns:
        //     The returned value of the method
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     inputObject or methodName
        //
        //   T:System.InvalidOperationException:
        //     Could not find method " + methodName + " with the appropriate input variables.
        public static TReturnType Call<TReturnType>(this object inputObject, string methodName, params object[] inputVariables)
        {
            if (inputObject == null)
            {
                throw new ArgumentNullException(nameof(inputObject));
            }

            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (inputVariables == null)
            {
                inputVariables = [];
            }

            Type type = inputObject.GetType();
            MethodInfo methodInfo = FindMethod(methodName, inputVariables, type);
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Could not find method " + methodName + " with the appropriate input variables.");
            }

            return (TReturnType)methodInfo.Invoke(inputObject, inputVariables);
        }

        //
        // Summary:
        //     Calls a method on an object
        //
        // Parameters:
        //   inputObject:
        //     Object to call the method on
        //
        //   methodName:
        //     Method name
        //
        //   inputVariables:
        //     (Optional)input variables for the method
        //
        // Type parameters:
        //   TGenericType1:
        //     Generic method type 1
        //
        //   TReturnType:
        //     Return type expected
        //
        // Returns:
        //     The returned value of the method
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     inputObject or methodName
        //
        //   T:System.InvalidOperationException:
        //     Could not find method " + methodName + " with the appropriate input variables.
        public static TReturnType Call<TGenericType1, TReturnType>(this object inputObject, string methodName, params object[] inputVariables)
        {
            if (inputObject == null)
            {
                throw new ArgumentNullException(nameof(inputObject));
            }

            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (inputVariables == null)
            {
                inputVariables = [];
            }

            Type type = inputObject.GetType();
            MethodInfo methodInfo = FindMethod(methodName, inputVariables, type);
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Could not find method " + methodName + " with the appropriate input variables.");
            }

            methodInfo = methodInfo.MakeGenericMethod(typeof(TGenericType1));
            return inputObject.Call<TReturnType>(methodInfo, inputVariables);
        }

        //
        // Summary:
        //     Calls a method on an object
        //
        // Parameters:
        //   inputObject:
        //     Object to call the method on
        //
        //   methodName:
        //     Method name
        //
        //   inputVariables:
        //     (Optional)input variables for the method
        //
        // Type parameters:
        //   TReturnType:
        //     Return type expected
        //
        // Returns:
        //     The returned value of the method
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     inputObject or methodName
        //
        //   T:System.InvalidOperationException:
        //     Could not find method " + methodName + " with the appropriate input variables.
        public static TReturnType Call<TReturnType>(this object inputObject, MethodInfo methodInfo, params object[] inputVariables)
        {
            if (inputObject == null)
            {
                throw new ArgumentNullException(nameof(inputObject));
            }

            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            if (inputVariables == null)
            {
                inputVariables = [];
            }

            return (TReturnType)methodInfo.Invoke(inputObject, inputVariables);
        }

        //
        // Summary:
        //     Finds a method with the specified name and parameters
        //
        // Parameters:
        //   methodName:
        //     Method name
        //
        //   inputVariables:
        //     Input variables for the method
        //
        //   type:
        //     Type to search for the method
        //
        // Returns:
        //     MethodInfo object if found, null otherwise
        private static MethodInfo FindMethod(string methodName, object[] inputVariables, Type type)
        {
            MethodInfo methodInfo = null;
            foreach (MethodInfo mi in type.GetMethods())
            {
                if (mi.Name == methodName)
                {
                    ParameterInfo[] parameters = mi.GetParameters();
                    if (parameters.Length == inputVariables.Length)
                    {
                        bool match = true;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (!parameters[i].ParameterType.IsAssignableFrom(inputVariables[i].GetType()))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            methodInfo = mi;
                            break;
                        }
                    }
                }
            }
            return methodInfo;
        }

        //
        // Summary:
        //     Checks if JIT tracking is enabled
        //
        // Parameters:
        //   attribute:
        //     Debuggable attribute
        //
        // Returns:
        //     True if JIT tracking is enabled, false otherwise
        private static bool IsJitTrackingEnabled(this DebuggableAttribute attribute)
        {
            return (attribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.EnableEditAndContinue) != 0;
        }
    }
}