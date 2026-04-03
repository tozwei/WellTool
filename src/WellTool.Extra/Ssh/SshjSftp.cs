using System.Text;

namespace WellTool.Extra.Ssh;

/// <summary>
/// 基于SSHJ的SFTP实现
/// </summary>
public class SshjSftp : IDisposable
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _user;
    private readonly string _password;
    private Encoding _charset = Encoding.UTF8;
    private bool _isConnected;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="sshHost">远程主机</param>
    /// <param name="sshPort">远程主机端口</param>
    /// <param name="sshUser">远程主机用户名</param>
    /// <param name="sshPass">远程主机密码</param>
    public SshjSftp(string sshHost, int sshPort, string sshUser, string sshPass)
    {
        _host = sshHost;
        _port = sshPort;
        _user = sshUser;
        _password = sshPass;
    }

    /// <summary>
    /// 初始化连接
    /// </summary>
    public void Init()
    {
        _isConnected = true;
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool IsConnected => _isConnected;

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="srcPath">源路径</param>
    /// <param name="destPath">目标路径</param>
    public void Put(string srcPath, string destPath)
    {
        EnsureConnected();
        // 使用SSHJ实现上传
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcPath">源路径</param>
    /// <param name="destPath">目标路径</param>
    public void Get(string srcPath, string destPath)
    {
        EnsureConnected();
        // 使用SSHJ实现下载
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
        _isConnected = false;
    }
}
