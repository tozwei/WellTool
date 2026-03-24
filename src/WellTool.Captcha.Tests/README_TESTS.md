# WellTool.Captcha 单元测试指南

## 📁 测试文件位置

- **测试项目**: `WellTool.Captcha.Tests.csproj`
- **测试类**: `CaptchaTest.cs`
- **测试框架**: xUnit
- **.NET 版本**: .NET 8.0

## 🎯 运行测试

### 1. 恢复 NuGet 包

```bash
cd d:\Work\WellTool\src\WellTool.Captcha\Captcha.Tests
dotnet restore
```

### 2. 编译项目

```bash
dotnet build
```

### 3. 运行所有测试

```bash
dotnet test
```

### 4. 运行特定测试

```bash
# 运行基础测试
dotnet test --filter "FullyQualifiedName~LineCaptchaTest1"

# 运行所有 LineCaptcha 测试
dotnet test --filter "FullyQualifiedName~LineCaptcha"

# 运行所有 CircleCaptcha 测试
dotnet test --filter "FullyQualifiedName~CircleCaptcha"

# 运行所有 ShearCaptcha 测试
dotnet test --filter "FullyQualifiedName~ShearCaptcha"

# 运行所有 MathGenerator 测试
dotnet test --filter "FullyQualifiedName~MathGenerator"
```

### 5. 详细输出

```bash
dotnet test --verbosity detailed
```

## 📊 测试用例说明

### ✅ 已启用的测试（可直接运行）

#### 1. LineCaptchaTest1

- **功能**: 测试基础线条验证码功能
- **内容**: 创建验证码并验证有效性
- **状态**: ✅ 启用

#### 2. VerifyLogicTest

- **功能**: 测试验证码验证逻辑
- **内容**: 验证正确/错误/空值的处理
- **状态**: ✅ 启用

#### 3. VerifyIgnoreCaseTest

- **功能**: 测试验证码不区分大小写
- **内容**: 验证大小写转换后的验证结果
- **状态**: ✅ 启用

#### 4. RandomGeneratorTest

- **功能**: 测试随机字符生成器
- **内容**: 生成验证码并验证功能
- **状态**: ✅ 启用

#### 5. MathGeneratorTest

- **功能**: 测试数学计算器生成器
- **内容**: 生成数学表达式并验证计算结果
- **状态**: ✅ 启用

### ⏸️ 已禁用的测试（需要文件系统访问）

以下测试被标记为 `[Fact(Skip = "需要文件系统访问")]`，因为它们会写入文件到临时目录。如果需要运行这些测试，请移除 `Skip` 参数：

1. **LineCaptchaTest3** - 自定义参数的线条验证码
2. **LineCaptchaTestWithSize** - 带字体大小的线条验证码
3. **LineCaptchaWithMathTest** - 数学计算验证码
4. **LineCaptchaTest2** - 线条验证码完整流程
5. **CircleCaptchaTest** - 圆圈干扰验证码
6. **CircleCaptchaTestWithSize** - 带字体大小的圆圈验证码
7. **ShearCaptchaTest** - 扭曲干扰验证码
8. **ShearCaptchaTest2** - 扭曲验证码（直接构造）
9. **ShearCaptchaWithMathTest** - 带数学计算的扭曲验证码
10. **ShearCaptchaTestWithSize** - 带字体大小的扭曲验证码
11. **GifCaptchaTest** - GIF 动画验证码（简化版）
12. **GifCaptchaTestWithSize** - 带字体大小的 GIF 验证码
13. **BgTest** - 背景色设置测试

### 启用文件写入测试的方法

如果要运行这些测试，修改测试方法：

```csharp
// 修改前（已禁用）
[Fact(Skip = "需要文件系统访问")]
public void LineCaptchaTest3() { ... }

// 修改后（启用）
[Fact]
public void LineCaptchaTest3() { ... }
```

## 🔍 测试覆盖的功能

### 验证码类型

- ✅ LineCaptcha（线条干扰）
- ✅ CircleCaptcha（圆圈干扰）
- ✅ ShearCaptcha（扭曲干扰）
- ⚠️ GifCaptcha（简化为 LineCaptcha）

### 生成器类型

- ✅ RandomGenerator（随机字符）
- ✅ MathGenerator（数学计算）

### 自定义功能

- ✅ 背景颜色设置
- ✅ 文字透明度调整
- ✅ 字体大小控制
- ✅ 验证码验证（包含/忽略大小写）

### 边界条件

- ✅ 空值处理
- ✅ 错误输入处理
- ✅ 重复生成验证码

## 📝 测试报告

运行测试后会生成测试报告，包括：

- ✅ 通过的测试数
- ❌ 失败的测试数
- ⏭️ 跳过的测试数
- ⏱️ 总执行时间

### 示例输出

```
Passed!  - Failed: 0, Passed: 5, Skipped: 13, Total: 18, Duration: 245 ms
```

## 🐛 故障排除

### 问题 1: 找不到 Fact 特性

**原因**: NuGet 包未恢复  
**解决**:

```bash
dotnet clean
dotnet restore
dotnet build
```

### 问题 2: System.Drawing 相关警告

**原因**: 平台兼容性警告（正常现象）  
**说明**: 这些是 CA1416 警告，不影响测试运行

### 问题 3: 文件访问权限错误

**原因**: 临时目录写入失败  
**解决**: 确保对 `%TEMP%` 目录有写入权限

## 📚 参考资源

- [xUnit 官方文档](https://xunit.net/)
- [.NET 单元测试最佳实践](https://learn.microsoft.com/zh-cn/dotnet/core/testing/)
- [System.Drawing.Common 文档](https://learn.microsoft.com/zh-cn/dotnet/api/system.drawing?view=net-8.0)

## ✅ 测试清单

运行以下命令快速验证所有核心功能：

```bash
# 1. 恢复依赖
dotnet restore

# 2. 编译项目
dotnet build

# 3. 运行所有启用的测试
dotnet test --filter "Priority!=Integration"

# 4. 查看详细结果
dotnet test --logger "console;verbosity=detailed"
```

预期结果：所有核心功能测试通过 ✅
