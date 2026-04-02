using System;
using System.Net;
using System.Threading;

namespace WellTool.Http.Server
{
    /// <summary>
    /// HTTP服务器基类
    /// </summary>
    public abstract class HttpServerBase
    {
        protected HttpListener _listener;
        protected Thread _thread;
        protected bool _running;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听端口</param>
        public HttpServerBase(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://+:{port}/");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostname">监听地址</param>
        /// <param name="port">监听端口</param>
        public HttpServerBase(string hostname, int port)
        {
            _listener = new HttpListener();
            if (hostname == "*" || hostname == "+")
            {
                _listener.Prefixes.Add($"http://+:{port}/");
            }
            else
            {
                _listener.Prefixes.Add($"http://{hostname}:{port}/");
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            _running = true;
            _listener.Start();
            _thread = new Thread(Run);
            _thread.Start();
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            _running = false;
            _listener.Stop();
            _thread?.Join();
        }

        /// <summary>
        /// 等待服务器停止
        /// </summary>
        public void WaitForStop()
        {
            _thread?.Join();
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning => _running && _listener.IsListening;

        /// <summary>
        /// 获取监听地址
        /// </summary>
        public IPEndPoint ListenAddress
        {
            get
            {
                try
                {
                    var uri = new Uri(_listener.Prefixes.GetEnumerator().Current?.ToString());
                    return new IPEndPoint(IPAddress.Any, uri.Port);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 运行服务器循环
        /// </summary>
        protected abstract void Run();

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">请求上下文</param>
        protected abstract void HandleRequest(HttpListenerContext context);
    }
}
