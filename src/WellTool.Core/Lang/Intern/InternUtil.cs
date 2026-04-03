namespace WellTool.Core.lang.intern;

using System;
using System.Collections.Generic;

/// <summary>
/// 字符串驻留接口
/// </summary>
public interface IInterner<T>
{
	/// <summary>
	/// 获取或添加驻留对象
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>驻留后的值</returns>
	T Intern(T value);
}

/// <summary>
/// JDK字符串驻留器
/// </summary>
public class JdkStringInterner : IInterner<string>
{
	private readonly Dictionary<string, string> _cache = new();

	public string Intern(string value)
	{
		if (value == null) return value;
		if (_cache.TryGetValue(value, out var existing))
			return existing;
		_cache[value] = value;
		return value;
	}
}

/// <summary>
/// 弱引用字符串驻留器
/// </summary>
public class WeakInterner : IInterner<string>
{
	private readonly Dictionary<string, WeakReference<string>> _cache = new();

	public string Intern(string value)
	{
		if (value == null) return value;

		if (_cache.TryGetValue(value, out var weakRef))
		{
			if (weakRef.TryGetTarget(out var existing))
				return existing;
		}

		_cache[value] = new WeakReference<string>(value);
		return value;
	}

	/// <summary>
	/// 清理已回收的引用
	/// </summary>
	public void Clean()
	{
		var keysToRemove = new List<string>();
		foreach (var kvp in _cache)
		{
			if (!kvp.Value.TryGetTarget(out _))
				keysToRemove.Add(kvp.Key);
		}
		foreach (var key in keysToRemove)
			_cache.Remove(key);
	}
}
