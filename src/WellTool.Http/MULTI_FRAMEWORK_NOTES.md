# WellTool.Http 多框架支持说明

## ⚠️ 当前状态

WellTool.Http 项目**暂时仅支持 .NET 8.0**，无法直接扩展到 .NET Standard 2.1 和 .NET 6.0。

---

## 📦 当前配置

### WellTool.Http.csproj

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
  <LangVersion>latest</LangVersion>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

---

## 🚫 无法支持多框架的原因

### 1. 使用了 .NET 8 特有的 API

#### CookieManager 和 HttpCookie

- **位置**: `Http/Cookie/GlobalCookieManager.cs`, `ThreadLocalCookieStore.cs`
- **问题**: 这些类型仅在 .NET 8 中可用
- **影响**: 无法在 .NET Standard 2.1 和 .NET 6 中编译

```csharp
// GlobalCookieManager.cs
private static CookieManager? _cookieManager;
public static void SetCookieManager(CookieManager? customCookieManager)

// ThreadLocalCookieStore.cs
public class ThreadLocalCookieStore : ConcurrentDictionary<string, HttpCookie>
```

#### SslProtocols

- **位置**: `Http/Ssl/DefaultSslInfo.cs`
- **问题**: 需要添加 `using System.Net.Security;`
- **影响**: 在所有框架中都需要修复

```csharp
public const SslProtocols DefaultSslProtocol = SslProtocols.Tls12 | SslProtocols.Tls13;
```

### 2. 代码结构问题（Java 转 C# 的遗留问题）

#### HttpBase.cs 中的重复定义

**问题示例**:

```csharp
// 字段定义（第 30 行）
protected bool IsHeaderAggregated { get; set; }

// 方法定义（第 302 行）
public bool IsHeaderAggregated()
{
    return IsHeaderAggregated;
}
```

同样的问题还出现在：

- `Headers` - 字段和方法重名
- `Charset` - 字段和方法重名  
- `HttpVersion` - 字段和方法重名

**原因**: 这是 Java 转 C# 时的常见问题。Java 中 getter 方法命名习惯与 C# 属性不同。

---

## 🔧 需要的修复工作

### 方案 1: 保持仅 .NET 8 支持（推荐）

**优点**:

- ✅ 无需修改代码
- ✅ 使用最新的 .NET 特性
- ✅ CookieManager 等类型可直接使用
- ✅ 快速上线

**缺点**:

- ❌ 不支持旧版本 .NET
- ❌ 用户群受限

### 方案 2: 重构代码以支持多框架（工作量大）

#### 步骤 1: 使用条件编译

```csharp
#if NET8_0_OR_GREATER
using System.Net;

public class GlobalCookieManager
{
    private static CookieManager? _cookieManager;
    // ...
}
#else
// 使用其他 Cookie 管理方案或返回 null
public class GlobalCookieManager
{
    public static void SetCookieManager(object? customCookieManager)
    {
        throw new PlatformNotSupportedException(
            "CookieManager 仅在 .NET 8+ 中支持");
    }
}
#endif
```

#### 步骤 2: 修复重复定义

将 Java 风格的 getter/setter 方法重命名或删除：

```csharp
// 修复前
protected string HttpVersion { get; set; }
public string HttpVersion() => HttpVersion;
public T SetHttpVersion(string httpVersion) => HttpVersion = httpVersion;

// 修复后（使用 C# 属性语法）
protected string HttpVersion { get; set; }
// 删除 HttpVersion() 和 SetHttpVersion() 方法
// 直接使用属性访问
```

#### 步骤 3: 添加缺失的 using

```csharp
// DefaultSslInfo.cs
using System.Net.Security;  // 添加这个
using System.Security.Cryptography.X509Certificates;
```

#### 步骤 4: 替换不可用的 API

对于 .NET Standard 2.1 和 .NET 6，需要使用替代方案：

```csharp
// 方案 A: 使用 HttpClientHandler.CookieContainer
#if !NET8_0_OR_GREATER
var handler = new HttpClientHandler
{
    CookieContainer = new CookieContainer()
};
#endif

// 方案 B: 完全移除 Cookie 管理功能
#endif
```

---

## 📊 工作量估算

| 任务 | 预计时间 | 复杂度 |
|------|---------|--------|
| 修复重复定义 | 2-4 小时 | ⭐⭐⭐ |
| 添加条件编译 | 4-8 小时 | ⭐⭐⭐⭐ |
| 替换 CookieManager | 4-6 小时 | ⭐⭐⭐⭐⭐ |
| 测试验证 | 2-4 小时 | ⭐⭐⭐ |
| **总计** | **12-22 小时** | **高** |

---

## 🎯 建议

### 短期方案（推荐）

**保持 .NET 8.0 单框架支持**

理由：

1. WellTool.Http 依赖 .NET 8 特有 API
2. 重构成本高（12-22 小时）
3. .NET 8 是 LTS 版本，已足够稳定
4. 可以快速投入使用

### 长期方案

如果确实需要多框架支持：

1. **评估需求**: 确认是否真的需要支持 .NET 6 或 .NET Standard
2. **逐步重构**:
   - 先修复重复定义问题
   - 再添加条件编译
   - 最后处理 CookieManager 等 API
3. **充分测试**: 在每个框架上都要测试

---

## 📝 其他项目状态

作为对比，其他项目的多框架支持情况：

| 项目 | 框架支持 | 状态 |
|------|---------|------|
| WellTool.Setting | netstandard2.1;net6.0;net8.0 | ✅ 完成 |
| WellTool.Captcha | netstandard2.1;net6.0;net8.0 | ✅ 完成 |
| WellTool.Captcha.Tests | net6.0;net8.0 | ✅ 完成 |
| **WellTool.Http** | **net8.0** | **⚠️ 仅 .NET 8** |

---

## 🔍 技术细节

### .NET 8 特有 API 列表

WellTool.Http 中使用的 .NET 8 特有类型：

1. **System.Net.CookieManager**
   - 用途：Cookie 管理
   - 替代方案：HttpClientHandler.CookieContainer（复杂）

2. **System.Net.HttpCookie**
   - 用途：单个 Cookie 表示
   - 替代方案：自定义类或 NameValueCollection

### 重复定义详细列表

在 `HttpBase.cs` 中：

```
第 30 行：protected bool IsHeaderAggregated { get; set; }
第 302 行：public bool IsHeaderAggregated() ← 重复

第 35 行：protected Dictionary<string, List<string>> Headers { get; }
第 272 行：public IReadOnlyDictionary<string, List<string>> Headers() ← 重复

第 40 行：protected Encoding? Charset { get; set; }
第 366 行：public string? Charset() ← 重复
第 376 行：public T Charset(string charset) ← 重载冲突

第 45 行：protected string HttpVersion { get; set; }
第 329 行：public string HttpVersion() ← 重复
```

---

## ✅ 验证清单

### 当前状态（仅 .NET 8）

- [x] 项目配置为 net8.0
- [ ] 编译通过（需要修复代码错误）
- [ ] 功能正常

### 多框架支持（如需实现）

- [ ] 修复所有重复定义
- [ ] 添加条件编译指令
- [ ] 替换或封装 .NET 8 特有 API
- [ ] 在 netstandard2.1 上编译通过
- [ ] 在 net6.0 上编译通过
- [ ] 在 net8.0 上编译通过
- [ ] 所有功能测试通过

---

## 📖 参考资源

- [.NET 8 新增功能](https://learn.microsoft.com/zh-cn/dotnet/core/whats-new/dotnet-8)
- [CookieManager Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.cookiemanager)
- [.NET 迁移指南](https://learn.microsoft.com/zh-cn/dotnet/core/porting/)

---

**更新日期**: 2026 年 3 月 24 日  
**维护者**: WellTool Team  
**状态**: ⚠️ 仅支持 .NET 8.0
