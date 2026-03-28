using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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

        /// <summary>
        /// 创建FileInfo对象
        /// </summary>
        /// <param name="paths">路径部分</param>
        /// <returns>FileInfo对象</returns>
        public static FileInfo GetFile(params string[] paths)
        {
            var path = Path.Combine(paths);
            return new FileInfo(path);
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>绝对路径</returns>
        public static string GetAbsolutePath(string path)
        {
            if (IsAbsolutePath(path))
            {
                return path;
            }
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 比较两个文件是否相等
        /// </summary>
        /// <param name="file1">文件1</param>
        /// <param name="file2">文件2</param>
        /// <returns>是否相等</returns>
        public static bool Equals(FileInfo file1, FileInfo file2)
        {
            if (file1 == null && file2 == null)
            {
                return true;
            }
            if (file1 == null || file2 == null)
            {
                return false;
            }
            if (!file1.Exists && !file2.Exists)
            {
                return file1.FullName.Equals(file2.FullName, StringComparison.OrdinalIgnoreCase);
            }
            if (!file1.Exists || !file2.Exists)
            {
                return false;
            }
            return file1.FullName.Equals(file2.FullName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 规范化路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>规范化后的路径</returns>
        public static string Normalize(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            // 简化实现，实际可能需要更复杂的处理
            return Path.GetFullPath(path).Replace('\\', '/');
        }

        /// <summary>
        /// 获取子路径
        /// </summary>
        /// <param name="parentPath">父路径</param>
        /// <param name="childPath">子路径</param>
        /// <returns>子路径</returns>
        public static string SubPath(string parentPath, string childPath)
        {
            var parentFullPath = Path.GetFullPath(parentPath).TrimEnd(Path.DirectorySeparatorChar);
            var childFullPath = Path.GetFullPath(childPath).TrimEnd(Path.DirectorySeparatorChar);
            if (childFullPath.StartsWith(parentFullPath, StringComparison.OrdinalIgnoreCase))
            {
                // 检查长度是否足够
                if (childFullPath.Length > parentFullPath.Length + 1)
                {
                    return childFullPath.Substring(parentFullPath.Length + 1).Replace('\\', '/');
                }
                return string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="level">层级</param>
        /// <returns>父目录</returns>
        public static FileInfo GetParent(FileInfo file, int level)
        {
            if (file == null || level < 0)
            {
                return file;
            }
            var directory = file.Directory;
            for (int i = 0; i < level && directory != null; i++)
            {
                directory = directory.Parent;
            }
            return directory == null ? null : new FileInfo(directory.FullName);
        }

        /// <summary>
        /// 最后一个分隔符的索引
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>索引</returns>
        public static int LastIndexOfSeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return -1;
            }
            var lastBackslash = path.LastIndexOf('\\');
            var lastSlash = path.LastIndexOf('/');
            return Math.Max(lastBackslash, lastSlash);
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>名称</returns>
        public static string GetName(string path)
        {
            var index = LastIndexOfSeparator(path);
            if (index == -1)
            {
                return path;
            }
            return path.Substring(index + 1).TrimEnd('/').TrimEnd('\\');
        }

        /// <summary>
        /// 获取主名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>主名称</returns>
        public static string MainName(string path)
        {
            var name = GetName(path);
            var dotIndex = name.LastIndexOf('.');
            if (dotIndex == -1)
            {
                return name;
            }
            return name.Substring(0, dotIndex);
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>扩展名</returns>
        public static string ExtName(string path)
        {
            var name = GetName(path);
            var dotIndex = name.LastIndexOf('.');
            if (dotIndex == -1)
            {
                return string.Empty;
            }
            return name.Substring(dotIndex + 1);
        }

        /// <summary>
        /// 获取MIME类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>MIME类型</returns>
        public static string GetMimeType(string fileName)
        {
            var extension = ExtName(fileName).ToLower();
            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                case "html":
                    return "text/html";
                case "css":
                    return "text/css";
                case "js":
                    return "text/javascript";
                case "doc":
                    return "application/msword";
                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "xls":
                    return "application/vnd.ms-excel";
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "ppt":
                    return "application/vnd.ms-powerpoint";
                case "pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "pdf":
                    return "application/pdf";
                case "zip":
                    return "application/zip";
                case "rar":
                    return "application/x-rar-compressed";
                case "wgt":
                    return "application/widget";
                case "webp":
                    return "image/webp";
                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// 判断是否为子路径
        /// </summary>
        /// <param name="parent">父路径</param>
        /// <param name="sub">子路径</param>
        /// <returns>是否为子路径</returns>
        public static bool IsSub(FileInfo parent, FileInfo sub)
        {
            if (parent == null || sub == null)
            {
                throw new ArgumentNullException(parent == null ? nameof(parent) : nameof(sub));
            }
            var parentFullPath = Path.GetFullPath(parent.FullName).TrimEnd(Path.DirectorySeparatorChar);
            var subFullPath = Path.GetFullPath(sub.FullName).TrimEnd(Path.DirectorySeparatorChar);
            
            // 确保父路径是子路径的真正前缀，需要考虑路径分隔符
            var parentWithSeparator = parentFullPath + Path.DirectorySeparatorChar;
            return subFullPath.Equals(parentFullPath, StringComparison.OrdinalIgnoreCase) || 
                   subFullPath.StartsWith(parentWithSeparator, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取文件总行数
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>总行数</returns>
        public static int GetTotalLines(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return 0;
            }
            using (var reader = new StreamReader(file.FullName))
            {
                int lines = 0;
                while (reader.ReadLine() != null)
                {
                    lines++;
                }
                return lines;
            }
        }

        /// <summary>
        /// 判断是否为绝对路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否为绝对路径</returns>
        public static bool IsAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            return Path.IsPathRooted(path);
        }

        /// <summary>
        /// 检查路径是否有跳转
        /// </summary>
        /// <param name="parent">父路径</param>
        /// <param name="sub">子路径</param>
        public static void CheckSlip(FileInfo parent, FileInfo sub)
        {
            if (!IsSub(parent, sub))
            {
                throw new ArgumentException("路径包含跳转，可能存在安全风险");
            }
        }

        /// <summary>
        /// 判断是否为Windows系统
        /// </summary>
        /// <returns>是否为Windows系统</returns>
        public static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        /// <summary>
        /// 获取用户主目录
        /// </summary>
        /// <returns>用户主目录</returns>
        public static string GetUserHomePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        /// <summary>
        /// 列出文件名
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>文件名数组</returns>
        public static string[] ListFileNames(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                directoryPath = ".";
            }
            if (!Directory.Exists(directoryPath))
            {
                return new string[0];
            }
            var files = Directory.GetFiles(directoryPath);
            var fileNames = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                fileNames[i] = GetFileName(files[i]);
            }
            return fileNames;
        }
    }
}