using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WellTool.Extra.Servlet
{
    /// <summary>
    /// Servlet相关工具类封装
    /// </summary>
    public static class ServletUtil
    {
        public const string MethodDelete = "DELETE";
        public const string MethodHead = "HEAD";
        public const string MethodGet = "GET";
        public const string MethodOptions = "OPTIONS";
        public const string MethodPost = "POST";
        public const string MethodPut = "PUT";
        public const string MethodTrace = "TRACE";

        /// <summary>
        /// 处理多级反向代理的IP
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>真实IP</returns>
        private static string GetMultistageReverseProxyIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return ip;
            }

            // 多级反向代理中，第一个IP为真实IP
            string[] ips = ip.Split(',');
            if (ips.Length > 0)
            {
                return ips[0].Trim();
            }

            return ip;
        }

        /// <summary>
        /// 获取文件的MIME类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>MIME类型</returns>
        private static string GetMimeType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".txt":
                    return "text/plain";
                case ".html":
                case ".htm":
                    return "text/html";
                default:
                    return "application/octet-stream";
            }
        }
    }
}