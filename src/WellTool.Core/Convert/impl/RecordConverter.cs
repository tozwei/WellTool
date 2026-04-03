using System;
using System.Collections.Generic;
using SystemDynamic = System.Dynamic;
using System.Reflection;

namespace WellDone.Core.Convert.Impl;

/// <summary>
/// Record转换器
/// </summary>
public class RecordConverter : AbstractConverter<SystemDynamic.ExpandoObject>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override SystemDynamic.ExpandoObject ConvertInternal(object value)
	{
		if (value is SystemDynamic.ExpandoObject expando)
		{
			return expando;
		}
		if (value is IDictionary<string, object> dict)
		{
			return ConvertDictionaryToExpando(dict);
		}
		throw new NotSupportedException($"Cannot convert {value?.GetType()} to ExpandoObject");
	}

	private static SystemDynamic.ExpandoObject ConvertDictionaryToExpando(IDictionary<string, object> dict)
	{
		var result = new SystemDynamic.ExpandoObject();
		var dictResult = (IDictionary<string, object>)result;
		foreach (var kvp in dict)
		{
			dictResult[kvp.Key] = kvp.Value;
		}
		return result;
	}
}
