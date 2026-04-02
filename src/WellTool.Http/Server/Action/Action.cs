namespace WellTool.Http.Server.Action
{
    /// <summary>
    /// HTTP请求处理器接口
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        void Handle(HttpServerRequest request, HttpServerResponse response);
    }
}