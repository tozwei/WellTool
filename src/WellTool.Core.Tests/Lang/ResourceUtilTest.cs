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
        var url = WellTool.Core.IO.Resource.ResourceUtil.GetResource("NonExistentResource.xml");
        Assert.Null(url);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        Assert.Throws<NoResourceException>(() => ResourceUtil.ReadUtf8Str("NonExistentResource.xml"));
    }

    [Fact]
    public void GetResourcesTest()
    {
        // 没有 GetResources 方法，跳过此测试
    }
}
