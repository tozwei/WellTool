using System;
using WellTool.Extra.Ftp;
using Xunit;

namespace WellTool.Extra.Tests;

/// <summary>
/// FtpConfig 配置类测试
/// </summary>
public class FtpConfigTest
{
    /// <summary>
    /// 测试创建默认配置
    /// </summary>
    [Fact]
    public void TestCreate()
    {
        var config = FtpConfig.Create();
        Assert.NotNull(config);
    }

    /// <summary>
    /// 测试无参构造
    /// </summary>
    [Fact]
    public void TestConstructor_NoParams()
    {
        var config = new FtpConfig();
        Assert.NotNull(config);
    }

    /// <summary>
    /// 测试基础构造
    /// </summary>
    [Fact]
    public void TestConstructor_Basic()
    {
        var config = new FtpConfig("ftp.example.com", 21, "user", "password", "UTF-8");
        
        Assert.Equal("ftp.example.com", config.Host);
        Assert.Equal(21, config.Port);
        Assert.Equal("user", config.User);
        Assert.Equal("password", config.Password);
        Assert.Equal("UTF-8", config.Charset);
    }

    /// <summary>
    /// 测试完整构造
    /// </summary>
    [Fact]
    public void TestConstructor_Full()
    {
        var config = new FtpConfig("ftp.example.com", 21, "user", "password", "UTF-8", "en", "UNIX");
        
        Assert.Equal("ftp.example.com", config.Host);
        Assert.Equal(21, config.Port);
        Assert.Equal("user", config.User);
        Assert.Equal("password", config.Password);
        Assert.Equal("UTF-8", config.Charset);
        Assert.Equal("en", config.ServerLanguageCode);
        Assert.Equal("UNIX", config.SystemKey);
    }

    /// <summary>
    /// 测试设置主机
    /// </summary>
    [Fact]
    public void TestSetHost()
    {
        var config = new FtpConfig();
        var result = config.SetHost("ftp.example.com");
        
        Assert.Same(config, result);
        Assert.Equal("ftp.example.com", config.Host);
    }

    /// <summary>
    /// 测试设置端口
    /// </summary>
    [Fact]
    public void TestSetPort()
    {
        var config = new FtpConfig();
        var result = config.SetPort(2121);
        
        Assert.Same(config, result);
        Assert.Equal(2121, config.Port);
    }

    /// <summary>
    /// 测试设置用户名
    /// </summary>
    [Fact]
    public void TestSetUser()
    {
        var config = new FtpConfig();
        var result = config.SetUser("testuser");
        
        Assert.Same(config, result);
        Assert.Equal("testuser", config.User);
    }

    /// <summary>
    /// 测试设置密码
    /// </summary>
    [Fact]
    public void TestSetPassword()
    {
        var config = new FtpConfig();
        var result = config.SetPassword("secret");
        
        Assert.Same(config, result);
        Assert.Equal("secret", config.Password);
    }

    /// <summary>
    /// 测试设置编码
    /// </summary>
    [Fact]
    public void TestSetCharset()
    {
        var config = new FtpConfig();
        var result = config.SetCharset("GBK");
        
        Assert.Same(config, result);
        Assert.Equal("GBK", config.Charset);
    }

    /// <summary>
    /// 测试设置连接超时
    /// </summary>
    [Fact]
    public void TestSetConnectionTimeout()
    {
        var config = new FtpConfig();
        var result = config.SetConnectionTimeout(30000);
        
        Assert.Same(config, result);
        Assert.Equal(30000, config.ConnectionTimeout);
    }

    /// <summary>
    /// 测试设置Socket超时
    /// </summary>
    [Fact]
    public void TestSetSoTimeout()
    {
        var config = new FtpConfig();
        var result = config.SetSoTimeout(15000);
        
        Assert.Same(config, result);
        Assert.Equal(15000, config.SoTimeout);
    }

    /// <summary>
    /// 测试设置服务器语言
    /// </summary>
    [Fact]
    public void TestSetServerLanguageCode()
    {
        var config = new FtpConfig();
        var result = config.SetServerLanguageCode("zh_CN");
        
        Assert.Same(config, result);
        Assert.Equal("zh_CN", config.ServerLanguageCode);
    }

    /// <summary>
    /// 测试设置系统关键词
    /// </summary>
    [Fact]
    public void TestSetSystemKey()
    {
        var config = new FtpConfig();
        var result = config.SetSystemKey("UNIX");
        
        Assert.Same(config, result);
        Assert.Equal("UNIX", config.SystemKey);
    }

    /// <summary>
    /// 测试Fluent API链式调用
    /// </summary>
    [Fact]
    public void TestFluentApi()
    {
        var config = new FtpConfig()
            .SetHost("ftp.example.com")
            .SetPort(2121)
            .SetUser("user")
            .SetPassword("pass")
            .SetCharset("UTF-8")
            .SetConnectionTimeout(30000)
            .SetSoTimeout(15000)
            .SetServerLanguageCode("en")
            .SetSystemKey("UNIX");
        
        Assert.Equal("ftp.example.com", config.Host);
        Assert.Equal(2121, config.Port);
        Assert.Equal("user", config.User);
        Assert.Equal("pass", config.Password);
        Assert.Equal("UTF-8", config.Charset);
        Assert.Equal(30000, config.ConnectionTimeout);
        Assert.Equal(15000, config.SoTimeout);
        Assert.Equal("en", config.ServerLanguageCode);
        Assert.Equal("UNIX", config.SystemKey);
    }
}

/// <summary>
/// FtpMode 枚举测试
/// </summary>
public class FtpModeTest
{
    /// <summary>
    /// 测试FtpMode枚举值存在
    /// </summary>
    [Fact]
    public void TestFtpMode_Active()
    {
        Assert.Equal(0, (int)FtpMode.Active);
    }

    /// <summary>
    /// 测试FtpMode枚举Passive值
    /// </summary>
    [Fact]
    public void TestFtpMode_Passive()
    {
        Assert.Equal(1, (int)FtpMode.Passive);
    }
}

/// <summary>
/// FtpException 异常测试
/// </summary>
public class FtpExceptionTest
{
    /// <summary>
    /// 测试构造异常 - 消息
    /// </summary>
    [Fact]
    public void TestConstructor_Message()
    {
        var ex = new FtpException("Test error message");
        Assert.Equal("Test error message", ex.Message);
    }

    /// <summary>
    /// 测试构造异常 - 消息和内部异常
    /// </summary>
    [Fact]
    public void TestConstructor_MessageAndInner()
    {
        var innerEx = new InvalidOperationException("Inner exception");
        var ex = new FtpException("Outer error", innerEx);
        
        Assert.Equal("Outer error", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }

    /// <summary>
    /// 测试异常可抛出和捕获
    /// </summary>
    [Fact]
    public async Task TestCanThrowAndCatch()
    {
        await Assert.ThrowsAsync<FtpException>(async () =>
        {
            throw new FtpException("FTP error");
        });
    }
}
