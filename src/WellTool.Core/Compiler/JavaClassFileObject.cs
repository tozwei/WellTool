using System;
using System.IO;

namespace WellTool.Core.Compiler;

/// <summary>
/// Java 字节码文件对象，用于在内存中暂存class字节码
/// </summary>
internal class JavaClassFileObject
{
	/// <summary>
	/// 字节码输出流
	/// </summary>
	private readonly MemoryStream _byteArrayOutputStream = new();

	/// <summary>
	/// 类名
	/// </summary>
	public string ClassName { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="className">编译后的class文件的类名</param>
	public JavaClassFileObject(string className)
	{
		ClassName = className;
	}

	/// <summary>
	/// 获得字节码输入流
	/// </summary>
	/// <returns>字节码输入流</returns>
	public Stream OpenInputStream()
	{
		return new MemoryStream(_byteArrayOutputStream.ToArray());
	}

	/// <summary>
	/// 获得字节码输出流
	/// </summary>
	/// <returns>字节码输出流</returns>
	public Stream OpenOutputStream()
	{
		return _byteArrayOutputStream;
	}

	/// <summary>
	/// 获取字节码
	/// </summary>
	/// <returns>字节数组</returns>
	public byte[] GetBytes()
	{
		return _byteArrayOutputStream.ToArray();
	}
}
