using System.Net;

namespace WellTool.Http.Http
{
    /// <summary>
    /// HTTP连接
    /// </summary>
    public class HttpConnection
    {
        private readonly HttpWebRequest _request;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="request">HTTP请求</param>
        public HttpConnection(HttpWebRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// 获取HTTP请求
        /// </summary>
        /// <returns>HTTP请求</returns>
        public HttpWebRequest GetRequest()
        {
            return _request;
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <returns>HTTP响应</returns>
        public HttpWebResponse Execute()
        {
            return (HttpWebResponse)_request.GetResponse();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            // HttpWebRequest 不需要显式关闭
        }
    }
}