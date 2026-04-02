using System;
using WellTool.Extra.Ssh;

namespace WellTool.Extra.Tests;

/// <summary>
/// Connector 连接器测试类
/// </summary>
public class ConnectorTest
{
    /// <summary>
    /// 测试无参构造
    /// </summary>
    [Fact]
    public void TestConstructor_NoParams()
    {
        var connector = new Connector();
        Assert.NotNull(connector);
    }

    /// <summary>
    /// 测试基础构造 - 用户密码组
    /// </summary>
    [Fact]
    public void TestConstructor_UserPasswordGroup()
    {
        var connector = new Connector("admin", "password123", "wheel");
        
        Assert.Equal("admin", connector.User);
        Assert.Equal("password123", connector.Password);
        Assert.Equal("wheel", connector.Group);
        Assert.Null(connector.Host);
        Assert.Equal(0, connector.Port);
    }

    /// <summary>
    /// 测试完整构造 - 主机端口用户密码
    /// </summary>
    [Fact]
    public void TestConstructor_Full()
    {
        var connector = new Connector("ssh.example.com", 22, "user", "pass");
        
        Assert.Equal("ssh.example.com", connector.Host);
        Assert.Equal(22, connector.Port);
        Assert.Equal("user", connector.User);
        Assert.Equal("pass", connector.Password);
    }

    /// <summary>
    /// 测试设置主机
    /// </summary>
    [Fact]
    public void TestSetHost()
    {
        var connector = new Connector();
        connector.Host = "new.host.com";
        
        Assert.Equal("new.host.com", connector.Host);
    }

    /// <summary>
    /// 测试设置端口
    /// </summary>
    [Fact]
    public void TestSetPort()
    {
        var connector = new Connector();
        connector.Port = 2222;
        
        Assert.Equal(2222, connector.Port);
    }

    /// <summary>
    /// 测试设置用户名
    /// </summary>
    [Fact]
    public void TestSetUser()
    {
        var connector = new Connector();
        connector.User = "testuser";
        
        Assert.Equal("testuser", connector.User);
    }

    /// <summary>
    /// 测试设置密码
    /// </summary>
    [Fact]
    public void TestSetPassword()
    {
        var connector = new Connector();
        connector.Password = "secret";
        
        Assert.Equal("secret", connector.Password);
    }

    /// <summary>
    /// 测试设置组
    /// </summary>
    [Fact]
    public void TestSetGroup()
    {
        var connector = new Connector();
        connector.Group = "developers";
        
        Assert.Equal("developers", connector.Group);
    }

    /// <summary>
    /// 测试ToString方法
    /// </summary>
    [Fact]
    public void TestToString()
    {
        var connector = new Connector("example.com", 22, "user", "pass");
        var str = connector.ToString();
        
        Assert.Contains("example.com", str);
        Assert.Contains("22", str);
        Assert.Contains("user", str);
    }

    /// <summary>
    /// 测试空连接器ToString
    /// </summary>
    [Fact]
    public void TestToString_Empty()
    {
        var connector = new Connector();
        var str = connector.ToString();
        
        Assert.NotNull(str);
        Assert.Contains("Connector", str);
    }
}

/// <summary>
/// ChannelType 枚举测试类
/// </summary>
public class ChannelTypeTest
{
    /// <summary>
    /// 测试所有枚举值存在
    /// </summary>
    [Fact]
    public void TestAllEnumValues()
    {
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.Session));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.Shell));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.Exec));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.X11));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.AgentForwarding));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.DirectTcpip));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.ForwardedTcpip));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.Sftp));
        Assert.True(Enum.IsDefined(typeof(ChannelType), ChannelType.Subsystem));
    }

    /// <summary>
    /// 测试枚举数量
    /// </summary>
    [Fact]
    public void TestEnumCount()
    {
        var values = Enum.GetValues(typeof(ChannelType));
        Assert.Equal(9, values.Length);
    }
}

/// <summary>
/// ChannelTypeExtensions 扩展方法测试类
/// </summary>
public class ChannelTypeExtensionsTest
{
    /// <summary>
    /// 测试Session类型值
    /// </summary>
    [Fact]
    public void TestSession()
    {
        Assert.Equal("session", ChannelType.Session.GetValue());
    }

    /// <summary>
    /// 测试Shell类型值
    /// </summary>
    [Fact]
    public void TestShell()
    {
        Assert.Equal("shell", ChannelType.Shell.GetValue());
    }

    /// <summary>
    /// 测试Exec类型值
    /// </summary>
    [Fact]
    public void TestExec()
    {
        Assert.Equal("exec", ChannelType.Exec.GetValue());
    }

    /// <summary>
    /// 测试X11类型值
    /// </summary>
    [Fact]
    public void TestX11()
    {
        Assert.Equal("x11", ChannelType.X11.GetValue());
    }

    /// <summary>
    /// 测试AgentForwarding类型值
    /// </summary>
    [Fact]
    public void TestAgentForwarding()
    {
        Assert.Equal("auth-agent@openssh.com", ChannelType.AgentForwarding.GetValue());
    }

    /// <summary>
    /// 测试DirectTcpip类型值
    /// </summary>
    [Fact]
    public void TestDirectTcpip()
    {
        Assert.Equal("direct-tcpip", ChannelType.DirectTcpip.GetValue());
    }

    /// <summary>
    /// 测试ForwardedTcpip类型值
    /// </summary>
    [Fact]
    public void TestForwardedTcpip()
    {
        Assert.Equal("forwarded-tcpip", ChannelType.ForwardedTcpip.GetValue());
    }

    /// <summary>
    /// 测试Sftp类型值
    /// </summary>
    [Fact]
    public void TestSftp()
    {
        Assert.Equal("sftp", ChannelType.Sftp.GetValue());
    }

    /// <summary>
    /// 测试Subsystem类型值
    /// </summary>
    [Fact]
    public void TestSubsystem()
    {
        Assert.Equal("subsystem", ChannelType.Subsystem.GetValue());
    }

    /// <summary>
    /// 测试所有类型转换
    /// </summary>
    [Theory]
    [InlineData(ChannelType.Session, "session")]
    [InlineData(ChannelType.Shell, "shell")]
    [InlineData(ChannelType.Exec, "exec")]
    [InlineData(ChannelType.X11, "x11")]
    [InlineData(ChannelType.Sftp, "sftp")]
    [InlineData(ChannelType.Subsystem, "subsystem")]
    public void TestAllTypes(ChannelType type, string expected)
    {
        Assert.Equal(expected, type.GetValue());
    }
}

/// <summary>
/// JschRuntimeException 异常测试类
/// </summary>
public class JschRuntimeExceptionTest
{
    /// <summary>
    /// 测试构造异常 - 消息
    /// </summary>
    [Fact]
    public void TestConstructor_Message()
    {
        var ex = new JschRuntimeException("SSH runtime error");
        Assert.Equal("SSH runtime error", ex.Message);
    }

    /// <summary>
    /// 测试构造异常 - 消息和内部异常
    /// </summary>
    [Fact]
    public void TestConstructor_MessageAndInner()
    {
        var innerEx = new InvalidOperationException("Connection lost");
        var ex = new JschRuntimeException("SSH error", innerEx);
        
        Assert.Equal("SSH error", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }

    /// <summary>
    /// 测试异常可抛出和捕获
    /// </summary>
    [Fact]
    public async Task TestCanThrowAndCatch()
    {
        await Assert.ThrowsAsync<JschRuntimeException>(async () =>
        {
            throw new JschRuntimeException("JSch error");
        });
    }


}
