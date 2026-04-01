using System;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 快速字节数组输出流
    /// </summary>
    public class FastByteArrayOutputStream : Stream
    {
        private byte[] _buffer;
        private int _position;
        private const int DefaultCapacity = 1024;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FastByteArrayOutputStream() : this(DefaultCapacity)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public FastByteArrayOutputStream(int capacity)
        {
            _buffer = new byte[capacity];
            _position = 0;
        }

        /// <summary>
        /// 写入一个字节
        /// </summary>
        /// <param name="value">字节值</param>
        public override void WriteByte(byte value)
        {
            EnsureCapacity(_position + 1);
            _buffer[_position++] = value;
        }

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">数量</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0 || count < 0 || offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            EnsureCapacity(_position + count);
            Array.Copy(buffer, offset, _buffer, _position, count);
            _position += count;
        }

        /// <summary>
        /// 确保容量
        /// </summary>
        /// <param name="minCapacity">最小容量</param>
        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity > _buffer.Length)
            {
                int newCapacity = Math.Max(_buffer.Length * 2, minCapacity);
                byte[] newBuffer = new byte[newCapacity];
                Array.Copy(_buffer, newBuffer, _position);
                _buffer = newBuffer;
            }
        }

        /// <summary>
        /// 重置流
        /// </summary>
        public void Reset()
        {
            _position = 0;
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public byte[] ToByteArray()
        {
            byte[] result = new byte[_position];
            Array.Copy(_buffer, result, _position);
            return result;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(_buffer, 0, _position);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public string ToString(Encoding encoding)
        {
            return encoding.GetString(_buffer, 0, _position);
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        public int Size => _position;

        #region Stream 接口实现
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => _position;
        public override long Position { get => _position; set => throw new NotSupportedException(); }
        public override void Flush() { }
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        #endregion
    }
}