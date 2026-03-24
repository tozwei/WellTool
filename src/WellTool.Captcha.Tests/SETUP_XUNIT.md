# 解决 xUnit 引用问题

## 🔴 当前问题

测试文件 `CaptchaTest.cs` 中添加了 `using Xunit;` 但 IDE 显示错误：

```
未能找到类型或命名空间名"Xunit"(是否缺少 using 指令或程序集引用?)
```

## ✅ 解决方案

### 方法 1: 通过命令行恢复（推荐）

打开终端，执行以下命令：

```bash
# 1. 进入测试项目目录
cd d:\Work\WellTool\src\WellTool.Captcha\Captcha.Tests

# 2. 恢复 NuGet 包
dotnet restore

# 3. 重新编译
dotnet build

# 4. 运行测试
dotnet test
```

### 方法 2: 在 Visual Studio 中恢复

1. **右键点击** `WellTool.Captcha.Tests` 项目
2. 选择 **"还原 NuGet 包"**
3. 等待恢复完成
4. **重新生成** 项目

### 方法 3: 清理后重新恢复

如果上述方法无效，尝试完全清理：

```bash
# 进入测试项目目录
cd d:\Work\WellTool\src\WellTool.Captcha\Captcha.Tests

# 删除 obj 和 bin 文件夹（PowerShell）
Remove-Item -Recurse -Force .\obj -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force .\bin -ErrorAction SilentlyContinue

# 或者使用命令提示符
rmdir /s /q obj
rmdir /s /q bin

# 恢复 NuGet 包
dotnet restore

# 重新编译
dotnet build
```

## 📦 已配置的 NuGet 包

测试项目已经配置了以下必要的包（在 `WellTool.Captcha.Tests.csproj` 中）：

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.6.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.4" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
<PackageReference Include="System.Drawing.Common" Version="8.0.0" />
```

这些包提供了：

- ✅ **xUnit**: 测试框架核心库（包含 `[Fact]` 等特性）
- ✅ **xunit.runner.visualstudio**: Visual Studio 测试运行器
- ✅ **Microsoft.NET.Test.Sdk**: .NET 测试 SDK
- ✅ **coverlet.collector**: 代码覆盖率收集器
- ✅ **System.Drawing.Common**: System.Drawing 跨平台支持

## 🔍 验证修复

执行以下命令验证是否修复成功：

```bash
cd d:\Work\WellTool\src\WellTool.Captcha\Captcha.Tests

# 查看恢复的包
dotnet list package

# 应该能看到 xunit 相关的包
```

## 🐛 常见问题

### 问题 1: 恢复后仍然报错

**原因**: IDE 缓存未刷新  
**解决**:

- Visual Studio: 重启 IDE
- VS Code: 按 `Ctrl+Shift+P`，输入 "Reload Window"
- Rider: File → Invalidate Caches / Restart

### 问题 2: 网络问题导致恢复失败

**原因**: NuGet 源访问超时  
**解决**:

```bash
# 使用国内镜像
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# 或者使用阿里云镜像
dotnet nuget add source https://mirrors.aliyun.com/nuget/v3/index.json -n aliyun
```

### 问题 3: 多个 .NET 版本冲突

**原因**: 系统中安装了多个版本的.NET  
**解决**:

```bash
# 查看已安装的 SDK
dotnet --list-sdks

# 确保使用 .NET 8.0
dotnet --version
```

## ✨ 成功标志

修复成功后，您应该看到：

✅ `using Xunit;` 不再显示错误  
✅ `[Fact]` 特性可以正常识别  
✅ IntelliSense 能提示 xUnit 相关代码  
✅ 可以正常编译和运行测试

## 📝 下一步

NuGet 包恢复成功后，可以运行测试：

```bash
# 运行所有启用的测试
dotnet test

# 运行特定测试
dotnet test --filter "FullyQualifiedName~LineCaptchaTest1"

# 查看详细结果
dotnet test --logger "console;verbosity=detailed"
```

预期输出示例：

```
Passed!  - Failed: 0, Passed: 5, Skipped: 13, Total: 18, Duration: 245 ms
```

## 📚 参考资源

- [xUnit 官方文档](https://xunit.net/)
- [dotnet restore 命令](https://learn.microsoft.com/zh-cn/dotnet/core/tools/dotnet-restore)
- [NuGet 包恢复故障排除](https://learn.microsoft.com/zh-cn/nuget/consume-packages/package-restore)
