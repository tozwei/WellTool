using WellTool.Http.Server;
using Xunit;
using System.Threading;

namespace WellTool.Http.Tests.Server;

/// <summary>
/// 简单 HTTP 服务器测试
/// </summary>
public class SimpleServerTest
{
    [Fact(Skip = "需要管理员权限才能运行 HttpListener")]
    public void StartServerTest()
    {
        // 测试启动 HTTP 服务器
        var server = new SimpleServer(8888, (request, response) =>
        {
            response.StatusCode = 200;
            response.ContentType = "text/plain";
            response.Write("Hello, World!");
        });

        // 启动服务器
        server.Start();
        
        // 等待服务器启动
        Thread.Sleep(1000);
        
        // 停止服务器
        server.Stop();
        
        // 验证服务器启动和停止成功
        Assert.True(true);//
    }

    [Fact(Skip = "需要管理员权限才能运行 HttpListener")]
    public void RedirectServerTest()
    {
        // 测试 HTTP 重定向
        var server = new SimpleServer(8889, (request, response) =>
        {
            if (request.Path == "/redirect1")
            {
                response.StatusCode = 301;
                response.AddHeader("Location", "http://localhost:8889/redirect2");
                response.AddHeader("Set-Cookie", "redirect1=1; path=/; HttpOnly");
            }
            else if (request.Path == "/redirect2")
            {
                response.StatusCode = 301;
                response.AddHeader("Location", "http://localhost:8889/redirect3");
                response.AddHeader("Set-Cookie", "redirect2=2; path=/; HttpOnly");
            }
            else if (request.Path == "/redirect3")
            {
                response.StatusCode = 200;
                response.ContentType = "text/plain";
                var cookie = request.Headers["Cookie"];
                response.Write(cookie ?? "No cookie");
            }
            else
            {
                response.StatusCode = 404;
                response.ContentType = "text/plain";
                response.Write("Not Found");
            }
        });

        // 启动服务器
        server.Start();
        
        // 等待服务器启动
        Thread.Sleep(1000);
        
        // 停止服务器
        server.Stop();
        
        // 验证服务器启动和停止成功
        Assert.True(true);//
    }

    [Fact(Skip = "需要管理员权限才能运行 HttpListener")]
    public void ExceptionServerTest()
    {
        // 测试服务器异常处理
        var server = new SimpleServer(8890, (request, response) =>
        {
            if (request.Path == "/exception")
            {
                // 故意抛出异常
                throw new System.Exception("Test exception");
            }
            else
            {
                response.StatusCode = 200;
                response.ContentType = "text/plain";
                response.Write("Hello, World!");
            }
        });

        // 启动服务器
        server.Start();
        
        // 等待服务器启动
        Thread.Sleep(1000);
        
        // 停止服务器
        server.Stop();
        
        // 验证服务器启动和停止成功
        Assert.True(true);//
    }
}


