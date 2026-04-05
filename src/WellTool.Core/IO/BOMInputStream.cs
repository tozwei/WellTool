// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 读取带BOM头的流内容，{@code GetCharset()}方法调用后会得到BOM头的编码，且会去除BOM头<br>
    /// BOM定义：http://www.unicode.org/unicode/faq/utf_bom.html<br>
    /// <ul>
    /// <li>00 00 FE FF = UTF-32, big-endian</li>
    /// <li>FF FE 00 00 = UTF-32, little-endian</li>
    /// <li>EF BB BF = UTF-8</li>
    /// <li>FE FF = UTF-16, big-endian</li>
    /// <li>FF FE = UTF-16, little-endian</li>
    /// </ul>
    /// </summary>
    public class BOMInputStream : Stream
    {
        private readonly Stream _in;
        private bool _isInited = false;
        private readonly string _defaultCharset;
        private string _charset;
        private byte[] _buffer;
        private int _bufferPosition;

        private const int BomSize = 4;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="inputStream">流</param>
        public BOMInputStream(Stream inputStream) : this(inputStream, Encoding.UTF8.WebName)
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="inputStream">流</param>
        /// <param name="defaultCharset">默认编码</param>
        public BOMInputStream(Stream inputStream, string defaultCharset)
        {
            _in = inputStream;
            _defaultCharset = defaultCharset;
        }

        /// <summary>
        /// 获取默认编码
        /// </summary>
        /// <returns>默认编码</returns>
        public string GetDefaultCharset()
        {
            return _defaultCharset;
        }

        /// <summary>
        /// 获取BOM头中的编码
        /// </summary>
        /// <returns>编码</returns>
        public string GetCharset()
        {
            if (!_isInited)
            {
                try
                {
                    Init();
                }
                catch (IOException ex)
                {
                    throw new IORuntimeException(ex);
                }
            }
            return _charset;
        }

        public override void Close()
        {
            _isInited = true;
            _in.Close();
            base.Close();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!_isInited)
            {
                try
                {
                    Init();
                }
                catch (IOException ex)
                {
                    throw new IORuntimeException(ex);
                }
            }

            if (_buffer != null && _bufferPosition < _buffer.Length)
            {
                var bytesToRead = System.Math.Min(count, _buffer.Length - _bufferPosition);
                Array.Copy(_buffer, _bufferPosition, buffer, offset, bytesToRead);
                _bufferPosition += bytesToRead;

                if (_bufferPosition >= _buffer.Length)
                {
                    _buffer = null;
                    _bufferPosition = 0;
                }

                return bytesToRead;
            }

            return _in.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            if (!_isInited)
            {
                try
                {
                    Init();
                }
                catch (IOException ex)
                {
                    throw new IORuntimeException(ex);
                }
            }

            if (_buffer != null && _bufferPosition < _buffer.Length)
            {
                return _buffer[_bufferPosition++];
            }

            return _in.ReadByte();
        }

        /// <summary>
        /// Read-ahead four bytes and check for BOM marks. <br>
        /// Extra bytes are unread back to the stream, only BOM bytes are skipped.
        /// </summary>
        /// <exception cref="IOException">读取引起的异常</exception>
        protected void Init()
        {
            if (_isInited)
            {
                return;
            }

            var bom = new byte[BomSize];
            var n = _in.Read(bom, 0, bom.Length);

            if (n >= 4 && bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == 0xFE && bom[3] == 0xFF)
            {
                _charset = "UTF-32BE";
                var unread = n - 4;
                if (unread > 0)
                {
                    _buffer = new byte[unread];
                    Array.Copy(bom, 4, _buffer, 0, unread);
                }
            }
            else if (n >= 4 && bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
            {
                _charset = "UTF-32LE";
                var unread = n - 4;
                if (unread > 0)
                {
                    _buffer = new byte[unread];
                    Array.Copy(bom, 4, _buffer, 0, unread);
                }
            }
            else if (n >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            {
                _charset = "UTF-8";
                var unread = n - 3;
                if (unread > 0)
                {
                    _buffer = new byte[unread];
                    Array.Copy(bom, 3, _buffer, 0, unread);
                }
            }
            else if (n >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
            {
                _charset = "UTF-16BE";
                var unread = n - 2;
                if (unread > 0)
                {
                    _buffer = new byte[unread];
                    Array.Copy(bom, 2, _buffer, 0, unread);
                }
            }
            else if (n >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
            {
                _charset = "UTF-16LE";
                var unread = n - 2;
                if (unread > 0)
                {
                    _buffer = new byte[unread];
                    Array.Copy(bom, 2, _buffer, 0, unread);
                }
            }
            else
            {
                // Unicode BOM mark not found, unread all bytes
                _charset = _defaultCharset;
                if (n > 0)
                {
                    _buffer = new byte[n];
                    Array.Copy(bom, 0, _buffer, 0, n);
                }
            }

            _isInited = true;
        }

        public override bool CanRead => _in.CanRead;

        public override bool CanSeek => _in.CanSeek;

        public override bool CanWrite => _in.CanWrite;

        public override long Length => _in.Length;

        public override long Position
        {
            get => _in.Position;
            set => _in.Position = value;
        }

        public override void Flush()
        {
            _in.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _in.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _in.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _in.Write(buffer, offset, count);
        }
    }
}