namespace WellTool.Extra.Tests;

using WellTool.Extra.Mail;

public class MailAccountUtilTest
{
    [Fact]
    public void ParseTest()
    {
        var config = "smtp.example.com:465:user@example.com:password";
        var account = MailAccountUtil.Parse(config);
        Assert.NotNull(account);
        Assert.Equal("user@example.com", account.User);
    }

    [Fact]
    public void ParseWithProtocolTest()
    {
        var config = "smtps://user:pass@smtp.example.com:465";
        var account = MailAccountUtil.Parse(config);
        Assert.NotNull(account);
    }

    [Fact]
    public void ParseEmptyTest()
    {
        var account = MailAccountUtil.Parse("");
        Assert.NotNull(account);
    }

    [Fact]
    public void ParseNullTest()
    {
        var account = MailAccountUtil.Parse(null);
        Assert.NotNull(account);
    }

    [Fact]
    public void IsLoginTest()
    {
        var account = new MailAccount { User = "test", Pass = "pass" };
        Assert.True(MailAccountUtil.IsLogin(account));
    }

    [Fact]
    public void GetSmtpHostTest()
    {
        var host = MailAccountUtil.GetSmtpHost("smtp.example.com");
        Assert.Equal("smtp.example.com", host);
    }

    [Fact]
    public void GetSmtpPortTest()
    {
        var port = MailAccountUtil.GetSmtpPort(true, true);
        Assert.Equal(465, port);
    }

    [Fact]
    public void GetSmtpPortNoSslTest()
    {
        var port = MailAccountUtil.GetSmtpPort(false, false);
        Assert.Equal(25, port);
    }
}
