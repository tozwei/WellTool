using System;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Bean;
using WellTool.Core.Convert;
using WellTool.Core.Util;

namespace WellTool.Core.Lang;

/// <summary>
/// 字典对象，扩充了HashMap中的方法
/// </summary>
[Serializable]
public class Dict : Dictionary<string, object>
{
	private const float DEFAULT_LOAD_FACTOR = 0.75f;
	private const int DEFAULT_INITIAL_CAPACITY = 16;

	/// <summary>
	/// 是否大小写不敏感
	/// </summary>
	private bool _caseInsensitive;

	/// <summary>
	/// 创建Dict
	/// </summary>
	/// <returns>Dict</returns>
	public static Dict Create()
	{
		return new Dict();
	}

	/// <summary>
	/// 将PO对象转为Dict
	/// </summary>
	/// <typeparam name="T">Bean类型</typeparam>
	/// <param name="bean">Bean对象</param>
	/// <returns>Vo</returns>
	public static Dict Parse<T>(T bean)
	{
		return Create().ParseBean(bean);
	}

	/// <summary>
	/// 根据给定的键值对数组创建Dict对象
	/// </summary>
	/// <param name="keysAndValues">键值对列表，必须奇数参数为key，偶数参数为value</param>
	/// <returns>Dict</returns>
	public static Dict Of(params object[] keysAndValues)
	{
		var dict = Create();
		string key = null;
		for (int i = 0; i < keysAndValues.Length; i++)
		{
			if (i % 2 == 0)
			{
				key = WellTool.Core.Convert.ConvertUtil.ToStr(keysAndValues[i]);
			}
			else
			{
				dict[key] = keysAndValues[i];
			}
		}
		return dict;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public Dict() : base(DEFAULT_INITIAL_CAPACITY)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="caseInsensitive">是否大小写不敏感</param>
	public Dict(bool caseInsensitive) : base(DEFAULT_INITIAL_CAPACITY)
	{
		_caseInsensitive = caseInsensitive;
	}

	/// <summary>
	/// 转换为Bean对象
	/// </summary>
	/// <typeparam name="T">Bean类型</typeparam>
	/// <param name="bean">Bean</param>
	/// <returns>Bean</returns>
	public T ToBean<T>(T bean)
	{
		return ToBean(bean, false);
	}

	/// <summary>
	/// 转换为Bean对象
	/// </summary>
	/// <typeparam name="T">Bean类型</typeparam>
	/// <param name="bean">Bean</param>
	/// <param name="isToCamelCase">是否转换为驼峰模式</param>
	/// <returns>Bean</returns>
	public T ToBean<T>(T bean, bool isToCamelCase)
	{
		return BeanUtil.FillBean(bean, this);
	}

	/// <summary>
	/// 填充Value Object对象
	/// </summary>
	/// <typeparam name="T">Bean类型</typeparam>
	/// <param name="clazz">Value Object的类</param>
	/// <returns>vo</returns>
	public T ToBean<T>(Type clazz)
	{
		return (T)BeanUtil.ToBean(this, clazz);
	}

	/// <summary>
	/// 将值对象转换为Dict
	/// </summary>
	/// <typeparam name="T">Bean类型</typeparam>
	/// <param name="bean">值对象</param>
	/// <returns>自己</returns>
	public Dict ParseBean<T>(T bean)
	{
		Assert.NotNull(bean, "Bean class must be not null");
		var map = BeanUtil.BeanToMap(bean);
		foreach (var kvp in map)
		{
			this[kvp.Key] = kvp.Value;
		}
		return this;
	}

	/// <summary>
	/// 过滤Map保留指定键值对
	/// </summary>
	/// <param name="keys">键列表</param>
	/// <returns>Dict结果</returns>
	public Dict Filter(params string[] keys)
	{
		var result = new Dict(_caseInsensitive);
		foreach (string key in keys)
		{
			if (ContainsKey(key))
			{
				result[key] = this[key];
			}
		}
		return result;
	}

	/// <summary>
	/// 获得特定类型值
	/// </summary>
	/// <typeparam name="T">值类型</typeparam>
	/// <param name="attr">字段名</param>
	/// <returns>字段值</returns>
	public T Get<T>(string attr)
	{
		return TryGetValue(CustomKey(attr), out var result) ? WellTool.Core.Convert.ConvertUtil.To<T>(result) : default;
	}

	/// <summary>
	/// 获得特定类型值，带默认值
	/// </summary>
	/// <typeparam name="T">值类型</typeparam>
	/// <param name="attr">字段名</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>字段值</returns>
	public T Get<T>(string attr, T defaultValue)
	{
		var result = Get<T>(attr);
		return result != null ? result : defaultValue;
	}

	/// <summary>
	/// 通过表达式获取JSON中嵌套的对象
	/// </summary>
	/// <typeparam name="T">目标类型</typeparam>
	/// <param name="expression">表达式</param>
	/// <returns>对象</returns>
	public T GetByPath<T>(string expression)
	{
		return WellTool.Core.Convert.ConvertUtil.To<T>(BeanPath.Create(expression).Get(this));
	}

	/// <summary>
	/// 通过表达式获取JSON中嵌套的对象
	/// </summary>
	/// <typeparam name="T">返回值类型</typeparam>
	/// <param name="expression">表达式</param>
	/// <param name="resultType">返回值类型</param>
	/// <returns>对象</returns>
	public T GetByPath<T>(string expression, Type resultType)
	{
		return WellTool.Core.Convert.ConvertUtil.ConvertTo<T>(GetByPath<object>(expression));
	}

	/// <summary>
	/// 将Key转为小写
	/// </summary>
	/// <param name="key">KEY</param>
	/// <returns>处理后的KEY</returns>
	protected string CustomKey(string key)
	{
		if (_caseInsensitive && !string.IsNullOrEmpty(key))
		{
			key = key.ToLower();
		}
		return key;
	}

	public new bool ContainsKey(object key)
	{
		return base.ContainsKey(CustomKey(key?.ToString()));
	}

	public new object this[string key]
	{
		get => base.TryGetValue(CustomKey(key), out var value) ? value : null;
		set => base[CustomKey(key)] = value;
	}

	public new bool Remove(string key)
	{
		return base.Remove(CustomKey(key));
	}

	public new bool TryGetValue(string key, out object value)
	{
		return base.TryGetValue(CustomKey(key), out value);
	}
}
