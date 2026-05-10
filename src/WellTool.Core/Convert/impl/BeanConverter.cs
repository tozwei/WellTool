using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// Bean杞崲鍣?
/// </summary>
public class BeanConverter : AbstractConverter<object>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
	protected override object ConvertInternal(object value)
	{
		return value;
	}
}

