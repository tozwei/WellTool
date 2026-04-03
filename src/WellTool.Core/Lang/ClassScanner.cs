using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 类扫描器，用于扫描指定程序集或目录下的类
    /// </summary>
    public class ClassScanner
    {
        private readonly string _packageName;
        private readonly string _packagePath;
        private readonly Func<Type, bool> _filter;
        private readonly bool _initialize;
        private readonly List<Type> _classes = new List<Type>();
        private readonly List<string> _loadErrors = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="filter">过滤器</param>
        /// <param name="initialize">是否初始化类</param>
        public ClassScanner(string packageName, Func<Type, bool> filter = null, bool initialize = false)
        {
            _packageName = packageName;
            _packagePath = packageName?.Replace('.', Path.DirectorySeparatorChar) ?? "";
            _filter = filter;
            _initialize = initialize;
        }

        /// <summary>
        /// 扫描程序集中的类
        /// </summary>
        /// <param name="assembly">程序集</param>
        public void ScanAssembly(Assembly assembly)
        {
            if (assembly == null) return;

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (MatchPackage(type) && MatchFilter(type))
                {
                    _classes.Add(type);
                }
            }
        }

        /// <summary>
        /// 扫描目录下的类
        /// </summary>
        /// <param name="directory">目录</param>
        public void ScanDirectory(string directory)
        {
            if (!Directory.Exists(directory)) return;

            var dllFiles = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);
            foreach (var dll in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    ScanAssembly(assembly);
                }
                catch
                {
                    // 忽略加载失败的程序集
                }
            }

            // 扫描源代码目录
            var csFiles = Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories);
            foreach (var csFile in csFiles)
            {
                try
                {
                    // 对于单个文件，我们无法直接加载类型
                    // 这里需要特殊处理，比如使用 Roslyn
                }
                catch
                {
                    // 忽略
                }
            }
        }

        /// <summary>
        /// 获取扫描到的所有类
        /// </summary>
        public IReadOnlyList<Type> GetClasses()
        {
            return _classes.AsReadOnly();
        }

        /// <summary>
        /// 获取加载失败的类名
        /// </summary>
        public IReadOnlyList<string> GetLoadErrors()
        {
            return _loadErrors.AsReadOnly();
        }

        /// <summary>
        /// 检查包名是否匹配
        /// </summary>
        private bool MatchPackage(Type type)
        {
            if (string.IsNullOrEmpty(_packageName)) return true;
            return type.Namespace?.StartsWith(_packageName) ?? false;
        }

        /// <summary>
        /// 检查过滤器是否匹配
        /// </summary>
        private bool MatchFilter(Type type)
        {
            return _filter?.Invoke(type) ?? true;
        }

        /// <summary>
        /// 扫描指定包下所有包含指定注解的类
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="annotationType">注解类型</param>
        /// <returns>匹配的类集合</returns>
        public static IEnumerable<Type> ScanByAnnotation(string packageName, Type annotationType)
        {
            var scanner = new ClassScanner(packageName, t => t.GetCustomAttribute(annotationType) != null);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in assemblies)
            {
                scanner.ScanAssembly(assembly);
            }

            return scanner.GetClasses();
        }

        /// <summary>
        /// 扫描指定包下所有类
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <returns>所有类</returns>
        public static IEnumerable<Type> ScanPackage(string packageName)
        {
            var scanner = new ClassScanner(packageName);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in assemblies)
            {
                scanner.ScanAssembly(assembly);
            }

            return scanner.GetClasses();
        }
    }
}
