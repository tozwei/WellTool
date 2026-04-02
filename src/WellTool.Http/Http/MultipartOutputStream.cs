using System.IO;

namespace WellTool.Http.Http
{
    /// <summary>
    /// 多部分输出流
    /// </summary>
    public class MultipartOutputStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly string _boundary;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerStream">内部流</param>
        /// <param name="boundary">边界</param>
        public MultipartOutputStream(Stream innerStream, string boundary)
        {
            _innerStream = innerStream;
            _boundary = boundary;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">计数</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// 写入边界
        /// </summary>
        public void WriteBoundary()
        {
            var boundaryBytes = System.Text.Encoding.UTF8.GetBytes($"--{_boundary}\r\n");
            _innerStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        }

        /// <summary>
        /// 写入结束边界
        /// </summary>
        public void WriteEndBoundary()
        {
            var boundaryBytes = System.Text.Encoding.UTF8.GetBytes($"--{_boundary}--\r\n");
            _innerStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">计数</param>
        /// <returns>读取的字节数</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// 寻求位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="origin">原点</param>
        /// <returns>新位置</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="value">长度</param>
        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Flush()
        {
            _innerStream.Flush();
        }

        /// <summary>
        /// 可读取
        /// </summary>
        public override bool CanRead => _innerStream.CanRead;

        /// <summary>
        /// 可写入
        /// </summary>
        public override bool CanWrite => _innerStream.CanWrite;

        /// <summary>
        /// 可寻求
        /// </summary>
        public override bool CanSeek => _innerStream.CanSeek;

        /// <summary>
        /// 长度
        /// </summary>
        public override long Length => _innerStream.Length;

        /// <summary>
        /// 位置
        /// </summary>
        public override long Position
        {
            get => _innerStream.Position;
            set => _innerStream.Position = value;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="disposing">是否释放</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _innerStream.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}