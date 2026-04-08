namespace WellTool.Extra.Tests;

using WellTool.Extra.Ssh;

public class JschUtilTest
{
    [Fact]
    public void GenerateLocalPortTest()
    {
        var port1 = JschUtil.GenerateLocalPort();
        var port2 = JschUtil.GenerateLocalPort();
        
        Assert.True(port2 > port1);
    }

    [Fact]
    public void SshNoneConstantTest()
    {
        Assert.Equal("none", JschUtil.SshNone);
    }

    [Fact]
    public void GetSessionWithPasswordTest()
    {
        // 测试创建会话（不实际连接）
        var session = JschUtil.GetSession("localhost", 22, "user", "password");
        
        Assert.NotNull(session);
        Assert.Equal("localhost", session.Host);
        Assert.Equal(22, session.Port);
        Assert.Equal("user", session.User);
        Assert.False(session.IsConnected);
    }

    [Fact]
    public void SshSessionConnectTest()
    {
        var session = JschUtil.GetSession("localhost", 22, "user", "password");
        
        // 测试连接方法
        session.Connect();
        Assert.True(session.IsConnected);
        
        // 测试断开
        session.Disconnect();
        Assert.False(session.IsConnected);
    }

    [Fact]
    public void SshSessionCreateSftpTest()
    {
        var session = JschUtil.GetSession("localhost", 22, "user", "password");
        
        var sftp = session.CreateSftp();
        Assert.NotNull(sftp);
    }

    [Fact]
    public void SshSessionExecCommandTest()
    {
        var session = JschUtil.GetSession("localhost", 22, "user", "password");
        
        // 不连接直接执行命令会自动连接
        var result = session.ExecCommand("ls -la");
        
        Assert.NotNull(result);
    }

    [Fact]
    public void SshSessionDisposeTest()
    {
        var session = JschUtil.GetSession("localhost", 22, "user", "password");
        session.Connect();
        
        // 测试Dispose
        session.Dispose();
        
        Assert.False(session.IsConnected);
    }
}
