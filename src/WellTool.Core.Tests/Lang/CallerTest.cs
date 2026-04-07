using System.Diagnostics;
using WellTool.Core.Lang.Caller;
using Xunit;

namespace WellTool.Core.Lang.Caller.Tests;


public class CallerTest
{
    [Fact]
    public void GetCallerTest()
    {
        var callerType = CallerUtil.GetCaller();
        Xunit.Assert.NotNull(callerType);
        Xunit.Assert.Equal(typeof(CallerTest), callerType);
    }

    [Fact]
    public void GetCallerDepthTest()
    {
        var callerType = CallerUtil.GetCaller(1);
        Xunit.Assert.NotNull(callerType);
    }
}
