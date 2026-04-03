using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.CodeDom.Compiler;
using System.Reflection;

namespace WellTool.Core.Compiler
{
    /// <summary>
    /// Java 源码编译器
    /// 支持动态编译 Java 源码并加载到 ClassLoader，从而获取动态加载的类
    /// </summary>
    public class JavaSourceCompiler
    {
        private readonly List<string> _sourceFiles = new List<string>();
        private readonly List<string> _sourceCodes = new List<string>();
        private readonly List<string> _libraryFiles = new List<string>();
        private readonly ClassLoader _parentClassLoader;

        /// <summary>
        /// 创建 Java 源码编译器实例
        /// </summary>
        /// <param name="parent">父类加载器</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public static JavaSourceCompiler Create(ClassLoader parent)
        {
            return new JavaSourceCompiler(parent);
        }

        public JavaSourceCompiler(ClassLoader parent)
        {
            _parentClassLoader = parent;
        }

        /// <summary>
        /// 向编译器加入待编译的资源
        /// </summary>
        /// <param name="resources">资源数组</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public JavaSourceCompiler AddSource(params Resource[] resources)
        {
            foreach (var resource in resources)
            {
                if (resource is FileResource fileRes)
                {
                    AddSource(fileRes.File);
                }
            }
            return this;
        }

        /// <summary>
        /// 向编译器加入待编译的文件
        /// </summary>
        /// <param name="files">文件数组</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public JavaSourceCompiler AddSource(params FileInfo[] files)
        {
            foreach (var file in files)
            {
                if (file.Exists)
                {
                    if (file.Extension.Equals(".java", StringComparison.OrdinalIgnoreCase))
                    {
                        _sourceFiles.Add(file.FullName);
                    }
                    else if (file.Extension.Equals(".jar", StringComparison.OrdinalIgnoreCase) ||
                             file.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        _libraryFiles.Add(file.FullName);
                    }
                    else if (file.Exists && File.GetAttributes(file.FullName).HasFlag(FileAttributes.Directory))
                    {
                        // 目录，递归添加
                        var dirFiles = Directory.GetFiles(file.FullName, "*.java", SearchOption.AllDirectories);
                        _sourceFiles.AddRange(dirFiles);
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// 向编译器加入待编译的源码 Map
        /// </summary>
        /// <param name="sourceCodeMap">源码 Map，key 为类名，value 为源码字符串</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public JavaSourceCompiler AddSource(Dictionary<string, string> sourceCodeMap)
        {
            foreach (var kvp in sourceCodeMap)
            {
                _sourceCodes.Add(kvp.Value);
            }
            return this;
        }

        /// <summary>
        /// 向编译器加入单个类的源码
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="sourceCode">源码字符串</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public JavaSourceCompiler AddSource(string className, string sourceCode)
        {
            _sourceCodes.Add(sourceCode);
            return this;
        }

        /// <summary>
        /// 加入编译 Java 源码时所需要的 jar 包
        /// </summary>
        /// <param name="files">jar 包数组</param>
        /// <returns>JavaSourceCompiler 实例</returns>
        public JavaSourceCompiler AddLibrary(params FileInfo[] files)
        {
            foreach (var file in files)
            {
                if (file.Exists && (file.Extension.Equals(".jar", StringComparison.OrdinalIgnoreCase) ||
                                     file.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase)))
                {
                    _libraryFiles.Add(file.FullName);
                }
            }
            return this;
        }

        /// <summary>
        /// 编译所有已添加的文件并返回类加载器
        /// </summary>
        /// <returns>ClassLoader 实例</returns>
        public ClassLoader Compile()
        {
            try
            {
                var codes = new List<string>();

                // 读取文件源码
                foreach (var file in _sourceFiles)
                {
                    if (File.Exists(file))
                    {
                        codes.Add(File.ReadAllText(file));
                    }
                }

                // 添加内存中的源码
                codes.AddRange(_sourceCodes);

                if (codes.Count == 0)
                {
                    throw new CompilerException("No source code to compile");
                }

                using (var provider = new CSharpCodeProvider())
                {
                    var parameters = new CompilerParameters
                    {
                        GenerateExecutable = false,
                        GenerateInMemory = true,
                        IncludeDebugInformation = false
                    };

                    // 添加引用的程序集
                    parameters.ReferencedAssemblies.Add("System.dll");
                    parameters.ReferencedAssemblies.Add("System.Core.dll");

                    foreach (var lib in _libraryFiles)
                    {
                        parameters.ReferencedAssemblies.Add(lib);
                    }

                    var results = provider.CompileAssemblyFromSource(parameters, codes.ToArray());

                    if (results.Errors.HasErrors)
                    {
                        var errorMessages = results.Errors
                            .Cast<CompilerError>()
                            .Select(error => $"Line {error.Line}: {error.ErrorText}")
                            .ToArray();
                        throw new CompilerException(string.Join(Environment.NewLine, errorMessages));
                    }

                    return new ClassLoader(results.CompiledAssembly);
                }
            }
            catch (PlatformNotSupportedException)
            {
                throw new CompilerException("Operation is not supported on this platform");
            }
        }

        /// <summary>
        /// 使用指定的编译参数编译所有文件并返回类加载器
        /// </summary>
        /// <param name="options">编译参数</param>
        /// <returns>ClassLoader 实例</returns>
        public ClassLoader Compile(List<string> options)
        {
            // C# 编译器不支持 Java 风格的编译参数
            return Compile();
        }
    }

    /// <summary>
    /// 资源接口
    /// </summary>
    public interface Resource
    {
    }

    /// <summary>
    /// 文件资源
    /// </summary>
    public class FileResource : Resource
    {
        public FileInfo File { get; }

        public FileResource(FileInfo file)
        {
            File = file;
        }
    }

    /// <summary>
    /// 自定义类加载器
    /// </summary>
    public class ClassLoader
    {
        private readonly Assembly _assembly;

        public ClassLoader(Assembly assembly)
        {
            _assembly = assembly;
        }

        /// <summary>
        /// 加载类
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>Type</returns>
        public Type LoadClass(string className)
        {
            return _assembly.GetType(className);
        }
    }
}