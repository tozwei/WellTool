using WellTool.Core.Lang.Caller;
using Xunit;

namespace WellTool.Core.Tests;

public class CallerTest
{
    [Fact]
    public void GetCallerTest()
    {
        var caller = CallerUtil.GetCaller();
        Assert.Equal(this.GetType(), caller);

        var caller0 = CallerUtil.GetCaller(0);
        Assert.Equal(typeof(CallerUtil), caller0);

        var caller1 = CallerUtil.GetCaller(1);
        Assert.Equal(this.GetType(), caller1);
    }

    [Fact]
    public void GetCallerCallerTest()
    {
        var callerCaller = CallerTestClass.GetCaller();
        Assert.Equal(this.GetType(), callerCaller);
    }

    private static class CallerTestClass
    {
        public static Type GetCaller()
        {
            return CallerUtil.GetCallerCaller();
        }
    }
}
