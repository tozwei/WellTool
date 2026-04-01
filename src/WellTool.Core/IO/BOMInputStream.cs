using System;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// BOM 输入流
    /// </summary>
    public class BOMInputStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly Encoding _encoding;
        private bool _bomRead;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream">内部流</param>
        public BOMInputStream(Stream stream) : this(stream, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream">内部流</param>
        /// <param name="defaultEncoding">默认编码</param>
        public BOMInputStream(Stream stream, Encoding defaultEncoding)
        {
            _innerStream = stream;
            _encoding = defaultEncoding ?? Encoding.UTF8;
            _bomRead = false;
        }

        /// <summary>
        /// 读取 BOM
        /// </summary>
        private void ReadBOM()
        {
            if (!_bomRead)
            {
                var bomBuffer = new byte[4];
                var bytesRead = _innerStream.Read(bomBuffer, 0, 4);
                
                // 检查 BOM
                if (bytesRead >= 3 && bomBuffer[0] == 0xEF && bomBuffer[1] == 0xBB && bomBuffer[2] == 0xBF)
                {
                    // UTF-8 BOM
                }
                else if (bytesRead >= 2 && bomBuffer[0] == 0xFF && bomBuffer[1] == 0xFE)
                {
                    // UTF-16 LE BOM
                }
                else if (bytesRead >= 2 && bomBuffer[0] == 0xFE && bomBuffer[1] == 0xFF)
                {
                    // UTF-16 BE BOM
                }
                else if (bytesRead >= 4 && bomBuffer[0] == 0x00 && bomBuffer[1] == 0x00 && bomBuffer[2] == 0xFE && bomBuffer[3] == 0xFF)
                {
                    // UTF-32 BE BOM
                }
                else if (bytesRead >= 4 && bomBuffer[0] == 0xFF && bomBuffer[1] == 0xFE && bomBuffer[2] == 0x00 && bomBuffer[3] == 0x00)
                {
                    // UTF-32 LE BOM
                }
                else
                {
                    // 没有 BOM，重置流位置
                    _innerStream.Position = 0;
                }
                
                _bomRead = true;
            }
        }

        /// <summary>
        /// 读取字节
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">数量</param>
        /// <returns>读取的字节数</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            ReadBOM();
            return _innerStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns>字节值</returns>
        public override int ReadByte()
        {
            ReadBOM();
            return _innerStream.ReadByte();
        }

        #region Stream 接口实现
        public override bool CanRead => _innerStream.CanRead;
        public override bool CanSeek => _innerStream.CanSeek;
        public override bool CanWrite => _innerStream.CanWrite;
        public override long Length => _innerStream.Length;
        public override long Position
        {
            get => _innerStream.Position;
            set => _innerStream.Position = value;
        }

        public override void Flush()
        {
            _innerStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            _innerStream.WriteByte(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _innerStream.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}