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

using System.Reflection;

namespace WellTool.Core.IO.Resource
{
    /// <summary>
    /// ClassPath单一资源访问类<br>
    /// 传入路径path必须为相对路径，如果传入绝对路径，会直接报错。<br>
    /// 传入的path所指向的资源必须存在，否则报错
    /// </summary>
    public class ClassPathResource : UrlResource
    {
        private readonly string _path;
        private readonly Assembly _assembly;
        private readonly Type _type;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">相对于ClassPath的路径</param>
        public ClassPathResource(string path) : this(path, null, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">相对于ClassPath的路径</param>
        /// <param name="assembly">{@link Assembly}</param>
        public ClassPathResource(string path, Assembly assembly) : this(path, assembly, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">相对于给定Type的路径</param>
        /// <param name="type">{@link Type} 用于定位路径</param>
        public ClassPathResource(string path, Type type) : this(path, null, type)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <param name="assembly">{@link Assembly}</param>
        /// <param name="type">{@link Type} 用于定位路径</param>
        public ClassPathResource(string path, Assembly assembly, Type type)
            : base((Uri)null, null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path", "Path must not be null");
            }

            var normalizedPath = NormalizePath(path);
            _path = normalizedPath;
            _name = string.IsNullOrEmpty(normalizedPath) ? null : Path.GetFileName(normalizedPath);

            _assembly = assembly ?? Assembly.GetCallingAssembly();
            _type = type;
            InitUri();
        }

        /// <summary>
        /// 获得Path
        /// </summary>
        /// <returns>path</returns>
        public string GetPath()
        {
            return _path;
        }

        /// <summary>
        /// 获得绝对路径Path<br>
        /// 对于不存在的资源，返回拼接后的绝对路径
        /// </summary>
        /// <returns>绝对路径path</returns>
        public string GetAbsolutePath()
        {
            if (Path.IsPathRooted(_path))
            {
                return _path;
            }
            // uri在初始化的时候已经断言，此处始终不为null
            return _uri.LocalPath;
        }

        /// <summary>
        /// 获得 {@link Assembly}
        /// </summary>
        /// <returns>{@link Assembly}</returns>
        public Assembly GetAssembly()
        {
            return _assembly;
        }

        /// <summary>
        /// 根据给定资源初始化Uri
        /// </summary>
        private void InitUri()
        {
            Uri uri = null;
            if (_type != null)
            {
                var stream = _type.Assembly.GetManifestResourceStream(_type, _path);
                if (stream != null)
                {
                    uri = new Uri($"assembly://{_type.Assembly.GetName().Name}/{_type.Namespace}/{_path}");
                }
            }
            else if (_assembly != null)
            {
                var resourceName = _assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(_path, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    uri = new Uri($"assembly://{_assembly.GetName().Name}/{resourceName}");
                }
            }
            else
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(_path, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    uri = new Uri($"assembly://{assembly.GetName().Name}/{resourceName}");
                }
            }

            if (uri == null)
            {
                throw new NoResourceException($"Resource of path [{_path}] not exist!");
            }

            _uri = uri;
        }

        /// <summary>
        /// 标准化Path格式
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>标准化后的path</returns>
        private string NormalizePath(string path)
        {
            // 标准化路径
            path = Path.GetRelativePath(".", path);
            path = path.Replace(Path.DirectorySeparatorChar, '/');

            if (Path.IsPathRooted(path))
            {
                throw new ArgumentException($"Path [{path}] must be a relative path !");
            }
            return path;
        }

        /// <summary>
        /// 获得 {@link Stream}
        /// </summary>
        /// <returns>{@link Stream}</returns>
        public override Stream GetStream()
        {
            if (_type != null)
            {
                var stream = _type.Assembly.GetManifestResourceStream(_type, _path);
                if (stream != null)
                {
                    return stream;
                }
            }
            else if (_assembly != null)
            {
                var resourceName = _assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(_path, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    var stream = _assembly.GetManifestResourceStream(resourceName);
                    if (stream != null)
                    {
                        return stream;
                    }
                }
            }
            else
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(_path, StringComparison.OrdinalIgnoreCase));
                if (resourceName != null)
                {
                    var stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream != null)
                    {
                        return stream;
                    }
                }
            }

            throw new NoResourceException($"Resource of path [{_path}] not exist!");
        }

        /// <summary>
        /// 返回路径
        /// </summary>
        /// <returns>返回classpath路径</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(_path) ? base.ToString() : "classpath:" + _path;
        }
    }
}