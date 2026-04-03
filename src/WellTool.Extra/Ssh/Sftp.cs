using System.Text;

namespace WellTool.Extra.Ssh;

/// <summary>
/// SFTP是Secure File Transfer Protocol的缩写，安全文件传送协议
/// 此类为基于SSH.NET的SFTP实现
/// </summary>
public class Sftp : IDisposable
{
    private ISshSession? _session;
    private ISftpClient? _sftpClient;
    private Encoding _charset = Encoding.UTF8;
    private bool _isConnected;

    /// <summary>
    /// 默认编码
    /// </summary>
    public static readonly Encoding DefaultCharset = Encoding.UTF8;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="sshHost">远程主机</param>
    /// <param name="sshPort">远程主机端口</param>
    /// <param name="sshUser">远程主机用户名</param>
    /// <param name="sshPass">远程主机密码</param>
    public Sftp(string sshHost, int sshPort, string sshUser, string sshPass)
        : this(sshHost, sshPort, sshUser, sshPass, DefaultCharset)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="sshHost">远程主机</param>
    /// <param name="sshPort">远程主机端口</param>
    /// <param name="sshUser">远程主机用户名</param>
    /// <param name="sshPass">远程主机密码</param>
    /// <param name="charset">编码</param>
    public Sftp(string sshHost, int sshPort, string sshUser, string sshPass, Encoding charset)
    {
        _charset = charset;
        _session = JschUtil.GetSession(sshHost, sshPort, sshUser, sshPass);
    }

    /// <summary>
    /// 初始化连接
    /// </summary>
    public void Init()
    {
        if (_session == null) return;
        _session.Connect();
        _sftpClient = _session.CreateSftp();
        _isConnected = true;
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool IsConnected => _isConnected;

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="srcPath">源路径(本地)</param>
    /// <param name="destPath">目标路径(远程)</param>
    public void Put(string srcPath, string destPath)
    {
        EnsureConnected();
        _sftpClient?.Put(srcPath, destPath);
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcPath">源路径(远程)</param>
    /// <param name="destPath">目标路径(本地)</param>
    public void Get(string srcPath, string destPath)
    {
        EnsureConnected();
        _sftpClient?.Get(srcPath, destPath);
    }

    /// <summary>
    /// 列出目录文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>文件名列表</returns>
    public IEnumerable<string> ListNames(string path)
    {
        EnsureConnected();
        return _sftpClient?.ListNames(path) ?? Enumerable.Empty<string>();
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="path">路径</param>
    public void Mkdir(string path)
    {
        EnsureConnected();
        _sftpClient?.Mkdir(path);
    }

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="path">路径</param>
    public void Rmdir(string path)
    {
        EnsureConnected();
        _sftpClient?.Rmdir(path);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">路径</param>
    public void Del(string path)
    {
        EnsureConnected();
        _sftpClient?.Del(path);
    }

    /// <summary>
    /// 重命名
    /// </summary>
    /// <param name="oldPath">旧路径</param>
    /// <param name="newPath">新路径</param>
    public void Rename(string oldPath, string newPath)
    {
        EnsureConnected();
        // 实现重命名
    }

    /// <summary>
    /// 检查路径是否存在
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>是否存在</returns>
    public bool Exists(string path)
    {
        EnsureConnected();
        try
        {
            var names = _sftpClient?.ListNames(Path.GetDirectoryName(path) ?? ".");
            return names?.Contains(path) ?? false;
        }
        catch
        {
            return false;
        }
    }

    private void EnsureConnected()
    {
        if (!_isConnected)
        {
            Init();
        }
    }

    public void Dispose()
    {
        _sftpClient?.Dispose();
        _session?.Dispose();
        _isConnected = false;
    }
}
