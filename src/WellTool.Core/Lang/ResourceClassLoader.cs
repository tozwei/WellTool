using System;
using System.Reflection;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 资源类加载器
    /// </summary>
    public class ResourceClassLoader
    {
        private readonly Assembly _assembly;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResourceClassLoader() : this(Assembly.GetCallingAssembly())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assembly">程序集</param>
        public ResourceClassLoader(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        /// <summary>
        /// 获取所有资源名称
        /// </summary>
        public string[] GetResourceNames()
        {
            var names = _assembly.GetManifestResourceNames();
            return names ?? new string[0];
        }

        /// <summary>
        /// 获取指定前缀的资源名称
        /// </summary>
        /// <param name="prefix">前缀</param>
        public string[] GetResourceNames(string prefix)
        {
            var allNames = GetResourceNames();
            if (string.IsNullOrEmpty(prefix))
            {
                return allNames;
            }

            return Array.FindAll(allNames, name => name.StartsWith(prefix));
        }

        /// <summary>
        /// 检查资源是否存在
        /// </summary>
        /// <param name="name">资源名称</param>
        public bool Exists(string name)
        {
            return _assembly.GetManifestResourceInfo(name) != null;
        }

        /// <summary>
        /// 加载资源文本
        /// </summary>
        public string LoadText(string name)
        {
            using (var stream = _assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 加载资源文本（指定编码）
        /// </summary>
        public string LoadText(string name, System.Text.Encoding encoding)
        {
            using (var stream = _assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new System.IO.StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 加载资源字节数组
        /// </summary>
        public byte[] LoadBytes(string name)
        {
            using (var stream = _assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 获取资源流
        /// </summary>
        public System.IO.Stream GetStream(string name)
        {
            return _assembly.GetManifestResourceStream(name);
        }
    }
}
