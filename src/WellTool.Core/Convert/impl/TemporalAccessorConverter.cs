using System;

namespace WellDone.Core.Convert.Impl;

/// <summary>
/// TemporalAccessorConverter转换器
/// </summary>
public class TemporalAccessorConverter : AbstractConverter<DateTime>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override DateTime ConvertInternal(object value)
	{
		if (value is DateTime dt)
		{
			return dt;
		}
		if (value is long l)
		{
			return DateTime.FromFileTimeUtc(l);
		}
		if (value is string s)
		{
			return DateTime.Parse(s);
		}
		throw new NotSupportedException($"Cannot convert {value?.GetType()} to DateTime");
	}
}
