using System;

namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// 默认异常过滤器
    /// </summary>
    public class DefaultExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <param name="ex">异常</param>
        public void HandleException(HttpServerRequest request, HttpServerResponse response, Exception ex)
        {
            response.StatusCode = 500;
            response.ContentType = "text/plain";
            response.Write($"Internal Server Error: {ex.Message}");
        }
    }
}