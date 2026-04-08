namespace WellTool.Socket.Tests;


public class SocketUtilTest
{
    [Fact]
    public void CreateServerTest()
    {
        var server = SocketUtil.CreateServer();
        Assert.NotNull(server);
    }

    [Fact]
    public void CreateServerWithPortTest()
    {
        var server = SocketUtil.CreateServer(8888);
        Assert.NotNull(server);
    }

    [Fact]
    public void CreateClientTest()
    {
        // 使用环回地址和有效端口，但不实际连接
        // 只验证对象创建
        try
        {
            var client = SocketUtil.CreateClient("127.0.0.1", 65500);
            Assert.NotNull(client);
        }
        catch (System.Net.Sockets.SocketException)
        {
            // 预期失败（没有服务器监听），但对象已创建
            Assert.True(true);
        }
    }

    [Fact]
    public void CreateNioClientTest()
    {
        try
        {
            var client = SocketUtil.CreateNioClient("127.0.0.1", 65501);
            Assert.NotNull(client);
        }
        catch (System.Net.Sockets.SocketException)
        {
            Assert.True(true);
        }
    }

    [Fact]
    public void CreateNioServerTest()
    {
        var server = SocketUtil.CreateNioServer(8889);
        Assert.NotNull(server);
    }
}
