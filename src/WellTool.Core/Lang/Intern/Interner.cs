using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang.Intern;

/// <summary>
/// 字符串驻留器接口
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public interface IInterner<T>
{
	/// <summary>
	/// 获取或添加驻留值
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>驻留后的值</returns>
	T Intern(T value);
}

/// <summary>
/// 字符串驻留器实现
/// </summary>
public class StringInterner : IInterner<string>
{
	private readonly Dictionary<string, string> _interned = new();
	private readonly object _lock = new();

	/// <inheritdoc />
	public string Intern(string value)
	{
		if (value == null)
			return null!;

		lock (_lock)
		{
			if (_interned.TryGetValue(value, out var interned))
				return interned;

			_interned[value] = value;
			return value;
		}
	}

	/// <summary>
	/// 清空驻留缓存
	/// </summary>
	public void Clear()
	{
		lock (_lock)
		{
			_interned.Clear();
		}
	}
}

/// <summary>
/// 弱引用驻留器，值可以被GC回收
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public class WeakInterner<T> where T : class
{
	private readonly Dictionary<T, WeakReference<T>> _interned = new();
	private readonly Dictionary<int, List<WeakReference<T>>> _buckets = new();
	private readonly object _lock = new();
	private readonly int _bucketCount;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="bucketCount">桶数量</param>
	public WeakInterner(int bucketCount = 64)
	{
		_bucketCount = bucketCount;
	}

	/// <summary>
	/// 获取或添加驻留值
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>驻留后的值</returns>
	public T Intern(T value)
	{
		if (value == null)
			return null!;

		lock (_lock)
		{
			var hash = value.GetHashCode();
			var bucketIndex = Math.Abs(hash % _bucketCount);

			if (!_buckets.ContainsKey(bucketIndex))
				_buckets[bucketIndex] = new List<WeakReference<T>>();

			var bucket = _buckets[bucketIndex];

			// 清理已回收的引用
			CleanupBucket(bucket);

			// 查找现有值
			foreach (var weakRef in bucket)
			{
				if (weakRef.TryGetTarget(out var existing) && existing.Equals(value))
					return existing;
			}

			// 添加新值
			var newRef = new WeakReference<T>(value);
			bucket.Add(newRef);
			_interned[value] = newRef;
			return value;
		}
	}

	/// <summary>
	/// 清空驻留缓存
	/// </summary>
	public void Clear()
	{
		lock (_lock)
		{
			_interned.Clear();
			_buckets.Clear();
		}
	}

	private void CleanupBucket(List<WeakReference<T>> bucket)
	{
		bucket.RemoveAll(wr => !wr.TryGetTarget(out _));
	}
}

/// <summary>
/// JDK风格的字符串驻留器
/// </summary>
public static class InternUtil
{
	private static readonly StringInterner StringInterner = new();

	/// <summary>
	/// 驻留字符串
	/// </summary>
	/// <param name="value">字符串</param>
	/// <returns>驻留后的字符串</returns>
	public static string Intern(string value)
	{
		return StringInterner.Intern(value);
	}

	/// <summary>
	/// 清空字符串驻留缓存
	/// </summary>
	public static void ClearInternCache()
	{
		StringInterner.Clear();
	}
}
