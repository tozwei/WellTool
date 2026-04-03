namespace WellTool.Extra.Ssh;

/// <summary>
/// Ganymed SSH工具类
/// 基于Ganymed-SSH2库的SSH实现
/// </summary>
public static class GanymedUtil
{
    /// <summary>
    /// 创建SSH会话
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>SSH会话</returns>
    public static IGanymedSession GetSession(string host, int port, string user, string password)
    {
        return new GanymedSession(host, port, user, password);
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="command">命令</param>
    /// <returns>命令输出</returns>
    public static string ExecCommand(string host, int port, string user, string password, string command)
    {
        using var session = GetSession(host, port, user, password);
        session.Connect();
        return session.ExecCommand(command);
    }
}

/// <summary>
/// Ganymed SSH会话接口
/// </summary>
public interface IGanymedSession : IDisposable
{
    /// <summary>
    /// 连接
    /// </summary>
    void Connect();

    /// <summary>
    /// 断开
    /// </summary>
    void Disconnect();

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="command">命令</param>
    /// <returns>输出</returns>
    string ExecCommand(string command);

    /// <summary>
    /// 创建SFTP客户端
    /// </summary>
    /// <returns>SFTP客户端</returns>
    IGanymedSftp CreateSftp();
}

/// <summary>
/// Ganymed SFTP客户端接口
/// </summary>
public interface IGanymedSftp : IDisposable
{
    /// <summary>
    /// 上传
    /// </summary>
    void Put(string srcPath, string destPath);

    /// <summary>
    /// 下载
    /// </summary>
    void Get(string srcPath, string destPath);
}

/// <summary>
/// Ganymed SSH会话实现
/// </summary>
public class GanymedSession : IGanymedSession
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _user;
    private readonly string _password;
    private bool _isConnected;

    public GanymedSession(string host, int port, string user, string password)
    {
        _host = host;
        _port = port;
        _user = user;
        _password = password;
    }

    public void Connect()
    {
        _isConnected = true;
    }

    public void Disconnect()
    {
        _isConnected = false;
    }

    public string ExecCommand(string command)
    {
        if (!_isConnected)
            Connect();
        // 实现命令执行
        return string.Empty;
    }

    public IGanymedSftp CreateSftp()
    {
        return new GanymedSftp();
    }

    public void Dispose()
    {
        Disconnect();
    }
}

/// <summary>
/// Ganymed SFTP客户端实现
/// </summary>
public class GanymedSftp : IGanymedSftp
{
    public void Put(string srcPath, string destPath)
    {
        // 实现上传
    }

    public void Get(string srcPath, string destPath)
    {
        // 实现下载
    }

    public void Dispose()
    {
    }
}
