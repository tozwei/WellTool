namespace WellTool.Core.collection.concurrent;

using System;
using System.Collections.Concurrent;

/// <summary>
/// 并发映射接口
/// </summary>
public interface IConcurrentMap<K, V>
{
	/// <summary>
	/// 获取值
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>值</returns>
	V? Get(K key);

	/// <summary>
	/// 放入值
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	/// <returns>旧值</returns>
	V? Put(K key, V value);

	/// <summary>
	/// 尝试放入值
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	/// <returns>是否放入成功</returns>
	bool TryPut(K key, V value);

	/// <summary>
	/// 移除值
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>旧值</returns>
	V? Remove(K key);

	/// <summary>
	/// 是否包含键
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>是否包含</returns>
	bool ContainsKey(K key);
}

/// <summary>
/// 基于ConcurrentDictionary的并发映射实现
/// </summary>
public class ConcurrentMap<K, V> : IConcurrentMap<K, V>
{
	private readonly ConcurrentDictionary<K, V> _map;

	public ConcurrentMap()
	{
		_map = new ConcurrentDictionary<K, V>();
	}

	public ConcurrentMap(int capacity)
	{
		_map = new ConcurrentDictionary<K, V>(Environment.ProcessorCount, capacity);
	}

	public V? Get(K key)
	{
		_map.TryGetValue(key, out V? value);
		return value;
	}

	public V? Put(K key, V value)
	{
		_map.TryAdd(key, value);
		return value;
	}

	public bool TryPut(K key, V value)
	{
		return _map.TryAdd(key, value);
	}

	public V? Remove(K key)
	{
		_map.TryRemove(key, out V? value);
		return value;
	}

	public bool ContainsKey(K key)
	{
		return _map.ContainsKey(key);
	}
}

/// <summary>
/// 基于WeakReference的软引用并发映射
/// </summary>
public class SoftConcurrentMap<K, V> where V : class
{
	private readonly ConcurrentDictionary<K, WeakReference<V>> _map = new();

	public V? Get(K key)
	{
		if (_map.TryGetValue(key, out var weakRef))
		{
			if (weakRef.TryGetTarget(out var target))
			{
				return target;
			}
			_map.TryRemove(key, out _);
		}
		return null;
	}

	public void Put(K key, V value)
	{
		_map[key] = new WeakReference<V>(value);
	}

	public bool ContainsKey(K key)
	{
		return _map.ContainsKey(key);
	}

	public void Clear()
	{
		_map.Clear();
	}
}
