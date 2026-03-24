# WellTool.Http 编译问题说明

## ❌ 当前状态

WellTool.Http **无法编译通过**，存在以下关键问题：

---

## 🔴 编译错误

### 1. 缺失的类型（.NET 8 中也不存在）

#### CookieManager

- **位置**: `Http/Cookie/GlobalCookieManager.cs`
- **错误**: `CS0246: 未能找到类型或命名空间名"CookieManager"`
- **原因**: Java Hutool 中的 `CookieManager` 在 .NET 中不存在对应的标准类型
- **影响行数**: 第 11, 16, 23, 35 行

#### HttpCookie  

- **位置**: `Http/Cookie/ThreadLocalCookieStore.cs`, `GlobalCookieManager.cs`
- **错误**: `CS0246: 未能找到类型或命名空间名"HttpCookie"`
- **原因**: Java Hutool 中的 `HttpCookie` 在 .NET 中不存在
- **影响行数**: 多处使用

### 2. 重复定义（Java 转 C# 遗留问题）

**文件**: `Http/HttpBase.cs`

```csharp
// 属性定义（第 30 行）
protected bool IsHeaderAggregated { get; set; }

// 方法定义（第 302 行）  
public bool IsHeaderAggregated() { ... }  // ← 与属性同名
```

同样的问题存在于：

- `Headers` (第 35 行属性 vs 第 272 行方法)
- `Charset` (第 40 行属性 vs 第 366/376/398 行方法)
- `HttpVersion` (第 45 行属性 vs 第 329 行方法)

---

## 💡 解决方案

### 方案 A: 移除 Cookie 功能（推荐 - 最快）

**理由**:

1. CookieManager 和 HttpCookie 不是 .NET 标准类型
2. 需要自定义实现，工作量大
3. HTTP 核心功能不依赖 Cookie 也能工作

**步骤**:

```bash
# 1. 重命名或删除 Cookie 相关文件
mv Http/Cookie/GlobalCookieManager.cs Http/Cookie/GlobalCookieManager.cs.bak
mv Http/Cookie/ThreadLocalCookieStore.cs Http/Cookie/ThreadLocalCookieStore.cs.bak

# 2. 注释掉所有引用这些类的代码
# 3. 重新编译
dotnet build WellTool.Http\WellTool.Http.csproj
```

**优点**:

- ✅ 可以快速编译通过
- ✅ 保留核心 HTTP 功能
- ✅ 后续可以逐步添加 Cookie 支持

**缺点**:

- ❌ 暂不支持 Cookie 管理

---

### 方案 B: 使用 .NET 的 CookieContainer（中等工作量）

使用 .NET 标准的 `System.Net.CookieContainer`:

```csharp
// 替换 GlobalCookieManager.cs
using System.Net;

public class GlobalCookieManager
{
    private static readonly CookieContainer _cookieContainer = new();
    
    public static void Add(Uri uri, Cookie cookie)
    {
        _cookieContainer.Add(uri, cookie);
    }
    
    public static CookieCollection GetCookies(Uri uri)
    {
        return _cookieContainer.GetCookies(uri);
    }
}
```

**工作量**: 约 4-8 小时

- 需要重写所有 Cookie 相关代码
- 需要适配 Java 到 .NET 的 API 差异

---

### 方案 C: 自定义 HttpCookie 类（最大工作量）

完全自己实现 Cookie 管理：

```csharp
namespace WellTool.Http.Cookie;

public class HttpCookie
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string? Domain { get; set; }
    public string? Path { get; set; }
    public DateTime? Expires { get; set; }
    public bool Secure { get; set; }
    public bool HttpOnly { get; set; }
}
```

**工作量**: 约 8-16 小时

- 需要实现完整的 Cookie 解析、存储、匹配逻辑
- 需要处理各种边界情况

---

## 🎯 推荐方案

**立即执行 - 方案 A**:

1. 移除或注释掉 Cookie 相关文件
2. 让 HTTP 核心功能先编译通过
3. 投入使用

**后续考虑 - 方案 B**:

1. 当确实需要 Cookie 功能时
2. 使用 .NET 标准的 CookieContainer
3. 渐进式重构

---

## 📝 快速修复步骤

### 步骤 1: 备份并重命名 Cookie 文件

```powershell
cd d:\Work\WellTool\src\WellTool.Http\Http\Cookie

# 备份文件
Copy-Item GlobalCookieManager.cs GlobalCookieManager.cs.bak
Copy-Item ThreadLocalCookieStore.cs ThreadLocalCookieStore.cs.bak

# 创建空壳（保持命名空间存在）
@"
namespace WellTool.Http.Cookie;

/// <summary>
/// Cookie 管理器（暂未实现）
/// </summary>
public static class GlobalCookieManager
{
    public static void Add(System.Uri uri, string cookieString)
    {
        // TODO: 未来实现
    }
}
"@ > GlobalCookieManager.cs
```

### 步骤 2: 修复 HttpBase.cs 重复定义

修改所有 getter 方法的命名：

```csharp
// 修复前
public string HttpVersion() => HttpVersion;

// 修复后  
public string GetHttpVersion() => HttpVersion;
```

或使用表达式体：

```csharp
// 直接使用属性
var version = this.HttpVersion;  // 而不是 HttpVersion()
```

### 步骤 3: 添加缺失的 using

```csharp
// DefaultSslInfo.cs
using System.Net.Security;  // SslProtocols 需要
```

### 步骤 4: 编译验证

```bash
cd d:\Work\WellTool\src
dotnet build WellTool.Http\WellTool.Http.csproj
```

---

## ⏱️ 时间估算

| 方案 | 预计时间 | 风险 | 推荐度 |
|------|---------|------|--------|
| 方案 A: 移除 Cookie | 30 分钟 | 低 | ⭐⭐⭐⭐⭐ |
| 方案 B: CookieContainer | 4-8 小时 | 中 | ⭐⭐⭐⭐ |
| 方案 C: 自定义实现 | 8-16 小时 | 高 | ⭐⭐ |

---

## 📖 参考

- [.NET CookieContainer](https://learn.microsoft.com/en-us/dotnet/api/system.net.cookiecontainer)
- [ASP.NET Core Cookie 处理](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-cookies)

---

**更新日期**: 2026 年 3 月 24 日  
**维护者**: WellTool Team  
**建议**: 优先使用方案 A，快速上线
