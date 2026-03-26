using System;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 读取文件到字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>字节数组</returns>
        public static byte[] ReadBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string ReadString(string filePath, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return File.ReadAllText(filePath, encoding);
        }

        /// <summary>
        /// 写入字节数组到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="bytes">字节数组</param>
        public static void WriteBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// 写入字符串到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        public static void WriteString(string filePath, string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            File.WriteAllText(filePath, str, encoding);
        }

        /// <summary>
        /// 追加字符串到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        public static void AppendString(string filePath, string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            File.AppendAllText(filePath, str, encoding);
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destPath">目标文件路径</param>
        public static void Copy(string sourcePath, string destPath)
        {
            File.Copy(sourcePath, destPath, true);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destPath">目标文件路径</param>
        public static void Move(string sourcePath, string destPath)
        {
#if NET6_0_OR_GREATER
            File.Move(sourcePath, destPath, true);
#else
            // .NET Standard 2.1 实现
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }
            File.Move(sourcePath, destPath);
#endif
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetSize(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 获取文件名（不含扩展名）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名（不含扩展名）</returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 获取文件名（含扩展名）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名（含扩展名）</returns>
        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 获取文件目录
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件目录</returns>
        public static string GetDirectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// 创建目录（如果不存在）
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除</param>
        public static void DeleteDirectory(string directoryPath, bool recursive = false)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive);
            }
        }

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>是否存在</returns>
        public static bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 列出目录中的文件
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>文件路径数组</returns>
        public static string[] ListFiles(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetFiles(directoryPath, searchPattern, searchOption);
        }

        /// <summary>
        /// 列出目录中的子目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>目录路径数组</returns>
        public static string[] ListDirectories(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetDirectories(directoryPath, searchPattern, searchOption);
        }
    }
}