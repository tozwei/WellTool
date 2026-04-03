using Xunit;

namespace WellTool.Http.Tests;

public class Issue3197Test
{
    [Fact(Skip = "Requires server")]
    public void GetTest()
    {
        // var s = HttpUtil.Get("http://localhost:8080/index");
        Assert.True(true);
    }
}
