using WellTool.Http;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WellTool.Http.Tests;

/// <summary>
/// HTTPS 测试类
/// </summary>
public class HttpsTest
{
    [Fact]
    public async Task GetTest()
    {
        // 测试 HTTPS 连接的线程安全性
        var tasks = new List<Task>();
        var count = 0;
        var lockObj = new object();

        for (int i = 0; i < 10; i++) // 减少线程数量以加快测试速度
        {
            tasks.Add(Task.Run(() =>
            {
                // 访问 HTTPS 网站
                var result = HttpUtil.Get("https://www.baidu.com/");
                
                // 增加计数
                lock (lockObj)
                {
                    count++;
                }
                
                // 验证返回结果不为空
                Assert.False(string.IsNullOrEmpty(result));
            }));
        }

        // 等待所有任务完成
        await Task.WhenAll(tasks);
        
        // 验证所有线程都执行完成
        Assert.Equal(10, count);
    }

    [Fact]
    public void HttpsRequestTest()
    {
        // 测试 HTTPS 请求
        var request = HttpRequest.Get("https://www.baidu.com/");
        var response = request.Execute();
        
        // 验证响应状态码
        Assert.Equal(200, response.Status);
        
        // 验证响应内容不为空
        Assert.False(string.IsNullOrEmpty(response.Body()));
    }
}
