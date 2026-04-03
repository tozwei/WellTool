using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// Bean转换器
/// </summary>
public class BeanConverter : AbstractConverter<object>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override object ConvertInternal(object value)
	{
		return value;
	}
}
