using WellTool.Core.Lang.Caller;
using Xunit;

namespace WellTool.Core.Tests;

public class CallerUtilLastTest
{
    [Fact]
    public void GetCallerTest()
    {
        var caller = CallerUtil.GetCaller();
        Assert.NotNull(caller);
    }

    [Fact]
    public void GetCallerLevelTest()
    {
        var caller = CallerUtil.GetCaller(0);
        Assert.NotNull(caller);
    }
}
