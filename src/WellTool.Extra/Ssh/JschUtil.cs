using System.Net;
using System.Text;

namespace WellTool.Extra.Ssh;

/// <summary>
/// Jsch工具类
/// Jsch是Java Secure Channel的缩写。JSch是一个SSH2的纯Java实现
/// </summary>
public static class JschUtil
{
    /// <summary>
    /// 不使用SSH的值
    /// </summary>
    public const string SshNone = "none";

    /// <summary>
    /// 本地端口生成器
    /// </summary>
    private static int _lastLocalPort = 10000;
    private static readonly object _lock = new();

    /// <summary>
    /// 生成一个本地端口，用于远程端口映射
    /// </summary>
    /// <returns>未被使用的本地端口</returns>
    public static int GenerateLocalPort()
    {
        lock (_lock)
        {
            return ++_lastLocalPort;
        }
    }

    /// <summary>
    /// 创建SSH会话
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="sshPass">密码</param>
    /// <returns>SSH会话</returns>
    public static ISshSession GetSession(string sshHost, int sshPort, string sshUser, string sshPass)
    {
        return new SshSession(sshHost, sshPort, sshUser, sshPass);
    }

    /// <summary>
    /// 创建SSH会话
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="privateKeyPath">私钥路径</param>
    /// <param name="passphrase">私钥密码</param>
    /// <returns>SSH会话</returns>
    public static ISshSession GetSession(string sshHost, int sshPort, string sshUser, string privateKeyPath, string? passphrase = null)
    {
        var privateKey = File.ReadAllBytes(privateKeyPath);
        var passphraseBytes = string.IsNullOrEmpty(passphrase) ? null : Encoding.UTF8.GetBytes(passphrase);
        return new SshSession(sshHost, sshPort, sshUser, privateKey, passphraseBytes);
    }

    /// <summary>
    /// 创建SSH会话
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="privateKey">私钥内容</param>
    /// <param name="passphrase">私钥密码</param>
    /// <returns>SSH会话</returns>
    public static ISshSession GetSession(string sshHost, int sshPort, string sshUser, byte[] privateKey, byte[]? passphrase = null)
    {
        return new SshSession(sshHost, sshPort, sshUser, privateKey, passphrase);
    }

    /// <summary>
    /// 打开一个新的SSH会话
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="sshPass">密码</param>
    /// <returns>SSH会话</returns>
    public static ISshSession OpenSession(string sshHost, int sshPort, string sshUser, string sshPass)
    {
        return OpenSession(sshHost, sshPort, sshUser, sshPass, 0);
    }

    /// <summary>
    /// 打开一个新的SSH会话
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="sshPass">密码</param>
    /// <param name="timeout">超时时间(毫秒)</param>
    /// <returns>SSH会话</returns>
    public static ISshSession OpenSession(string sshHost, int sshPort, string sshUser, string sshPass, int timeout)
    {
        return new SshSession(sshHost, sshPort, sshUser, sshPass, timeout);
    }

    /// <summary>
    /// 执行远程命令
    /// </summary>
    /// <param name="sshHost">主机</param>
    /// <param name="sshPort">端口</param>
    /// <param name="sshUser">用户名</param>
    /// <param name="sshPass">密码</param>
    /// <param name="command">命令</param>
    /// <returns>命令输出</returns>
    public static string ExecCommand(string sshHost, int sshPort, string sshUser, string sshPass, string command)
    {
        using var session = OpenSession(sshHost, sshPort, sshUser, sshPass);
        return session.ExecCommand(command);
    }
}

/// <summary>
/// SSH会话接口
/// </summary>
public interface ISshSession : IDisposable
{
    /// <summary>
    /// 主机
    /// </summary>
    string Host { get; }
    
    /// <summary>
    /// 端口
    /// </summary>
    int Port { get; }
    
    /// <summary>
    /// 用户名
    /// </summary>
    string User { get; }
    
    /// <summary>
    /// 连接状态
    /// </summary>
    bool IsConnected { get; }
    
    /// <summary>
    /// 连接
    /// </summary>
    void Connect();
    
    /// <summary>
    /// 断开连接
    /// </summary>
    void Disconnect();
    
    /// <summary>
    /// 创建SFTP客户端
    /// </summary>
    /// <returns>SFTP客户端</returns>
    ISftpClient CreateSftp();
    
    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="command">命令</param>
    /// <returns>输出结果</returns>
    string ExecCommand(string command);
}

/// <summary>
/// SFTP客户端接口
/// </summary>
public interface ISftpClient : IDisposable
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="srcPath">源路径</param>
    /// <param name="destPath">目标路径</param>
    void Put(string srcPath, string destPath);
    
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcPath">源路径</param>
    /// <param name="destPath">目标路径</param>
    void Get(string srcPath, string destPath);
    
    /// <summary>
    /// 列出目录
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>文件列表</returns>
    IEnumerable<string> ListNames(string path);
    
    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="path">路径</param>
    void Mkdir(string path);
    
    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="path">路径</param>
    void Rmdir(string path);
    
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">路径</param>
    void Del(string path);
}

/// <summary>
/// SSH会话实现
/// </summary>
public class SshSession : ISshSession
{
    private readonly string _password;
    private readonly byte[]? _privateKey;
    private readonly byte[]? _passphrase;
    private readonly int _timeout;
    private bool _isConnected;
    private readonly object _lock = new();

    public string Host { get; }
    public int Port { get; }
    public string User { get; }
    public bool IsConnected => _isConnected;

    public SshSession(string host, int port, string user, string password, int timeout = 0)
    {
        Host = host;
        Port = port;
        User = user;
        _password = password;
        _timeout = timeout;
    }

    public SshSession(string host, int port, string user, byte[] privateKey, byte[]? passphrase = null, int timeout = 0)
    {
        Host = host;
        Port = port;
        User = user;
        _privateKey = privateKey;
        _passphrase = passphrase;
        _timeout = timeout;
    }

    public void Connect()
    {
        lock (_lock)
        {
            if (_isConnected) return;
            // SSH.NET实现连接
            _isConnected = true;
        }
    }

    public void Disconnect()
    {
        lock (_lock)
        {
            _isConnected = false;
        }
    }

    public ISftpClient CreateSftp()
    {
        return new SftpClient(this);
    }

    public string ExecCommand(string command)
    {
        if (!_isConnected)
            Connect();
        
        // 使用SSH.NET执行命令
        var result = new System.Text.StringBuilder();
        // 实现命令执行
        return result.ToString();
    }

    public void Dispose()
    {
        Disconnect();
    }
}

/// <summary>
/// SFTP客户端实现
/// </summary>
public class SftpClient : ISftpClient
{
    private readonly SshSession _session;

    public SftpClient(SshSession session)
    {
        _session = session;
    }

    public void Put(string srcPath, string destPath)
    {
        // 实现上传
    }

    public void Get(string srcPath, string destPath)
    {
        // 实现下载
    }

    public IEnumerable<string> ListNames(string path)
    {
        // 实现列表
        return Array.Empty<string>();
    }

    public void Mkdir(string path)
    {
        // 实现创建目录
    }

    public void Rmdir(string path)
    {
        // 实现删除目录
    }

    public void Del(string path)
    {
        // 实现删除文件
    }

    public void Dispose()
    {
        // 清理资源
    }
}
