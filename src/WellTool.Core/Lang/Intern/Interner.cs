using System.Collections.Generic;

namespace WellTool.Core.Lang.Intern;

/// <summary>
/// 字符串驻留器
/// </summary>
public class StringInterner : IInterner<string>
{
	private readonly Dictionary<string, string> _interned = new();

	/// <summary>
	/// 获取驻留的字符串
	/// </summary>
	/// <param name="str">原始字符串</param>
	/// <returns>驻留后的字符串</returns>
	public string Intern(string str)
	{
		if (str == null)
		{
			return null;
		}

		if (_interned.TryGetValue(str, out var interned))
		{
			return interned;
		}

		_interned[str] = str;
		return str;
	}
}

/// <summary>
/// 弱引用字符串驻留器
/// </summary>
public class WeakStringInterner
{
	private readonly Dictionary<string, WeakReference<string>> _interned = new();
	private readonly List<string> _pool = new();
	private const int MaxPoolSize = 1000;

	/// <summary>
	/// 获取驻留的字符串
	/// </summary>
	/// <param name="str">原始字符串</param>
	/// <returns>驻留后的字符串</returns>
	public string Intern(string str)
	{
		if (str == null)
		{
			return null;
		}

		if (_interned.TryGetValue(str, out var weakRef))
		{
			if (weakRef.TryGetTarget(out var existing))
			{
				return existing;
			}
		}

		// 清理过期的引用
		CleanupExpired();

		// 添加到池
		if (_pool.Count >= MaxPoolSize)
		{
			_pool.RemoveAt(0);
		}
		_pool.Add(str);
		_interned[str] = new WeakReference<string>(str);

		return str;
	}

	private void CleanupExpired()
	{
		var keysToRemove = new List<string>();
		foreach (var kvp in _interned)
		{
			if (!kvp.Value.TryGetTarget(out _))
			{
				keysToRemove.Add(kvp.Key);
			}
		}
		foreach (var key in keysToRemove)
		{
			_interned.Remove(key);
		}
	}
}
