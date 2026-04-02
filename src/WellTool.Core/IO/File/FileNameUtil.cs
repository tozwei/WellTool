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
using System.Text.RegularExpressions;

namespace WellTool.Core.IO.File
{
    /// <summary>
    /// 文件名相关工具类
    /// </summary>
    public static class FileNameUtil
    {
        /// <summary>
        /// .java文件扩展名
        /// </summary>
        public static readonly string ExtJava = ".java";
        /// <summary>
        /// .class文件扩展名
        /// </summary>
        public static readonly string ExtClass = ".class";
        /// <summary>
        /// .jar文件扩展名
        /// </summary>
        public static readonly string ExtJar = ".jar";

        /// <summary>
        /// 类Unix路径分隔符
        /// </summary>
        public static readonly char UnixSeparator = '/';
        /// <summary>
        /// Windows路径分隔符
        /// </summary>
        public static readonly char WindowsSeparator = '\\';

        /// <summary>
        /// Windows下文件名中的无效字符
        /// </summary>
        private static readonly Regex FileNameInvalidPatternWin = new Regex("[\\/:*?\"<>|\r\n]");

        /// <summary>
        /// 特殊后缀
        /// </summary>
        private static readonly string[] SpecialSuffix = { "tar.bz2", "tar.Z", "tar.gz", "tar.xz" };

        /// <summary>
        /// 返回文件名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件名</returns>
        public static string GetName(FileInfo file)
        {
            return file?.Name;
        }

        /// <summary>
        /// 返回文件名<br>
        /// <pre>
        /// "d:/test/aaa" 返回 "aaa"
        /// "/test/aaa.jpg" 返回 "aaa.jpg"
        /// </pre>
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名</returns>
        public static string GetName(string filePath)
        {
            if (filePath == null)
            {
                return null;
            }
            int len = filePath.Length;
            if (len == 0)
            {
                return filePath;
            }
            if (IsFileSeparator(filePath[len - 1]))
            {
                // 以分隔符结尾的去掉结尾分隔符
                len--;
            }

            int begin = 0;
            char c;
            for (int i = len - 1; i > -1; i--)
            {
                c = filePath[i];
                if (IsFileSeparator(c))
                {
                    // 查找最后一个路径分隔符（/或者\）
                    begin = i + 1;
                    break;
                }
            }

            return filePath.Substring(begin, len - begin);
        }

        /// <summary>
        /// 获取文件后缀名，扩展名不带“.”
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>扩展名</returns>
        public static string GetSuffix(FileInfo file)
        {
            return ExtName(file);
        }

        /// <summary>
        /// 获得文件后缀名，扩展名不带“.”
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>扩展名</returns>
        public static string GetSuffix(string fileName)
        {
            return ExtName(fileName);
        }

        /// <summary>
        /// 返回主文件名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>主文件名</returns>
        public static string GetPrefix(FileInfo file)
        {
            return MainName(file);
        }

        /// <summary>
        /// 返回主文件名
        /// </summary>
        /// <param name="fileName">完整文件名</param>
        /// <returns>主文件名</returns>
        public static string GetPrefix(string fileName)
        {
            return MainName(fileName);
        }

        /// <summary>
        /// 返回主文件名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>主文件名</returns>
        public static string MainName(FileInfo file)
        {
            if (file == null)
            {
                return null;
            }
            if (file.Attributes.HasFlag(FileAttributes.Directory))
            {
                return file.Name;
            }
            return MainName(file.Name);
        }

        /// <summary>
        /// 返回主文件名
        /// </summary>
        /// <param name="fileName">完整文件名</param>
        /// <returns>主文件名</returns>
        public static string MainName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            int len = fileName.Length;
            if (len == 0)
            {
                return fileName;
            }

            // 多级扩展名的主文件名
            foreach (var specialSuffix in SpecialSuffix)
            {
                if (fileName.EndsWith("." + specialSuffix))
                {
                    return fileName.Substring(0, len - specialSuffix.Length - 1);
                }
            }

            if (IsFileSeparator(fileName[len - 1]))
            {
                len--;
            }

            int begin = 0;
            int end = len;
            char c;
            for (int i = len - 1; i >= 0; i--)
            {
                c = fileName[i];
                if (len == end && c == '.')
                {
                    // 查找最后一个文件名和扩展名的分隔符：.
                    end = i;
                }
                // 查找最后一个路径分隔符（/或者\），如果这个分隔符在.之后，则继续查找，否则结束
                if (IsFileSeparator(c))
                {
                    begin = i + 1;
                    break;
                }
            }

            return fileName.Substring(begin, end - begin);
        }

        /// <summary>
        /// 获取文件扩展名（后缀名），扩展名不带“.”
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>扩展名</returns>
        public static string ExtName(FileInfo file)
        {
            if (file == null)
            {
                return null;
            }
            if (file.Attributes.HasFlag(FileAttributes.Directory))
            {
                return null;
            }
            return ExtName(file.Name);
        }

        /// <summary>
        /// 获得文件的扩展名（后缀名），扩展名不带“.”
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>扩展名</returns>
        public static string ExtName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            int index = fileName.LastIndexOf('.');
            if (index == -1)
            {
                return string.Empty;
            }
            else
            {
                int secondToLastIndex = fileName.Substring(0, index).LastIndexOf('.');
                string substr = fileName.Substring(secondToLastIndex == -1 ? index : secondToLastIndex + 1);
                if (Array.Exists(SpecialSuffix, s => s.Equals(substr)))
                {
                    return substr;
                }

                string ext = fileName.Substring(index + 1);
                // 扩展名中不能包含路径相关的符号
                return ext.Contains(UnixSeparator) || ext.Contains(WindowsSeparator) ? string.Empty : ext;
            }
        }

        /// <summary>
        /// 清除文件名中的在Windows下不支持的非法字符，包括： \ / : * ? " &lt; &gt; |
        /// </summary>
        /// <param name="fileName">文件名（必须不包括路径，否则路径符将被替换）</param>
        /// <returns>清理后的文件名</returns>
        public static string CleanInvalid(string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? fileName : FileNameInvalidPatternWin.Replace(fileName, string.Empty);
        }

        /// <summary>
        /// 文件名中是否包含在Windows下不支持的非法字符，包括： \ / : * ? " &lt; &gt; |
        /// </summary>
        /// <param name="fileName">文件名（必须不包括路径，否则路径符将被替换）</param>
        /// <returns>是否包含非法字符</returns>
        public static bool ContainsInvalid(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && FileNameInvalidPatternWin.IsMatch(fileName);
        }

        /// <summary>
        /// 根据文件名检查文件类型，忽略大小写
        /// </summary>
        /// <param name="fileName">文件名，例如hutool.png</param>
        /// <param name="extNames">被检查的扩展名数组，同一文件类型可能有多种扩展名，扩展名不带“.”</param>
        /// <returns>是否是指定扩展名的类型</returns>
        public static bool IsType(string fileName, params string[] extNames)
        {
            string ext = ExtName(fileName);
            return extNames.Any(e => string.Equals(ext, e, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 判断是否为文件分隔符
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>是否为文件分隔符</returns>
        private static bool IsFileSeparator(char c)
        {
            return c == UnixSeparator || c == WindowsSeparator || c == Path.DirectorySeparatorChar;
        }
    }
}