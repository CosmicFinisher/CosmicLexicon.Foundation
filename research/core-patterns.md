# Core Framework Patterns Research

## Overview

This document details the core patterns and implementation strategies used across the Core Framework, based on analysis of the actual codebase.

## String Processing Patterns

### String Manipulation

1. StringBuilder Usage
```csharp
public static string Center(string? input, int length, string padding = " ")
{
    StringBuilder sb = new(length);
    sb.Append(padding[0], leftPadding);
    sb.Append(input);
    sb.Append(padding[0], rightPadding);
    return sb.ToString();
}
```

2. String Pooling
```csharp
private static readonly Regex STRIP_HTML_REGEX = 
    new(@"<[^>]*>", RegexOptions.Compiled);

public static string StripHTML(string? html)
{
    if (string.IsNullOrEmpty(html))
        return string.Empty;
    return STRIP_HTML_REGEX.Replace(html, string.Empty);
}
```

### Text Transformation

1. Character Processing
```csharp
public static string RemoveDiacritics(string? input)
{
    string normalizedString = input.Normalize(NormalizationForm.FormD);
    StringBuilder stringBuilder = new StringBuilder(normalizedString.Length);
    
    foreach (char c in normalizedString)
    {
        if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            stringBuilder.Append(c);
    }
    
    return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
}
```

2. String Splitting
```csharp
public static IEnumerable<string> SplitSemicolonSeparatedList(string text)
{
    int start = 0;
    bool inQuote = false;
    for (int i = 0; i < text.Length; i++)
    {
        if (text[i] == '"')
            inQuote = !inQuote;

        if (!inQuote && text[i] == ';')
        {
            yield return text.Substring(start, i - start).Trim();
            start = i + 1;
        }
    }
    yield return text.Substring(start).Trim();
}
```

## Collection Patterns

### Array Management

1. Array Pooling
```csharp
public static IEnumerable<T> Concat<T>(
    this IEnumerable<T> enumerable1, 
    params IEnumerable<T>[] additions)
{
    ArrayPool<IEnumerable<T>> shared = ArrayPool<IEnumerable<T>>.Shared;
    IEnumerable<T>[] array = shared.Rent(additions.Length + 1);
    try
    {
        array[0] = enumerable1;
        Array.Copy(additions, 0, array, 1, additions.Length);
        return array.Where(x => x != null)
                   .SelectMany(x => x)
                   .ToArray();
    }
    finally
    {
        shared.Return(array);
    }
}
```

2. Collection Initialization
```csharp
List<PropertyInfo> props = new(type.GetProperties());
StringBuilder sb = new(input.Length * 2);
```

## Extension Methods

### String Extensions

1. Null-Safe Operations
```csharp
public static string StripQuotes(string text)
{
    if (string.IsNullOrEmpty(text))
        return text;

    if (text.StartsWith('"') && text.EndsWith('"'))
        return text.Substring(1, text.Length - 2);
        
    return text;
}
```

2. Format Extensions
```csharp
public static string Format(string format, 
    object arg0, object arg1, object arg2, 
    IFormatProvider? formatProvider = null)
{
    formatProvider ??= CultureInfo.CurrentCulture;
    return string.Format(formatProvider, format, arg0, arg1, arg2);
}
```

## Reflection Patterns

### Type Management

1. Type Caching
```csharp
public static IReadOnlyList<Type> GetBaseClasses(this Type type)
{
    var list = new List<Type>();
    while (type.BaseType is { } && type.BaseType != typeof(object))
    {
        list.Add(type.BaseType);
        type = type.BaseType;
    }
    return list;
}
```

2. Interface Filtering
```csharp
public static Type[] GetInterfaces(
    this Type type, 
    InterfaceFilter interfaceFilter, 
    Type? targetInterface = null)
{
    var allInterfaces = type.GetInterfaces()
        ?.Where(x => x.IsInterface)
        ?.ToArray() ?? [];
        
    // Filter implementation
}
```

## Error Handling

### Argument Validation

1. Null Checks
```csharp
public static T ThrowIfNull<T>(T argument, string paramName)
{
    if (argument == null)
        throw new ArgumentNullException(paramName);
    return argument;
}
```

2. State Validation
```csharp
public static string MustBeAbsolute(string path)
{
    if (!Path.IsPathRooted(path))
        throw new ArgumentException($"Path '{path}' is not absolute");
    return path;
}
```

## Threading Patterns

### Synchronization

1. Lock Management
```csharp
private readonly object syncRoot = new object();

public void Operation()
{
    lock (syncRoot)
    {
        // Thread-safe operation
    }
}
```

2. Thread Safety
```csharp
public class ThreadSafeComponent
{
    private volatile int state;
    private readonly object syncLock = new object();
    
    public void Update()
    {
        lock (syncLock)
        {
            // Update state
        }
    }
}
```

## Documentation Patterns

### XML Documentation

```csharp
/// <summary>
/// Formats a string with the specified format and objects.
/// </summary>
/// <param name="format">A composite format string.</param>
/// <param name="objects">An array of objects to format.</param>
/// <returns>The formatted string.</returns>
/// <exception cref="ArgumentNullException">format is null.</exception>
public static string FormatString(
    [StringSyntax("CompositeFormat")] string format, 
    params object?[] objects)
{
    ArgumentNullException.ThrowIfNull(format);
    return string.Format(CultureInfo.CurrentCulture, format, objects);
}
```

## Testing Patterns

### Unit Tests

1. Test Organization
```csharp
public class StringHelpersTests
{
    [Fact]
    public void StripHTML_WithValidInput_RemovesHTMLTags()
    {
        // Arrange
        string input = "<p>Test</p>";
        
        // Act
        string result = StringHelpers.StripHTML(input);
        
        // Assert
        Assert.Equal("Test", result);
    }
}
```

2. Edge Cases
```csharp
[Fact]
public void QuoteIfNeeded_NullPath_ReturnsNull()
{
    string? result = PathHelpers.QuoteIfNeeded(null);
    Assert.Null(result);
}
```

## References

### Implementation Files
- src/text/src/xString/StringHelpers.cs
- src/collections/src/ListExtensions.cs
- src/reflection/src/TypeExtensions.cs

### Test Files
- src/text/tests/unit/StringHelpersTests.cs
- src/io/tests/unit/PathHelpersTests.cs
- src/reflection/tests/unit/TypeCacheForTTests.cs