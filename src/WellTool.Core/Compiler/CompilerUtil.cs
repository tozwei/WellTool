using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

namespace WellTool.Core.Compiler
{
    public static class CompilerUtil
    {
        public static Assembly Compile(string code, params string[] references)
        {
            using (var provider = new CSharpCodeProvider())
            {
                var parameters = new CompilerParameters
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true,
                    IncludeDebugInformation = false
                };

                if (references != null && references.Length > 0)
                {
                    parameters.ReferencedAssemblies.AddRange(references);
                }

                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");

                var results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    var errorMessages = results.Errors
                        .Cast<CompilerError>()
                        .Select(error => $"Line {error.Line}: {error.ErrorText}")
                        .ToArray();
                    throw new CompilerException(string.Join(Environment.NewLine, errorMessages));
                }

                return results.CompiledAssembly;
            }
        }

        public static object CreateInstance(Assembly assembly, string typeName, params object[] args)
        {
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                throw new CompilerException($"Type {typeName} not found in assembly");
            }
            return Activator.CreateInstance(type, args);
        }

        public static T CreateInstance<T>(Assembly assembly, string typeName, params object[] args)
        {
            return (T)CreateInstance(assembly, typeName, args);
        }

        public static Assembly CompileFromFile(string filePath, params string[] references)
        {
            var code = File.ReadAllText(filePath);
            return Compile(code, references);
        }

        public static Assembly CompileFromFiles(IEnumerable<string> filePaths, params string[] references)
        {
            var codes = filePaths.Select(File.ReadAllText).ToArray();
            using (var provider = new CSharpCodeProvider())
            {
                var parameters = new CompilerParameters
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true,
                    IncludeDebugInformation = false
                };

                if (references != null && references.Length > 0)
                {
                    parameters.ReferencedAssemblies.AddRange(references);
                }

                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");

                var results = provider.CompileAssemblyFromSource(parameters, codes);

                if (results.Errors.HasErrors)
                {
                    var errorMessages = results.Errors
                        .Cast<CompilerError>()
                        .Select(error => $"Line {error.Line}: {error.ErrorText}")
                        .ToArray();
                    throw new CompilerException(string.Join(Environment.NewLine, errorMessages));
                }

                return results.CompiledAssembly;
            }
        }
    }
}
