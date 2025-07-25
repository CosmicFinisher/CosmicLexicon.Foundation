# OpenEchoSystem.Core.Text

## Overview

OpenEchoSystem.Core.Text provides high-performance text processing utilities, including string manipulation, JSON handling, and regular expressions support. It focuses on efficient text operations while maintaining security and performance.

## Features

- Efficient string manipulation utilities
- Optimized JSON serialization/deserialization
- Enhanced regular expressions support
- String pooling optimizations
- Text encoding utilities
- String comparison helpers

## Key Components

### String Utilities
- String manipulation helpers
- String pooling
- Efficient concatenation

### JSON Processing
- Secure JSON serialization
- Custom converters
- Performance optimizations

### Regular Expressions
- Enhanced regex patterns
- Regex caching
- Performance utilities

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2025+ or VS Code

### Installation
Add the package reference to your project:<PackageReference Include="OpenEchoSystem.Core.Text" Version="1.0.0" />
### Basic Usage using OpenEchoSystem.Core.Text;
using OpenEchoSystem.Core.Text.Json;

// Use text processing features
var result = text.ToPooledString();
## Performance Considerations

- Uses Span<T>/Memory<T> for efficient operations
- Implements string pooling
- Optimizes memory allocations
- Caches compiled regular expressions

## Security

- Safe serialization practices
- Type whitelisting for deserialization
- Controlled object instantiation

## Contributing

See the main project contribution guidelines.

## License

This project is licensed under the MIT License.