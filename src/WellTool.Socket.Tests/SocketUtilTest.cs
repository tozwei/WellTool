namespace WellTool.Socket.Tests;

using Well.Socket;

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
        var client = SocketUtil.CreateClient("localhost", 8888);
        Assert.NotNull(client);
    }

    [Fact]
    public void CreateNioClientTest()
    {
        var client = SocketUtil.CreateNioClient("localhost", 8888);
        Assert.NotNull(client);
    }

    [Fact]
    public void CreateNioServerTest()
    {
        var server = SocketUtil.CreateNioServer(8889);
        Assert.NotNull(server);
    }
}
