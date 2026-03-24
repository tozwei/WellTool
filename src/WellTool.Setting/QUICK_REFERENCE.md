# WellTool.Setting 快速参考指南

## 🚀 快速开始

### 打开解决方案

```bash
# Visual Studio
devenv WellTool.sln

# Rider
rider WellTool.sln
```

### 编译项目

```bash
cd d:\Work\WellTool\src

# 编译整个解决方案
dotnet build WellTool.sln

# 只编译 Setting 项目
dotnet build WellTool.Setting\WellTool.Setting.csproj
```

### 运行测试

```bash
# 运行所有 Setting 测试
dotnet test WellTool.Setting\WellTool.Setting.Tests.csproj

# 运行所有测试（包括 Captcha）
dotnet test WellTool.sln

# 带详细输出
dotnet test --logger "console;verbosity=detailed"
```

---

## 📦 项目结构

```
WellTool/
├── WellTool.Captcha/          # 验证码库
│   ├── Captcha/               # 源代码
│   └── WellTool.Captcha.csproj
├── WellTool.Setting/          # 配置文件库 ⭐
│   ├── Setting/               # Setting 核心代码
│   │   ├── Dialect/           # 方言实现 (Props, Yaml)
│   │   ├── Yaml/              # YAML 工具
│   │   ├── AbsSetting.cs      # 抽象基类
│   │   ├── GroupedMap.cs      # 分组 Map
│   │   ├── Setting.cs         # 主设置类
│   │   ├── SettingLoader.cs   # 加载器
│   │   ├── SettingUtil.cs     # 工具类
│   │   └── ...
│   ├── WellTool.Setting.Tests/ # 单元测试 ⭐
│   │   ├── TestData/          # 测试数据
│   │   ├── SettingTest.cs
│   │   ├── PropsTest.cs
│   │   ├── YamlUtilTest.cs
│   │   └── ...
│   ├── WellTool.Setting.csproj
│   └── WellTool.Setting.Tests.csproj
└── WellTool.sln               # 解决方案
```

---

## 💻 使用示例

### 1. 读取 .setting 文件

```csharp
using WellTool.Setting;

// 创建 Setting 实例
var setting = new Setting("config.setting", true);

// 获取值
string driver = setting.GetStr("driver");
int port = setting.GetInt("port", 3306);
bool enabled = setting.GetBool("enabled", true);

// 按分组获取
string dbDriver = setting.GetByGroup("driver", "database");
```

### 2. 写入配置

```csharp
// 设置值
setting.Put("app.name", "MyApp");
setting.Put("app.version", "1.0.0");

// 保存
setting.Store();
```

### 3. 变量替换

```csharp
// config.setting:
// base_path = /opt/app
// log_path = ${base_path}/logs

var setting = new Setting("config.setting", true);
string logPath = setting.GetStr("log_path"); 
// 输出：/opt/app/logs
```

### 4. 读取 Properties 文件

```csharp
using WellTool.Setting.Dialect;

var props = PropsUtil.Read("application.properties");
string value = props.GetStr("database.url");
```

### 5. 读取 YAML 文件

```csharp
using WellTool.Setting.Yaml;

var yamlData = YamlUtil.LoadByPath("config.yaml");
string name = yamlData["name"]?.ToString();
```

---

## 🧪 测试命令

### 基本测试

```bash
cd d:\Work\WellTool\src
dotnet test WellTool.Setting\WellTool.Setting.Tests.csproj
```

### 带覆盖率

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=html
start coverlet.html
```

### 特定测试类

```bash
# 只运行 SettingTest
dotnet test --filter "FullyQualifiedName~SettingTest"

# 只运行 PropsTest
dotnet test --filter "FullyQualifiedName~PropsTest"
```

---

## 🔧 常见问题

### Q1: 测试找不到 TestData 文件？

**解决方法**: 手动复制测试数据

```bash
Copy-Item -Path WellTool.Setting.Tests\TestData `
          -Destination WellTool.Setting.Tests\bin\Debug\net8.0\TestData `
          -Recurse -Force
```

### Q2: 编译时出现 CS0436 警告？

这是正常的，是因为类型在同一个程序集中定义。不影响功能。

### Q3: 如何在 VS 中调试测试？

1. 在测试方法上设置断点
2. 右键点击测试方法
3. 选择 "调试测试" 或按 Ctrl+R, Ctrl+T

---

## 📊 NuGet 包依赖

### WellTool.Setting

```xml
<PackageReference Include="YamlDotNet" Version="13.7.1" />
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0" />
```

### WellTool.Setting.Tests

```xml
<PackageReference Include="xunit" Version="2.6.6" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
```

---

## 🎯 下一步建议

### 1. 修复测试数据自动复制

修改 `WellTool.Setting.Tests.csproj`:

```xml
<ItemGroup>
  <Content Include="TestData\**">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

### 2. 发布到 NuGet

```bash
# 打包
dotnet pack WellTool.Setting\WellTool.Setting.csproj -c Release

# 推送到 NuGet.org
dotnet nuget push WellTool.Setting.*.nupkg `
  -k YOUR_API_KEY `
  -s https://api.nuget.org/v3/index.json
```

### 3. 添加更多测试

参考 Java hutool-setting 的测试用例，补充更多边界情况测试。

---

## 📖 参考文档

- [README_COMPLETE.md](README_COMPLETE.md) - 完整功能说明
- [EXAMPLES.md](EXAMPLES.md) - 详细使用示例
- [CONVERSION_SUMMARY.md](CONVERSION_SUMMARY.md) - Java 转 .NET 说明
- [README_TESTS.md](WellTool.Setting.Tests/README_TESTS.md) - 测试指南
- [BUILD_AND_TEST_STATUS.md](BUILD_AND_TEST_STATUS.md) - 编译和测试状态

---

## 🌟 特性对比

| 功能 | Java hutool-setting | .NET WellTool.Setting |
|------|---------------------|------------------------|
| .setting 支持 | ✅ | ✅ |
| .properties 支持 | ✅ | ✅ |
| .yaml 支持 | ✅ | ✅ |
| 变量替换 | ✅ | ✅ |
| 分组配置 | ✅ | ✅ |
| 线程安全 | ✅ | ✅ (ConcurrentDictionary) |
| 类型转换 | ✅ | ✅ |
| 流式加载 | ✅ | ✅ |
| 自动重载 | ❌ | ❌ |

---

## 📞 支持

如有问题，请：

1. 查看上述参考文档
2. 检查测试示例代码
3. 联系开发团队

---

**最后更新**: 2026 年 3 月 24 日  
**版本**: 1.0.0  
**框架**: .NET 8.0
