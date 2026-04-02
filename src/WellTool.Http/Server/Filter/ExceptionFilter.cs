using System;

namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// 异常过滤器接口
    /// </summary>
    public interface IExceptionFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <param name="ex">异常</param>
        void HandleException(HttpServerRequest request, HttpServerResponse response, Exception ex);
    }
}