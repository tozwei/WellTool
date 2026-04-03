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

using System.IO;
using System.Text;
using System.Linq;

namespace WellTool.Core.IO.File
{
    /// <summary>
    /// Path对象操作封装
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// 目录是否为空
        /// </summary>
        /// <param name="dirPath">目录</param>
        /// <returns>是否为空</returns>
        public static bool IsDirEmpty(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return true;
            }
            return !Directory.EnumerateFileSystemEntries(dirPath).Any();
        }

        /// <summary>
        /// 递归遍历目录以及子目录中的所有文件<br>
        /// 如果提供path为文件，直接返回过滤结果
        /// </summary>
        /// <param name="path">当前遍历文件或目录</param>
        /// <param name="fileFilter">文件过滤规则对象，选择要保留的文件，只对文件有效，不过滤目录，null表示接收全部文件</param>
        /// <returns>文件列表</returns>
        public static List<FileInfo> LoopFiles(string path, Func<FileInfo, bool> fileFilter = null)
        {
            return LoopFiles(path, -1, fileFilter);
        }

        /// <summary>
        /// 递归遍历目录以及子目录中的所有文件<br>
        /// 如果提供path为文件，直接返回过滤结果
        /// </summary>
        /// <param name="path">当前遍历文件或目录</param>
        /// <param name="maxDepth">遍历最大深度，-1表示遍历到没有目录为止</param>
        /// <param name="fileFilter">文件过滤规则对象，选择要保留的文件，只对文件有效，不过滤目录，null表示接收全部文件</param>
        /// <returns>文件列表</returns>
        public static List<FileInfo> LoopFiles(string path, int maxDepth, Func<FileInfo, bool> fileFilter = null)
        {
            return LoopFiles(path, maxDepth, false, fileFilter);
        }

        /// <summary>
        /// 递归遍历目录以及子目录中的所有文件<br>
        /// 如果提供path为文件，直接返回过滤结果
        /// </summary>
        /// <param name="path">当前遍历文件或目录</param>
        /// <param name="maxDepth">遍历最大深度，-1表示遍历到没有目录为止</param>
        /// <param name="isFollowLinks">是否跟踪软链（快捷方式）</param>
        /// <param name="fileFilter">文件过滤规则对象，选择要保留的文件，只对文件有效，不过滤目录，null表示接收全部文件</param>
        /// <returns>文件列表</returns>
        public static List<FileInfo> LoopFiles(string path, int maxDepth, bool isFollowLinks, Func<FileInfo, bool> fileFilter = null)
        {
            var fileList = new List<FileInfo>();

            if (!Exists(path, isFollowLinks))
            {
                return fileList;
            }

            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists && !fileInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                if (fileFilter == null || fileFilter(fileInfo))
                {
                    fileList.Add(fileInfo);
                }
                return fileList;
            }

            SearchOption searchOption = maxDepth == 0 ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            try
            {
                foreach (var file in Directory.EnumerateFiles(path, "*", searchOption))
                {
                    var fi = new FileInfo(file);
                    if (fileFilter == null || fileFilter(fi))
                    {
                        fileList.Add(fi);
                    }
                }
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }

            return fileList;
        }

        /// <summary>
        /// 删除文件或者文件夹，不追踪软链<br>
        /// 注意：删除文件夹时不会判断文件夹是否为空，如果不空则递归删除子文件或文件夹<br>
        /// 某个文件删除失败会终止删除操作
        /// </summary>
        /// <param name="path">文件对象</param>
        /// <returns>成功与否</returns>
        public static bool Del(string path)
        {
            if (string.IsNullOrEmpty(path) || !Exists(path))
            {
                return true;
            }

            try
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                {
                    Directory.Delete(path, true);
                }
                else
                {
                    DelFile(path);
                }
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
            return true;
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="src">源文件路径</param>
        /// <param name="target">目标文件或目录，如果为目录使用与源文件相同的文件名</param>
        /// <param name="overwrite">是否覆盖</param>
        /// <returns>目标路径</returns>
        public static string CopyFile(string src, string target, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException("src", "Source File is null !");
            }
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target", "Destination File or directory is null !");
            }

            var targetPath = IsDirectory(target) ? Path.Combine(target, Path.GetFileName(src)) : target;
            // 创建级联父目录
            MkParentDirs(targetPath);

            try
            {
                System.IO.File.Copy(src, targetPath, overwrite);
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }

            return targetPath;
        }

        /// <summary>
        /// 拷贝文件或目录，拷贝规则为：
        /// <ul>
        ///     <li>源文件为目录，目标也为目录或不存在，则拷贝整个目录到目标目录下</li>
        ///     <li>源文件为文件，目标为目录或不存在，则拷贝文件到目标目录下</li>
        ///     <li>源文件为文件，目标也为文件，则在overwrite为true的情况下覆盖之</li>
        /// </ul>
        /// </summary>
        /// <param name="src">源文件路径，如果为目录会在目标中创建新目录</param>
        /// <param name="target">目标文件或目录，如果为目录使用与源文件相同的文件名</param>
        /// <param name="overwrite">是否覆盖</param>
        /// <returns>目标路径</returns>
        public static string Copy(string src, string target, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException("src", "Src path must be not null !");
            }
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target", "Target path must be not null !");
            }

            if (IsDirectory(src))
            {
                return CopyContent(src, Path.Combine(target, Path.GetFileName(src)), overwrite);
            }
            return CopyFile(src, target, overwrite);
        }

        /// <summary>
        /// 拷贝目录下的所有文件或目录到目标目录中，此方法不支持文件对文件的拷贝。
        /// <ul>
        ///     <li>源文件为目录，目标也为目录或不存在，则拷贝目录下所有文件和目录到目标目录下</li>
        ///     <li>源文件为文件，目标为目录或不存在，则拷贝文件到目标目录下</li>
        /// </ul>
        /// </summary>
        /// <param name="src">源文件路径，如果为目录只在目标中创建新目录</param>
        /// <param name="target">目标目录，如果为目录使用与源文件相同的文件名</param>
        /// <param name="overwrite">是否覆盖</param>
        /// <returns>目标路径</returns>
        public static string CopyContent(string src, string target, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException("src", "Src path must be not null !");
            }
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target", "Target path must be not null !");
            }

            try
            {
                Mkdir(target);
                foreach (var file in Directory.EnumerateFiles(src, "*", SearchOption.AllDirectories))
                {
                    var relativePath = Path.GetRelativePath(src, file);
                    var targetFilePath = Path.Combine(target, relativePath);
                    MkParentDirs(targetFilePath);
                    System.IO.File.Copy(file, targetFilePath, overwrite);
                }
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
            return target;
        }

        /// <summary>
        /// 判断是否为目录，如果path为null或空，则返回false<br>
        /// 此方法不会追踪到软链对应的真实地址，即软链被当作文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>如果为目录true</returns>
        public static bool IsDirectory(string path)
        {
            return IsDirectory(path, false);
        }

        /// <summary>
        /// 判断是否为目录，如果path为null或空，则返回false
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isFollowLinks">是否追踪到软链对应的真实地址</param>
        /// <returns>如果为目录true</returns>
        public static bool IsDirectory(string path, bool isFollowLinks)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                var fileInfo = new FileInfo(path);
                if (isFollowLinks && fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    // 处理软链接
                    var targetPath = ResolveSymlink(path);
                    return IsDirectory(targetPath, isFollowLinks);
                }
                return fileInfo.Attributes.HasFlag(FileAttributes.Directory);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取指定位置的子路径部分，支持负数，例如index为-1表示从后数第一个节点位置
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="index">路径节点位置，支持负数（负数从后向前计数）</param>
        /// <returns>获取的子路径</returns>
        public static string GetPathEle(string path, int index)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var parts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var len = parts.Length;
            if (index < 0)
            {
                index = len + index;
                if (index < 0)
                {
                    index = 0;
                }
            }
            else if (index >= len)
            {
                index = len - 1;
            }
            return parts[index];
        }

        /// <summary>
        /// 获取指定位置的最后一个子路径部分
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>获取的最后一个子路径</returns>
        public static string GetLastPathEle(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获得输入流
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>输入流</returns>
        public static FileStream GetInputStream(string path)
        {
            try
            {
                return new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
        }

        /// <summary>
        /// 获得一个文件读取器
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>StreamReader对象</returns>
        public static StreamReader GetUtf8Reader(string path)
        {
            return GetReader(path, Encoding.UTF8);
        }

        /// <summary>
        /// 获得一个文件读取器
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">字符集</param>
        /// <returns>StreamReader对象</returns>
        public static StreamReader GetReader(string path, Encoding encoding)
        {
            try
            {
                return new StreamReader(path, encoding);
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
        }

        /// <summary>
        /// 读取文件的所有内容为byte数组
        /// </summary>
        /// <param name="path">文件</param>
        /// <returns>byte数组</returns>
        public static byte[] ReadBytes(string path)
        {
            try
            {
                return System.IO.File.ReadAllBytes(path);
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
        }

        /// <summary>
        /// 获得输出流
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>输出流</returns>
        public static FileStream GetOutputStream(string path)
        {
            try
            {
                MkParentDirs(path);
                return new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }
        }

        /// <summary>
        /// 修改文件或目录的文件名，不变更路径，只是简单修改文件名<br>
        /// </summary>
        /// <param name="path">被修改的文件</param>
        /// <param name="newName">新的文件名，包括扩展名</param>
        /// <param name="isOverride">是否覆盖目标文件</param>
        /// <returns>目标文件路径</returns>
        public static string Rename(string path, string newName, bool isOverride)
        {
            var newPath = Path.Combine(Path.GetDirectoryName(path), newName);
            return Move(path, newPath, isOverride);
        }

        /// <summary>
        /// 移动文件或目录到目标中
        /// </summary>
        /// <param name="src">源文件或目录路径</param>
        /// <param name="target">目标路径，如果为目录，则移动到此目录下</param>
        /// <param name="isOverride">是否覆盖目标文件</param>
        /// <returns>目标文件路径</returns>
        public static string Move(string src, string target, bool isOverride)
        {
            if (string.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException("src", "Source path must be not null !");
            }
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target", "Target path must be not null !");
            }

            if (IsDirectory(target))
            {
                target = Path.Combine(target, Path.GetFileName(src));
            }

            if (System.IO.File.Exists(target) || Directory.Exists(target))
            {
                if (!isOverride)
                {
                    throw new IOException($"Target already exists: {target}");
                }
                Del(target);
            }

            MkParentDirs(target);

            try
            {
                if (IsDirectory(src))
                {
                    Directory.Move(src, target);
                }
                else
                {
                    System.IO.File.Move(src, target);
                }
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }

            return target;
        }

        /// <summary>
        /// 移动文件或目录内容到目标中
        /// </summary>
        /// <param name="src">源文件或目录路径</param>
        /// <param name="target">目标路径，如果为目录，则移动到此目录下</param>
        /// <param name="isOverride">是否覆盖目标文件</param>
        /// <returns>目标文件路径</returns>
        public static string MoveContent(string src, string target, bool isOverride)
        {
            if (string.IsNullOrEmpty(src))
            {
                throw new ArgumentNullException("src", "Source path must be not null !");
            }
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target", "Target path must be not null !");
            }

            Mkdir(target);

            foreach (var file in Directory.EnumerateFiles(src, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(src, file);
                var targetFilePath = Path.Combine(target, relativePath);
                MkParentDirs(targetFilePath);
                if (System.IO.File.Exists(targetFilePath))
                {
                    if (!isOverride)
                    {
                        continue;
                    }
                    System.IO.File.Delete(targetFilePath);
                }
                System.IO.File.Move(file, targetFilePath);
            }

            return target;
        }

        /// <summary>
        /// 检查两个文件是否是同一个文件<br>
        /// 所谓文件相同，是指路径是否指向同一个文件或文件夹
        /// </summary>
        /// <param name="file1">文件1</param>
        /// <param name="file2">文件2</param>
        /// <returns>是否相同</returns>
        public static bool Equals(string file1, string file2)
        {
            if (string.IsNullOrEmpty(file1) || string.IsNullOrEmpty(file2))
            {
                return false;
            }
            try
            {
                return string.Equals(Path.GetFullPath(file1), Path.GetFullPath(file2), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为文件，如果path为null或空，则返回false
        /// </summary>
        /// <param name="path">文件</param>
        /// <param name="isFollowLinks">是否跟踪软链（快捷方式）</param>
        /// <returns>如果为文件true</returns>
        public static bool IsFile(string path, bool isFollowLinks)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                var fileInfo = new FileInfo(path);
                if (isFollowLinks && fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    // 处理软链接
                    var targetPath = ResolveSymlink(path);
                    return IsFile(targetPath, isFollowLinks);
                }
                return !fileInfo.Attributes.HasFlag(FileAttributes.Directory);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为符号链接文件
        /// </summary>
        /// <param name="path">被检查的文件</param>
        /// <returns>是否为符号链接文件</returns>
        public static bool IsSymlink(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                var fileInfo = new FileInfo(path);
                return fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件或目录是否存在
        /// </summary>
        /// <param name="path">文件</param>
        /// <param name="isFollowLinks">是否跟踪软链（快捷方式）</param>
        /// <returns>是否存在</returns>
        public static bool Exists(string path, bool isFollowLinks = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                if (System.IO.File.Exists(path) || Directory.Exists(path))
                {
                    return true;
                }
                if (isFollowLinks && IsSymlink(path))
                {
                    var targetPath = ResolveSymlink(path);
                    return Exists(targetPath, isFollowLinks);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否存在且为非目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isFollowLinks">是否追踪到软链对应的真实地址</param>
        /// <returns>如果为非目录且存在true</returns>
        public static bool IsExistsAndNotDirectory(string path, bool isFollowLinks = false)
        {
            return Exists(path, isFollowLinks) && !IsDirectory(path, isFollowLinks);
        }

        /// <summary>
        /// 判断给定的目录是否为给定文件或文件夹的子目录
        /// </summary>
        /// <param name="parent">父目录</param>
        /// <param name="sub">子目录</param>
        /// <returns>子目录是否为父目录的子目录</returns>
        public static bool IsSub(string parent, string sub)
        {
            if (string.IsNullOrEmpty(parent) || string.IsNullOrEmpty(sub))
            {
                return false;
            }
            try
            {
                var parentPath = Path.GetFullPath(parent);
                var subPath = Path.GetFullPath(sub);
                return subPath.StartsWith(parentPath, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将路径转换为标准的绝对路径
        /// </summary>
        /// <param name="path">文件或目录路径</param>
        /// <returns>转换后的路径</returns>
        public static string ToAbsNormal(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            try
            {
                return Path.GetFullPath(path);
            }
            catch
            {
                return path;
            }
        }

        /// <summary>
        /// 获得文件的MimeType
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>MimeType</returns>
        public static string GetMimeType(string file)
        {
            if (string.IsNullOrEmpty(file) || !System.IO.File.Exists(file))
            {
                return null;
            }
            try
            {
                var extension = Path.GetExtension(file).ToLowerInvariant();
                switch (extension)
                {
                    case ".txt": return "text/plain";
                    case ".csv": return "text/csv";
                    case ".html": return "text/html";
                    case ".xml": return "application/xml";
                    case ".json": return "application/json";
                    case ".pdf": return "application/pdf";
                    case ".zip": return "application/zip";
                    case ".jpg":
                    case ".jpeg": return "image/jpeg";
                    case ".png": return "image/png";
                    case ".gif": return "image/gif";
                    case ".bmp": return "image/bmp";
                    case ".mp3": return "audio/mpeg";
                    case ".mp4": return "video/mp4";
                    case ".avi": return "video/x-msvideo";
                    case ".doc":
                    case ".docx": return "application/msword";
                    case ".xls":
                    case ".xlsx": return "application/vnd.ms-excel";
                    case ".ppt":
                    case ".pptx": return "application/vnd.ms-powerpoint";
                    default: return "application/octet-stream";
                }
            }
            catch
            {
                return "application/octet-stream";
            }
        }

        /// <summary>
        /// 创建所给目录及其父目录
        /// </summary>
        /// <param name="dir">目录</param>
        /// <returns>目录</returns>
        public static string Mkdir(string dir)
        {
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch (System.Exception e)
                {
                    throw new IORuntimeException(e);
                }
            }
            return dir;
        }

        /// <summary>
        /// 创建所给文件或目录的父目录
        /// </summary>
        /// <param name="path">文件或目录</param>
        /// <returns>父目录</returns>
        public static string MkParentDirs(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var parentDir = Path.GetDirectoryName(path);
            return Mkdir(parentDir);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件名</returns>
        public static string GetName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 创建临时文件
        /// </summary>
        /// <param name="prefix">前缀，至少3个字符</param>
        /// <param name="suffix">后缀，如果null则使用默认.tmp</param>
        /// <param name="dir">临时文件创建的所在目录</param>
        /// <returns>临时文件</returns>
        public static string CreateTempFile(string prefix, string suffix = null, string dir = null)
        {
            if (string.IsNullOrEmpty(prefix) || prefix.Length < 3)
            {
                throw new ArgumentException("Prefix must be at least 3 characters long");
            }

            int exceptionsCount = 0;
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(dir))
                    {
                        return System.IO.Path.GetTempFileName();
                    }
                    else
                    {
                        Mkdir(dir);
                        var tempFileName = $"{prefix}{Guid.NewGuid()}{(string.IsNullOrEmpty(suffix) ? ".tmp" : suffix)}";
                        return Path.Combine(dir, tempFileName);
                    }
                }
                catch (System.Exception e)
                {
                    if (++exceptionsCount >= 50)
                    {
                        throw new IORuntimeException(e);
                    }
                }
            }
        }

        /// <summary>
        /// 删除文件或空目录，不追踪软链
        /// </summary>
        /// <param name="path">文件对象</param>
        private static void DelFile(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch (System.Exception e)
            {
                // 可能遇到只读文件，无法删除
                var fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                    System.IO.File.Delete(path);
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 解析符号链接
        /// </summary>
        /// <param name="path">符号链接路径</param>
        /// <returns>符号链接指向的真实路径</returns>
        private static string ResolveSymlink(string path)
        {
            try
            {
                // 在Windows上，符号链接可以通过FileInfo.ResolveLinkTarget获取
                // 注意：ResolveLinkTarget在netstandard2.1中可能不可用
                var fileInfo = new FileInfo(path);
                // 由于ResolveLinkTarget在netstandard2.1中可能不可用，这里直接返回原始路径
                // 实际项目中可能需要使用P/Invoke或其他方式来处理符号链接
                return path;
            }
            catch
            {
                return path;
            }
        }
    }
}