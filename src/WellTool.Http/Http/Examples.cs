using WellTool.Http;

namespace WellTool.Http.Examples;

/// <summary>
/// 使用示例
/// </summary>
public class Examples
{
    /// <summary>
    /// GET 请求示例
    /// </summary>
    public async Task GetExample()
    {
        // 简单 GET 请求
        var response = await HttpRequest.Get("https://api.example.com/data")
            .Timeout(5000)
            .ExecuteAsync();

        if (response.IsOk())
        {
            var content = response.Body();
            Console.WriteLine(content);
        }

        response.Dispose();
    }

    /// <summary>
    /// POST 请求示例（表单数据）
    /// </summary>
    public async Task PostFormExample()
    {
        var formData = new Dictionary<string, object?>
        {
            { "username", "test" },
            { "password", "123456" }
        };

        var response = await HttpRequest.Post("https://api.example.com/login")
            .Form(formData)
            .Timeout(10000)
            .ExecuteAsync();

        if (response.IsOk())
        {
            var result = response.Body();
            Console.WriteLine(result);
        }

        response.Dispose();
    }

    /// <summary>
    /// POST 请求示例（JSON）
    /// </summary>
    public async Task PostJsonExample()
    {
        var json = "{\"name\":\"test\",\"value\":123}";

        var response = await HttpRequest.Post("https://api.example.com/api/data")
            .Body(json, "application/json")
            .Timeout(10000)
            .ExecuteAsync();

        if (response.IsOk())
        {
            var result = response.Body();
            Console.WriteLine(result);
        }

        response.Dispose();
    }

    /// <summary>
    /// 带认证的请求示例
    /// </summary>
    public async Task AuthExample()
    {
        var response = await HttpRequest.Get("https://api.example.com/protected")
            .BasicAuth("username", "password")
            .BearerAuth("your-jwt-token")
            .ExecuteAsync();

        if (response.IsOk())
        {
            var content = response.Body();
            Console.WriteLine(content);
        }

        response.Dispose();
    }

    /// <summary>
    /// 下载文件示例
    /// </summary>
    public void DownloadFileExample()
    {
        // 下载文件
        HttpUtil.DownloadFile("https://example.com/file.zip", "C:\\Downloads\\file.zip");
    }

    /// <summary>
    /// 使用工具类发送请求
    /// </summary>
    public void HttpUtilExample()
    {
        // 简单 GET
        var result = HttpUtil.Get("https://api.example.com/data");
        Console.WriteLine(result);

        // GET 带参数
        var paramMap = new Dictionary<string, object?>
        {
            { "page", "1" },
            { "size", "10" }
        };
        var result2 = HttpUtil.Get("https://api.example.com/list", paramMap);
        Console.WriteLine(result2);

        // POST 表单
        var postData = new Dictionary<string, object?>
        {
            { "key", "value" }
        };
        var result3 = HttpUtil.Post("https://api.example.com/submit", postData);
        Console.WriteLine(result3);
    }

    /// <summary>
    /// 自定义配置示例
    /// </summary>
    public async Task CustomConfigExample()
    {
        var config = HttpConfig.Create()
            .Timeout(30000)
            .SetMaxRedirectCount(5)
            .SetHttpProxy("proxy.example.com", 8080);

        var response = await HttpRequest.Get("https://api.example.com/data")
            .SetConfig(config)
            .DisableCache()
            .ExecuteAsync();

        if (response.IsOk())
        {
            var content = response.Body();
            Console.WriteLine(content);
        }

        response.Dispose();
    }
}
