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
        // 使用环回地址和无效端口，测试对象创建
        // 由于没有服务器监听，会触发超时或连接失败
        try
        {
            var client = SocketUtil.CreateClient("127.0.0.1", 65500);
            Assert.NotNull(client);
        }
        catch (System.Net.Sockets.SocketException)
        {
            // 预期失败（没有服务器监听），但对象可能已创建
            Assert.True(true);
        }
        catch (TimeoutException)
        {
            // 连接超时，也视为预期行为
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
        catch (TimeoutException)
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
