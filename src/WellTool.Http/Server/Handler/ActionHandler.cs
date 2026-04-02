using System;
using System.IO;
using System.Net;
using WellTool.Http.Server.Action;

namespace WellTool.Http.Server.Handler
{
    /// <summary>
    /// Action处理器，用于处理HTTP请求
    /// </summary>
    public class ActionHandler
    {
        private readonly IAction _action;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action">Action处理器</param>
        public ActionHandler(IAction action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        public void Handle(HttpServerRequest request, HttpServerResponse response)
        {
            _action.Handle(request, response);
        }

        /// <summary>
        /// 处理请求（使用HttpListenerContext）
        /// </summary>
        /// <param name="context">HttpListener上下文</param>
        public void Handle(HttpListenerContext context)
        {
            var request = new HttpServerRequest(context.Request);
            var response = new HttpServerResponse(context.Response);

            try
            {
                _action.Handle(request, response);
            }
            finally
            {
                response.Close();
            }
        }
    }
}
