# WellTool.Setting 多目标框架支持指南

## ✅ 已完成

WellTool.Setting 现已支持多目标框架编译，可以在以下平台上运行：

- ✅ **.NET Standard 2.1** - 跨平台兼容层
- ✅ **.NET 6.0** - .NET 6 LTS
- ✅ **.NET 8.0** - .NET 8 LTS（最新）

---

## 📦 项目配置

### WellTool.Setting.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFrameworks>netstandard2.1;net6.0;net8.0</TargetFrameworks>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0" />
    <PackageReference Include="YamlDotNet" Version="13.7.1" />
  </ItemGroup>

</Project>
```

### 关键配置说明

| 配置项 | 值 | 说明 |
|--------|-----|------|
| `TargetFrameworks` | `netstandard2.1;net6.0;net8.0` | 多目标框架（注意是复数形式 `TargetFrameworks`） |
| `LangVersion` | `latest` | 使用最新的 C# 语言版本 |
| `Nullable` | `enable` | 启用可空引用类型 |
| `ImplicitUsings` | `enable` | 启用隐式 using |

---

## 🎯 各框架特性对比

### .NET Standard 2.1

- **兼容性**: ⭐⭐⭐⭐⭐ (最佳)
- **运行时**: .NET Framework 4.8, .NET Core 3.0+, .NET 5+, .NET 6+, .NET 8+
- **用途**: 类库开发，最大范围兼容
- **限制**:
  - 不支持 `System.Text.Json` 最新特性
  - 部分 API 需要条件编译

### .NET 6.0 (LTS)

- **兼容性**: ⭐⭐⭐⭐
- **运行时**: .NET 6.0+
- **用途**: 现代应用开发
- **优势**:
  - 性能提升
  - 更好的 Nullable 支持
  - 模式匹配增强

### .NET 8.0 (LTS)

- **兼容性**: ⭐⭐⭐
- **运行时**: .NET 8.0+
- **用途**: 最新应用开发
- **优势**:
  - 最佳性能
  - 所有最新特性
  - 长期支持

---

## 🔧 编译命令

### 编译所有目标框架

```bash
cd d:\Work\WellTool\src\WellTool.Setting
dotnet build WellTool.Setting.csproj
```

**输出示例**:

```
WellTool.Setting net8.0 成功
WellTool.Setting netstandard2.1 成功
WellTool.Setting net6.0 成功
```

### 编译特定框架

```bash
# 只编译 .NET Standard 2.1
dotnet build WellTool.Setting.csproj -f netstandard2.1

# 只编译 .NET 6.0
dotnet build WellTool.Setting.csproj -f net6.0

# 只编译 .NET 8.0
dotnet build WellTool.Setting.csproj -f net8.0
```

### 发布所有框架

```bash
dotnet pack WellTool.Setting.csproj -c Release
```

这会为每个框架生成独立的 DLL。

---

## 📂 输出目录结构

编译后的文件结构：

```
WellTool.Setting/bin/Debug/
├── netstandard2.1/
│   ├── WellTool.Setting.dll
│   ├── WellTool.Setting.pdb
│   └── WellTool.Setting.deps.json
├── net6.0/
│   ├── WellTool.Setting.dll
│   ├── WellTool.Setting.pdb
│   └── WellTool.Setting.deps.json
└── net8.0/
    ├── WellTool.Setting.dll
    ├── WellTool.Setting.pdb
    └── WellTool.Setting.deps.json
```

---

## 🎯 使用方式

### 在项目中引用

#### 方式 1: 项目引用

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\WellTool.Setting\WellTool.Setting.csproj" />
  </ItemGroup>
</Project>
```

#### 方式 2: NuGet 包引用（发布后）

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="WellTool.Setting" Version="1.0.0" />
  </ItemGroup>
</Project>
```

### 自动框架匹配

当您引用 WellTool.Setting 时，NuGet 会自动选择与您的项目匹配的框架：

- 如果您的项目是 `net8.0` → 使用 `net8.0` 版本
- 如果您的项目是 `net6.0` → 使用 `net6.0` 版本
- 如果您的项目是 `netstandard2.1` 或 `.NET Framework` → 使用 `netstandard2.1` 版本

---

## ⚠️ 注意事项

### 1. NuGet 包版本

当前使用的包都支持所有目标框架：

```xml
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0" />
<PackageReference Include="YamlDotNet" Version="13.7.1" />
```

✅ **Microsoft.Extensions.Logging.Abstractions 8.0.0**: 支持 netstandard2.0+
✅ **YamlDotNet 13.7.1**: 支持 netstandard2.0+

### 2. 条件编译（如需要）

如果某些代码只在特定框架下可用，可以使用条件编译：

```csharp
#if NET8_0_OR_GREATER
    // .NET 8 特有代码
#elif NET6_0_OR_GREATER
    // .NET 6 特有代码
#else
    // .NET Standard 2.1 代码
#endif
```

### 3. 测试项目

测试项目通常只需要针对一个框架：

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
</PropertyGroup>
```

---

## 🧪 测试

### 运行测试

```bash
# 测试项目默认使用 net8.0
dotnet test WellTool.Setting.Tests\WellTool.Setting.Tests.csproj
```

### 如果需要测试所有框架

可以创建多框架的测试项目：

```xml
<PropertyGroup>
  <TargetFrameworks>net6.0;net8.0</TargetFrameworks>
</PropertyGroup>
```

---

## 📊 框架选择建议

### 选择 .NET Standard 2.1，如果您需要

- ✅ 最大的兼容性
- ✅ 支持 .NET Framework 4.8
- ✅ 类库开发
- ✅ 跨多个 .NET 版本

### 选择 .NET 6.0，如果您需要

- ✅ 现代应用开发
- ✅ 长期支持 (LTS)
- ✅ 良好的性能和特性平衡
- ✅ 不需要支持 .NET Framework

### 选择 .NET 8.0，如果您需要

- ✅ 最新特性和最佳性能
- ✅ 长期支持 (LTS)
- ✅ 现代化应用
- ✅ 不需要向后兼容

---

## 🚀 发布到 NuGet

### 1. 打包

```bash
cd d:\Work\WellTool\src\WellTool.Setting
dotnet pack WellTool.Setting.csproj -c Release -o nupkgs
```

### 2. 检查生成的包

```bash
ls nupkgs/
# WellTool.Setting.1.0.0.nupkg
```

### 3. 查看包内容

```bash
# 使用 7-Zip 或其他工具查看
# 应该包含 lib/netstandard2.1/, lib/net6.0/, lib/net8.0/
```

### 4. 推送到 NuGet.org

```bash
dotnet nuget push nupkgs\WellTool.Setting.1.0.0.nupkg `
  -k YOUR_API_KEY `
  -s https://api.nuget.org/v3/index.json
```

---

## 📈 性能对比

| 框架 | 启动时间 | 内存占用 | 执行速度 |
|------|---------|---------|---------|
| netstandard2.1 | 基准 | +10% | 基准 |
| net6.0 | -20% | 基准 | +15% |
| net8.0 | -30% | -5% | +25% |

*数据仅供参考，实际性能取决于具体使用场景*

---

## 🔍 故障排查

### 问题 1: 编译失败，提示找不到类型

**原因**: 某些 API 在低版本框架中不可用

**解决**:

```csharp
#if NET6_0_OR_GREATER
// 使用新 API
#else
// 使用替代方案
#endif
```

### 问题 2: 运行时出现 MethodNotFoundException

**原因**: 引用了不同版本的依赖包

**解决**: 确保所有项目使用相同版本的 NuGet 包

### 问题 3: 警告 CS8604 (null 引用)

**原因**: Nullable 检查不严格

**解决**: 添加 null 检查或使用 `!` 操作符

---

## 📖 参考资源

- [多目标框架官方文档](https://learn.microsoft.com/zh-cn/dotnet/standard/frameworks)
- [.NET Standard 规范](https://learn.microsoft.com/zh-cn/dotnet/standard/net-standard)
- [.NET 6 新增功能](https://learn.microsoft.com/zh-cn/dotnet/core/whats-new/dotnet-6)
- [.NET 8 新增功能](https://learn.microsoft.com/zh-cn/dotnet/core/whats-new/dotnet-8)

---

## ✅ 验证清单

- [x] 项目文件配置为多目标框架
- [x] 所有框架都能成功编译
- [x] NuGet 包版本兼容所有目标框架
- [x] 没有使用框架特有的 API（除非有条件编译）
- [x] 测试项目能正常运行
- [x] 输出目录包含所有框架的 DLL

---

**更新日期**: 2026 年 3 月 24 日  
**维护者**: WellTool Team  
**状态**: ✅ 完成
