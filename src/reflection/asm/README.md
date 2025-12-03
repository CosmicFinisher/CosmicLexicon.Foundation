# ConsmicLexicon.Foundation.Reflection.Assembly Namespace Overview

## Purpose

The `ConsmicLexicon.Foundation.Reflection.Assembly` namespace provides extensions and utilities for working with .NET assemblies, types, and properties. It simplifies common reflection tasks and offers convenient ways to query and filter types within an assembly.

## Key Classes

*   **AssemblyExtensions**: Provides extension methods for the `Assembly` class, enabling easier access to types and properties.
*   **AssemblyTypeFilter**: Defines filters for selecting specific types within an assembly based on criteria like name, attributes, and interfaces.
*   **AssemblyTypeQuery**: Encapsulates a query for types within an assembly, allowing for filtering and retrieval of types that match specific criteria.
*   **GenericTypeExtensions**: Provides extension methods for working with generic types.
*   **InterfaceFilter**: Defines a filter for selecting types that implement specific interfaces.
*   **PropertyInfoExtensions**: Provides extension methods for working with `PropertyInfo` objects.
*   **TypeExtensions**: Provides extension methods for working with `Type` objects.

## Usage Examples

### Getting all types in an assembly:

```csharp
Assembly assembly = typeof(AssemblyExtensions).Assembly;
IEnumerable<Type> allTypes = assembly.GetTypes();
```

### Filtering types by name:

```csharp
Assembly assembly = typeof(AssemblyExtensions).Assembly;
IEnumerable<Type> filteredTypes = assembly.GetTypes(new AssemblyTypeFilter { Name = "MyType" });
```

### Filtering types by interface:

```csharp
Assembly assembly = typeof(AssemblyExtensions).Assembly;
IEnumerable<Type> filteredTypes = assembly.GetTypes(new InterfaceFilter { InterfaceType = typeof(IDisposable) });
```

### Checking if a property has a specific attribute:

```csharp
PropertyInfo property = typeof(MyClass).GetProperty("MyProperty");
bool hasAttribute = property.HasAttribute<MyAttribute>();
```

## Scope

This document covers the core functionalities of the `ConsmicLexicon.Foundation.Reflection.Assembly` namespace, focusing on assembly-level reflection tasks. It does not include detailed documentation of individual methods or properties.

## Dependencies

*   .NET Standard 2.0 or later