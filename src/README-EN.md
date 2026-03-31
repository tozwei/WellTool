# WellTool

## .NET Version of Hutool

WellTool is a feature-rich .NET utility library designed to provide .NET developers with a convenient toolset similar to Hutool in Java. It encapsulates commonly used utility methods, simplifies the development process, and improves development efficiency.

As the .NET version of Hutool, WellTool maintains the design philosophy and core functionality of Hutool, while being optimized and adapted for the .NET platform, providing .NET developers with a utility library that better aligns with language features.

## Project Structure

WellTool adopts a modular design, organizing different functions into independent projects:

- **WellTool.Core**：Core utility classes providing basic functionality
- **WellTool.AI**：AI-related tools
- **WellTool.Aop**：AOP-related functionality
- **WellTool.BloomFilter**：Bloom filter implementation
- **WellTool.Cache**：Cache-related functionality
- **WellTool.Captcha**：Captcha generation
- **WellTool.Cron**：Scheduled tasks
- **WellTool.Crypto**：Encryption and decryption
- **WellTool.DB**：Database operations
- **WellTool.Dfa**：Sensitive word filtering
- **WellTool.Http**：HTTP client
- **WellTool.Json**：JSON processing
- **WellTool.Jwt**：JWT tools
- **WellTool.Log**：Logging functionality
- **WellTool.Poi**：Excel processing
- **WellTool.Script**：Script execution
- **WellTool.Setting**：Configuration management
- **WellTool.Socket**：Socket communication
- **WellTool.System**：System information
- **WellTool.All**：Collection of all modules

## Main Features

- **String Processing**：String operations, encoding conversion, etc.
- **Date and Time**：Date and time formatting, calculation, etc.
- **Collection Tools**：Collection operations, conversion, etc.
- **IO Operations**：File, stream processing, etc.
- **Encryption and Decryption**：Various encryption algorithm implementations
- **HTTP Client**：Convenient HTTP requests
- **JSON Processing**：JSON serialization and deserialization
- **JWT**：JSON Web Token tools
- **Cache**：Multiple cache implementations
- **Captcha**：Image captcha generation
- **Scheduled Tasks**：Cron expression support
- **Sensitive Word Filtering**：DFA algorithm implementation
- **System Information**：Get system hardware and software information
- **AI Integration**：Support for multiple AI models

## Installation

Install using NuGet Package Manager:

```bash
dotnet add package WellTool.All
```

Or install specific modules:

```bash
dotnet add package WellTool.Core
dotnet add package WellTool.Http
dotnet add package WellTool.Json
# Other modules...
```

## Usage Examples

### String Processing

```csharp
using WellTool.Core.Util;

// Check if string is empty
bool isEmpty = StrUtil.IsEmpty("test");

// String formatting
string formatted = StrUtil.Format("Hello, {0}!", "World");
```

### HTTP Request

```csharp
using WellTool.Http;

// Send GET request
var response = HttpUtil.Get("https://api.example.com");

// Send POST request
var postResponse = HttpUtil.Post("https://api.example.com", "{\"name\": \"test\"}");
```

### JSON Processing

```csharp
using WellTool.Json;

// JSON serialization
string json = JSONUtil.ToJson(new { Name = "test", Age = 18 });

// JSON deserialization
var obj = JSONUtil.ToObject<Dictionary<string, object>>(json);
```

## Contribution Guide

1. Fork this repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the Apache License 2.0. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or suggestions, please submit through GitHub Issues.

---

**WellTool - Make .NET development easier!**