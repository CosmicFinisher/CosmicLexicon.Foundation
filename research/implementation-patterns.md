# Implementation Patterns for Core Framework

## Overview

This document details the implementation patterns used throughout the Core Framework, leveraging .NET 9's modern features and best practices. For architecture details, see [Architecture Overview](../docs/architecture/overview.md).

## Project Organization

### Directory Structuresrc/
src/
└── collections/
    ├── Foundation.Structures/
    │   ├── src/
    │   │   └── Foundation.Structures.csproj
    │   └── tests/
    │       └── unit/
    │           └── Foundation.Structures.Tests.Unit.csproj
    └── Foundation.Structures.Generic/
        ├── src/
        │   └── Foundation.Structures.Generic.csproj
        └── tests/
            └── unit/
                └── Foundation.Structures.Generic.Tests.Unit.csproj
├── threading/
    ├── Core.Threading/
    │   ├── src/
    │   │   └── Core.Threading.csproj
    │   └── tests/
    │       └── unit/
    │           └── Core.Threading.Tests.Unit.csproj
    ├── Foundation.Concurrency.Tasks/
    │   ├── src/
    │   │   └── Foundation.Concurrency.Tasks.csproj
    │   └── tests/
    │       └── unit/
    │           └── Foundation.Concurrency.Tasks.Tests.Unit.csproj
    └── Foundation.Concurrency.Timers/
        ├── src/
        │   └── Foundation.Concurrency.Timers.csproj
        └── tests/
            └── unit/
                └── Foundation.Concurrency.Timers.Tests.Unit.csproj/
└── └── text/
    ├── Foundation.Formats/
    │   ├── src/
    │   │   └── Foundation.Formats.csproj
    │   └── tests/
    │       └── unit/
    │           └── Foundation.Formats.Tests.Unit.csproj
    ├── Foundation.Formats.Json/
    │   ├── src/
    │   │   └── Foundation.Formats.Json.csproj
    │   └── tests/
    │       └── unit/
    │           └── Foundation.Formats.Json.Tests.Unit.csproj
    └── Foundation.Formats.Regex/
        ├── src/
        │   └── Foundation.Formats.Regex.csproj
        └── tests/
            └── unit/
                └── Foundation.Formats.Regex.Tests.Unit.csproj/
### Modern Project Configuration

1. Main Projects (.NET 9)<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>$(NetPrimaryTargetFramework)</TargetFramework>
    <RootNamespace>ConsmicLexicon.Foundation.{Component}</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <EnablePreviewFeatures>true</EnablePreviewFeatures>
    <IsAotCompatible>true</IsAotCompatible>
  </PropertyGroup>
</Project>
2. Test Projects<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>$(NetPrimaryTargetFramework)</TargetFramework>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
    <EnablePreviewFeatures>true</EnablePreviewFeatures>
  </PropertyGroup>
</Project>
## Modern Code Patterns

### Extended Type System Features
// Required members
public class Configuration
{
    public required string Name { get; init; }
    public required ICollection<string> Values { get; init; }
}

// Collection expressions
public static class CollectionHelpers
{
    public static IReadOnlyList<T> CreateReadOnly<T>(T item) => [item];
    public static IReadOnlyCollection<T> IsNullOrEmpty<T>(
        this IReadOnlyCollection<T>? source) => source ?? [];
}

// Primary constructors
public class Service(ILogger logger, IConfiguration config)
{
    public void Execute() => logger.Log(config.ToString());
}
### Modern Performance Patterns

1. Span Operationspublic static class SpanHelpers
{
    public static bool TryParseSpan<T>(ReadOnlySpan<char> input, 
        [NotNullWhen(true)] out T? result) where T : struct
    {
        // Efficient parsing using spans
    }
    
    public static void ProcessBuffer(Span<byte> buffer)
    {
        // Zero-allocation buffer processing
    }
}
2. Memory Managementpublic sealed class BufferManager : IDisposable
{
    private readonly MemoryPool<byte> _pool = MemoryPool<byte>.Shared;
    
    public IMemoryOwner<byte> RentMemory(int size) => _pool.Rent(size);
    
    public void Dispose() => _pool.Dispose();
}
### Thread Safety Patterns

1. Modern Synchronizationpublic class ThreadSafeCache<TKey, TValue> where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> _cache = new();
    
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory) =>
        _cache.GetOrAdd(key, valueFactory);
}
2. Async Patternspublic class AsyncOperations
{
    public async ValueTask<T> ExecuteAsync<T>(
        Func<CancellationToken, ValueTask<T>> operation,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await operation(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
    }
}
### Modern Error Handling

1. Exception Handlingpublic static class Guard
{
    public static T ThrowIfNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
        return value;
    }
    
    public static void ThrowIfEmpty<T>(IEnumerable<T> collection,
        [CallerArgumentExpression(nameof(collection))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(collection, paramName);
        if (!collection.Any())
            throw new ArgumentException("Collection must not be empty", paramName);
    }
}
2. Result Patternpublic readonly record struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Exception? Error { get; }
    
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = null;
    }
    
    private Result(Exception error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Exception error) => new(error);
}
## Testing Patterns

### Unit Testing
public class ModernTests
{
    [Theory]
    [InlineData("input", true)]
    [InlineData("", false)]
    public void Validate_InputString_ReturnsExpectedResult(
        string input, bool expected)
    {
        // Arrange
        var validator = new Validator();
        
        // Act
        var result = validator.IsValid(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public async Task AsyncOperation_CompletesSuccessfully()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        var operation = new AsyncOperation();
        
        // Act
        var result = await operation.ExecuteAsync(cts.Token);
        
        // Assert
        Assert.NotNull(result);
    }
}
## Documentation Patterns

### XML Documentation with Modern Features
/// <summary>
/// Performs an async operation with the specified parameters.
/// </summary>
/// <typeparam name="T">The type of the result.</typeparam>
/// <param name="input">The input to process.</param>
/// <param name="cancellationToken">
/// A token to monitor for cancellation requests.
/// </param>
/// <returns>
/// A <see cref="ValueTask{T}"/> representing the asynchronous operation.
/// </returns>
/// <exception cref="ArgumentNullException">
/// Thrown when <paramref name="input"/> is <see langword="null"/>.
/// </exception>
/// <example>
/// <code>
/// var result = await component.ProcessAsync(input, CancellationToken.None);
/// </code>
/// </example>
public async ValueTask<T> ProcessAsync<T>(T input, 
    CancellationToken cancellationToken = default)
{
    // Implementation
}
## References

### Implementation Files
- [Collections Implementation](../src/collections/src/README.Foundation.Structures.md)
- [Threading Implementation](../src/threading/src/README.Core.Threading.md)
- [Text Processing](../src/text/src/README.Foundation.Formats.md)

### Related Documentation
- [Architecture Overview](../docs/architecture/overview.md)
- [Performance Optimizations](performance-optimizations.md)
- [Testing Strategy](testing-strategy.md)
- [Design Decisions](design-decisions.md)

### Package References
- [Core Framework NuGet](https://www.nuget.org/packages/ConsmicLexicon.Foundation/)
- [Collections Package](https://www.nuget.org/packages/ConsmicLexicon.Foundation.Structures/)
- [Threading Package](https://www.nuget.org/packages/ConsmicLexicon.Foundation.Concurrency/)