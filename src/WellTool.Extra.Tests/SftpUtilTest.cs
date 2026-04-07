namespace WellTool.Extra.Tests;

using WellTool.Extra;

public class SftpUtilTest
{
    [Fact(Skip = "Requires SFTP server")]
    public void ConnectTest()
    {
        // 实际使用时，需要替换为真实的服务器信息
        string host = "sftp.example.com";
        int port = 22;
        string username = "test";
        string password = "password";
        
        // 这里只是展示如何使用 SshUtil 类
        var sshUtil = SshUtil.Instance;
        
        // 执行一个简单的命令来测试连接
        string result = sshUtil.ExecuteCommand(host, port, username, password, "pwd");
        Assert.Contains("Command executed", result);
    }

    [Fact(Skip = "Requires SFTP server")]
    public void UploadTest()
    {
        // 实际使用时，需要替换为真实的服务器信息和文件路径
        string host = "sftp.example.com";
        int port = 22;
        string username = "test";
        string password = "password";
        string localFilePath = @"C:\test\localfile.txt";
        string remoteFilePath = "/home/test/remotefile.txt";
        
        // 这里只是展示如何使用 SshUtil 类上传文件
        var sshUtil = SshUtil.Instance;
        sshUtil.UploadFile(host, port, username, password, localFilePath, remoteFilePath);
        
        // 验证上传操作没有抛出异常
        Assert.True(true);
    }

    [Fact(Skip = "Requires SFTP server")]
    public void DownloadTest()
    {
        // 实际使用时，需要替换为真实的服务器信息和文件路径
        string host = "sftp.example.com";
        int port = 22;
        string username = "test";
        string password = "password";
        string remoteFilePath = "/home/test/remotefile.txt";
        string localFilePath = @"C:\test\localfile.txt";
        
        // 这里只是展示如何使用 SshUtil 类下载文件
        var sshUtil = SshUtil.Instance;
        sshUtil.DownloadFile(host, port, username, password, remoteFilePath, localFilePath);
        
        // 验证下载操作没有抛出异常
        Assert.True(true);
    }

    [Fact(Skip = "Requires SFTP server")]
    public void DeleteTest()
    {
        // 实际使用时，需要替换为真实的服务器信息
        string host = "sftp.example.com";
        int port = 22;
        string username = "test";
        string password = "password";
        
        // 这里只是展示如何使用 SshUtil 类执行删除命令
        var sshUtil = SshUtil.Instance;
        string result = sshUtil.ExecuteCommand(host, port, username, password, "rm /home/test/remotefile.txt");
        Assert.Contains("Command executed", result);
    }
}
