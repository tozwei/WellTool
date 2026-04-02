using System;
using System.Net.Mail;
using System.Text;
using WellTool.Extra.Mail;

namespace WellTool.Extra.Tests;

/// <summary>
/// MailAccount 邮件账户测试类
/// </summary>
public class MailAccountTest
{
    /// <summary>
    /// 测试默认构造
    /// </summary>
    [Fact]
    public void TestConstructor_Default()
    {
        var account = new MailAccount();
        Assert.NotNull(account);
        Assert.Equal(Encoding.UTF8, account.Charset);
        Assert.False(account.Splitlongparameters);
        Assert.True(account.Encodefilename);
        Assert.False(account.StarttlsEnable);
    }

    /// <summary>
    /// 测试设置主机
    /// </summary>
    [Fact]
    public void TestSetHost()
    {
        var account = new MailAccount();
        var result = account.SetHost("smtp.example.com");
        
        Assert.Same(account, result);
        Assert.Equal("smtp.example.com", account.Host);
    }

    /// <summary>
    /// 测试设置端口
    /// </summary>
    [Fact]
    public void TestSetPort()
    {
        var account = new MailAccount();
        var result = account.SetPort(587);
        
        Assert.Same(account, result);
        Assert.Equal(587, account.Port);
    }

    /// <summary>
    /// 测试设置认证
    /// </summary>
    [Fact]
    public void TestSetAuth()
    {
        var account = new MailAccount();
        var result = account.SetAuth(true);
        
        Assert.Same(account, result);
        Assert.True(account.Auth);
    }

    /// <summary>
    /// 测试设置用户名
    /// </summary>
    [Fact]
    public void TestSetUser()
    {
        var account = new MailAccount();
        var result = account.SetUser("testuser");
        
        Assert.Same(account, result);
        Assert.Equal("testuser", account.User);
    }

    /// <summary>
    /// 测试设置密码
    /// </summary>
    [Fact]
    public void TestSetPass()
    {
        var account = new MailAccount();
        var result = account.SetPass("secret123");
        
        Assert.Same(account, result);
        Assert.Equal("secret123", account.Pass);
    }

    /// <summary>
    /// 测试设置发件人
    /// </summary>
    [Fact]
    public void TestSetFrom()
    {
        var account = new MailAccount();
        var result = account.SetFrom("sender@example.com");
        
        Assert.Same(account, result);
        Assert.Equal("sender@example.com", account.From);
    }

    /// <summary>
    /// 测试设置调试模式
    /// </summary>
    [Fact]
    public void TestSetDebug()
    {
        var account = new MailAccount();
        var result = account.SetDebug(true);
        
        Assert.Same(account, result);
        Assert.True(account.Debug);
    }

    /// <summary>
        /// 测试设置字符集
        /// </summary>
        [Fact]
        public void TestSetCharset()
        {
            var account = new MailAccount();
            var result = account.SetCharset(Encoding.UTF8);
            
            Assert.Same(account, result);
            Assert.Equal(Encoding.UTF8.EncodingName, account.Charset.EncodingName);
        }

    /// <summary>
    /// 测试设置超长参数分割
    /// </summary>
    [Fact]
    public void TestSetSplitLongParameters()
    {
        var account = new MailAccount();
        account.SetSplitlongparameters(true);
        
        Assert.True(account.Splitlongparameters);
    }

    /// <summary>
    /// 测试设置文件名编码
    /// </summary>
    [Fact]
    public void TestSetEncodeFilename()
    {
        var account = new MailAccount();
        account.SetEncodefilename(false);
        
        Assert.False(account.Encodefilename);
    }

    /// <summary>
    /// 测试设置STARTTLS
    /// </summary>
    [Fact]
    public void TestSetStarttlsEnable()
    {
        var account = new MailAccount();
        var result = account.SetStarttlsEnable(true);
        
        Assert.Same(account, result);
        Assert.True(account.StarttlsEnable);
    }

    /// <summary>
    /// 测试设置SSL
    /// </summary>
    [Fact]
    public void TestSetSslEnable()
    {
        var account = new MailAccount();
        var result = account.SetSslEnable(true);
        
        Assert.Same(account, result);
        Assert.True(account.SslEnable);
    }

    /// <summary>
    /// 测试设置SSL协议
    /// </summary>
    [Fact]
    public void TestSetSslProtocols()
    {
        var account = new MailAccount();
        account.SetSslProtocols("TLS TLSv1.2");
        
        Assert.Equal("TLS TLSv1.2", account.SslProtocolsValue);
    }

    /// <summary>
    /// 测试设置Socket工厂类
    /// </summary>
    [Fact]
    public void TestSetSocketFactoryClass()
    {
        var account = new MailAccount();
        var result = account.SetSocketFactoryClass("CustomFactory");
        
        Assert.Same(account, result);
        Assert.Equal("CustomFactory", account.SocketFactoryClass);
    }

    /// <summary>
    /// 测试设置超时
    /// </summary>
    [Fact]
    public void TestSetTimeout()
    {
        var account = new MailAccount();
        var result = account.SetTimeout(30000);
        
        Assert.Same(account, result);
        Assert.Equal(30000, account.Timeout);
    }

    /// <summary>
    /// 测试设置连接超时
    /// </summary>
    [Fact]
    public void TestSetConnectionTimeout()
    {
        var account = new MailAccount();
        var result = account.SetConnectionTimeout(15000);
        
        Assert.Same(account, result);
        Assert.Equal(15000, account.ConnectionTimeout);
    }

    /// <summary>
    /// 测试设置写入超时
    /// </summary>
    [Fact]
    public void TestSetWriteTimeout()
    {
        var account = new MailAccount();
        var result = account.SetWriteTimeout(10000);
        
        Assert.Same(account, result);
        Assert.Equal(10000, account.WriteTimeout);
    }

    /// <summary>
    /// 测试设置自定义属性
    /// </summary>
    [Fact]
    public void TestSetCustomProperty()
    {
        var account = new MailAccount();
        var result = account.SetCustomProperty("custom.key", "custom.value");
        
        Assert.Same(account, result);
        Assert.True(account.CustomProperty.ContainsKey("custom.key"));
        Assert.Equal("custom.value", account.CustomProperty["custom.key"]);
    }

    /// <summary>
    /// 测试自定义属性忽略空键
    /// </summary>
    [Fact]
    public void TestSetCustomProperty_IgnoreEmptyKey()
    {
        var account = new MailAccount();
        account.SetCustomProperty("", "value");
        account.SetCustomProperty("   ", "value");
        
        Assert.Empty(account.CustomProperty);
    }

    /// <summary>
    /// 测试自定义属性忽略null值
    /// </summary>
    [Fact]
    public void TestSetCustomProperty_IgnoreNullValue()
    {
        var account = new MailAccount();
        account.SetCustomProperty("key", null);
        
        Assert.Empty(account.CustomProperty);
    }

    /// <summary>
    /// 测试ConfigureSmtpClient方法
    /// </summary>
    [Fact]
    public void TestConfigureSmtpClient()
    {
        var account = new MailAccount()
            .SetHost("smtp.example.com")
            .SetPort(587)
            .SetAuth(true)
            .SetUser("user")
            .SetPass("pass")
            .SetStarttlsEnable(true);
        
        using var client = new SmtpClient();
        account.ConfigureSmtpClient(client);
        
        Assert.Equal("smtp.example.com", client.Host);
        Assert.Equal(587, client.Port);
        Assert.True(client.EnableSsl);
    }

    /// <summary>
    /// 测试DefaultIfEmpty方法 - 从邮箱地址提取域名
    /// </summary>
    [Fact]
    public void TestDefaultIfEmpty_ExtractDomain()
    {
        var account = new MailAccount()
            .SetFrom("user@gmail.com")
            .DefaultIfEmpty();
        
        // 验证邮箱地址被正确提取用于用户名
        Assert.Equal("user@gmail.com", account.User);
        // 验证域名被正确提取并设置为主机
        Assert.Contains("gmail.com", account.Host);
    }

    /// <summary>
    /// 测试DefaultIfEmpty方法 - 完整格式发件人
    /// </summary>
    [Fact]
    public void TestDefaultIfEmpty_FromWithName()
    {
        var account = new MailAccount()
            .SetFrom("Sender Name <sender@domain.com>")
            .SetPass("password")
            .DefaultIfEmpty();
        
        Assert.Equal("smtp.domain.com", account.Host);
        Assert.Equal("sender@domain.com", account.User);
        Assert.True(account.Auth);
    }

    /// <summary>
    /// 测试ToString方法
    /// </summary>
    [Fact]
    public void TestToString()
    {
        var account = new MailAccount()
            .SetHost("smtp.example.com")
            .SetPort(25)
            .SetAuth(false)
            .SetUser("user")
            .SetPass("secret")
            .SetFrom("test@example.com");
        
        var str = account.ToString();
        Assert.Contains("smtp.example.com", str);
        Assert.Contains("25", str);
    }

    /// <summary>
    /// 测试Fluent API链式调用
    /// </summary>
    [Fact]
    public void TestFluentApi()
    {
        var account = new MailAccount()
            .SetHost("smtp.example.com")
            .SetPort(465)
            .SetAuth(true)
            .SetUser("user")
            .SetPass("pass")
            .SetFrom("sender@example.com")
            .SetDebug(true)
            .SetCharset(Encoding.UTF8)
            .SetSslEnable(true)
            .SetTimeout(30000)
            .SetCustomProperty("custom", "value");
        
        Assert.Equal("smtp.example.com", account.Host);
        Assert.Equal(465, account.Port);
        Assert.True(account.Auth);
        Assert.Equal("user", account.User);
        Assert.Equal("pass", account.Pass);
        Assert.Equal("sender@example.com", account.From);
        Assert.True(account.Debug);
        Assert.True(account.SslEnable);
        Assert.Equal(30000, account.Timeout);
        Assert.True(account.CustomProperty.ContainsKey("custom"));
    }
}

/// <summary>
/// MailException 邮件异常测试
/// </summary>
public class MailExceptionTest
{
    /// <summary>
    /// 测试构造异常 - 消息
    /// </summary>
    [Fact]
    public void TestConstructor_Message()
    {
        var ex = new MailException("Mail sending failed");
        Assert.Equal("Mail sending failed", ex.Message);
    }

    /// <summary>
    /// 测试构造异常 - 消息和内部异常
    /// </summary>
    [Fact]
    public void TestConstructor_MessageAndInner()
    {
        var innerEx = new InvalidOperationException("Connection failed");
        var ex = new MailException("Failed to send mail", innerEx);
        
        Assert.Equal("Failed to send mail", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }
}
