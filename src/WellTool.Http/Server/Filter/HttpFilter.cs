namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// HTTP过滤器接口
    /// </summary>
    public interface IHttpFilter
    {
        /// <summary>
        /// 过滤请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <returns>是否继续处理请求</returns>
        bool DoFilter(HttpServerRequest request, HttpServerResponse response);
    }
}