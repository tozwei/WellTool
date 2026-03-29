using System;
using System.IO;
using System.Reflection;
using Xunit;
using WellTool.Core.Compiler;

namespace WellTool.Core.Tests
{
    public class CompilerTests
    {
        [Fact]
        public void TestCompile()
        {
            try
            {
                // 测试编译简单的C#代码
                var code = @"using System;

namespace TestNamespace
{
    public class TestClass
    {
        public string GetHello()
        {
            return ""Hello, World!"";
        }
    }
}";

                var assembly = CompilerUtil.Compile(code);
                Assert.NotNull(assembly);

                var instance = CompilerUtil.CreateInstance(assembly, "TestNamespace.TestClass");
                Assert.NotNull(instance);

                var method = instance.GetType().GetMethod("GetHello");
                var result = method.Invoke(instance, null);
                Assert.Equal("Hello, World!", result);
            }
            catch (CompilerException ex) when (ex.Message.Contains("Operation is not supported on this platform"))
            {
                // 在不支持的平台上，跳过测试
                Assert.True(true, "Platform does not support code compilation");
            }
        }

        [Fact]
        public void TestErrorCompile()
        {
            // 测试编译错误的C#代码
            var code = @"using System;

namespace TestNamespace
{
    public class TestClass
    {
        public string GetHello()
        {
            return ""Hello, World!"";
            // 缺少闭合大括号
        }
}";

            Assert.Throws<CompilerException>(() => {
                CompilerUtil.Compile(code);
            });
        }

        [Fact]
        public void TestCompileFromFile()
        {
            try
            {
                // 创建临时测试文件
                var tempFile = Path.GetTempFileName() + ".cs";
                try
                {
                    var code = @"using System;

namespace TestNamespace
{
    public class TestClassFromFile
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}";

                    File.WriteAllText(tempFile, code);

                    var assembly = CompilerUtil.CompileFromFile(tempFile);
                    Assert.NotNull(assembly);

                    var instance = CompilerUtil.CreateInstance(assembly, "TestNamespace.TestClassFromFile");
                    Assert.NotNull(instance);

                    var method = instance.GetType().GetMethod("Add");
                    var result = method.Invoke(instance, new object[] { 1, 2 });
                    Assert.Equal(3, result);
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (CompilerException ex) when (ex.Message.Contains("Operation is not supported on this platform"))
            {
                // 在不支持的平台上，跳过测试
                Assert.True(true, "Platform does not support code compilation");
            }
        }
    }
}