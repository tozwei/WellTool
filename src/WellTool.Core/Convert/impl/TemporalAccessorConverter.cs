using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// TemporalAccessorConverter杞崲鍣?
/// </summary>
public class TemporalAccessorConverter : AbstractConverter<DateTime>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
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

