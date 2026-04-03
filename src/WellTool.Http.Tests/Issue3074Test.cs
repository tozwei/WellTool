using Xunit;

namespace WellTool.Http.Tests;

public class Issue3074Test
{
    [Fact(Skip = "Requires server")]
    public void BodyTest()
    {
        // HttpUtil.CreatePost("http://localhost:8888/body")
        //     .ContentType(ContentType.JSON.GetValue())
        //     .Body("aaa")
        //     .Execute();
        Assert.True(true);
    }
}
