using System;
using System.Collections.Generic;
using System.Threading;
using WellTool.Core.Util;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicBoolean杞崲鍣?
/// </summary>
public class AtomicBooleanConverter : AbstractConverter<AtomicBoolean>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
	protected override AtomicBoolean ConvertInternal(object value)
	{
		if (value is bool b)
		{
			return new AtomicBoolean(b);
		}
		var valueStr = ConvertToStr(value);
		return new AtomicBoolean(StrUtil.ToBoolean(valueStr));
	}
}

/// <summary>
/// AtomicBoolean灏佽绫?
/// </summary>
public class AtomicBoolean
{
	private int _value;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="initialValue">鍒濆鍊?/param>
	public AtomicBoolean(bool initialValue)
	{
		_value = initialValue ? 1 : 0;
	}

	/// <summary>
	/// 鑾峰彇鍊?
	/// </summary>
	public bool Value => _value == 1;

	/// <summary>
	/// 璁剧疆鍊?
	/// </summary>
	/// <param name="newValue">鏂板€?/param>
	public void Set(bool newValue)
	{
		Interlocked.Exchange(ref _value, newValue ? 1 : 0);
	}

	/// <summary>
	/// 杞崲涓篵ool
	/// </summary>
	public static implicit operator bool(AtomicBoolean atomic) => atomic.Value;
}

