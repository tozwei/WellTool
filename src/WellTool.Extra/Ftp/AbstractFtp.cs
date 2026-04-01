using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.Extra.Ftp
{
    /// <summary>
    /// 抽象FTP类，用于定义通用的FTP方法
    /// </summary>
    public abstract class AbstractFtp : IDisposable
    {
        /// <summary>
        /// 默认编码
        /// </summary>
        public static readonly string DefaultCharset = "UTF-8";

        /// <summary>
        /// FTP配置
        /// </summary>
        protected FtpConfig FtpConfig { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">FTP配置</param>
        protected AbstractFtp(FtpConfig config)
        {
            this.FtpConfig = config;
        }

        /// <summary>
        /// 如果连接超时的话，重新进行连接
        /// </summary>
        /// <returns>this</returns>
        public abstract AbstractFtp ReconnectIfTimeout();

        /// <summary>
        /// 打开指定目录，具体逻辑取决于实现，例如在FTP中，进入失败返回false， SFTP中则抛出异常
        /// </summary>
        /// <param name="directory">目录路径</param>
        /// <returns>是否打开目录</returns>
        public abstract bool Cd(string directory);

        /// <summary>
        /// 打开上级目录
        /// </summary>
        /// <returns>是否打开目录</returns>
        public bool ToParent()
        {
            return Cd("..");
        }

        /// <summary>
        /// 远程当前目录（工作目录）
        /// </summary>
        /// <returns>远程当前目录</returns>
        public abstract string Pwd();

        /// <summary>
        /// 判断给定路径是否为目录
        /// </summary>
        /// <param name="dir">被判断的路径</param>
        /// <returns>是否为目录</returns>
        public bool IsDir(string dir)
        {
            var workDir = Pwd();
            try
            {
                return Cd(dir);
            }
            finally
            {
                Cd(workDir);
            }
        }

        /// <summary>
        /// 在当前远程目录（工作目录）下创建新的目录
        /// </summary>
        /// <param name="dir">目录名</param>
        /// <returns>是否创建成功</returns>
        public abstract bool Mkdir(string dir);

        /// <summary>
        /// 文件或目录是否存在
        /// </summary>
        /// <param name="path">目录</param>
        /// <returns>是否存在</returns>
        public bool Exist(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            // 目录验证
            if (IsDir(path))
            {
                return true;
            }

            if (Path.GetFileName(path) == string.Empty)
            {
                return false;
            }

            var fileName = Path.GetFileName(path);
            if (".".Equals(fileName) || "..".Equals(fileName))
            {
                return false;
            }

            // 文件验证
            var dir = string.IsNullOrEmpty(Path.GetDirectoryName(path)) ? "." : Path.GetDirectoryName(path);
            // 检查父目录为目录且是否存在
            if (!IsDir(dir))
            {
                return false;
            }

            List<string> names;
            try
            {
                names = Ls(dir);
            }
            catch (FtpException)
            {
                return false;
            }

            return ContainsIgnoreCase(names, fileName);
        }

        /// <summary>
        /// 遍历某个目录下所有文件和目录，不会递归遍历
        /// </summary>
        /// <param name="path">需要遍历的目录</param>
        /// <returns>文件和目录列表</returns>
        public abstract List<string> Ls(string path);

        /// <summary>
        /// 删除指定目录下的指定文件
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>是否存在</returns>
        public abstract bool DelFile(string path);

        /// <summary>
        /// 删除文件夹及其文件夹下的所有文件
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns>是否删除成功</returns>
        public abstract bool DelDir(string dirPath);

        /// <summary>
        /// 创建指定文件夹及其父目录，从根目录开始创建，创建完成后回到默认的工作目录
        /// </summary>
        /// <param name="dir">文件夹路径，绝对路径</param>
        public void MkDirs(string dir)
        {
            var dirs = dir.Trim().Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            var now = Pwd();

            if (dirs.Length > 0 && dir.StartsWith("/"))
            {
                // 首位为空，表示以/开头
                Cd("/");
            }

            foreach (var s in dirs)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    bool exist = true;
                    try
                    {
                        if (!Cd(s))
                        {
                            exist = false;
                        }
                    }
                    catch (FtpException)
                    {
                        exist = false;
                    }

                    if (!exist)
                    {
                        // 目录不存在时创建
                        Mkdir(s);
                        Cd(s);
                    }
                }
            }

            // 切换回工作目录
            Cd(now);
        }

        /// <summary>
        /// 将本地文件上传到目标服务器，目标文件名为destPath，若destPath为目录，则目标文件名将与file文件名相同。
        /// 覆盖模式
        /// </summary>
        /// <param name="destPath">服务端路径，可以为null 或者相对路径或绝对路径</param>
        /// <param name="file">需要上传的文件</param>
        /// <returns>是否成功</returns>
        public abstract bool Upload(string destPath, FileInfo file);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="outFile">输出文件或目录</param>
        public abstract void Download(string path, FileInfo outFile);

        /// <summary>
        /// 下载文件-避免未完成的文件
        /// 此方法原理是先在目标文件同级目录下创建临时文件，下载之，等下载完毕后重命名，避免因下载错误导致的文件不完整。
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="outFile">输出文件或目录</param>
        /// <param name="tempFileSuffix">临时文件后缀，默认".temp"</param>
        public void Download(string path, FileInfo outFile, string tempFileSuffix)
        {
            if (string.IsNullOrWhiteSpace(tempFileSuffix))
            {
                tempFileSuffix = ".temp";
            }
            else if (!tempFileSuffix.StartsWith("."))
            {
                tempFileSuffix = "." + tempFileSuffix;
            }

            // 目标文件真实名称
            var fileName = outFile.Attributes.HasFlag(FileAttributes.Directory) ? Path.GetFileName(path) : outFile.Name;
            // 临时文件名称
            var tempFileName = fileName + tempFileSuffix;

            // 临时文件
            var tempFile = new FileInfo(Path.Combine(outFile.Attributes.HasFlag(FileAttributes.Directory) ? outFile.FullName : outFile.DirectoryName, tempFileName));
            try
            {
                Download(path, tempFile);
                // 重命名下载好的临时文件
                if (tempFile.Exists)
                {
                    var finalFile = new FileInfo(Path.Combine(tempFile.DirectoryName, fileName));
                    if (finalFile.Exists)
                    {
                        finalFile.Delete();
                    }
                    tempFile.MoveTo(finalFile.FullName);
                }
            }
            catch (Exception e)
            {
                // 异常则删除临时文件
                if (tempFile.Exists)
                {
                    tempFile.Delete();
                }
                throw new FtpException(e);
            }
        }

        /// <summary>
        /// 递归下载FTP服务器上文件到本地(文件目录和服务器同步), 服务器上有新文件会覆盖本地文件
        /// </summary>
        /// <param name="sourcePath">ftp服务器目录</param>
        /// <param name="destDir">本地目录</param>
        public abstract void RecursiveDownloadFolder(string sourcePath, DirectoryInfo destDir);

        /// <summary>
        /// 重命名文件/目录
        /// </summary>
        /// <param name="from">原路径</param>
        /// <param name="to">目标路径</param>
        public abstract void Rename(string from, string to);

        /// <summary>
        /// 无异常关闭
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// 是否包含指定字符串，忽略大小写
        /// </summary>
        /// <param name="names">文件或目录名列表</param>
        /// <param name="nameToFind">要查找的文件或目录名</param>
        /// <returns>是否包含</returns>
        private static bool ContainsIgnoreCase(List<string> names, string nameToFind)
        {
            if (names == null || names.Count == 0)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(nameToFind))
            {
                return false;
            }
            return names.Any(name => name.Equals(nameToFind, StringComparison.OrdinalIgnoreCase));
        }
    }
}