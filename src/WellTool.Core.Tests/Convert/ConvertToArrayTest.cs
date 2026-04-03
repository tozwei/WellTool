using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToArrayTest
{
    [Fact]
    public void ToArrayIntTest()
    {
        var result = Convert.ToArray<int>("1,2,3,4,5");
        Assert.Equal(5, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(5, result[4]);
    }

    [Fact]
    public void ToArrayStringTest()
    {
        var result = Convert.ToArray<string>("a,b,c");
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void ToArrayLongTest()
    {
        var result = Convert.ToArray<long>("1000000,2000000");
        Assert.Equal(2, result.Length);
        Assert.Equal(1000000L, result[0]);
    }

    [Fact]
    public void ToArrayDoubleTest()
    {
        var result = Convert.ToArray<double>("1.1,2.2,3.3");
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void ToBoolArrayTest()
    {
        var result = Convert.ToArray<bool>("true,false,true");
        Assert.Equal(3, result.Length);
        Assert.True(result[0]);
        Assert.False(result[1]);
    }

    [Fact]
    public void ToCharArrayTest()
    {
        var result = Convert.ToArray<char>("a,b,c");
        Assert.Equal(3, result.Length);
    }
}
