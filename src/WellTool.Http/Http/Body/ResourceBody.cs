using System.IO;
using WellTool.Http.Body;

namespace WellTool.Http.Http.Body
{
    /// <summary>
    /// 资源类型的HTTP请求体
    /// </summary>
    public class ResourceBody : IRequestBody
    {
        private readonly Stream _resource;

        /// <summary>
        /// 创建HTTP请求体
        /// </summary>
        /// <param name="resource">资源流</param>
        /// <returns>ResourceBody</returns>
        public static ResourceBody Create(Stream resource)
        {
            return new ResourceBody(resource);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="resource">资源流</param>
        public ResourceBody(Stream resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// 写出数据到输出流
        /// </summary>
        /// <param name="outputStream">输出流</param>
        public void Write(Stream outputStream)
        {
            if (_resource != null)
            {
                _resource.CopyTo(outputStream);
            }
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (_resource == null)
            {
                return string.Empty;
            }

            _resource.Position = 0;
            using var reader = new StreamReader(_resource);
            return reader.ReadToEnd();
        }
    }
}