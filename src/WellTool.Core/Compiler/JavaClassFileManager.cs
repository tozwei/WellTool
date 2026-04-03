using System;
using System.Collections.Generic;
using WellTool.Core.IO.Resource;

namespace WellTool.Core.Compiler;

/// <summary>
/// Java 字节码文件对象管理器
/// </summary>
internal class JavaClassFileManager
{
	/// <summary>
	/// 存储java字节码文件对象映射
	/// </summary>
	private readonly Dictionary<string, FileObjectResource> _classFileObjectMap = new();

	/// <summary>
	/// 加载动态编译生成类的父类加载器
	/// </summary>
	private readonly Type _parent;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="parent">父类加载器</param>
	public JavaClassFileManager(Type parent)
	{
		_parent = parent;
	}

	/// <summary>
	/// 获得Java字节码文件对象
	/// </summary>
	/// <param name="className">类名</param>
	/// <returns>Java字节码文件对象</returns>
	public JavaClassFileObject GetJavaFileForOutput(string className)
	{
		var javaFileObject = new JavaClassFileObject(className);
		_classFileObjectMap[className] = new FileObjectResource(javaFileObject);
		return javaFileObject;
	}

	/// <summary>
	/// 获取字节码
	/// </summary>
	/// <param name="className">类名</param>
	/// <returns>字节数组</returns>
	public byte[] GetBytes(string className)
	{
		if (_classFileObjectMap.TryGetValue(className, out var resource))
		{
			return resource.ReadBytes();
		}
		return null;
	}
}
