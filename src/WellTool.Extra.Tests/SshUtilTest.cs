namespace WellTool.Extra.Tests;

/// <summary>
/// SshUtil 测试类
/// </summary>
public class SshUtilTest
{
    private readonly SshUtil _sshUtil;

    public SshUtilTest()
    {
        _sshUtil = new SshUtil();
    }

    [Fact]
    public void TestExecuteCommand_ValidCommand_ReturnsResult()
    {
        // 测试执行命令
        var result = _sshUtil.ExecuteCommand(
            "localhost",
            22,
            "user",
            "password",
            "ls -la"
        );
        
        Assert.NotNull(result);
        Assert.Contains("ls -la", result);
    }

    [Fact]
    public void TestExecuteCommand_DifferentHosts_ReturnsHostInfo()
    {
        // 测试不同主机
        var result = _sshUtil.ExecuteCommand(
            "192.168.1.100",
            2222,
            "admin",
            "pass",
            "whoami"
        );
        
        Assert.NotNull(result);
        Assert.Contains("192.168.1.100", result);
        Assert.Contains("2222", result);
    }

    [Fact]
    public void TestUploadFile_ValidParameters_DoesNotThrow()
    {
        // 测试上传文件（模拟）
        var tempFile = Path.GetTempFileName();
        try
        {
            var exception = Record.Exception(() =>
                _sshUtil.UploadFile(
                    "localhost",
                    22,
                    "user",
                    "password",
                    tempFile,
                    "/remote/path/file.txt"
                )
            );
            
            Assert.Null(exception);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestDownloadFile_ValidParameters_DoesNotThrow()
    {
        // 测试下载文件（模拟）
        var tempFile = Path.GetTempFileName();
        try
        {
            var exception = Record.Exception(() =>
                _sshUtil.DownloadFile(
                    "localhost",
                    22,
                    "user",
                    "password",
                    "/remote/path/file.txt",
                    tempFile
                )
            );
            
            Assert.Null(exception);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestSshUtil_Instance_NotNull()
    {
        // 测试实例不为空
        Assert.NotNull(_sshUtil);
    }

    [Fact]
    public void TestSshUtil_SingletonInstance_ReturnsSameInstance()
    {
        // 测试单例实例
        var instance1 = SshUtil.Instance;
        var instance2 = SshUtil.Instance;
        
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void TestSshException_Constructor_MessageOnly()
    {
        // 测试异常构造函数
        var exception = new SshException("Test error");
        
        Assert.Equal("Test error", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void TestSshException_Constructor_MessageAndInnerException()
    {
        // 测试异常构造函数（带内部异常）
        var innerException = new Exception("Inner error");
        var exception = new SshException("Test error", innerException);
        
        Assert.Equal("Test error", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Theory]
    [InlineData("localhost", 22)]
    [InlineData("192.168.1.1", 2222)]
    [InlineData("example.com", 22)]
    public void TestExecuteCommand_VariousHosts_Succeeds(string host, int port)
    {
        // 测试各种主机
        var result = _sshUtil.ExecuteCommand(
            host,
            port,
            "user",
            "password",
            "echo test"
        );
        
        Assert.NotNull(result);
    }

    [Fact]
    public void TestExecuteCommand_LongCommand_ReturnsCommandInResult()
    {
        // 测试长命令
        var longCommand = "find /var/log -name '*.log' -mtime +7 -exec rm {} \\;";
        var result = _sshUtil.ExecuteCommand(
            "localhost",
            22,
            "user",
            "password",
            longCommand
        );
        
        Assert.Contains(longCommand, result);
    }

    [Fact]
    public void TestUploadFile_NonExistentLocalFile_DoesNotThrow()
    {
        // 测试不存在的本地文件（模拟实现）
        var nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        var exception = Record.Exception(() =>
            _sshUtil.UploadFile(
                "localhost",
                22,
                "user",
                "password",
                nonExistentFile,
                "/remote/path/file.txt"
            )
        );
        
        // 模拟实现不检查文件存在性
        Assert.Null(exception);
    }
}
