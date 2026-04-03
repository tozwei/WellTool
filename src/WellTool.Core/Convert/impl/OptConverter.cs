using System;
using System.Collections.Generic;

namespace WellDone.Core.Convert.Impl;

/// <summary>
/// OptConverter转换器
/// </summary>
public class OptConverter : AbstractConverter<Opt>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override Opt ConvertInternal(object value)
	{
		return Opt.Of(value);
	}
}

/// <summary>
/// Optional-like wrapper for nullable values
/// </summary>
public class Opt
{
	/// <summary>
	/// 空值
	/// </summary>
	public static readonly Opt Empty = new Opt(null);

	private readonly object _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">值</param>
	private Opt(object value)
	{
		_value = value;
	}

	/// <summary>
	/// 创建Opt
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>Opt</returns>
	public static Opt Of(object value)
	{
		return value == null ? Empty : new Opt(value);
	}

	/// <summary>
	/// 是否有值
	/// </summary>
	public bool IsPresent => _value != null;

	/// <summary>
	/// 获取值
	/// </summary>
	public object Get() => _value;

	/// <summary>
	/// 获取值或默认值
	/// </summary>
	/// <param name="defaultValue">默认值</param>
	/// <returns>值或默认值</returns>
	public object OrElse(object defaultValue) => _value ?? defaultValue;
}
