using System;
using System.IO;
using System.IO.Compression;

namespace WellTool.Core.Util
{
    /// <summary>
    /// ZIP 工具类
    /// </summary>
    public class ZipUtil
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="targetZip">目标 ZIP 文件路径</param>
        public static void CompressFile(string sourceFile, string targetZip)
        {
            using var zipArchive = ZipFile.Open(targetZip, ZipArchiveMode.Create);
            zipArchive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile));
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="sourceDir">源目录路径</param>
        /// <param name="targetZip">目标 ZIP 文件路径</param>
        public static void CompressDirectory(string sourceDir, string targetZip)
        {
            ZipFile.CreateFromDirectory(sourceDir, targetZip);
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="sourceZip">源 ZIP 文件路径</param>
        /// <param name="targetDir">目标目录路径</param>
        public static void ExtractToDirectory(string sourceZip, string targetDir)
        {
            ZipFile.ExtractToDirectory(sourceZip, targetDir);
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data">要压缩的字节数组</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] Compress(byte[] data)
        {
            using var memoryStream = new MemoryStream();
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
            gzipStream.Write(data, 0, data.Length);
            gzipStream.Flush();
            gzipStream.Close();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="data">要解压缩的字节数组</param>
        /// <returns>解压缩后的字节数组</returns>
        public static byte[] Decompress(byte[] data)
        {
            using var memoryStream = new MemoryStream(data);
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="text">要压缩的字符串</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressString(string text)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(text);
            return Compress(data);
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="data">要解压缩的字节数组</param>
        /// <returns>解压缩后的字符串</returns>
        public static string DecompressString(byte[] data)
        {
            var decompressedData = Decompress(data);
            return System.Text.Encoding.UTF8.GetString(decompressedData);
        }
    }
}