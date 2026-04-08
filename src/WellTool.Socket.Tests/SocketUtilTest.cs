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
        // 只验证CreateClient方法可以接受参数并返回非空对象
        // 不实际建立连接（因为没有服务器监听）
        var client = SocketUtil.CreateClient("127.0.0.1", 0);
        Assert.NotNull(client);
    }

    [Fact]
    public void CreateNioClientTest()
    {
        // 只验证CreateNioClient方法可以接受参数并返回非空对象
        var client = SocketUtil.CreateNioClient("127.0.0.1", 0);
        Assert.NotNull(client);
    }

    [Fact]
    public void CreateNioServerTest()
    {
        var server = SocketUtil.CreateNioServer(8889);
        Assert.NotNull(server);
    }
}
