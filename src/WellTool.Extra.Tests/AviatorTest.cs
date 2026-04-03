namespace WellTool.Extra.Tests;

using Well.Extra.Expression;

public class AviatorTest
{
    [Fact]
    public void ExecTest()
    {
        var result = AviatorUtil.Exec("1 + 2");
        Assert.Equal(3L, result);
    }

    [Fact]
    public void ExecWithEnvTest()
    {
        var env = new Dictionary<string, object> { { "a", 1 }, { "b", 2 } };
        var result = AviatorUtil.Exec("a + b", env);
        Assert.Equal(3L, result);
    }

    [Fact]
    public void CompileTest()
    {
        var compiled = AviatorUtil.Compile("1 + 2");
        Assert.NotNull(compiled);
    }

    [Fact]
    public void ExecuteTest()
    {
        var compiled = AviatorUtil.Compile("1 + 2");
        var result = AviatorUtil.Execute(compiled);
        Assert.Equal(3L, result);
    }

    [Fact]
    public void ExecStringTest()
    {
        var result = AviatorUtil.Exec("'hello'");
        Assert.Equal("hello", result);
    }

    [Fact]
    public void ExecBooleanTest()
    {
        var result = AviatorUtil.Exec("true && false");
        Assert.False((bool)result);
    }

    [Fact]
    public void ExecNullTest()
    {
        var result = AviatorUtil.Exec("nil");
        Assert.Null(result);
    }

    [Fact]
    public void ExecArrayTest()
    {
        var result = AviatorUtil.Exec("['a', 'b', 'c']");
        Assert.NotNull(result);
    }

    [Fact]
    public void ExecMapTest()
    {
        var result = AviatorUtil.Exec("{'a': 1, 'b': 2}");
        Assert.NotNull(result);
    }

    [Fact]
    public void ExecFunctionTest()
    {
        var result = AviatorUtil.Exec("string.length('hello')");
        Assert.Equal(5L, result);
    }
}
