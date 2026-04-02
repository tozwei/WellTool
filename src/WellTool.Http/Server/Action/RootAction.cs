namespace WellTool.Http.Server.Action
{
    /// <summary>
    /// 根路径处理器
    /// </summary>
    public class RootAction : IAction
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        public void Handle(HttpServerRequest request, HttpServerResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.Write("<html><body><h1>Welcome to WellTool HTTP Server</h1></body></html>");
        }
    }
}