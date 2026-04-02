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
using System.Net;
using System.Text;

namespace WellTool.Core.IO.Resource
{
    /// <summary>
    /// URL资源访问类
    /// </summary>
    public class UrlResource : IResource
    {
        protected Uri _uri;
        private long _lastModified = 0;
        protected string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="uri">URI</param>
        public UrlResource(Uri uri) : this(uri, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="name">资源名称</param>
        public UrlResource(Uri uri, string name)
        {
            _uri = uri;
            if (uri != null && uri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                var file = new FileInfo(uri.LocalPath);
                if (file.Exists)
                {
                    _lastModified = file.LastWriteTime.Ticks;
                }
            }
            _name = string.IsNullOrEmpty(name) ? (uri != null ? Path.GetFileName(uri.LocalPath) : null) : name;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="urlString">URL字符串</param>
        public UrlResource(string urlString) : this(new Uri(urlString), null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="urlString">URL字符串</param>
        /// <param name="name">资源名称</param>
        public UrlResource(string urlString, string name) : this(new Uri(urlString), name)
        {
        }

        /// <summary>
        /// 获取资源名，例如文件资源的资源名为文件名
        /// </summary>
        /// <returns>资源名</returns>
        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// 获得解析后的{@link System.Uri}，无对应Uri的返回{@code null}
        /// </summary>
        /// <returns>解析后的{@link System.Uri}</returns>
        public Uri GetUri()
        {
            return _uri;
        }

        /// <summary>
        /// 获得 {@link Stream}
        /// </summary>
        /// <returns>{@link Stream}</returns>
        public virtual Stream GetStream()
        {
            if (_uri == null)
            {
                throw new NoResourceException("Resource URI is null!");
            }
            try
            {
                var request = WebRequest.Create(_uri);
                var response = request.GetResponse();
                return response.GetResponseStream();
            }
            catch (Exception e)
            {
                throw new NoResourceException(e);
            }
        }

        /// <summary>
        /// 检查资源是否变更<br>
        /// 一般用于文件类资源，检查文件是否被修改过。
        /// </summary>
        /// <returns>是否变更</returns>
        public bool IsModified()
        {
            // lastModified == 0表示此资源非文件资源
            if (_lastModified == 0 || _uri == null || !_uri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            var file = new FileInfo(_uri.LocalPath);
            return file.Exists && _lastModified != file.LastWriteTime.Ticks;
        }

        /// <summary>
        /// 获得FileInfo
        /// </summary>
        /// <returns>{@link FileInfo}</returns>
        public FileInfo GetFile()
        {
            if (_uri == null || !_uri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            return new FileInfo(_uri.LocalPath);
        }

        /// <summary>
        /// 将资源内容写出到流，不关闭输出流，但是关闭资源流
        /// </summary>
        /// <param name="output">输出流</param>
        /// <exception cref="IORuntimeException">IO异常</exception>
        public void WriteTo(Stream output)
        {
            using (var input = GetStream())
            {
                input.CopyTo(output);
            }
        }

        /// <summary>
        /// 获得StreamReader
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>{@link StreamReader}</returns>
        public StreamReader GetReader(Encoding encoding)
        {
            return new StreamReader(GetStream(), encoding);
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装{@link IOException}</exception>
        public string ReadStr(Encoding encoding)
        {
            using (var reader = GetReader(encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        public string ReadUtf8Str()
        {
            return ReadStr(Encoding.UTF8);
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        public byte[] ReadBytes()
        {
            using (var stream = GetStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 返回路径
        /// </summary>
        /// <returns>返回URI路径</returns>
        public override string ToString()
        {
            return _uri?.ToString() ?? "null";
        }

        /// <summary>
        /// 获取资源长度
        /// </summary>
        /// <returns>资源长度</returns>
        public long Size()
        {
            if (_uri == null)
            {
                return 0;
            }
            try
            {
                var request = WebRequest.Create(_uri);
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                return response.ContentLength;
            }
            catch
            {
                return 0;
            }
        }
    }
}