using System.IO;
using System.Text;

namespace WellTool.Http.Http
{
    /// <summary>
    /// HTTP下载器
    /// </summary>
    public class HttpDownloader
    {
        private readonly HttpRequest _request;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="request">HTTP请求</param>
        public HttpDownloader(HttpRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// 下载文件到指定路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否下载成功</returns>
        public bool Download(string filePath)
        {
            var response = _request.Execute();
            response.WriteBody(filePath);
            return true;
        }

        /// <summary>
        /// 下载文件到流
        /// </summary>
        /// <param name="stream">目标流</param>
        /// <returns>是否下载成功</returns>
        public bool Download(Stream stream)
        {
            var response = _request.Execute();
            var bytes = response.BodyBytes();
            if (bytes == null)
            {
                return false;
            }

            stream.Write(bytes, 0, bytes.Length);
            return true;
        }
    }
}