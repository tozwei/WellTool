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
    /// 基于快速缓冲FastByteBuffer的OutputStream，随着数据的增长自动扩充缓冲区
    /// <p>
    /// 可以通过{@link #ToArray()}和 {@link #ToString()}来获取数据
    /// <p>
    /// {@link #Close()}方法无任何效果，当流被关闭后不会抛出IOException
    /// <p>
    /// 这种设计避免重新分配内存块而是分配新增的缓冲区，缓冲区不会被GC，数据也不会被拷贝到其他缓冲区。
    /// </summary>
    public class FastByteArrayOutputStream : Stream
    {
        private readonly FastByteBuffer _buffer;

        /// <summary>
        /// 构造
        /// </summary>
        public FastByteArrayOutputStream() : this(1024)
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="size">预估大小</param>
        public FastByteArrayOutputStream(int size)
        {
            _buffer = new FastByteBuffer(size);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _buffer.Append(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            _buffer.Append(value);
        }

        public int Size()
        {
            return _buffer.Size();
        }

        /// <summary>
        /// 此方法无任何效果，当流被关闭后不会抛出IOException
        /// </summary>
        public override void Close()
        {
            // nop
        }

        public void Reset()
        {
            _buffer.Reset();
        }

        /// <summary>
        /// 写出
        /// </summary>
        /// <param name="outputStream">输出流</param>
        /// <exception cref="IORuntimeException">IO异常</exception>
        public void WriteTo(Stream outputStream)
        {
            int index = _buffer.Index();
            if (index < 0)
            {
                // 无数据写出
                return;
            }
            byte[] buf;
            try
            {
                for (int i = 0; i < index; i++)
                {
                    buf = _buffer.GetArray(i);
                    outputStream.Write(buf, 0, buf.Length);
                }
                outputStream.Write(_buffer.GetArray(index), 0, _buffer.Offset());
            }
            catch (IOException e)
            {
                throw new IORuntimeException(e);
            }
        }

        /// <summary>
        /// 转为Byte数组
        /// </summary>
        /// <returns>Byte数组</returns>
        public byte[] ToByteArray()
        {
            return _buffer.ToArray();
        }

        public override string ToString()
        {
            return ToString(Encoding.UTF8);
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="encodingName">编码</param>
        /// <returns>字符串</returns>
        public string ToString(string encodingName)
        {
            return ToString(Encoding.GetEncoding(encodingName));
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="encoding">编码,null表示默认编码</param>
        /// <returns>字符串</returns>
        public string ToString(Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(ToByteArray());
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => _buffer.Size();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            // nop
        }
    }
}