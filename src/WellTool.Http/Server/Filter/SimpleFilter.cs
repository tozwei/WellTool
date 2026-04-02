namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// 简单过滤器基类
    /// </summary>
    public abstract class SimpleFilter : IHttpFilter
    {
        /// <summary>
        /// 过滤请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <returns>是否继续处理请求</returns>
        public bool DoFilter(HttpServerRequest request, HttpServerResponse response)
        {
            return true;
        }
    }
}