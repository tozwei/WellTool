using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using WellTool.Http.Server.Action;
using WellTool.Http.Server.Filter;

namespace WellTool.Http.Server
{
    /// <summary>
    /// 简易HTTP服务器，基于HttpListener
    /// </summary>
    public class SimpleServer : HttpServerBase
    {
        private Action<HttpServerRequest, HttpServerResponse> _actionHandler;
        private readonly List<IHttpFilter> _filters = new List<IHttpFilter>();
        private readonly Dictionary<string, IAction> _actions = new Dictionary<string, IAction>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听端口</param>
        public SimpleServer(int port)
            : base(port)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostname">监听地址</param>
        /// <param name="port">监听端口</param>
        public SimpleServer(string hostname, int port)
            : base(hostname, port)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="handler">请求处理函数</param>
        public SimpleServer(int port, Action<HttpServerRequest, HttpServerResponse> handler)
            : base(port)
        {
            _actionHandler = handler;
        }

        /// <summary>
        /// 添加Action处理器
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="action">Action处理器</param>
        /// <returns>自身引用</returns>
        public SimpleServer AddAction(string path, IAction action)
        {
            // 确保路径以/开头
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            _actions[path] = action;
            return this;
        }

        /// <summary>
        /// 添加Action处理器（使用Lambda表达式）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="handler">处理函数</param>
        /// <returns>自身引用</returns>
        public SimpleServer AddAction(string path, Action<HttpServerRequest, HttpServerResponse> handler)
        {
            return AddAction(path, new LambdaAction(handler));
        }

        /// <summary>
        /// 添加HTTP过滤器
        /// </summary>
        /// <param name="filter">HTTP过滤器</param>
        /// <returns>自身引用</returns>
        public SimpleServer AddFilter(IHttpFilter filter)
        {
            _filters.Add(filter);
            return this;
        }

        /// <summary>
        /// 设置根目录，用于静态文件服务
        /// </summary>
        /// <param name="rootDir">根目录路径</param>
        /// <returns>自身引用</returns>
        public SimpleServer SetRoot(string rootDir)
        {
            return AddAction("/", new RootAction(rootDir));
        }

        /// <summary>
        /// 设置全局请求处理器（当没有匹配到Action时调用）
        /// </summary>
        /// <param name="handler">处理函数</param>
        /// <returns>自身引用</returns>
        public SimpleServer SetHandler(Action<HttpServerRequest, HttpServerResponse> handler)
        {
            _actionHandler = handler;
            return this;
        }

        /// <summary>
        /// 设置全局请求处理器
        /// </summary>
        /// <param name="action">Action处理器</param>
        /// <returns>自身引用</returns>
        public SimpleServer SetHandler(IAction action)
        {
            _actionHandler = (req, res) => action.Handle(req, res);
            return this;
        }

        /// <summary>
        /// 获取服务器地址
        /// </summary>
        public EndPoint Address
        {
            get
            {
                try
                {
                    if (_listener.Prefixes.Count > 0)
                    {
                        var prefix = _listener.Prefixes.GetEnumerator().Current?.ToString();
                        if (!string.IsNullOrEmpty(prefix))
                        {
                            var uri = new Uri(prefix);
                            return new IPEndPoint(IPAddress.Any, uri.Port);
                        }
                    }
                }
                catch
                {
                    // ignore
                }
                return new IPEndPoint(IPAddress.Any, 0);
            }
        }

        protected override void Run()
        {
            while (_running)
            {
                try
                {
                    var context = _listener.GetContext();
                    ThreadPool.QueueUserWorkItem(_ => HandleRequest(context));
                }
                catch (System.Exception ex) when (!_running)
                {
                    // 服务器停止时忽略异常
                    break;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }
            }
        }

        protected override void HandleRequest(HttpListenerContext context)
        {
            var request = new HttpServerRequest(context.Request);
            var response = new HttpServerResponse(context.Response);

            try
            {
                // 执行过滤器链
                if (!ExecuteFilters(request, response))
                {
                    return;
                }

                // 查找匹配的Action
                var path = request.Path;
                if (_actions.TryGetValue(path, out var action))
                {
                    action.Handle(request, response);
                }
                else if (_actionHandler != null)
                {
                    _actionHandler(request, response);
                }
                else
                {
                    response.Send404("No handler found for path: " + path);
                }
            }
            catch (System.Exception ex)
            {
                HandleException(request, response, ex);
            }
            finally
            {
                response.Close();
            }
        }

        /// <summary>
        /// 执行过滤器链
        /// </summary>
        private bool ExecuteFilters(HttpServerRequest request, HttpServerResponse response)
        {
            foreach (var filter in _filters)
            {
                if (!filter.DoFilter(request, response))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        private void HandleException(HttpServerRequest request, HttpServerResponse response, Exception ex)
        {
            try
            {
                response.StatusCode = 500;
                response.ContentType = "text/html;charset=utf-8";
                response.Write($"<html><body><h1>500 Internal Server Error</h1><p>{System.Web.HttpUtility.HtmlEncode(ex.Message)}</p></body></html>");
            }
            catch
            {
                // 忽略响应错误
            }
        }

        /// <summary>
        /// Lambda表达式Action封装
        /// </summary>
        private class LambdaAction : IAction
        {
            private readonly Action<HttpServerRequest, HttpServerResponse> _handler;

            public LambdaAction(Action<HttpServerRequest, HttpServerResponse> handler)
            {
                _handler = handler;
            }

            public void Handle(HttpServerRequest request, HttpServerResponse response)
            {
                _handler(request, response);
            }
        }
    }
}
