using System;
using System.Text;

namespace WellTool.Core.Builder;

/// <summary>
/// ToString构建器
/// </summary>
public class ToStringBuilder
{
	private readonly StringBuilder _sb;
	private readonly bool _isShort;
	private readonly string _className;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="obj">对象</param>
	public ToStringBuilder(object obj) : this(obj, false)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="obj">对象</param>
	/// <param name="isShort">是否简短模式</param>
	public ToStringBuilder(object obj, bool isShort)
	{
		_isShort = isShort;
		_className = obj?.GetType().Name ?? "null";
		_sb = new StringBuilder();
	}

	/// <summary>
	/// 添加字段
	/// </summary>
	/// <param name="name">字段名</param>
	/// <param name="value">值</param>
	/// <returns>this</returns>
	public ToStringBuilder Append(string name, object value)
	{
		if (_sb.Length > 0)
			_sb.Append(_isShort ? "," : ", ");
		_sb.Append(name);
		_sb.Append("=");
		_sb.Append(value);
		return this;
	}

	/// <summary>
	/// 添加值
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>this</returns>
	public ToStringBuilder Append(object value)
	{
		if (_sb.Length > 0)
			_sb.Append(_isShort ? "," : ", ");
		_sb.Append(value);
		return this;
	}

	/// <summary>
	/// 构建字符串
	/// </summary>
	public override string ToString()
	{
		return $"{_className}{{{_sb}}}";
	}
}
