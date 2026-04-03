using System.Text;

namespace WellTool.Extra.Ssh;

/// <summary>
/// Jsch会话池，用于管理SSH会话的重用
/// </summary>
public class JschSessionPool
{
    /// <summary>
    /// 获取单例实例
    /// </summary>
    public static JschSessionPool Instance { get; } = new JschSessionPool();

    private readonly Dictionary<string, ISshSession> _sessionCache = new();
    private readonly object _lock = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    public JschSessionPool() { }

    /// <summary>
    /// 获取会话
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>SSH会话</returns>
    public ISshSession GetSession(string host, int port, string user, string password)
    {
        var key = GetSessionKey(host, port, user);
        lock (_lock)
        {
            if (_sessionCache.TryGetValue(key, out var session) && session.IsConnected)
            {
                return session;
            }

            var newSession = JschUtil.GetSession(host, port, user, password);
            newSession.Connect();
            _sessionCache[key] = newSession;
            return newSession;
        }
    }

    /// <summary>
    /// 获取会话(私钥认证)
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    /// <param name="privateKeyPath">私钥路径</param>
    /// <param name="passphrase">私钥密码</param>
    /// <returns>SSH会话</returns>
    public ISshSession GetSession(string host, int port, string user, string privateKeyPath, string? passphrase)
    {
        var key = GetSessionKey(host, port, user);
        lock (_lock)
        {
            if (_sessionCache.TryGetValue(key, out var session) && session.IsConnected)
            {
                return session;
            }

            var newSession = JschUtil.GetSession(host, port, user, privateKeyPath, passphrase);
            newSession.Connect();
            _sessionCache[key] = newSession;
            return newSession;
        }
    }

    /// <summary>
    /// 获取会话(私钥认证)
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    /// <param name="privateKey">私钥内容</param>
    /// <param name="passphrase">私钥密码</param>
    /// <returns>SSH会话</returns>
    public ISshSession GetSession(string host, int port, string user, byte[] privateKey, byte[]? passphrase)
    {
        var key = GetSessionKey(host, port, user);
        lock (_lock)
        {
            if (_sessionCache.TryGetValue(key, out var session) && session.IsConnected)
            {
                return session;
            }

            var newSession = JschUtil.GetSession(host, port, user, privateKey, passphrase);
            newSession.Connect();
            _sessionCache[key] = newSession;
            return newSession;
        }
    }

    /// <summary>
    /// 移除会话
    /// </summary>
    /// <param name="host">主机</param>
    /// <param name="port">端口</param>
    /// <param name="user">用户名</param>
    public void RemoveSession(string host, int port, string user)
    {
        var key = GetSessionKey(host, port, user);
        lock (_lock)
        {
            if (_sessionCache.TryGetValue(key, out var session))
            {
                session.Dispose();
                _sessionCache.Remove(key);
            }
        }
    }

    /// <summary>
    /// 清空所有会话
    /// </summary>
    public void Clear()
    {
        lock (_lock)
        {
            foreach (var session in _sessionCache.Values)
            {
                session.Dispose();
            }
            _sessionCache.Clear();
        }
    }

    private static string GetSessionKey(string host, int port, string user)
    {
        return $"{host}:{port}:{user}";
    }
}
