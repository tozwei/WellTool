using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WellTool.Core.Exceptions;
using WellTool.Core.IO;
using WellTool.Core.Util;

namespace WellTool.Core.Lang;

/// <summary>
/// 外部Jar的类加载器（.NET版本仅做接口定义）
/// </summary>
public class JarClassLoader
{
	/// <summary>
	/// 加载Jar到ClassPath
	/// </summary>
	/// <param name="dir">jar文件或所在目录</param>
	/// <returns>JarClassLoader</returns>
	public static JarClassLoader Load(DirectoryInfo dir)
	{
		var loader = new JarClassLoader();
		return loader;
	}

	/// <summary>
	/// 加载Jar文件到指定loader中
	/// </summary>
	/// <param name="jarFile">被加载的jar</param>
	public static void LoadJar(object loader, FileInfo jarFile)
	{
		throw new NotSupportedException("JarClassLoader is not supported in .NET");
	}

	/// <summary>
	/// 加载Jar文件到System ClassLoader中
	/// </summary>
	/// <param name="jarFile">被加载的jar</param>
	public static void LoadJarToSystemClassLoader(FileInfo jarFile)
	{
		throw new NotSupportedException("JarClassLoader is not supported in .NET");
	}

	/// <summary>
	/// 构造
	/// </summary>
	public JarClassLoader()
	{
	}

	/// <summary>
	/// 增加jar文件
	/// </summary>
	/// <param name="jarFileOrDir">jar文件或者jar文件所在目录</param>
	/// <returns>this</returns>
	public JarClassLoader AddJar(DirectoryInfo jarFileOrDir)
	{
		return this;
	}
}
