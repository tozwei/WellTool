using Xunit;
using System.Linq;

namespace WellTool.Core.Tests.Convert;

public class CastUtilTest
{
    [Fact]
    public void CastTest()
    {
        var obj = "Hello" as object;
        var str = WellTool.Core.Converter.CastUtil.Cast<string>(obj);
        Assert.Equal("Hello", str);
    }

    [Fact]
    public void CastToTest()
    {
        var obj = "123" as object;
        var result = WellTool.Core.Converter.CastUtil.CastTo<int>(obj);
        Assert.Equal(123, result);
    }

    [Fact]
    public void CastUpTest()
    {
        var collection = new System.Collections.ObjectModel.Collection<object> { "a", "b", "c" };
        var result = WellTool.Core.Converter.CastUtil.CastUp<string>(collection);
        Assert.Equal(3, result.Count);
        Assert.Equal("a", result.ElementAt(0));
    }

    [Fact]
    public void CastDownTest()
    {
        var collection = new System.Collections.ObjectModel.Collection<object> { "a", "b", "c" };
        var result = WellTool.Core.Converter.CastUtil.CastDown<object>(collection);
        Assert.Equal(3, result.Count);
    }
}