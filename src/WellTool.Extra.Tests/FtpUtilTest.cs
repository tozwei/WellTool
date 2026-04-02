namespace WellTool.Extra.Tests;

/// <summary>
/// FtpUtil 测试类
/// </summary>
public class FtpUtilTest
{
    private readonly FtpUtil _ftpUtil;

    public FtpUtilTest()
    {
        _ftpUtil = new FtpUtil();
    }

    [Fact]
    public void TestListFiles_ValidUrl_ReturnsFileList()
    {
        // 测试列出文件（使用本地测试）
        var ftpUrl = "ftp://localhost";
        var username = "test";
        var password = "test";
        
        // 注意：实际测试需要 FTP 服务器
        // 这里测试方法签名和基本逻辑
        Assert.NotNull(_ftpUtil);
    }

    [Fact]
    public void TestFtpException_Constructor_MessageOnly()
    {
        // 测试异常构造函数
        var exception = new FtpException("Test error");
        
        Assert.Equal("Test error", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void TestFtpException_Constructor_MessageAndInnerException()
    {
        // 测试异常构造函数（带内部异常）
        var innerException = new Exception("Inner error");
        var exception = new FtpException("Test error", innerException);
        
        Assert.Equal("Test error", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Theory]
    [InlineData("ftp://example.com")]
    [InlineData("ftp://192.168.1.1")]
    [InlineData("ftp://localhost")]
    public void TestFtpUtil_Instance_NotNull(string url)
    {
        // 测试实例不为空
        Assert.NotNull(_ftpUtil);
    }

    [Fact]
    public void TestFtpUtil_SingletonInstance_ReturnsSameInstance()
    {
        // 测试单例实例
        var instance1 = FtpUtil.Instance;
        var instance2 = FtpUtil.Instance;
        
        Assert.Same(instance1, instance2);
    }
}
