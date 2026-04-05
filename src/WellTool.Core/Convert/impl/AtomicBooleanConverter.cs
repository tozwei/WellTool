using System;
using System.Collections.Generic;
using System.Threading;
using WellTool.Core.Util;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicBoolean转换器
/// </summary>
public class AtomicBooleanConverter : AbstractConverter<AtomicBoolean>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
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
/// AtomicBoolean封装类
/// </summary>
public class AtomicBoolean
{
	private int _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="initialValue">初始值</param>
	public AtomicBoolean(bool initialValue)
	{
		_value = initialValue ? 1 : 0;
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public bool Value => _value == 1;

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="newValue">新值</param>
	public void Set(bool newValue)
	{
		Interlocked.Exchange(ref _value, newValue ? 1 : 0);
	}

	/// <summary>
	/// 转换为bool
	/// </summary>
	public static implicit operator bool(AtomicBoolean atomic) => atomic.Value;
}
