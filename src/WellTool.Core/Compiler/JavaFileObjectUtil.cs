using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.Core.Compiler
{
    /// <summary>
    /// 封装 JavaFileObject 相关工具类
    /// </summary>
    public static class JavaFileObjectUtil
    {
        /// <summary>
        /// 获取指定文件（或目录）下的所有待编译的 Java 文件，并返回 JavaFileObject 列表
        /// 支持 .java、.jar、.zip 文件
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <returns>Java 文件对象列表</returns>
        public static List<FileInfo> GetJavaFileObjects(FileInfo file)
        {
            var result = new List<FileInfo>();
            
            if (!file.Exists)
            {
                return result;
            }

            if (file.Extension.Equals(".java", StringComparison.OrdinalIgnoreCase))
            {
                result.Add(file);
            }
            else if (file.Extension.Equals(".jar", StringComparison.OrdinalIgnoreCase) || 
                     file.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                // 支持 jar 和 zip 文件
                result.Add(file);
            }
            else if (file.Exists && File.GetAttributes(file.FullName).HasFlag(FileAttributes.Directory))
            {
                // 目录，递归获取
                var dir = new DirectoryInfo(file.FullName);
                result.AddRange(dir.GetFiles("*.java", SearchOption.AllDirectories));
            }

            return result;
        }

        /// <summary>
        /// 判断指定文件名是否为 JAR 或 ZIP 文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否为 JAR 或 ZIP 文件</returns>
        public static bool IsJarOrZipFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            string ext = Path.GetExtension(fileName).ToLower();
            return ext == ".jar" || ext == ".zip";
        }

        /// <summary>
        /// 判断指定文件名是否为 Java 源文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否为 Java 源文件</returns>
        public static bool IsJavaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWith(".java", StringComparison.OrdinalIgnoreCase);
        }
    }
}