using System.IO;

namespace WellTool.Http.Http
{
    /// <summary>
    /// HTTP资源
    /// </summary>
    public class HttpResource
    {
        private readonly Stream _stream;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream">资源流</param>
        public HttpResource(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// 获取资源流
        /// </summary>
        /// <returns>资源流</returns>
        public Stream GetStream()
        {
            return _stream;
        }

        /// <summary>
        /// 读取资源为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string ReadAsString()
        {
            if (_stream == null)
            {
                return string.Empty;
            }

            _stream.Position = 0;
            using var reader = new StreamReader(_stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 读取资源为字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public byte[] ReadAsBytes()
        {
            if (_stream == null)
            {
                return new byte[0];
            }

            _stream.Position = 0;
            using var memoryStream = new MemoryStream();
            _stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 关闭资源
        /// </summary>
        public void Close()
        {
            _stream?.Close();
        }
    }
}