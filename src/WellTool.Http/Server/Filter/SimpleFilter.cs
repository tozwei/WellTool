namespace WellTool.Http.Server.Filter
{
    /// <summary>
    /// 简单过滤器基类，提供基础实现
    /// </summary>
    public abstract class SimpleFilter : IHttpFilter
    {
        /// <summary>
        /// 过滤请求的预处理逻辑
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <returns>是否继续处理请求</returns>
        protected virtual bool PreFilter(HttpServerRequest request, HttpServerResponse response)
        {
            return true;
        }

        /// <summary>
        /// 过滤请求的后处理逻辑
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        protected virtual void PostFilter(HttpServerRequest request, HttpServerResponse response)
        {
        }

        /// <summary>
        /// 过滤请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        /// <returns>是否继续处理请求</returns>
        public virtual bool DoFilter(HttpServerRequest request, HttpServerResponse response)
        {
            var result = PreFilter(request, response);
            if (result)
            {
                PostFilter(request, response);
            }
            return result;
        }
    }
}
