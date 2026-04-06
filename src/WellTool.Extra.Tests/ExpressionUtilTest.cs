namespace WellTool.Extra.Tests;

using WellTool.Extra.Expression;

public class ExpressionUtilTest
{
    [Fact]
    public void EvalTest()
    {
        var result = ExpressionUtil.Eval("1 + 2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void EvalStringTest()
    {
        var result = ExpressionUtil.Eval("'hello' + ' world'");
        Assert.Equal("hello world", result);
    }

    [Fact]
    public void EvalBooleanTest()
    {
        var result = ExpressionUtil.Eval("true && false");
        Assert.False((bool)result);
    }

    [Fact]
    public void EvalNullTest()
    {
        var result = ExpressionUtil.Eval(null);
        Assert.Null(result);
    }

    [Fact]
    public void EvalEmptyTest()
    {
        var result = ExpressionUtil.Eval("");
        Assert.Equal("", result);
    }

    [Fact]
    public void EvalComplexTest()
    {
        var result = ExpressionUtil.Eval("1 + 2 * 3");
        Assert.Equal(7, result);
    }

    [Fact]
    public void EvalWithVariablesTest()
    {
        var result = ExpressionUtil.Eval("a + b", new Dictionary<string, object> { { "a", 1 }, { "b", 2 } });
        Assert.Equal(3, result);
    }

    [Fact]
    public void EvalDivisionTest()
    {
        var result = ExpressionUtil.Eval("10 / 2");
        Assert.Equal(5, result);
    }

    [Fact]
    public void EvalSubtractionTest()
    {
        var result = ExpressionUtil.Eval("5 - 3");
        Assert.Equal(2, result);
    }
}
