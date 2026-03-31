# WellTool

## .Net版的Hutool

WellTool 是一个功能丰富的 .NET 工具库，旨在为 .NET 开发者提供类似 Java 中 Hutool 的便捷工具集。它封装了常用的工具方法，简化了开发过程，提高了开发效率。

作为 .NET 版本的 Hutool，WellTool 保持了 Hutool 的设计理念和核心功能，同时针对 .NET 平台进行了优化和适配，为 .NET 开发者提供了更加符合语言特性的工具库。

## 项目结构

WellTool 采用模块化设计，将不同功能组织到独立的项目中：

- **WellTool.Core**：核心工具类，提供基础功能
- **WellTool.AI**：AI 相关工具
- **WellTool.Aop**：AOP 相关功能
- **WellTool.BloomFilter**：布隆过滤器实现
- **WellTool.Cache**：缓存相关功能
- **WellTool.Captcha**：验证码生成
- **WellTool.Cron**：定时任务
- **WellTool.Crypto**：加密解密
- **WellTool.DB**：数据库操作
- **WellTool.Dfa**：敏感词过滤
- **WellTool.Http**：HTTP 客户端
- **WellTool.Json**：JSON 处理
- **WellTool.Jwt**：JWT 工具
- **WellTool.Log**：日志功能
- **WellTool.Poi**：Excel 处理
- **WellTool.Script**：脚本执行
- **WellTool.Setting**：配置管理
- **WellTool.Socket**：Socket 通信
- **WellTool.System**：系统信息
- **WellTool.All**：所有模块的集合

## 主要功能

- **字符串处理**：字符串操作、编码转换等
- **日期时间**：日期时间格式化、计算等
- **集合工具**：集合操作、转换等
- **IO 操作**：文件、流处理等
- **加密解密**：各种加密算法实现
- **HTTP 客户端**：便捷的 HTTP 请求
- **JSON 处理**：JSON 序列化和反序列化
- **JWT**：JSON Web Token 工具
- **缓存**：多种缓存实现
- **验证码**：图片验证码生成
- **定时任务**：Cron 表达式支持
- **敏感词过滤**：DFA 算法实现
- **系统信息**：获取系统硬件、软件信息
- **AI 集成**：多种 AI 模型支持

## 安装

使用 NuGet 包管理器安装：

```bash
dotnet add package WellTool.All
```

或者安装特定模块：

```bash
dotnet add package WellTool.Core
dotnet add package WellTool.Http
dotnet add package WellTool.Json
# 其他模块...
```

## 使用示例

### 字符串处理

```csharp
using WellTool.Core.Util;

// 字符串判空
bool isEmpty = StrUtil.IsEmpty("test");

// 字符串格式化
string formatted = StrUtil.Format("Hello, {0}!", "World");
```

### HTTP 请求

```csharp
using WellTool.Http;

// 发送 GET 请求
var response = HttpUtil.Get("https://api.example.com");

// 发送 POST 请求
var postResponse = HttpUtil.Post("https://api.example.com", "{\"name\": \"test\"}");
```

### JSON 处理

```csharp
using WellTool.Json;

// JSON 序列化
string json = JSONUtil.ToJson(new { Name = "test", Age = 18 });

// JSON 反序列化
var obj = JSONUtil.ToObject<Dictionary<string, object>>(json);
```

## 贡献指南

1. Fork 本仓库
2. 创建特性分支
3. 提交更改
4. 推送到分支
5. 创建 Pull Request

## 许可证

本项目采用 Apache License 2.0 许可证。详见 [LICENSE](LICENSE) 文件。

## 联系方式

如有问题或建议，请通过 GitHub Issues 提交。

---

**WellTool - 让 .NET 开发更简单！**