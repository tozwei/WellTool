namespace WellTool.Extra.Tests;

/// <summary>
/// ExpressionUtil 测试类
/// </summary>
public class ExpressionUtilTest
{
    private readonly ExpressionUtil _expressionUtil;

    public ExpressionUtilTest()
    {
        _expressionUtil = new ExpressionUtil();
    }

    [Fact]
    public void TestEvaluate_SimpleAddition_ReturnsCorrectResult()
    {
        // 测试简单加法表达式
        var result = _expressionUtil.Evaluate("1 + 2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void TestEvaluate_SimpleSubtraction_ReturnsCorrectResult()
    {
        // 测试简单减法表达式
        var result = _expressionUtil.Evaluate("5 - 3");
        Assert.Equal(2, result);
    }

    [Fact]
    public void TestEvaluate_SimpleMultiplication_ReturnsCorrectResult()
    {
        // 测试简单乘法表达式
        var result = _expressionUtil.Evaluate("2 * 3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void TestEvaluate_SimpleDivision_ReturnsCorrectResult()
    {
        // 测试简单除法表达式
        var result = _expressionUtil.Evaluate("6 / 2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void TestEvaluate_ComplexExpression_ReturnsCorrectResult()
    {
        // 测试复杂表达式
        var result = _expressionUtil.Evaluate("(2 + 3) * 4");
        Assert.Equal(20, result);
    }

    [Fact]
    public void TestEvaluate_WithParameters_ReturnsCorrectResult()
    {
        // 测试带参数的表达式
        var parameters = new Dictionary<string, object>
        {
            { "a", 5 },
            { "b", 3 }
        };
        var result = _expressionUtil.Evaluate("a + b", parameters);
        Assert.Equal(8, result);
    }

    [Fact]
    public void TestEvaluate_WithParameters_Subtraction_ReturnsCorrectResult()
    {
        // 测试带参数的减法
        var parameters = new Dictionary<string, object>
        {
            { "x", 10 },
            { "y", 4 }
        };
        var result = _expressionUtil.Evaluate("x - y", parameters);
        Assert.Equal(6, result);
    }

    [Fact]
    public void TestEvaluate_WithParameters_Multiplication_ReturnsCorrectResult()
    {
        // 测试带参数的乘法
        var parameters = new Dictionary<string, object>
        {
            { "m", 7 },
            { "n", 8 }
        };
        var result = _expressionUtil.Evaluate("m * n", parameters);
        Assert.Equal(56, result);
    }

    [Fact]
    public void TestEvaluate_WithParameters_Division_ReturnsCorrectResult()
    {
        // 测试带参数的除法
        var parameters = new Dictionary<string, object>
        {
            { "p", 20 },
            { "q", 4 }
        };
        var result = _expressionUtil.Evaluate("p / q", parameters);
        Assert.Equal(5, result);
    }

    [Fact]
    public void TestEvaluate_WithDecimal_ReturnsCorrectResult()
    {
        // 测试小数表达式
        var result = _expressionUtil.Evaluate("10.5 + 2.5");
        Assert.Equal(13.0, result);
    }

    [Fact]
    public void TestEvaluate_WithNegativeNumber_ReturnsCorrectResult()
    {
        // 测试负数表达式
        var result = _expressionUtil.Evaluate("-5 + 3");
        Assert.Equal(-2, result);
    }

    [Fact]
    public void TestEvaluate_T_GenericMethod_ReturnsCorrectType()
    {
        // 测试泛型方法
        var result = _expressionUtil.Evaluate<int>("2 + 3");
        Assert.Equal(5, result);
    }

    [Fact]
    public void TestEvaluate_T_WithParameters_ReturnsCorrectResult()
    {
        // 测试带参数的泛型方法
        var parameters = new Dictionary<string, object>
        {
            { "a", 10 },
            { "b", 20 }
        };
        var result = _expressionUtil.Evaluate<int>("a + b", parameters);
        Assert.Equal(30, result);
    }

    [Fact]
    public void TestEvaluate_WithParentheses_ReturnsCorrectResult()
    {
        // 测试括号表达式
        var result = _expressionUtil.Evaluate("(1 + 2) * (3 + 4)");
        Assert.Equal(21, result);
    }

    [Fact]
    public void TestEvaluate_OperatorPrecedence_ReturnsCorrectResult()
    {
        // 测试运算符优先级
        var result = _expressionUtil.Evaluate("2 + 3 * 4");
        Assert.Equal(14, result);
    }

    [Fact]
    public void TestEvaluate_Modulo_ReturnsCorrectResult()
    {
        // 测试取模运算
        var result = _expressionUtil.Evaluate("10 % 3");
        Assert.Equal(1, result);
    }

    [Fact]
    public void TestEvaluate_EmptyExpression_ThrowsException()
    {
        // 测试空表达式
        Assert.Throws<ExpressionException>(() => _expressionUtil.Evaluate(""));
    }

    [Fact]
    public void TestEvaluate_InvalidExpression_ThrowsException()
    {
        // 测试无效表达式
        Assert.Throws<ExpressionException>(() => _expressionUtil.Evaluate("abc + def"));
    }
}
