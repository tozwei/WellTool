using System;
using System.Collections.Generic;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// OptConverter杞崲鍣?
/// </summary>
public class OptConverter : AbstractConverter<Opt>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
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
	/// 绌哄€?
	/// </summary>
	public static readonly Opt Empty = new Opt(null);

	private readonly object _value;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="value">鍊?/param>
	private Opt(object value)
	{
		_value = value;
	}

	/// <summary>
	/// 鍒涘缓Opt
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>Opt</returns>
	public static Opt Of(object value)
	{
		return value == null ? Empty : new Opt(value);
	}

	/// <summary>
	/// 鏄惁鏈夊€?
	/// </summary>
	public bool IsPresent => _value != null;

	/// <summary>
	/// 鑾峰彇鍊?
	/// </summary>
	public object Get() => _value;

	/// <summary>
	/// 鑾峰彇鍊兼垨榛樿鍊?
	/// </summary>
	/// <param name="defaultValue">榛樿鍊?/param>
	/// <returns>鍊兼垨榛樿鍊?/returns>
	public object OrElse(object defaultValue) => _value ?? defaultValue;
}

