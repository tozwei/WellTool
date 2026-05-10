using System;
using System.Collections.Generic;
using SystemDynamic = System.Dynamic;
using System.Reflection;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// Record杞崲鍣?
/// </summary>
public class RecordConverter : AbstractConverter<SystemDynamic.ExpandoObject>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
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

