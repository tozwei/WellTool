using WellTool.Socket;

namespace WellTool.Socket.Tests;

public class SocketTests
{
    [Fact]
    public void TestSocketConfig()
    {
        // Test that SocketConfig can be created
        var config = new SocketConfig();
        Assert.NotNull(config);
        Assert.True(config.ThreadPoolSize > 0);
    }

    [Fact]
    public void TestSocketUtil()
    {
        // Test that SocketUtil can be accessed
        Assert.Null(SocketUtil.GetRemoteAddress(null));
    }

    [Fact]
    public void TestChannelUtil()
    {
        // Test that ChannelUtil can be accessed
        ChannelUtil.SetDefaultPoolSize(4);
        // Just verify the method doesn't throw
    }
}
