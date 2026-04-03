using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// Jar类加载器，在.NET中主要用于动态加载程序集
    /// </summary>
    public class JarClassLoader : AssemblyLoadContext
    {
        private readonly List<string> _paths = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public JarClassLoader() : base(isCollectible: true)
        {
        }

        /// <summary>
        /// 从目录加载
        /// </summary>
        /// <param name="directory">目录</param>
        /// <returns>类加载器</returns>
        public static JarClassLoader Load(string directory)
        {
            var loader = new JarClassLoader();
            loader.AddJar(directory);
            return loader;
        }

        /// <summary>
        /// 从JAR文件加载
        /// </summary>
        /// <param name="jarFile">JAR文件路径</param>
        /// <returns>类加载器</returns>
        public static JarClassLoader LoadJar(string jarFile)
        {
            var loader = new JarClassLoader();
            if (File.Exists(jarFile))
            {
                loader.AddPath(Path.GetDirectoryName(jarFile));
            }
            return loader;
        }

        /// <summary>
        /// 添加路径
        /// </summary>
        /// <param name="path">路径</param>
        public void AddPath(string path)
        {
            if (Directory.Exists(path))
            {
                _paths.Add(path);
            }
            else if (File.Exists(path))
            {
                var ext = Path.GetExtension(path).ToLower();
                if (ext == ".dll" || ext == ".exe")
                {
                    _paths.Add(Path.GetDirectoryName(path));
                }
            }
        }

        /// <summary>
        /// 添加JAR文件
        /// </summary>
        /// <param name="jarFileOrDir">JAR文件或目录</param>
        public void AddJar(string jarFileOrDir)
        {
            if (Directory.Exists(jarFileOrDir))
            {
                var jarFiles = Directory.GetFiles(jarFileOrDir, "*.jar");
                foreach (var jar in jarFiles)
                {
                    AddPath(Path.GetDirectoryName(jar));
                }
            }
            else if (File.Exists(jarFileOrDir) && Path.GetExtension(jarFileOrDir).ToLower() == ".jar")
            {
                AddPath(Path.GetDirectoryName(jarFileOrDir));
            }
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            foreach (var path in _paths)
            {
                var dllPath = Path.Combine(path, assemblyName.Name + ".dll");
                if (File.Exists(dllPath))
                {
                    return LoadFromAssemblyPath(dllPath);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取所有已加载的程序集
        /// </summary>
        public IEnumerable<Assembly> GetLoadedAssemblies()
        {
            return Assemblies;
        }
    }
}
