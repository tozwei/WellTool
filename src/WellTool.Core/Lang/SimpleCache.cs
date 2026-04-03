using System.Collections.Concurrent;

namespace WellTool.Core.Lang;

/// <summary>
/// 简单缓存，无超时机制
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class SimpleCache<K, V> where K : notnull
{
	private readonly ConcurrentDictionary<K, V> _cache = new ConcurrentDictionary<K, V>();
	private readonly ConcurrentDictionary<K, DateTime> _timetable = new ConcurrentDictionary<K, DateTime>();

	/// <summary>
	/// 获取缓存值，如果不存在则调用工厂方法创建
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="factory">工厂方法</param>
	/// <returns>缓存值</returns>
	public V GetOrCreate(K key, Func<V> factory)
	{
		if (_cache.TryGetValue(key, out var value))
			return value;

		value = factory();
		_cache[key] = value;
		_timetable[key] = DateTime.Now;
		return value;
	}

	/// <summary>
	/// 获取缓存值
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>缓存值</returns>
	public V? Get(K key)
	{
		_cache.TryGetValue(key, out var value);
		return value;
	}

	/// <summary>
	/// 设置缓存值
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	public void Put(K key, V value)
	{
		_cache[key] = value;
		_timetable[key] = DateTime.Now;
	}

	/// <summary>
	/// 移除缓存
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>被移除的值</returns>
	public V? Remove(K key)
	{
		_timetable.TryRemove(key, out _);
		_cache.TryRemove(key, out var value);
		return value;
	}

	/// <summary>
	/// 清空缓存
	/// </summary>
	public void Clear()
	{
		_cache.Clear();
		_timetable.Clear();
	}

	/// <summary>
	/// 获取缓存数量
	/// </summary>
	public int Count => _cache.Count;

	/// <summary>
	/// 是否包含键
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>是否包含</returns>
	public bool ContainsKey(K key)
	{
		return _cache.ContainsKey(key);
	}

	/// <summary>
	/// 获取缓存的创建时间
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>创建时间</returns>
	public DateTime? GetCreateTime(K key)
	{
		return _timetable.TryGetValue(key, out var time) ? time : null;
	}

	/// <summary>
	/// 获取所有键
	/// </summary>
	/// <returns>键集合</returns>
	public IEnumerable<K> Keys => _cache.Keys;

	/// <summary>
	/// 获取所有值
	/// </summary>
	/// <returns>值集合</returns>
	public IEnumerable<V> Values => _cache.Values;
}
