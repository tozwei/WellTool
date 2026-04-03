using WellTool.Script;
using Xunit;

namespace WellTool.Script.Tests;

public class ScriptUtilTests
{
    [Fact]
    public void TestExecuteJavaScript()
    {
        // 测试执行简单的 JavaScript 代码
        var result = ScriptUtil.Instance.ExecuteJavaScript("1 + 1");
        Assert.Equal(2, result);
    }

    [Fact]
    public void TestExecuteJavaScriptWithType()
    {
        // 测试执行 JavaScript 代码并返回指定类型的结果
        var result = ScriptUtil.Instance.ExecuteJavaScript<int>("2 + 3");
        Assert.Equal(5, result);
    }

    [Fact]
    public void TestCreateJavaScriptEngine()
    {
        // 测试创建 JavaScript 引擎
        var engine = ScriptUtil.Instance.CreateJavaScriptEngine();
        Assert.NotNull(engine);
    }

    [Fact]
    public void TestCreateFullSupportScriptEngine()
    {
        // 测试创建全功能脚本引擎
        var engine = ScriptUtil.Instance.CreateFullSupportScriptEngine();
        Assert.NotNull(engine);
    }

    [Fact]
    public void TestCreateFullSupportScriptEngineWithName()
    {
        // 测试根据脚本名创建全功能脚本引擎
        var engine = ScriptUtil.Instance.CreateFullSupportScriptEngine("javascript");
        Assert.NotNull(engine);
    }

    [Fact]
    public void TestJavaScriptEngineExecute()
    {
        // 测试 JavaScript 引擎的执行方法
        var engine = new JavaScriptEngine();
        var result = engine.Execute("3 * 4");
        Assert.Equal(12, result);
    }

    [Fact]
    public void TestJavaScriptEngineExecuteWithType()
    {
        // 测试 JavaScript 引擎的泛型执行方法
        var engine = new JavaScriptEngine();
        var result = engine.Execute<string>("'Hello, ' + 'WellTool'");
        Assert.Equal("Hello, WellTool", result);
    }
}

