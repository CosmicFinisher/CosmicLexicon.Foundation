# OpenEchoSystem.Core.IO

## Overview

OpenEchoSystem.Core.IO provides enhanced IO operations and utilities for .NET applications. It focuses on performance and reliability while extending standard IO capabilities.

## Features

- Enhanced IO operations
- Stream utilities
- File system helpers
- Path manipulation
- Buffer management
- Async IO support

## Key Components

### IO Operations
- Stream extensions
- File operations
- Directory utilities
- Path helpers

### Buffer Management
- Buffer pooling
- Memory efficiency
- Stream buffering

### Async Support
- Async IO operations
- Cancellation support
- Progress tracking

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2025+ or VS Code

### Installation
Add the package reference to your project:<PackageReference Include="OpenEchoSystem.Core.IO" Version="1.0.0" />
### Basic Usage using OpenEchoSystem.Core.IO;

// Use IO features
using var buffer = BufferPool.Rent(1024);
## Performance Considerations

- Buffer pooling
- Minimal allocations
- Efficient async operations
- Stream optimization

## Contributing

See the main project contribution guidelines.

## License

This project is licensed under the MIT License.