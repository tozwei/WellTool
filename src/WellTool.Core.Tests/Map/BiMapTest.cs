using WellTool.Core.Map;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class BiMapTest
{
    [Fact]
    public void GetTest()
    {
        var biMap = new BiMap<string, int>();
        biMap.Add("aaa", 111);
        biMap.Add("bbb", 222);

        Assert.Equal(111, biMap["aaa"]);
        Assert.Equal(222, biMap["bbb"]);

        Assert.Equal("aaa", biMap.GetKey(111));
        Assert.Equal("bbb", biMap.GetKey(222));
    }

    [Fact]
    public void InverseTest()
    {
        var biMap = new BiMap<string, int>();
        biMap.Add("aaa", 111);
        biMap.Add("bbb", 222);

        // 手动创建反向映射
        var inverse = new BiMap<int, string>();
        inverse.Add(111, "aaa");
        inverse.Add(222, "bbb");
        
        Assert.Equal("aaa", inverse[111]);
        Assert.Equal("bbb", inverse[222]);
    }
}
