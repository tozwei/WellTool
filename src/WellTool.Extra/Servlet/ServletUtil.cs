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
        /// 获取客户端IP地址
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <returns>客户端IP地址</returns>
        public static string GetClientIp(object request)
        {
            // 实现获取客户端IP地址
            if (request == null)
            {
                return "127.0.0.1";
            }

            try
            {
                // 尝试使用反射获取IP地址
                var property = request.GetType().GetProperty("UserHostAddress");
                if (property != null)
                {
                    var ip = property.GetValue(request) as string;
                    if (!string.IsNullOrEmpty(ip))
                    {
                        return GetMultistageReverseProxyIp(ip);
                    }
                }

                // 尝试从请求头获取
                var header = GetHeader(request, "X-Forwarded-For");
                if (!string.IsNullOrEmpty(header))
                {
                    return GetMultistageReverseProxyIp(header);
                }

                header = GetHeader(request, "Proxy-Client-IP");
                if (!string.IsNullOrEmpty(header))
                {
                    return header;
                }

                header = GetHeader(request, "WL-Proxy-Client-IP");
                if (!string.IsNullOrEmpty(header))
                {
                    return header;
                }

                header = GetHeader(request, "HTTP_CLIENT_IP");
                if (!string.IsNullOrEmpty(header))
                {
                    return header;
                }

                header = GetHeader(request, "HTTP_X_FORWARDED_FOR");
                if (!string.IsNullOrEmpty(header))
                {
                    return GetMultistageReverseProxyIp(header);
                }
            }
            catch
            {
                // 忽略异常，返回默认值
            }

            return "127.0.0.1";
        }

        /// <summary>
        /// 判断是否为IE浏览器
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <returns>是否为IE</returns>
        public static bool IsIe(object request)
        {
            // 实现判断是否为IE浏览器
            if (request == null)
            {
                return false;
            }

            try
            {
                var userAgent = GetHeader(request, "User-Agent");
                if (!string.IsNullOrEmpty(userAgent))
                {
                    return userAgent.Contains("MSIE") || userAgent.Contains("Trident");
                }
            }
            catch
            {
                // 忽略异常，返回默认值
            }

            return false;
        }

        /// <summary>
        /// 获取请求头信息
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <param name="headerName">请求头名称</param>
        /// <returns>请求头值</returns>
        public static string GetHeader(object request, string headerName)
        {
            // 实现获取请求头信息
            if (request == null || string.IsNullOrEmpty(headerName))
            {
                return string.Empty;
            }

            try
            {
                // 尝试使用反射获取请求头
                var method = request.GetType().GetMethod("Headers");
                if (method != null)
                {
                    var headers = method.Invoke(request, null);
                    if (headers != null)
                    {
                        var getMethod = headers.GetType().GetMethod("Get", new[] { typeof(string) });
                        if (getMethod != null)
                        {
                            var value = getMethod.Invoke(headers, new object[] { headerName });
                            return value as string ?? string.Empty;
                        }
                    }
                }

                // 尝试直接获取Headers属性
                var property = request.GetType().GetProperty("Headers");
                if (property != null)
                {
                    var headers = property.GetValue(request);
                    if (headers != null)
                    {
                        var indexer = headers.GetType().GetProperty("Item", new[] { typeof(string) });
                        if (indexer != null)
                        {
                            var value = indexer.GetValue(headers, new object[] { headerName });
                            return value as string ?? string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // 忽略异常，返回空字符串
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取请求参数集合
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <returns>参数字典</returns>
        public static Dictionary<string, string> GetParams(object request)
        {
            // 实现获取请求参数集合
            var paramsDict = new Dictionary<string, string>();
            if (request == null)
            {
                return paramsDict;
            }

            try
            {
                // 尝试使用反射获取参数集合
                var method = request.GetType().GetMethod("Params");
                if (method != null)
                {
                    var paramsCollection = method.Invoke(request, null);
                    if (paramsCollection != null)
                    {
                        var keysProperty = paramsCollection.GetType().GetProperty("Keys");
                        if (keysProperty != null)
                        {
                            var keys = keysProperty.GetValue(paramsCollection);
                            if (keys is System.Collections.ICollection collection)
                            {
                                foreach (var key in collection)
                                {
                                    var keyStr = key.ToString();
                                    var indexer = paramsCollection.GetType().GetProperty("Item", new[] { key.GetType() });
                                    if (indexer != null)
                                    {
                                        var value = indexer.GetValue(paramsCollection, new[] { key });
                                        paramsDict[keyStr] = value?.ToString() ?? string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }

                // 尝试直接获取Params属性
                var property = request.GetType().GetProperty("Params");
                if (property != null)
                {
                    var paramsCollection = property.GetValue(request);
                    if (paramsCollection != null)
                    {
                        var keysProperty = paramsCollection.GetType().GetProperty("Keys");
                        if (keysProperty != null)
                        {
                            var keys = keysProperty.GetValue(paramsCollection);
                            if (keys is System.Collections.ICollection collection)
                            {
                                foreach (var key in collection)
                                {
                                    var keyStr = key.ToString();
                                    var indexer = paramsCollection.GetType().GetProperty("Item", new[] { key.GetType() });
                                    if (indexer != null)
                                    {
                                        var value = indexer.GetValue(paramsCollection, new[] { key });
                                        paramsDict[keyStr] = value?.ToString() ?? string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // 忽略异常，返回空字典
            }

            return paramsDict;
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <param name="name">参数名称</param>
        /// <returns>参数值</returns>
        public static string GetParam(object request, string name)
        {
            // 实现获取请求参数
            if (request == null || string.IsNullOrEmpty(name))
            {
                return null;
            }

            try
            {
                // 尝试使用反射获取参数
                var method = request.GetType().GetMethod("QueryString");
                if (method != null)
                {
                    var queryString = method.Invoke(request, null);
                    if (queryString != null)
                    {
                        var indexer = queryString.GetType().GetProperty("Item", new[] { typeof(string) });
                        if (indexer != null)
                        {
                            var paramValue = indexer.GetValue(queryString, new object[] { name });
                            return paramValue as string;
                        }
                    }
                }

                // 尝试直接获取QueryString属性
                var property = request.GetType().GetProperty("QueryString");
                if (property != null)
                {
                    var queryString = property.GetValue(request);
                    if (queryString != null)
                    {
                        var indexer = queryString.GetType().GetProperty("Item", new[] { typeof(string) });
                        if (indexer != null)
                        {
                            var paramValue = indexer.GetValue(queryString, new object[] { name });
                            return paramValue as string;
                        }
                    }
                }

                // 尝试从Params中获取
                var paramsDict = GetParams(request);
                if (paramsDict.TryGetValue(name, out var value))
                {
                    return value;
                }
            }
            catch
            {
                // 忽略异常，返回null
            }

            return null;
        }

        /// <summary>
        /// 获取Int类型参数
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static int GetInt(object request, string name, int defaultValue)
        {
            // 实现获取Int类型参数
            var value = GetParam(request, name);
            if (int.TryParse(value, out var result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Bool类型参数
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static bool GetBool(object request, string name, bool defaultValue)
        {
            // 实现获取Bool类型参数
            var value = GetParam(request, name);
            if (bool.TryParse(value, out var result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Double类型参数
        /// </summary>
        /// <param name="request">Http请求对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static double GetDouble(object request, string name, double defaultValue)
        {
            // 实现获取Double类型参数
            var value = GetParam(request, name);
            if (double.TryParse(value, out var result))
            {
                return result;
            }
            return defaultValue;
        }

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