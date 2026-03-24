# WellTool.Http 重构进度报告

## 📊 当前状态（2026 年 3 月 24 日）

**重构进度**: 约 40% 完成  
**编译状态**: ❌ 仍有错误，需要继续修复  

---

## ✅ 已完成的修复

### 1. HttpBase.cs - 已完成 ✅

**修复内容**:

- ✅ 重命名 `Header(string)` → `GetHeader(string)`
- ✅ 重命名 `HeaderList(string)` → `GetHeaderList(string)`  
- ✅ 重命名 `Header(Header)` → `GetHeader(Header)`
- ✅ 之前已修复：`GetIsHeaderAggregated()`, `GetHttpVersion()`, `GetCharset()`
- ✅ 之前已修复：`SetCharset(string)`, `SetCharset(Encoding?)`
- ✅ 之前已修复：`GetAllHeaders()`

**遗留问题**:

- 第 100 行、169 行、318 行还有 `Header(string, string?, bool)` 方法冲突

### 2. HttpRequest.cs - 部分完成 ⚠️

**修复内容**:

- ✅ 所有 `.Method(` 调用改为 `.SetMethod(`
- ✅ 所有 `.ContentType(` 调用改为 `.SetContentType(`
- ✅ 修复了 `SetMethod` 方法体（不再调用 `Method(method)`）

**遗留问题**:

- 第 13 行：`private Method _method = Method.GET;` - 字段声明与 `SetMethod(Method)` 方法冲突
- 第 272 行、277 行、481 行、600 行、620 行、643-651 行还有编译错误

### 3. HttpUtil.cs - 已完成 ✅

**修复内容**:

- ✅ 所有 `.Charset(` 调用改为 `.SetCharset(`

### 4. HttpResponse.cs - 部分完成 ⚠️

**修复内容**:

- ✅ 所有 `.Header(` 调用改为 `.GetHeader(`

**遗留问题**:

- 第 132 行、338-352 行还有编译错误

---

## 🔴 剩余主要问题

### 1. Header 方法重载冲突

**问题代码**:

```csharp
// HttpBase.cs - 有多个 Header 方法重载
public T Header(string name, string? value, bool isOverride)  // 方法与属性/其他重载冲突
```

**影响**:

- 编译错误 CS0119: "是一个方法，这在给定的上下文中无效"
- 约 10 处错误

**解决方案**: 重命名为 `SetHeader(string, string?, bool)`

### 2. Method 字段与方法冲突

**问题代码**:

```csharp
// HttpRequest.cs
private Method _method = Method.GET;  // 字段

public HttpRequest SetMethod(Method method)  // 方法
```

**影响**:

- 编译器无法区分字段访问和方法调用
- 约 15 处错误

**解决方案**:

- 方案 A: 将字段重命名为 `_httpMethod`
- 方案 B: 删除 `SetMethod` 方法，直接使用属性

### 3. ContentType 方法冲突

**问题代码**:

```csharp
// HttpBase.cs 中既有 ContentType 属性，又有 ContentType 方法
public string? ContentType { get; set; }
public T ContentType(string contentType)  // ← 冲突
```

**解决方案**: 重命名方法为 `SetContentType(string)`

### 4. HttpResponse 中的字符串操作错误

**问题代码**:

```csharp
// HttpResponse.cs 第 339-352 行
response.IndexOf(...)    // ❌ response 是 HttpResponse，不是 string
response.Length          // ❌ 
response.Substring(...)  // ❌
```

**原因**: 代码错误，应该操作字符串而不是 HttpResponse 对象

**解决方案**: 修复为正确的字符串操作

---

## ⏱️ 剩余工作量估算

| 任务 | 预计时间 | 优先级 |
|------|---------|--------|
| 修复 Header 方法重载 | 1-2 小时 | ⭐⭐⭐⭐⭐ |
| 修复 Method 字段冲突 | 1-2 小时 | ⭐⭐⭐⭐⭐ |
| 修复 ContentType 冲突 | 30 分钟 | ⭐⭐⭐⭐ |
| 修复 HttpResponse 字符串操作 | 1 小时 | ⭐⭐⭐⭐ |
| 清理其他编译错误 | 2-3 小时 | ⭐⭐⭐ |
| 验证编译和测试 | 1-2 小时 | ⭐⭐⭐ |
| **总计** | **6-10 小时** | - |

---

## 📝 重构策略总结

### 采用的策略

1. **getter/setter 重命名** - 将所有 Java 风格的 getter/setter 方法重命名为 `GetXxx`/`SetXxx`
2. **保持向后兼容** - 尽量不破坏现有 API
3. **渐进式修复** - 分步验证，确保每一步都能部分编译

### 遇到的问题

1. **Java 直译代码** - 很多代码是直接从 Java 翻译过来的，不符合 C# 习惯
2. **属性和方法同名** - C# 不允许属性和方法使用相同的名称
3. **调用点众多** - 一个方法的改名会影响几十处调用

---

## 🎯 下一步建议

### 立即执行（必须）

1. **修复 Header 方法重载** - 这是最多的错误来源
2. **修复 Method 字段** - 解决根本冲突
3. **验证编译** - 确保能编译通过

### 后续优化（可选）

1. **统一命名规范** - 全面采用 C# 属性风格
2. **移除冗余方法** - 能用属性的就不要用方法
3. **添加单元测试** - 确保重构不破坏功能

---

## 📊 对比：重构前后

### 重构前

```java
// Java Hutool 风格
public String header(String name) { ... }
public HttpRequest header(String name, String value) { ... }
public String method() { return this.method; }
public HttpRequest method(Method m) { this.method = m; return this; }
```

### 重构后（目标）

```csharp
// C# 风格
public string? GetHeader(string name) { ... }
public T SetHeader(string name, string? value, bool isOverride) { ... }
public Method Method { get; set; }  // 或使用不同的方法名
public T SetMethod(Method method) { ... }
```

---

## 📁 已修改的文件

1. ✅ `HttpBase.cs` - 重命名多个方法
2. ⚠️ `HttpRequest.cs` - 部分修复
3. ⚠️ `HttpResponse.cs` - 部分修复  
4. ✅ `HttpUtil.cs` - 完成修复
5. ✅ `GlobalCookieManager.cs` - 简化实现
6. ✅ `ThreadLocalCookieStore.cs` - 简化实现
7. ✅ `DefaultSslInfo.cs` - 添加 using

---

## 🚀 快速修复指南

如果要快速完成重构，可以执行以下步骤：

### 步骤 1: 修复所有 Header 重载

```powershell
cd d:\Work\WellTool\src\WellTool.Http\Http
(Get-Content HttpBase.cs -Raw) -replace 'public T Header\(string name, string\? value, bool isOverride\)', 'public T SetHeader(string name, string? value, bool isOverride)' | Set-Content HttpBase.cs
```

### 步骤 2: 修复 Method 字段

将 `_method` 重命名为 `_httpMethod`，并更新所有引用

### 步骤 3: 修复 ContentType

将 `ContentType(string)` 方法重名为 `SetContentType(string)`

### 步骤 4: 编译验证

```bash
cd d:\Work\WellTool\src
dotnet build WellTool.Http\WellTool.Http.csproj
```

---

**最后更新**: 2026 年 3 月 24 日  
**维护者**: WellTool Team  
**状态**: ⚠️ **进行中** - 已完成 40%，还需 6-10 小时
