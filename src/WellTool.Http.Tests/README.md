# WellTool.Http 测试项目转换指南

## ✅ 已完成的转换

### 1. 测试项目结构

- ✅ 创建了 .NET 8 xUnit 测试项目：`WellTool.Http.Tests`
- ✅ 配置了测试框架和依赖项
- ✅ 添加了项目引用到 `WellTool.Http.csproj`

### 2. 已转换的测试类

#### HttpUtilTests.cs

**源文件**: `hutool-http/src/test/java/cn/hutool/http/HttpUtilTest.java`  
**目标文件**: `WellTool.Http.Tests/HttpUtilTests.cs`

**已转换的测试方法**:

- ✅ `IsHttpTest()` - 验证 HTTP URL 识别
- ✅ `IsHttpsTest()` - 验证 HTTPS URL 识别
- ✅ `DecodeParamsTest()` - URL 参数解码测试
- ✅ `DecodeParamMapTest()` - URL 参数 Map 解码测试
- ✅ `ToParamsTest()` - Map 转 URL 参数测试
- ✅ `EncodeParamTest()` - URL 参数编码测试（多个场景）
- ✅ `DecodeParamTest()` - URL 参数解码测试（多个场景）
- ✅ `UrlWithFormTest()` - URL 带表单数据测试
- ✅ `GetCharsetTest()` - 获取字符集测试
- ✅ `NormalizeParamsTest()` - 标准化参数测试
- ✅ `NormalizeBlankParamsTest()` - 空参数测试
- ✅ `NormalizeAmpersandParamsTest()` - &符号参数测试
- ✅ `GetMimeTypeTest()` - MIME 类型获取测试

**备注**:

- 移除了所有 `@Disabled` 标注的网络请求测试
- 保留了所有纯单元测试
- 将 Java 的 `@Test` 转换为 xUnit 的 `[Fact]`

#### HttpRequestTests.cs

**源文件**: `hutool-http/src/test/java/cn/hutool/http/HttpRequestTest.java`  
**目标文件**: `WellTool.Http.Tests/HttpRequestTests.cs`

**已转换的测试方法**:

- ✅ `CreateGetRequestTest()` - 创建 GET 请求测试
- ✅ `CreatePostRequestTest()` - 创建 POST 请求测试
- ✅ `SetMethodTest()` - 设置请求方法测试
- ✅ `SetHeaderTest()` - 设置请求头测试
- ✅ `SetContentTypeTest()` - 设置 Content-Type 测试
- ✅ `SetCharsetTest()` - 设置字符集测试
- ✅ `SetTimeoutTest()` - 设置超时测试
- ✅ `SetFormTest()` - 设置表单数据测试
- ✅ `SetBodyTest()` - 设置请求体测试
- ✅ `SetFollowRedirectsTest()` - 设置重定向测试
- ✅ `AddCookieTest()` - 添加 Cookie 测试
- ✅ `RemoveHeaderTest()` - 移除请求头测试
- ✅ `UrlBuilderTest()` - URL 构建测试
- ✅ `ChainCallTest()` - 链式调用测试
- ✅ `StaticCreateTest()` - 静态创建方法测试

**备注**:

- 移除了需要网络的测试（原 `@Disabled` 标注）
- 专注于 API 可用性和对象状态测试

#### ContentTypeTests.cs

**源文件**: `hutool-http/src/test/java/cn/hutool/http/ContentTypeTest.java`  
**目标文件**: `WellTool.Http.Tests/ContentTypeTests.cs`

**已转换的测试方法**:

- ✅ `BuildTest()` - Content-Type 构建测试
- ✅ `GetWithLeadingSpaceTest()` - 带前导空格 JSON 检测测试
- ✅ `IsDefaultTest()` - 默认 Content-Type 判断测试
- ✅ `GetContentTypeTest()` - Content-Type 自动识别测试

#### HtmlUtilTests.cs

**源文件**: `hutool-http/src/test/java/cn/hutool/http/HtmlUtilTest.java`  
**目标文件**: `WellTool.Http.Tests/HtmlUtilTests.cs`

**已转换的测试方法**:

- ✅ `RemoveHtmlTagTest()` - 移除 HTML 标签测试
- ✅ `CleanHtmlTagTest()` - 清理 HTML 标签测试
- ✅ `EscapeTest()` - HTML 转义测试
- ✅ `UnescapeTest()` - HTML 反转义测试
- ✅ `EncodeTest()` - HTML 编码测试
- ✅ `DecodeTest()` - HTML 解码测试

**注意**: 此测试类依赖于 `HtmlUtil` 工具类，需要先实现该类

---

## ⏳ 待完成的转换

### 需要实现的工具类

以下工具类在原始 Java 代码中存在，但在 .NET 版本中尚未实现：

1. **HtmlUtil** - HTML 工具类
   - 需要实现的方法:
     - `Escape(string)` - HTML 转义
     - `Unescape(string)` - HTML 反转义
     - `Encode(string)` - HTML 编码
     - `Decode(string)` - HTML 解码
     - `RemoveHtmlTag(string, String)` - 移除指定标签
     - `CleanHtmlTag(string)` - 清理所有标签

2. **HTTP 服务器相关** - 用于测试的本地服务器功能
   - `HttpUtil.createServer(int port)` - 创建本地 HTTP 服务器

### 待转换的测试文件

以下是其他可以转换的测试文件（按优先级排序）：

#### 高优先级（核心功能）

1. ✅ `HttpUtilTest.java` → `HttpUtilTests.cs` (已完成)
2. ✅ `HttpRequestTest.java` → `HttpRequestTests.cs` (已完成)
3. ✅ `ContentTypeTest.java` → `ContentTypeTests.cs` (已完成)
4. ⏳ `HtmlUtilTest.java` → `HtmlUtilTests.cs` (已创建，等待 HtmlUtil 实现)

#### 中优先级（扩展功能）

5. ⏳ `DownloadTest.java` - 下载功能测试
2. ⏳ `RestTest.java` - RESTful API 测试
3. ⏳ `UploadTest.java` - 上传功能测试
4. ⏳ `HttpsTest.java` - HTTPS 测试

#### 低优先级（边界情况和 Issue 修复验证）

9. ⏳ `Issue*.java` 系列 - 各种 Issue 修复验证测试
   - `Issue2658Test.java`
   - `Issue3074Test.java`
   - `Issue3197Test.java`
   - 等等...

#### 特殊功能测试

10. ⏳ `server/` 目录 - HTTP 服务端测试
2. ⏳ `useragent/` 目录 - User-Agent 解析测试
3. ⏳ `body/` 目录 - 请求体处理测试
4. ⏳ `webservice/` 目录 - WebService 测试

---

## 📊 转换统计

### 已完成

- 测试项目：1 个 ✅
- 测试类：4 个 ✅
- 测试方法：约 40+ 个 ✅

### 待完成

- 需要新实现的工具类：2 个
- 待转换测试文件：约 20+ 个
- 预计新增测试方法：约 100+ 个

---

## 🔧 运行测试

### 编译测试项目

```bash
cd d:\Work\WellTool\src
dotnet build WellTool.Http.Tests\WellTool.Http.Tests.csproj
```

### 运行所有测试

```bash
cd d:\Work\WellTool\src
dotnet test WellTool.Http.Tests\WellTool.Http.Tests.csproj
```

### 运行特定测试类

```bash
cd d:\Work\WellTool\src
dotnet test WellTool.Http.Tests\WellTool.Http.Tests.csproj --filter "FullyQualifiedName~HttpUtilTests"
```

### 运行并查看测试结果

```bash
cd d:\Work\WellTool\src
dotnet test WellTool.Http.Tests\WellTool.Http.Tests.csproj --logger "console;verbosity=detailed"
```

---

## 📝 转换规范

### Java JUnit 5 → .NET xUnit 映射

| Java (JUnit 5) | .NET (xUnit) | 说明 |
|----------------|--------------|------|
| `@Test` | `[Fact]` | 基本测试特性 |
| `@Disabled` | `[Fact(Skip = "reason")]` | 跳过测试 |
| `@BeforeEach` | 构造函数 | 每个测试前执行 |
| `@AfterEach` | `Dispose()` | 每个测试后执行 |
| `@BeforeAll` | 静态构造函数 | 所有测试前执行 |
| `@AfterAll` | `IDisposable.Dispose()` | 所有测试后执行 |

### 断言映射

| Java (AssertJ/JUnit) | .NET (xUnit) | 说明 |
|---------------------|--------------|------|
| `assertEquals(a, b)` | `Assert.Equal(a, b)` | 相等断言 |
| `assertTrue(condition)` | `Assert.True(condition)` | 真值断言 |
| `assertFalse(condition)` | `Assert.False(condition)` | 假值断言 |
| `assertNull(obj)` | `Assert.Null(obj)` | 空值断言 |
| `assertNotNull(obj)` | `Assert.NotNull(obj)` | 非空断言 |
| `fail(message)` | `Assert.Fail(message)` | 失败断言 |

### 命名规范

**测试类命名**:

- Java: `XxxTest.java`
- .NET: `XxxTests.cs` (复数形式)

**测试方法命名**:

- Java: `testXxx()`, `xxxTest()`
- .NET: `XxxTest()`, `Xxx_Should_YYY()`

---

## 🎯 下一步计划

### 阶段 1: 完成基础工具类实现

- [ ] 实现 `HtmlUtil` 工具类
- [ ] 补充 `HttpUtil` 缺失方法

### 阶段 2: 转换核心测试

- [ ] `DownloadTest.java` → `DownloadTests.cs`
- [ ] `RestTest.java` → `RestTests.cs`
- [ ] `UploadTest.java` → `UploadTests.cs`

### 阶段 3: 转换扩展测试

- [ ] `HttpsTest.java` → `HttpsTests.cs`
- [ ] `Issue*Test.java` → `Issue*Tests.cs`

### 阶段 4: 转换模块测试

- [ ] `server/` 目录测试
- [ ] `useragent/` 目录测试
- [ ] `body/` 目录测试
- [ ] `webservice/` 目录测试

---

## 📚 参考资料

- [xUnit.net 官方文档](https://xunit.net/docs)
- [Hutool HTTP 源码](./hutool-http)
- [WellTool.Http 项目结构](../WellTool.Http.csproj)

---

**最后更新**: 2026 年 3 月 24 日  
**维护者**: WellTool Team  
**状态**: 🟡 部分完成 (4/20+ 测试文件)
