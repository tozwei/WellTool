using System;
using System.IO;
using System.Text;

namespace WellTool.Core.Compiler;

/// <summary>
/// Java 源码文件对象
/// </summary>
internal class JavaSourceFileObject
{
	/// <summary>
	/// 输入流
	/// </summary>
	private Stream _inputStream;

	/// <summary>
	/// Source code
	/// </summary>
	private string _sourceCode;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="uri">需要编译的文件uri</param>
	public JavaSourceFileObject(Uri uri)
	{
	}

	/// <summary>
	/// 构造，支持String类型的源码
	/// </summary>
	/// <param name="className">需要编译的类名</param>
	/// <param name="code">需要编译的类源码</param>
	public JavaSourceFileObject(string className, string code)
	{
		_sourceCode = code;
	}

	/// <summary>
	/// 构造，支持流中读取源码
	/// </summary>
	/// <param name="name">需要编译的文件名</param>
	/// <param name="inputStream">输入流</param>
	public JavaSourceFileObject(string name, Stream inputStream)
	{
		_inputStream = inputStream;
	}

	/// <summary>
	/// 获得类源码的输入流
	/// </summary>
	/// <returns>类源码的输入流</returns>
	public Stream OpenInputStream()
	{
		if (_inputStream == null && !string.IsNullOrEmpty(_sourceCode))
		{
			_inputStream = new MemoryStream(Encoding.UTF8.GetBytes(_sourceCode));
		}
		return _inputStream;
	}

	/// <summary>
	/// 获得类源码
	/// </summary>
	/// <returns>需要编译的类的源码</returns>
	public string GetCharContent()
	{
		if (_sourceCode == null)
		{
			using var reader = new StreamReader(OpenInputStream(), Encoding.UTF8);
			_sourceCode = reader.ReadToEnd();
		}
		return _sourceCode;
	}
}
