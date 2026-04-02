using Xunit;
using WellTool.Extra.Servlet;

namespace WellTool.Extra.Tests;

/// <summary>
/// ServletUtil 测试类
/// </summary>
public class ServletUtilTest
{
    [Fact]
    public void TestServletUtil_StaticClass_Exists()
    {
        // 测试静态类存在
        Assert.NotNull(typeof(ServletUtil));
    }
}
