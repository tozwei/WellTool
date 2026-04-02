using System;
using System.IO;
using System.Net.Mime;

namespace WellTool.Http.Server.Action
{
    /// <summary>
    /// 默认的根路径处理器，用于处理静态文件请求
    /// </summary>
    public class RootAction : IAction
    {
        /// <summary>
        /// 默认首页文件名
        /// </summary>
        public const string DefaultIndexFileName = "index.html";

        private readonly DirectoryInfo _rootDir;
        private readonly string[] _indexFileNames;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootDir">网页根目录路径</param>
        public RootAction(string rootDir)
            : this(new DirectoryInfo(rootDir), DefaultIndexFileName)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootDir">网页根目录</param>
        public RootAction(DirectoryInfo rootDir)
            : this(rootDir, DefaultIndexFileName)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootDir">网页根目录路径</param>
        /// <param name="indexFileNames">首页文件名列表</param>
        public RootAction(string rootDir, params string[] indexFileNames)
            : this(new DirectoryInfo(rootDir), indexFileNames)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootDir">网页根目录</param>
        /// <param name="indexFileNames">首页文件名列表</param>
        public RootAction(DirectoryInfo rootDir, params string[] indexFileNames)
        {
            _rootDir = rootDir;
            _indexFileNames = indexFileNames.Length > 0 ? indexFileNames : new[] { DefaultIndexFileName };
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="response">HTTP响应</param>
        public void Handle(HttpServerRequest request, HttpServerResponse response)
        {
            var path = request.Path;

            // 移除开头的斜杠
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            var file = new FileInfo(Path.Combine(_rootDir.FullName, path));

            if (file.Exists)
            {
                if (file.Exists && file.Attributes.HasFlag(FileAttributes.Directory))
                {
                    // 查找首页文件
                    foreach (var indexFileName in _indexFileNames)
                    {
                        var indexFile = new FileInfo(Path.Combine(file.FullName, indexFileName));
                        if (indexFile.Exists)
                        {
                            WriteFile(response, indexFile);
                            return;
                        }
                    }
                }
                else
                {
                    // 写入文件
                    WriteFile(response, file);
                    return;
                }
            }

            // 404
            response.Send404("404 Not Found!");
        }

        /// <summary>
        /// 写入文件到响应
        /// </summary>
        /// <param name="response">HTTP响应</param>
        /// <param name="file">文件</param>
        private void WriteFile(HttpServerResponse response, FileInfo file)
        {
            var contentType = GetContentType(file.Extension);
            var fileName = file.Name;

            response.SetContentType(contentType);
            response.SetContentLength(file.Length);

            // 设置下载文件名
            if (!contentType.StartsWith("text/") && !contentType.Contains("javascript"))
            {
                response.SetContentDisposition(fileName);
            }

            using var fs = file.OpenRead();
            response.Write(fs);
        }

        /// <summary>
        /// 根据文件扩展名获取Content-Type
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        /// <returns>Content-Type</returns>
        private static string GetContentType(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".html" or ".htm" => "text/html",
                ".css" => "text/css",
                ".js" => "application/javascript",
                ".json" => "application/json",
                ".xml" => "application/xml",
                ".txt" => "text/plain",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".svg" => "image/svg+xml",
                ".ico" => "image/x-icon",
                ".pdf" => "application/pdf",
                ".zip" => "application/zip",
                ".gz" or ".gzip" => "application/gzip",
                ".woff" => "font/woff",
                ".woff2" => "font/woff2",
                ".ttf" => "font/ttf",
                ".eot" => "application/vnd.ms-fontobject",
                ".otf" => "font/otf",
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".ogg" => "audio/ogg",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                _ => "application/octet-stream"
            };
        }
    }
}
