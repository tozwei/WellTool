namespace WellTool.Extra.Tests;

using WellTool.Extra.Servlet;

public class ServletUtilTest
{
    [Fact]
    public void GetClientIpTest()
    {
        var ip = ServletUtil.GetClientIp(null);
        Assert.NotNull(ip);
    }

    [Fact]
    public void IsIeTest()
    {
        Assert.False(ServletUtil.IsIe(null));
    }

    [Fact]
    public void GetHeaderTest()
    {
        var header = ServletUtil.GetHeader(null, "User-Agent");
        Assert.NotNull(header);
    }

    [Fact]
    public void GetParamsTest()
    {
        var params = ServletUtil.GetParams(null);
        Assert.NotNull(params);
    }

    [Fact]
    public void GetParamTest()
    {
        var param = ServletUtil.GetParam(null, "test");
        Assert.Null(param);
    }

    [Fact]
    public void GetIntTest()
    {
        var value = ServletUtil.GetInt(null, "page", 1);
        Assert.Equal(1, value);
    }

    [Fact]
    public void GetBoolTest()
    {
        var value = ServletUtil.GetBool(null, "debug", false);
        Assert.False(value);
    }

    [Fact]
    public void GetDoubleTest()
    {
        var value = ServletUtil.GetDouble(null, "lat", 0.0);
        Assert.Equal(0.0, value);
    }
}
