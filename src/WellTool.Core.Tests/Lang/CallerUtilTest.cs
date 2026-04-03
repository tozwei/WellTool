using WellTool.Core.Lang.Caller;
using Xunit;

namespace WellTool.Core.Tests;

public class CallerUtilTest
{
    [Fact]
    public void GetCallerMethodNameTest()
    {
        var callerMethodName = CallerUtil.GetCallerMethodName(false);
        Assert.Equal("GetCallerMethodNameTest", callerMethodName);

        var fullCallerMethodName = CallerUtil.GetCallerMethodName(true);
        Assert.Contains("CallerUtilTest.GetCallerMethodNameTest", fullCallerMethodName);
    }
}
