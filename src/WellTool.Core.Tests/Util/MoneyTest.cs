using WellTool.Core.Math;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class MoneyTest
{
    [Fact]
    public void ConstructorTest()
    {
        var money = new Money(100.50m);
        Assert.Equal(100.50m, money.Amount);
    }

    [Fact]
    public void AddTest()
    {
        var money1 = new Money(100);
        var money2 = new Money(50);
        var result = money1.Add(money2);
        Assert.Equal(150m, result.Amount);
    }

    [Fact]
    public void SubtractTest()
    {
        var money1 = new Money(100);
        var money2 = new Money(30);
        var result = money1.Subtract(money2);
        Assert.Equal(70m, result.Amount);
    }

    [Fact]
    public void MultiplyTest()
    {
        var money = new Money(100);
        var result = money.Multiply(2);
        Assert.Equal(200m, result.Amount);
    }

    [Fact]
    public void DivideTest()
    {
        var money = new Money(100);
        var result = money.Divide(4);
        Assert.Equal(25m, result.Amount);
    }

    [Fact]
    public void CompareToTest()
    {
        var money1 = new Money(100);
        var money2 = new Money(200);
        Assert.True(money1.CompareTo(money2) < 0);
        Assert.True(money2.CompareTo(money1) > 0);
        Assert.True(money1.CompareTo(money1) == 0);
    }

    [Fact]
    public void GreaterThanTest()
    {
        var money1 = new Money(200);
        var money2 = new Money(100);
        Assert.True(money1 > money2);
    }

    [Fact]
    public void LessThanTest()
    {
        var money1 = new Money(100);
        var money2 = new Money(200);
        Assert.True(money1 < money2);
    }

    [Fact]
    public void ToStringTest()
    {
        var money = new Money(100.50m);
        Assert.Contains("100", money.ToString());
    }
}
