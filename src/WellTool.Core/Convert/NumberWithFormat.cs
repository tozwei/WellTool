using System;
using System.Globalization;

namespace WellTool.Core.Convert;

/// <summary>
/// 带格式的数字包装类
/// </summary>
public class NumberWithFormat
{
	/// <summary>
	/// 值
	/// </summary>
	public double Value { get; }

	/// <summary>
	/// 格式
	/// </summary>
	public string Format { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">值</param>
	/// <param name="format">格式</param>
	public NumberWithFormat(double value, string format)
	{
		Value = value;
		Format = format;
	}

	/// <summary>
	/// 转换为字符串
	/// </summary>
	public override string ToString()
	{
		if (string.IsNullOrEmpty(Format))
			return Value.ToString();
		return Value.ToString(Format);
	}

	/// <summary>
	/// 隐式转换
	/// </summary>
	public static implicit operator double(NumberWithFormat nf) => nf.Value;

	/// <summary>
	/// 隐式转换
	/// </summary>
	public static implicit operator string(NumberWithFormat nf) => nf.ToString();
}
