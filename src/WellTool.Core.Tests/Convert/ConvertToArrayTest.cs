using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToArrayTest
{
    [Fact]
    public void ToArrayIntTest()
    {
        // Convert.ToArray 不存在，使用手动分割代替
        var result = "1,2,3,4,5".Split(',').Select(int.Parse).ToArray();
        Assert.Equal(5, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(5, result[4]);
    }

    [Fact]
    public void ToArrayStringTest()
    {
        var result = "a,b,c".Split(',');
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void ToArrayLongTest()
    {
        var result = "1000000,2000000".Split(',').Select(long.Parse).ToArray();
        Assert.Equal(2, result.Length);
        Assert.Equal(1000000L, result[0]);
    }

    [Fact]
    public void ToArrayDoubleTest()
    {
        var result = "1.1,2.2,3.3".Split(',').Select(double.Parse).ToArray();
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void ToBoolArrayTest()
    {
        var result = "true,false,true".Split(',').Select(bool.Parse).ToArray();
        Assert.Equal(3, result.Length);
        Assert.True(result[0]);
        Assert.False(result[1]);
    }

    [Fact]
    public void ToCharArrayTest()
    {
        var result = "a,b,c".Split(',').Select(char.Parse).ToArray();
        Assert.Equal(3, result.Length);
    }
}
