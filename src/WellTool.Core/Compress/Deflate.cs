using System;
using System.IO;
using System.IO.Compression;

namespace WellTool.Core.Compress
{
    /// <summary>
    /// Deflate压缩工具类
    /// </summary>
    public static class Deflate
    {
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="bytes">要压缩的字节数组</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] Compress(byte[] bytes)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(outputStream, CompressionMode.Compress))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);
                }
                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="bytes">要解压的字节数组</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] Decompress(byte[] bytes)
        {
            using (var inputStream = new MemoryStream(bytes))
            {
                using (var deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        deflateStream.CopyTo(outputStream);
                        return outputStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="inputStream">输入流</param>
        /// <returns>压缩后的流</returns>
        public static Stream Compress(Stream inputStream)
        {
            var outputStream = new MemoryStream();
            using (var deflateStream = new DeflateStream(outputStream, CompressionMode.Compress, true))
            {
                inputStream.CopyTo(deflateStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }

        /// <summary>
        /// 解压流
        /// </summary>
        /// <param name="inputStream">输入流</param>
        /// <returns>解压后的流</returns>
        public static Stream Decompress(Stream inputStream)
        {
            var outputStream = new MemoryStream();
            using (var deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(outputStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }
    }
}