using System;
using System.IO;
using System.Net;
using System.Text;

namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// 默认异常过滤器
    /// </summary>
    public class DefaultExceptionFilter : IExceptionFilter
    {
        private readonly bool _showStackTrace;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="showStackTrace">是否显示堆栈跟踪</param>
        public DefaultExceptionFilter(bool showStackTrace = true)
        {
            _showStackTrace = showStackTrace;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <param name="ex">异常</param>
        public void HandleException(HttpServerRequest request, HttpServerResponse response, Exception ex)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "text/html;charset=utf-8";

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\">");
            sb.AppendLine("<title>500 Internal Server Error</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 40px; }");
            sb.AppendLine("h1 { color: #d32f2f; }");
            sb.AppendLine(".error-info { background-color: #f5f5f5; padding: 15px; border-radius: 5px; }");
            sb.AppendLine(".stack-trace { background-color: #263238; color: #eceff1; padding: 15px; border-radius: 5px; overflow-x: auto; }");
            sb.AppendLine("pre { margin: 0; white-space: pre-wrap; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<h1>HTTP 500 - Internal Server Error</h1>");
            sb.AppendLine($"<p>Path: {WebUtility.HtmlEncode(request?.Path ?? "Unknown")}</p>");
            sb.AppendLine($"<p>Method: {request?.Method ?? "Unknown"}</p>");

            sb.AppendLine("<div class=\"error-info\">");
            sb.AppendLine($"<h3>Error: {WebUtility.HtmlEncode(ex.Message)}</h3>");
            sb.AppendLine("</div>");

            if (_showStackTrace)
            {
                sb.AppendLine("<h3>Stack Trace:</h3>");
                sb.AppendLine("<div class=\"stack-trace\">");
                sb.AppendLine("<pre>");
                sb.AppendLine(WebUtility.HtmlEncode(ex.ToString()));
                sb.AppendLine("</pre>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<hr>");
            sb.AppendLine("<p>Powered by WellTool HTTP Server</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            try
            {
                response.Write(sb.ToString());
            }
            catch
            {
                // 忽略写入错误
            }
        }
    }
}
