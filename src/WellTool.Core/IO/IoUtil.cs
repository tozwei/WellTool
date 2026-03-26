using System;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// IO工具类
    /// </summary>
    public static class IoUtil
    {
        /// <summary>
        /// 默认缓冲区大小
        /// </summary>
        public const int DEFAULT_BUFFER_SIZE = 8192;

        /// <summary>
        /// 读取输入流到字节数组
        /// </summary>
        /// <param name="stream">输入流</param>
        /// <returns>字节数组</returns>
        public static byte[] ReadBytes(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 读取输入流到字符串
        /// </summary>
        /// <param name="stream">输入流</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string ReadString(Stream stream, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 将字节数组写入输出流
        /// </summary>
        /// <param name="stream">输出流</param>
        /// <param name="bytes">字节数组</param>
        public static void WriteBytes(Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 将字符串写入输出流
        /// </summary>
        /// <param name="stream">输出流</param>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        public static void WriteString(Stream stream, string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 复制流
        /// </summary>
        /// <param name="input">输入流</param>
        /// <param name="output">输出流</param>
        /// <returns>复制的字节数</returns>
        public static long Copy(Stream input, Stream output)
        {
            return Copy(input, output, DEFAULT_BUFFER_SIZE);
        }

        /// <summary>
        /// 复制流（带缓冲区大小）
        /// </summary>
        /// <param name="input">输入流</param>
        /// <param name="output">输出流</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>复制的字节数</returns>
        public static long Copy(Stream input, Stream output, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            long totalBytes = 0;
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
                totalBytes += bytesRead;
            }
            return totalBytes;
        }

        /// <summary>
        /// 关闭流
        /// </summary>
        /// <param name="stream">流</param>
        public static void Close(Stream stream)
        {
            stream?.Close();
        }

        /// <summary>
        /// 关闭流（静默模式，忽略异常）
        /// </summary>
        /// <param name="stream">流</param>
        public static void CloseQuietly(Stream stream)
        {
            try
            {
                stream?.Close();
            }
            catch { }
        }

        /// <summary>
        /// 刷新并关闭流
        /// </summary>
        /// <param name="stream">流</param>
        public static void FlushAndClose(Stream stream)
        {
            if (stream != null)
            {
                stream.Flush();
                stream.Close();
            }
        }

        /// <summary>
        /// 刷新并关闭流（静默模式，忽略异常）
        /// </summary>
        /// <param name="stream">流</param>
        public static void FlushAndCloseQuietly(Stream stream)
        {
            try
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// 创建临时文件
        /// </summary>
        /// <returns>临时文件路径</returns>
        public static string CreateTempFile()
        {
            return Path.GetTempFileName();
        }

        /// <summary>
        /// 创建临时目录
        /// </summary>
        /// <returns>临时目录路径</returns>
        public static string CreateTempDirectory()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns>临时目录路径</returns>
        public static string GetTempDirectory()
        {
            return Path.GetTempPath();
        }
    }
}