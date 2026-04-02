using System;
using System.IO;
using System.Text;
using WellTool.Core.IO;

namespace WellTool.Core.Net.Multipart
{
    /// <summary>
    /// Http请求解析流，提供了专门针对带文件的form表单的解析
    /// </summary>
    public class MultipartRequestInputStream : BufferedStream
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="inputStream">输入流</param>
        public MultipartRequestInputStream(Stream inputStream) : base(inputStream)
        {
        }

        /// <summary>
        /// 读取byte字节流，在末尾抛出异常
        /// </summary>
        /// <returns>byte</returns>
        /// <exception cref="IOException">读取异常</exception>
        public byte ReadByte()
        {
            int i = base.ReadByte();
            if (i == -1)
            {
                throw new IOException("End of HTTP request stream reached");
            }
            return (byte)i;
        }

        /// <summary>
        /// 跳过指定位数的 bytes.
        /// </summary>
        /// <param name="i">跳过的byte数</param>
        /// <exception cref="IOException">IO异常</exception>
        public void SkipBytes(long i)
        {
            long len = base.Seek(i, SeekOrigin.Current);
            if (len != i)
            {
                throw new IOException("Unable to skip data in HTTP request");
            }
        }

        /// <summary>
        /// part部分边界
        /// </summary>
        protected byte[] Boundary;

        /// <summary>
        /// 输入流中读取边界
        /// </summary>
        /// <returns>边界</returns>
        /// <exception cref="IOException">读取异常</exception>
        public byte[] ReadBoundary()
        {
            using (var boundaryOutput = new MemoryStream(1024))
            {
                byte b;
                // skip optional whitespaces
                while ((b = ReadByte()) <= ' ')
                {
                }
                boundaryOutput.WriteByte(b);

                // now read boundary chars
                while ((b = ReadByte()) != '\r')
                {
                    boundaryOutput.WriteByte(b);
                }
                if (boundaryOutput.Length == 0)
                {
                    throw new IOException("Problems with parsing request: invalid boundary");
                }
                SkipBytes(1);
                Boundary = new byte[boundaryOutput.Length + 2];
                Array.Copy(boundaryOutput.ToArray(), 0, Boundary, 2, Boundary.Length - 2);
                Boundary[0] = (byte) '\r';
                Boundary[1] = (byte) '\n';
                return Boundary;
            }
        }

        /// <summary>
        /// 最后一个头部信息
        /// </summary>
        protected UploadFileHeader LastHeader;

        /// <summary>
        /// 获取最后一个头部信息
        /// </summary>
        /// <returns>最后一个头部信息</returns>
        public UploadFileHeader GetLastHeader()
        {
            return LastHeader;
        }

        /// <summary>
        /// 从流中读取文件头部信息， 如果达到末尾则返回null
        /// </summary>
        /// <param name="encoding">字符集</param>
        /// <returns>头部信息， 如果达到末尾则返回null</returns>
        /// <exception cref="IOException">读取异常</exception>
        public UploadFileHeader ReadDataHeader(Encoding encoding)
        {
            var dataHeader = ReadDataHeaderString(encoding);
            if (dataHeader != null)
            {
                LastHeader = new UploadFileHeader(dataHeader);
            }
            else
            {
                LastHeader = null;
            }
            return LastHeader;
        }

        /// <summary>
        /// 读取数据头信息字符串
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>数据头信息字符串</returns>
        /// <exception cref="IOException">IO异常</exception>
        protected string ReadDataHeaderString(Encoding encoding)
        {
            using (var data = new MemoryStream())
            {
                byte b;
                while (true)
                {
                    // end marker byte on offset +0 and +2 must be 13
                    if ((b = ReadByte()) != '\r')
                    {
                        data.WriteByte(b);
                        continue;
                    }
                    Mark(4);
                    SkipBytes(1);
                    int i = ReadByte();
                    if (i == -1)
                    {
                        // reached end of stream
                        return null;
                    }
                    if (i == '\r')
                    {
                        Reset();
                        break;
                    }
                    Reset();
                    data.WriteByte(b);
                }
                SkipBytes(3);
                return encoding == null ? Encoding.Default.GetString(data.ToArray()) : encoding.GetString(data.ToArray());
            }
        }

        /// <summary>
        /// 读取字节流，直到下一个boundary
        /// </summary>
        /// <param name="encoding">编码，null表示系统默认编码</param>
        /// <returns>读取的字符串</returns>
        /// <exception cref="IOException">读取异常</exception>
        public string ReadString(Encoding encoding)
        {
            using (var output = new FastByteArrayOutputStream())
            {
                Copy(output);
                return output.ToString(encoding);
            }
        }

        /// <summary>
        /// 字节流复制到out，直到下一个boundary
        /// </summary>
        /// <param name="output">输出流</param>
        /// <returns>复制的字节数</returns>
        /// <exception cref="IOException">读取异常</exception>
        public long Copy(Stream output)
        {
            long count = 0;
            while (true)
            {
                byte b = ReadByte();
                if (IsBoundary(b))
                {
                    break;
                }
                output.WriteByte(b);
                count++;
            }
            return count;
        }

        /// <summary>
        /// 复制字节流到out， 大于maxBytes或者文件末尾停止
        /// </summary>
        /// <param name="output">输出流</param>
        /// <param name="limit">最大字节数</param>
        /// <returns>复制的字节数</returns>
        /// <exception cref="IOException">读取异常</exception>
        public long Copy(Stream output, long limit)
        {
            long count = 0;
            while (true)
            {
                byte b = ReadByte();
                if (IsBoundary(b))
                {
                    break;
                }
                output.WriteByte(b);
                count++;
                if (count > limit)
                {
                    break;
                }
            }
            return count;
        }

        /// <summary>
        /// 跳过边界表示
        /// </summary>
        /// <returns>跳过的字节数</returns>
        /// <exception cref="IOException">读取异常</exception>
        public long SkipToBoundary()
        {
            long count = 0;
            while (true)
            {
                byte b = ReadByte();
                count++;
                if (IsBoundary(b))
                {
                    break;
                }
            }
            return count;
        }

        /// <summary>
        /// 检查是否为边界的标志
        /// </summary>
        /// <param name="b">byte</param>
        /// <returns>是否为边界的标志</returns>
        /// <exception cref="IOException">读取异常</exception>
        public bool IsBoundary(byte b)
        {
            int boundaryLen = Boundary.Length;
            Mark(boundaryLen + 1);
            int bpos = 0;
            while (b == Boundary[bpos])
            {
                b = ReadByte();
                bpos++;
                if (bpos == boundaryLen)
                {
                    return true; // boundary found!
                }
            }
            Reset();
            return false;
        }
    }
}