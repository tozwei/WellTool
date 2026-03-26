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

using System;
using System.IO;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// IO 工具类
    /// </summary>
    public static class IOUtil
    {
        /// <summary>
        /// 默认缓冲区大小
        /// </summary>
        public const int DEFAULT_BUFFER_SIZE = 8192;

        /// <summary>
        /// 读取流中的所有内容为字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">编码，默认为 UTF-8</param>
        /// <returns>流中的内容</returns>
        public static string ReadString(Stream stream, Encoding encoding = null)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            encoding = encoding ?? Encoding.UTF8;
            using var reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 读取流中的所有内容为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>流中的内容</returns>
        public static byte[] ReadBytes(Stream stream)
        {
            if (stream == null)
            {
                return Array.Empty<byte>();
            }

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 向流中写入字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码，默认为 UTF-8</param>
        public static void WriteString(Stream stream, string content, Encoding encoding = null)
        {
            if (stream == null || string.IsNullOrEmpty(content))
            {
                return;
            }

            encoding = encoding ?? Encoding.UTF8;
            using var writer = new StreamWriter(stream, encoding);
            writer.Write(content);
        }

        /// <summary>
        /// 向流中写入字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="bytes">字节数组</param>
        public static void WriteBytes(Stream stream, byte[] bytes)
        {
            if (stream == null || bytes == null || bytes.Length == 0)
            {
                return;
            }

            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 复制流
        /// </summary>
        /// <param name="source">源流</param>
        /// <param name="destination">目标流</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>复制的字节数</returns>
        public static long Copy(Stream source, Stream destination, int bufferSize = DEFAULT_BUFFER_SIZE)
        {
            if (source == null || destination == null)
            {
                return 0;
            }

            var buffer = new byte[bufferSize];
            long totalBytes = 0;
            int bytesRead;
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
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
            stream?.Dispose();
        }

        /// <summary>
        /// 关闭读取器
        /// </summary>
        /// <param name="reader">读取器</param>
        public static void Close(TextReader reader)
        {
            reader?.Dispose();
        }

        /// <summary>
        /// 关闭写入器
        /// </summary>
        /// <param name="writer">写入器</param>
        public static void Close(TextWriter writer)
        {
            writer?.Dispose();
        }

        /// <summary>
        /// 创建临时文件
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns>临时文件路径</returns>
        public static string CreateTempFile(string prefix = "temp", string suffix = ".tmp")
        {
            string tempFileName = Path.GetTempFileName();
            string tempPath = Path.Combine(Path.GetTempPath(), $"{prefix}{Path.GetFileNameWithoutExtension(tempFileName)}{suffix}");
            File.Move(tempFileName, tempPath);
            return tempPath;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        public static void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite = false)
        {
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destinationFilePath, overwrite);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        public static void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            if (File.Exists(sourceFilePath))
            {
                File.Move(sourceFilePath, destinationFilePath);
            }
        }

        /// <summary>
        /// 读取文件内容为字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码，默认为 UTF-8</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string filePath, Encoding encoding = null)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            encoding = encoding ?? Encoding.UTF8;
            return File.ReadAllText(filePath, encoding);
        }

        /// <summary>
        /// 读取文件内容为字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容</returns>
        public static byte[] ReadFileBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return Array.Empty<byte>();
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 写入字符串到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码，默认为 UTF-8</param>
        public static void WriteFile(string filePath, string content, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            File.WriteAllText(filePath, content, encoding);
        }

        /// <summary>
        /// 写入字节数组到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="bytes">字节数组</param>
        public static void WriteFile(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// 追加字符串到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码，默认为 UTF-8</param>
        public static void AppendFile(string filePath, string content, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            File.AppendAllText(filePath, content, encoding);
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件是否存在</returns>
        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小，单位：字节</returns>
        public static long GetFileSize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            return new FileInfo(filePath).Length;
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string filePath)
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
        /// 获取文件所在目录
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件所在目录</returns>
        public static string GetDirectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// 创建目录
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
        /// 列出目录中的文件
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>文件路径列表</returns>
        public static string[] ListFiles(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Array.Empty<string>();
            }

            return Directory.GetFiles(directoryPath, searchPattern, searchOption);
        }

        /// <summary>
        /// 列出目录中的子目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>目录路径列表</returns>
        public static string[] ListDirectories(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Array.Empty<string>();
            }

            return Directory.GetDirectories(directoryPath, searchPattern, searchOption);
        }
    }
}
