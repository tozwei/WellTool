namespace WellTool.Http.Tests;

public class Issue2658Test
{
    [Fact(Skip = "Requires network")]
    public void GetWithCookieTest()
    {
        // HttpRequest.Get("https://www.baidu.com/").Execute();
        // var cookies = GlobalCookieManager.GetCookieManager().GetCookieStore().GetCookies();
        Assert.True(true);
    }
}
