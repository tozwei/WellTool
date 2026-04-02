using Xunit;
using WellTool.Extra.Compress;

namespace WellTool.Extra.Tests;

/// <summary>
/// CompressUtil 测试类
/// </summary>
public class CompressUtilTest
{
    [Fact]
    public void TestCompressUtil_Instance_NotNull()
    {
        // 测试实例不为空
        Assert.NotNull(CompressUtil.Instance);
    }
}
