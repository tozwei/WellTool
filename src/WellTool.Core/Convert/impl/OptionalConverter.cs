namespace WellTool.Core.Convert.Impl;

using System;

/// <summary>
/// {@link Optional}瀵硅薄杞崲鍣?
/// </summary>
public class OptionalConverter : AbstractConverter<object>
{
	private const long SerialVersionUid = 1L;

	/// <summary>
	/// 杞崲鍐呴儴鏂规硶
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>杞崲鍚庣殑Optional</returns>
	protected override object ConvertInternal(object value)
	{
		return value;
	}
}

