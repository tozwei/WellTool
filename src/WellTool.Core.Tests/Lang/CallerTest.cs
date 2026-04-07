using System.Diagnostics;
using WellTool.Core.Lang.Caller;
using Xunit;

public class CallerTest
{
    [Fact]
    public void GetCallerTest()
    {
        var callerType = CallerUtil.GetCaller();
        Assert.NotNull(callerType);
        Assert.Equal(typeof(CallerTest), callerType);
    }

    [Fact]
    public void GetCallerDepthTest()
    {
        var callerType = CallerUtil.GetCaller(1);
        Assert.NotNull(callerType);
    }
}
