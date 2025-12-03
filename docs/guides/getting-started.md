# Getting Started with Core Framework

## Quick Start Guide

### Prerequisites
- .NET 9.0 SDK or higher
- Visual Studio 2025+ or VS Code with C# extensions
- Basic knowledge of C# and .NET

### Installation

1. Add NuGet packages:dotnet add package ConsmicLexicon.Foundation              # Core utilities
dotnet add package ConsmicLexicon.Foundation.Structures  # Collections
dotnet add package ConsmicLexicon.Foundation.Concurrency    # Threading utilities
2. Import namespaces:using ConsmicLexicon.Foundation;
using ConsmicLexicon.Foundation.Structures;
using ConsmicLexicon.Foundation.Concurrency;
## Core Components

### Collections// Thread-safe collections
var concurrentSet = new ConcurrentHashSet<string>();
var orderedMap = new OrderedConcurrentDictionary<int, string>();

// High-performance collections
using var memoryOptimizedList = new PooledList<int>();
using var customComparer = new ValueTypeComparer<MyStruct>();[Learn more about Collections](../../src/collections/README.Foundation.Structures.md)

### Text Processing// JSON handling
using var json = new JsonDocument();
var formatter = new SmartFormatter();

// Regular expressions
using var regex = new OptimizedRegex(@"\w+");[Learn more about Text Processing](../../src/text/README.Foundation.Formats.md)

### Threading// High-precision timing
using var timer = new HighPrecisionTimer();

// Advanced task patterns
using var scheduler = new TaskScheduler();[Learn more about Threading](../../src/threading/README.Core.Threading.md)

## Best Practices

### Performance Optimization// Use value types for small objects
readonly struct Point
{
    public float X { get; init; }
    public float Y { get; init; }
}

// Use spans for array operations
Span<byte> buffer = stackalloc byte[1024];[View Performance Guidelines](../../research/performance-optimizations.md)

### Memory Management// Use pooled objects
using var pooledObject = Pool<MyClass>.Rent();

// Dispose properly
using var disposable = new DisposableResource();[View Memory Management Patterns](../../research/core-patterns.md)

### Error Handling// Use Result pattern
public Result<int> Calculate()
{
    try
    {
        return Result.Success(42);
    }
    catch (Exception ex)
    {
        return Result.Error<int>(ex);
    }
}[View Error Handling Patterns](../../research/implementation-patterns.md)

## Advanced Topics

### Cloud Integration
- [Cloud-Native Patterns](../../research/design-decisions.md#cloud-native)
- [Distributed Systems](../../research/framework-architecture.md#distributed)
- [Microservices Integration](../../docs/architecture/overview.md#microservices)

### Security
- [Cryptography Usage](../../src/security/crypto/README.Foundation.Cryptography.md)
- [Security Best Practices](../../src/security/README.Foundation.Identity.md)
- [Authentication Patterns](../../research/implementation-patterns.md#security)

### Testing
- [Unit Testing Guide](../../research/testing-strategy.md)
- [Integration Testing](../../docs/test_plan/master_acceptance_test_plan.md)
- [Performance Testing](../../research/performance-optimizations.md#benchmarking)

## Next Steps

1. Review the [Architecture Overview](../architecture/overview.md)
2. Explore [Implementation Patterns](../../research/implementation-patterns.md)
3. Check [API Documentation](../api/index.md)
4. Join our [Community](../../CONTRIBUTING.md)

## Additional Resources

- [Detailed Documentation](../../README.md)
- [Design Decisions](../../research/design-decisions.md)
- [Version History](../../research/migration-and-versioning.md)
- [Known Issues](https://github.com/ConsmicFinisher/ConsmicLexicon.Foundation/issues)

## Need Help?

- Review our [FAQ](../faq.md)
- Check [Common Issues](../troubleshooting.md)
<!-- - Join our [Discord Community](https://discord.gg/openEmergent) -->
- Submit an [Issue](https://github.com/ConsmicFinisher/ConsmicLexicon.Foundation/issues)
