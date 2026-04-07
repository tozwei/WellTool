using WellTool.Core.Lang.Caller;
using Xunit;

namespace WellTool.Core.Tests;

public class CallerUtilTest
{
    [Fact]
    public void GetCallerTest()
    {
        var caller = CallerUtil.GetCaller();
        Assert.Equal(typeof(CallerUtilTest), caller);

        var callerWithDepth = CallerUtil.GetCaller(2);
        Assert.Equal(typeof(CallerUtilTest), callerWithDepth);
    }
}
