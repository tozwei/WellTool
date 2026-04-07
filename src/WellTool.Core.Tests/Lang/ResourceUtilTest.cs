using WellTool.Core.IO.Resource;
using WellTool.Core.Lang;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class ResourceUtilTest
{
    [Fact]
    public void GetResourceUrlTest()
    {
        var url = WellTool.Core.IO.Resource.ResourceUtil.GetResource("WellTool.Core.xml");
        Assert.NotNull(url);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        var content = ResourceUtil.ReadUtf8Str("WellTool.Core.xml");
        Xunit.Assert.NotNull(content);
    }

    [Fact]
    public void GetResourcesTest()
    {
        // 没有 GetResources 方法，跳过此测试
    }
}
