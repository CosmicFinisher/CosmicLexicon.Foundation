# Core Framework for .NET 9

A modern, high-performance .NET framework designed for cloud-native applications and enterprise systems. Built on .NET 9's latest features with a focus on performance, security, and developer productivity.

[![NuGet](https://img.shields.io/nuget/v/ConsmicLexicon.Foundation.xsvg)](https://www.nuget.org/packages/ConsmicLexicon.Foundation/)

## Documentation

- [Architecture Overview](docs/architecture/overview.md) - System design principles and architectural decisions
- [Getting Started Guide](docs/guides/getting-started.md) - Quick start tutorials and examples
- [API Documentation](docs/api/index.md) - Detailed API references
- [Implementation Patterns](research/implementation-patterns.md) - Development guidelines and best practices
- [Performance Best Practices](research/performance-optimizations.md) - Performance optimization techniques
- [Migration Guide](research/migration-and-versioning.md) - Version updates and migration instructions

## Key Features

### Modern Collections

- [Thread-safe Collections](src/collections/README.Foundation.Structures.md) - High-performance concurrent collections
- [Generic Collections](src/collections/generic/README.Foundation.Structures.Generic.md) - Type-safe generic collection implementations
- [LINQ Extensions](src/linq/README.Foundation.Linq.md) - Enhanced LINQ capabilities

### High-Performance Text Processing

- [Text Utilities](src/text/README.Foundation.Formats.md) - Efficient string manipulation and text processing
- [JSON Processing](src/text/json/README.Foundation.Formats.Json.md) - High-performance JSON serialization/deserialization
- [Regular Expressions](src/text/regex/README.Foundation.Formats.RegularExpressions.md) - Optimized regular expression engine

### Advanced Threading

- [Threading Primitives](src/threading/README.Core.Threading.md) - Low-level threading constructs
- [Task Parallel Library](src/threading/tasks/README.Foundation.Concurrency.Tasks.md) - Advanced async/await patterns
- [High-Precision Timing](src/threading/timers/README.Foundation.Concurrency.Timers.md) - Precise timing operations

### Runtime & Security

- [Runtime Services](src/runtime/README.Foundation.Host.md) - Runtime optimization and services
- [Security Features](src/security/README.Foundation.Identity.md) - Security patterns and practices
- [Cryptography](src/security/crypto/README.Foundation.Cryptography.md) - Modern cryptographic operations

## Getting Started

### Prerequisites
dotnet --version # Must be .NET 9.0 or higher
### Installation
# Install core package
dotnet add package ConsmicLexicon.Foundation

# Install specific modules
dotnet add package ConsmicLexicon.Foundation.Structures  # For collections
dotnet add package ConsmicLexicon.Foundation.Concurrency    # For threading
dotnet add package ConsmicLexicon.Foundation.Identity     # For security features
### Quick Example
using ConsmicLexicon.Foundation;
using ConsmicLexicon.Foundation.Structures;
using ConsmicLexicon.Foundation.Concurrency;
using ConsmicLexicon.Foundation.Identity;

// Create a thread-safe collection
var safeCollection = new ConcurrentHashSet<string>();

// Use high-performance text processing
using var jsonDoc = new JsonDocument();

// Utilize high-precision timing
using var timer = new HighPrecisionTimer();

// Implement secure operations
using var crypto = new SecureOperations();
## Development

### Building
dotnet build
dotnet test
For detailed testing information, see our [Testing Strategy](research/testing-strategy.md).

## Contributing

We welcome contributions! See our [Contributing Guide](CONTRIBUTING.md) for:

- Code of Conduct
- Development Workflow
- Pull Request Guidelines
- Testing Requirements

## Project Status

- [Release Notes](CHANGELOG.md)
- [Roadmap](docs/architecture/overview.md#future-directions)
- [Known Issues](https://github.com/ConsmicFinisher/ConsmicLexicon.Foundation/issues)

## Additional Resources

- [Design Decisions](research/design-decisions.md) - Architectural and design choices
- [Core Patterns](research/core-patterns.md) - Common implementation patterns
- [Runtime Patterns](research/runtime-patterns.md) - Runtime optimization patterns
- [Framework Architecture](research/framework-architecture.md) - Detailed architecture documentation

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Related Projects

- [Core Extensions](src/ext/README.Core.Extensions.md) - Additional framework extensions
- [Core Diagnostics](src/diagnostics/README.Core.Diagnostics.md) - Debugging and diagnostics tools
- [Core Globalization](src/globalization/README.Core.Globalization.md) - Internationalization support
