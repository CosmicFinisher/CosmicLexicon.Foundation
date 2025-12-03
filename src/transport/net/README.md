<!-- Logo -->
<p align="center">
  <img src="https://raw.githubusercontent.com/ConsmicFinisher/ConsmicLexicon.Foundation/main/docs/assets/logo-net.png" alt="ConsmicLexicon.Foundation.Net Logo" width="120"/>
</p>

<p align="center">
  <a href="https://github.com/ConsmicFinisher/ConsmicLexicon.Foundation/actions/workflows/build.yml"><img src="https://img.shields.io/github/actions/workflow/status/ConsmicFinisher/ConsmicLexicon.Foundation/build.yml?branch=main&label=build" alt="Build Status"></a>
  <a href="https://www.nuget.org/packages/ConsmicLexicon.Foundation.Net"><img src="https://img.shields.io/nuget/v/ConsmicLexicon.Foundation.Net.svg?label=nuget" alt="NuGet"></a>
  <a href="https://opensource.org/licenses/MIT"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="MIT License"></a>
  <a href="https://github.com/ConsmicFinisher/ConsmicLexicon.Foundation"><img src="https://img.shields.io/github/stars/ConsmicFinisher/ConsmicLexicon.Foundation?style=social" alt="GitHub stars"></a>
</p>

# ConsmicLexicon.Foundation.Net

ConsmicLexicon.Foundation.Net is a modern, high-performance .NET 9 networking library designed for reliability, efficiency, and ease of use. It provides essential building blocks for network communication, protocol handling, and async operations, making it easy to build robust distributed systems and networked applications.

---

## ✨ Features

- **Efficient Network Operations:** Fast, async-friendly socket and connection utilities.
- **Protocol Helpers:** Simplifies implementation of custom and standard protocols.
- **Connection Management:** Tools for managing, pooling, and monitoring network connections.
- **Async Support:** Designed for modern async/await workflows.
- **Extensible & Modular:** Integrates seamlessly with other ConsmicLexicon.Foundation libraries.

---

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK

### Installation
Install via NuGet:> dotnet add package ConsmicLexicon.Foundation.Net
### Basic Usage Exampleusing ConsmicLexicon.Foundation.Net;

// Create and manage a network connection
using var connection = new NetworkConnection("example.com", 443);
await connection.OpenAsync();
await connection.SendAsync(data);
var response = await connection.ReceiveAsync();
---

## 📚 Documentation & Navigation
- [API Reference](../docs/api/net.md)
- [Project Overview](../../integrated_documentation_overview.md)
- [Other Core Libraries](../)
  - [Collections](../../collections/README.CORE.COLLECTIONS.md)
  - [Text](../../text/README.CORE.TEXT.md)
  - [Threading](../../threading/README.CORE.THREADING.md)
  - [Security](../../security/README.CORE.SECURITY.md)

---

## 🤝 Contributing
We welcome contributions! Please see the [contributing guide](../../CONTRIBUTING.md) for details on our process, code style, and how to get started.

---

## 🛡️ License
This project is licensed under the MIT License.

---

## 🧭 About ConsmicLexicon.Foundation
ConsmicLexicon.Foundation is a modular, high-performance foundation for .NET 9+ applications, providing essential utilities for collections, networking, threading, security, and more. Explore the [full suite of libraries](../../integrated_documentation_overview.md) to supercharge your .NET projects.