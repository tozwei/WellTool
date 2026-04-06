using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class NumberUtilLastTest
{
    [Fact]
    public void AddTest()
    {
        Assert.Equal(7.37, NumberUtil.Add(3.15, 4.22), 0.01);
    }

    [Fact]
    public void SubTest()
    {
        Assert.Equal(1.07, NumberUtil.Sub(4.22, 3.15), 0.01);
    }

    [Fact]
    public void MulTest()
    {
        Assert.Equal(13.293, NumberUtil.Mul(3.15, 4.22), 0.001);
    }

    [Fact]
    public void DivTest()
    {
        Assert.Equal(2.0, NumberUtil.Div(10, 5), 0.001);
    }

    [Fact]
    public void IsIntegerTest()
    {
        Assert.True(NumberUtil.IsInteger("123"));
        Assert.True(NumberUtil.IsInteger("-123"));
        Assert.False(NumberUtil.IsInteger("123.45"));
    }

    [Fact]
    public void IsLongTest()
    {
        Assert.True(NumberUtil.IsLong("123"));
        Assert.False(NumberUtil.IsLong("123.45"));
    }

    [Fact]
    public void IsDoubleTest()
    {
        Assert.True(NumberUtil.IsDouble("123.45"));
        Assert.False(NumberUtil.IsDouble("abc"));
    }

    [Fact]
    public void MaxTest()
    {
        Assert.Equal(10, NumberUtil.Max(1, 5, 10, 3));
    }

    [Fact]
    public void MinTest()
    {
        Assert.Equal(1, NumberUtil.Min(1, 5, 10, 3));
    }

    [Fact]
    public void AverageTest()
    {
        Assert.Equal(5.0, NumberUtil.Average(3, 4, 5, 6, 7), 0.001);
    }

    [Fact]
    public void RoundTest()
    {
        Assert.Equal(3.14, NumberUtil.Round(3.14159, 2), 0.001);
        Assert.Equal(3.1, NumberUtil.Round(3.14, 1), 0.001);
    }

    [Fact]
    public void FloorTest()
    {
        Assert.Equal(3.0, NumberUtil.Floor(3.9), 0.001);
    }

    [Fact]
    public void CeilTest()
    {
        Assert.Equal(4.0, NumberUtil.Ceil(3.1), 0.001);
    }

    [Fact]
    public void AbsTest()
    {
        Assert.Equal(10, NumberUtil.Abs(-10));
    }

    [Fact]
    public void NegateTest()
    {
        Assert.Equal(-10, NumberUtil.Negate(10));
    }

    [Fact]
    public void IsDivisibleTest()
    {
        Assert.True(NumberUtil.IsDivisible(10, 5));
        Assert.False(NumberUtil.IsDivisible(10, 3));
    }

    [Fact]
    public void IsEvenTest()
    {
        Assert.True(NumberUtil.IsEven(10));
        Assert.False(NumberUtil.IsEven(11));
    }

    [Fact]
    public void IsOddTest()
    {
        Assert.True(NumberUtil.IsOdd(11));
        Assert.False(NumberUtil.IsOdd(10));
    }

    [Fact]
    public void ClampTest()
    {
        Assert.Equal(100, NumberUtil.Clamp(150, 0, 100));
        Assert.Equal(50, NumberUtil.Clamp(50, 0, 100));
    }

    [Fact]
    public void PercentOfTest()
    {
        Assert.Equal(50.0, NumberUtil.PercentOf(50, 100), 0.001);
    }

    [Fact]
    public void SqrtTest()
    {
        Assert.Equal(10.0, NumberUtil.Sqrt(100), 0.001);
    }

    [Fact]
    public void PowTest()
    {
        Assert.Equal(1000.0, NumberUtil.Pow(10, 3), 0.001);
    }

    [Fact]
    public void FactorialTest()
    {
        Assert.Equal(120, NumberUtil.Factorial(5));
    }

    [Fact]
    public void MedianTest()
    {
        Assert.Equal(5, NumberUtil.Median(1, 3, 5, 7, 9));
    }
}
