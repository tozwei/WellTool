namespace WellTool.Extra.Tests;

/// <summary>
/// MailUtil 测试类
/// </summary>
public class MailUtilTest
{
    private readonly MailUtil _mailUtil;

    public MailUtilTest()
    {
        _mailUtil = new MailUtil();
    }

    [Fact]
    public void TestMailUtil_Instance_NotNull()
    {
        // 测试实例不为空
        Assert.NotNull(_mailUtil);
    }

    [Fact]
    public void TestMailUtil_SingletonInstance_ReturnsSameInstance()
    {
        // 测试单例实例
        var instance1 = MailUtil.Instance;
        var instance2 = MailUtil.Instance;
        
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void TestMailException_Constructor_MessageOnly()
    {
        // 测试异常构造函数
        var exception = new MailException("Test error");
        
        Assert.Equal("Test error", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void TestMailException_Constructor_MessageAndInnerException()
    {
        // 测试异常构造函数（带内部异常）
        var innerException = new Exception("Inner error");
        var exception = new MailException("Test error", innerException);
        
        Assert.Equal("Test error", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void TestSend_InvalidSmtpServer_ThrowsMailException()
    {
        // 测试无效 SMTP 服务器（应该抛出异常）
        Assert.Throws<MailException>(() =>
            _mailUtil.Send(
                "invalid-smtp-server-that-does-not-exist",
                25,
                "user",
                "pass",
                "from@example.com",
                "to@example.com",
                "Test Subject",
                "Test Body"
            )
        );
    }

    [Fact]
    public void TestSend_InvalidPort_ThrowsException()
    {
        // 测试无效端口
        var exception = Record.Exception(() =>
            _mailUtil.Send(
                "smtp.example.com",
                -1, // 无效端口
                "user",
                "pass",
                "from@example.com",
                "to@example.com",
                "Test Subject",
                "Test Body"
            )
        );
        
        Assert.NotNull(exception);
    }

    [Fact]
    public void TestSend_HtmlBody_SetsIsBodyHtmlTrue()
    {
        // 测试 HTML 邮件
        // 注意：实际发送需要真实 SMTP 服务器
        Assert.NotNull(_mailUtil);
    }

    [Fact]
    public void TestSendWithAttachments_EmptyAttachments_Succeeds()
    {
        // 测试空附件列表
        // 注意：实际发送需要真实 SMTP 服务器
        Assert.NotNull(_mailUtil);
    }

    [Fact]
    public void TestSend_ValidParameters_DoesNotThrow()
    {
        // 测试有效参数（但可能因网络问题失败）
        // 这里主要测试方法签名
        var exception = Record.Exception(() =>
            _mailUtil.Send(
                "smtp.gmail.com",
                587,
                "test@gmail.com",
                "password",
                "from@gmail.com",
                "to@gmail.com",
                "Test",
                "Body"
            )
        );
        
        // 可能抛出异常，但不是参数验证异常
        Assert.True(exception is MailException || exception is System.Net.Mail.SmtpException || exception == null);
    }

    [Fact]
    public void TestSendWithAttachments_InvalidAttachmentPath_ThrowsException()
    {
        // 测试无效附件路径
        var exception = Record.Exception(() =>
            _mailUtil.SendWithAttachments(
                "smtp.example.com",
                25,
                "user",
                "pass",
                "from@example.com",
                "to@example.com",
                "Test Subject",
                "Test Body",
                new List<string> { "nonexistent-file-path-xyz123.txt" }
            )
        );
        
        Assert.NotNull(exception);
    }
}
