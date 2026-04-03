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
using System.Reflection;
using System.Text;

namespace WellTool.Core.IO.Resource
{
    /// <summary>
    /// Resource资源工具类
    /// </summary>
    public static class ResourceUtil
    {
        /// <summary>
        /// 读取Classpath下的资源为字符串，使用UTF-8编码
        /// </summary>
        /// <param name="resource">资源路径，使用相对ClassPath的路径</param>
        /// <returns>资源内容</returns>
        public static string ReadUtf8Str(string resource)
        {
            return GetResourceObj(resource).ReadUtf8Str();
        }

        /// <summary>
        /// 读取Classpath下的资源为字符串
        /// </summary>
        /// <param name="resource">可以是绝对路径，也可以是相对路径（相对ClassPath）</param>
        /// <param name="encoding">编码</param>
        /// <returns>资源内容</returns>
        public static string ReadStr(string resource, Encoding encoding)
        {
            return GetResourceObj(resource).ReadStr(encoding);
        }

        /// <summary>
        /// 读取Classpath下的资源为byte[]
        /// </summary>
        /// <param name="resource">可以是绝对路径，也可以是相对路径（相对ClassPath）</param>
        /// <returns>资源内容</returns>
        public static byte[] ReadBytes(string resource)
        {
            return GetResourceObj(resource).ReadBytes();
        }

        /// <summary>
        /// 从ClassPath资源中获取{@link Stream}
        /// </summary>
        /// <param name="resource">ClassPath资源</param>
        /// <returns>{@link Stream}</returns>
        /// <exception cref="NoResourceException">资源不存在异常</exception>
        public static System.IO.Stream GetStream(string resource)
        {
            return GetResourceObj(resource).GetStream();
        }

        /// <summary>
        /// 从ClassPath资源中获取{@link Stream}，当资源不存在时返回null
        /// </summary>
        /// <param name="resource">ClassPath资源</param>
        /// <returns>{@link Stream}</returns>
        public static System.IO.Stream GetStreamSafe(string resource)
        {
            try
            {
                return GetResourceObj(resource).GetStream();
            }
            catch (NoResourceException)
            {
                // ignore
            }
            return null;
        }

        /// <summary>
        /// 从ClassPath资源中获取{@link StreamReader}
        /// </summary>
        /// <param name="resource">ClassPath资源</param>
        /// <returns>{@link StreamReader}</returns>
        public static StreamReader GetUtf8Reader(string resource)
        {
            return GetReader(resource, Encoding.UTF8);
        }

        /// <summary>
        /// 从ClassPath资源中获取{@link StreamReader}
        /// </summary>
        /// <param name="resource">ClassPath资源</param>
        /// <param name="encoding">编码</param>
        /// <returns>{@link StreamReader}</returns>
        public static StreamReader GetReader(string resource, Encoding encoding)
        {
            return GetResourceObj(resource).GetReader(encoding);
        }

        /// <summary>
        /// 获得资源的Uri<br>
        /// 路径用/分隔，例如:
        /// <pre>
        /// config/a/db.config
        /// spring/xml/test.xml
        /// </pre>
        /// </summary>
        /// <param name="resource">资源（相对Classpath的路径）</param>
        /// <returns>资源Uri</returns>
        public static Uri GetResource(string resource)
        {
            return GetResource(resource, null);
        }

        /// <summary>
        /// 获得资源相对路径对应的Uri
        /// </summary>
        /// <param name="resource">资源相对路径，{@code null}和""都表示classpath根路径</param>
        /// <param name="baseType">基准Type，获得的相对路径相对于此Type所在路径，如果为{@code null}则相对ClassPath</param>
        /// <returns>{@link Uri}</returns>
        public static Uri GetResource(string resource, Type baseType)
        {
            resource = string.IsNullOrEmpty(resource) ? "" : resource;
            if (baseType != null)
            {
                var assembly = baseType.Assembly;
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(resource, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    return new Uri($"assembly://{assembly.GetName().Name}/{resourceName}");
                }
            }
            else
            {
                var assembly = Assembly.GetCallingAssembly();
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(resource, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    return new Uri($"assembly://{assembly.GetName().Name}/{resourceName}");
                }
            }
            return null;
        }

        /// <summary>
        /// 获取{@link IResource} 资源对象<br>
        /// 如果提供路径为绝对路径或路径以file:开头，返回{@link FileResource}，否则返回{@link ClassPathResource}
        /// </summary>
        /// <param name="path">路径，可以是绝对路径，也可以是相对路径（相对ClassPath）</param>
        /// <returns>{@link IResource} 资源对象</returns>
        public static IResource GetResourceObj(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("file:") || Path.IsPathRooted(path))
                {
                    return new FileResource(path);
                }
            }
            return new ClassPathResource(path);
        }
    }
}