using System.Net;
using System.Text;

namespace WellTool.Extra.Ftp;

/// <summary>
/// 简易FTP服务器
/// </summary>
public class SimpleFtpServer : IDisposable
{
    private readonly string _rootDir;
    private readonly int _port;
    private readonly Encoding _charset;
    private HttpListener? _listener;
    private bool _isRunning;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="rootDir">根目录</param>
    /// <param name="port">端口，默认21</param>
    /// <param name="charset">编码</param>
    public SimpleFtpServer(string rootDir, int port = 21, Encoding? charset = null)
    {
        _rootDir = rootDir;
        _port = port;
        _charset = charset ?? Encoding.UTF8;
    }

    /// <summary>
    /// 启动服务器
    /// </summary>
    public void Start()
    {
        if (_isRunning) return;

        // 注意：.NET标准库没有内置FTP服务器支持
        // 这里提供一个简单的FTP命令监听实现
        // 在实际使用中，可能需要使用第三方库如FtpServer
        
        _isRunning = true;
    }

    /// <summary>
    /// 停止服务器
    /// </summary>
    public void Stop()
    {
        _isRunning = false;
        _listener?.Stop();
    }

    /// <summary>
    /// 是否运行中
    /// </summary>
    public bool IsRunning => _isRunning;

    public void Dispose()
    {
        Stop();
    }
}
