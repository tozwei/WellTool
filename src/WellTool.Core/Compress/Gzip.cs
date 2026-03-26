using System;
using System.IO;
using System.IO.Compression;

namespace WellTool.Core.Compress
{
    /// <summary>
    /// Gzip压缩工具类
    /// </summary>
    public static class Gzip
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
                using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
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
                using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        gzipStream.CopyTo(outputStream);
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
            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                inputStream.CopyTo(gzipStream);
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
            using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(outputStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }
    }
}