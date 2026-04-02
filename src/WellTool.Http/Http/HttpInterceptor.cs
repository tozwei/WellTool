namespace WellTool.Http.Http
{
    /// <summary>
    /// HTTP拦截器接口
    /// </summary>
    public interface IHttpInterceptor
    {
        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        void Intercept(HttpRequest request);
    }
}