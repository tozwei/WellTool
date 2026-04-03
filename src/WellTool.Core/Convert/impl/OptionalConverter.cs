namespace WellTool.Core.Convert.impl;

using System;

/// <summary>
/// {@link Optional}对象转换器
/// </summary>
public class OptionalConverter : AbstractConverter<Optional?>
{
	private const long SerialVersionUid = 1L;

	/// <summary>
	/// 转换内部方法
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>转换后的Optional</returns>
	protected override Optional<object> ConvertInternal(object value)
	{
		return Optional<object>.OfNullable(value);
	}
}
