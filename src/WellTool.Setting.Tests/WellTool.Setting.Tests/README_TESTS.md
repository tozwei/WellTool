# WellTool.Setting 测试指南

## 📋 测试项目说明

本测试项目包含了从 Java Hutool-setting 项目转换而来的完整单元测试套件。

### 测试覆盖

| 测试类 | 测试内容 | 测试方法数 |
|--------|----------|------------|
| **SettingTest** | Setting 核心功能测试 | 6 个 |
| **PropsTest** | Properties 文件支持测试 | 5 个 |
| **YamlUtilTest** | YAML 读写功能测试 | 5 个 |
| **SettingUtilTest** | Setting 工具类测试 | 3 个 |
| **PropsUtilTest** | Props 工具类测试 | 2 个 |
| **总计** | - | **21 个测试** |

## 🚀 运行测试

### 方法 1: 命令行运行

```bash
# 进入测试项目目录
cd d:\Work\WellTool\src\WellTool.Setting\WellTool.Setting.Tests

# 恢复 NuGet 包
dotnet restore

# 运行所有测试
dotnet test

# 运行特定测试类
dotnet test --filter "FullyQualifiedName~SettingTest"

# 查看详细结果
dotnet test --logger "console;verbosity=detailed"
```

### 方法 2: Visual Studio

1. 打开解决方案或项目
2. 右键点击 `WellTool.Setting.Tests` 项目
3. 选择 **"运行测试"** 或使用快捷键 `Ctrl+R, T`

### 方法 3: Visual Studio Code

```bash
# 使用 .NET Test Explorer 插件
# 或者命令行
dotnet test
```

## 📁 测试数据

测试文件位于 `TestData/` 目录：

- `test.setting` - Setting 格式测试文件
- `test.properties` - Properties 格式测试文件
- `test.yaml` - YAML 格式测试文件

这些文件会随测试项目复制到输出目录。

## ✅ 测试用例详解

### 1. SettingTest - Setting 核心功能测试

#### SettingTest1

测试 Setting 基本功能，包括：

- ✅ 分组读取
- ✅ 变量替换（本分组）
- ✅ 变量替换（跨分组）
- ✅ 默认值处理

#### SettingTestForCustom

测试自定义 Setting（手动添加配置）：

- ✅ 动态添加配置项
- ✅ 分组管理
- ✅ 默认分组操作

#### VariableReplacementTest

测试变量替换功能：

- ✅ 基础变量定义
- ✅ 变量引用语法
- ✅ 自动替换机制

#### TypeConversionTest

测试类型转换：

- ✅ int 类型转换
- ✅ bool 类型转换
- ✅ double 类型转换
- ✅ long 类型转换

#### ArrayTypeTest

测试数组类型：

- ✅ 逗号分隔解析
- ✅ 数组长度验证
- ✅ 元素包含检查

#### GroupManagementTest

测试分组管理：

- ✅ 多分组创建
- ✅ 分组键值对操作
- ✅ 分组查询
- ✅ 分组删除

### 2. PropsTest - Properties 文件支持测试

#### PropTest

测试 Properties 基本功能：

- ✅ 文件加载
- ✅ 键值对读取

#### TypeConversionTest

测试 Properties 类型转换：

- ✅ 各种类型的转换
- ✅ 默认值处理

#### CommentHandlingTest

测试注释处理：

- ✅ #注释识别
- ✅ 注释跳过

#### SeparatorTest

测试分隔符：

- ✅ =分隔符
- ✅ :分隔符
- ✅ 带空格的 separator

#### NullAndDefaultTest

测试空值和默认值：

- ✅ 空字符串处理
- ✅ null 值处理
- ✅ 默认值返回

### 3. YamlUtilTest - YAML 读写功能测试

#### LoadByPathTest

测试 YAML 加载功能：

- ✅ 文件路径加载
- ✅ 基本类型读取
- ✅ 嵌套结构访问

#### LoadTypedObjectTest

测试强类型加载：

- ✅ 反序列化为对象
- ✅ 属性映射

#### DumpTest

测试 YAML 序列化：

- ✅ 对象转 YAML
- ✅ 文件写出
- ✅ 内容验证

#### LoadFromStreamTest

测试流式加载：

- ✅ MemoryStream 读取
- ✅ UTF8 编码支持

#### ComplexYamlStructureTest

测试复杂结构：

- ✅ 列表解析
- ✅ 嵌套字典
- ✅ 混合结构

### 4. SettingUtilTest - Setting 工具类测试

#### GetTest

测试 Get 方法：

- ✅ 单例获取
- ✅ 缓存机制

#### GetFirstFoundTest

测试查找第一个存在的文件：

- ✅ 多路径查找
- ✅ 异常处理

#### CachingTest

测试缓存机制：

- ✅ 实例复用
- ✅ 引用相等性

### 5. PropsUtilTest - Props 工具类测试

#### GetTest

测试 Get 方法：

- ✅ 静态方法获取
- ✅ 文件加载

#### GetFirstFoundTest

测试查找功能：

- ✅ 优先级查找
- ✅ null 安全

## 🐛 故障排除

### 问题 1: 找不到测试数据文件

**错误**: `FileNotFoundException: Could not find file 'TestData/test.setting'`

**解决**:

```bash
# 确保在正确的目录运行
cd bin/Debug/net8.0
# 或者从项目根目录运行
cd d:\Work\WellTool\src\WellTool.Setting\WellTool.Setting.Tests
dotnet test
```

### 问题 2: 测试失败 - 变量替换不正确

**检查**:

- 确认 test.setting 文件中的变量语法正确
- 确认使用了 `isUseVariable=true` 参数

### 问题 3: YAML 测试失败

**检查**:

- 确认 YamlDotNet 包已安装
- 确认 YAML 文件格式正确

## 📊 预期结果

### 成功输出示例

```
Passed!  - Failed: 0, Passed: 21, Skipped: 0, Total: 21, Duration: 156 ms
```

### 测试结果分类

- ✅ **通过 (Passed)**: 所有 21 个测试应该全部通过
- ❌ **失败 (Failed)**: 不应该有失败的测试
- ⏭️ **跳过 (Skipped)**: 没有跳过的测试

## 🔍 代码覆盖率

如果需要查看代码覆盖率：

```bash
# 安装 coverlet
dotnet add package coverlet.collector

# 运行测试并生成覆盖率报告
dotnet test /p:CollectCoverage=true

# 生成 HTML 报告
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=html
```

## 📝 添加新测试

### 测试模板

```csharp
using WellTool.Setting;
using Xunit;

namespace WellTool.Setting.Tests;

public class NewFeatureTest
{
    [Fact]
    public void TestNewFeature()
    {
        // Arrange
        var setting = new Setting();
        
        // Act
        var result = setting.GetStr("key");
        
        // Assert
        Assert.Equal("expected", result);
    }
}
```

### 最佳实践

1. **命名规范**: `[MethodName]_[Scenario]_[ExpectedResult]`
2. **AAA 模式**: Arrange-Act-Assert
3. **独立性**: 每个测试应该独立运行
4. **可重复性**: 测试结果应该一致

## 🎯 测试维护

### 更新测试数据

如果需要修改测试数据文件：

1. 编辑 `TestData/` 目录下的文件
2. 确保修改不会影响现有测试
3. 必要时添加新的测试用例

### 废弃测试

如果某个功能被移除或重构：

1. 标记测试为 `[Obsolete]`
2. 或直接删除测试
3. 更新本文档

## 📖 参考资料

- [xUnit 官方文档](https://xunit.net/)
- [.NET 测试指南](https://learn.microsoft.com/zh-cn/dotnet/core/testing/)
- [Java 原版测试](../hutool-setting/src/test/java/cn/hutool/setting/)

---

**最后更新**: 2026 年 3 月 24 日  
**维护者**: WellTool Team
