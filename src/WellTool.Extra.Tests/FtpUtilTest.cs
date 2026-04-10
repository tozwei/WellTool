namespace WellTool.Extra.Tests;

using WellTool.Extra;

public class FtpUtilTest
{
    [Fact(Skip = "Requires FTP server")]
    public void ConnectTest()
    {
        // 实际使用时，需要替换为真实的服务器信息
        string ftpUrl = "ftp://ftp.example.com";
        string username = "test";
        string password = "password";
        
        // 这里只是展示如何使用 FtpUtil 类
        var ftpUtil = FtpUtil.Instance;
        
        // 列出文件来测试连接
        var files = ftpUtil.ListFiles(ftpUrl, "/", username, password);
        Assert.NotNull(files);
    }

    [Fact(Skip = "Requires FTP server")]
    public void UploadTest()
    {
        // 实际使用时，需要替换为真实的服务器信息和文件路径
        string ftpUrl = "ftp://ftp.example.com";
        string username = "test";
        string password = "password";
        string localFilePath = @"C:\test\localfile.txt";
        string remoteFilePath = "remotefile.txt";
        
        // 这里只是展示如何使用 FtpUtil 类上传文件
        var ftpUtil = FtpUtil.Instance;
        ftpUtil.Upload(ftpUrl, localFilePath, remoteFilePath, username, password);
        
        // 验证上传操作没有抛出异常
        Assert.True(true);//
    }

    [Fact(Skip = "Requires FTP server")]
    public void DownloadTest()
    {
        // 实际使用时，需要替换为真实的服务器信息和文件路径
        string ftpUrl = "ftp://ftp.example.com";
        string username = "test";
        string password = "password";
        string remoteFilePath = "remotefile.txt";
        string localFilePath = @"C:\test\localfile.txt";
        
        // 这里只是展示如何使用 FtpUtil 类下载文件
        var ftpUtil = FtpUtil.Instance;
        ftpUtil.Download(ftpUrl, remoteFilePath, localFilePath, username, password);
        
        // 验证下载操作没有抛出异常
        Assert.True(true);//
    }

    [Fact(Skip = "Requires FTP server")]
    public void DeleteTest()
    {
        // 实际使用时，需要替换为真实的服务器信息
        string ftpUrl = "ftp://ftp.example.com";
        string username = "test";
        string password = "password";
        string remoteFilePath = "remotefile.txt";
        
        // 这里只是展示如何使用 FtpUtil 类删除文件
        var ftpUtil = FtpUtil.Instance;
        ftpUtil.Delete(ftpUrl, remoteFilePath, username, password);
        
        // 验证删除操作没有抛出异常
        Assert.True(true);//
    }
}
