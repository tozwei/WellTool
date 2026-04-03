using System;
using System.IO;
using System.Text;
using WellTool.Core.Text;

namespace WellTool.Core.IO;

/// <summary>
/// 快速字符串写出器，相比StringWriter非线程安全，速度更快
/// </summary>
public class FastStringWriter : TextWriter
{
	private readonly StrBuilder _builder;

	/// <summary>
	/// 构造
	/// </summary>
	public FastStringWriter() : this(StrBuilder.DefaultCapacity)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="initialSize">初始容量</param>
	public FastStringWriter(int initialSize)
	{
		if (initialSize < 0)
		{
			initialSize = StrBuilder.DefaultCapacity;
		}
		_builder = new StrBuilder(initialSize);
	}

	public override void Write(char value)
	{
		_builder.Append(value);
	}

	public override void Write(string value)
	{
		_builder.Append(value);
	}

	public override void Write(char[] buffer, int index, int count)
	{
		_builder.Append(buffer, index, count);
	}

	public override void Flush()
	{
	}

	public override string ToString()
	{
		return _builder.ToString();
	}

	public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
}
