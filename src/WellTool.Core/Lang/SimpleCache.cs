using System;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Lang.Mutable;
using WellTool.Core.Lang.Ref;

namespace WellTool.Core.Lang;

/// <summary>
/// 简单缓存，无超时实现，默认使用弱引用实现缓存自动清理
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class SimpleCache<K, V> : IEnumerable<KeyValuePair<K, V>> where K : class
{
	private readonly Dictionary<Mutable<K>, V> _rawMap;
	private readonly object _lock = new();
	private readonly Dictionary<K, object> _keyLockMap = new();

	/// <summary>
	/// 构造，默认使用弱引用实现缓存自动清理
	/// </summary>
	public SimpleCache()
	{
		_rawMap = new Dictionary<Mutable<K>, V>();
	}

	/// <summary>
	/// 从缓存池中查找值
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>值</returns>
	public V Get(K key)
	{
		lock (_lock)
		{
			return _rawMap.TryGetValue(MutableObj<K>.Of(key), out var value) ? value : default;
		}
	}

	/// <summary>
	/// 从缓存中获得对象，当对象不在缓存中或已经过期返回supplier产生的对象
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="supplier">如果不存在回调方法，用于生产值对象</param>
	/// <returns>值对象</returns>
	public V Get(K key, Func0<V> supplier)
	{
		return Get(key, null, supplier);
	}

	/// <summary>
	/// 从缓存中获得对象，当对象不在缓存中或已经过期返回supplier产生的对象
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="validPredicate">检查结果对象是否可用</param>
	/// <param name="supplier">如果不存在回调方法或结果不可用，用于生产值对象</param>
	/// <returns>值对象</returns>
	public V Get(K key, Func<bool, V> validPredicate, Func0<V> supplier)
	{
		V v = Get(key);
		if (validPredicate != null && v != null && !validPredicate(v))
		{
			v = default;
		}
		if (v == null && supplier != null)
		{
			object keyLock = _keyLockMap.GetOrAdd(key, _ => new object());
			lock (keyLock)
			{
				// 双重检查
				v = Get(key);
				if (v == null || (validPredicate != null && !validPredicate(v)))
				{
					try
					{
						v = supplier.Call();
					}
					catch (Exception e)
					{
						throw new Exception("Supplier execution failed", e);
					}
					Put(key, v);
				}
			}
			_keyLockMap.Remove(key);
		}
		return v;
	}

	/// <summary>
	/// 放入缓存
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	/// <returns>值</returns>
	public V Put(K key, V value)
	{
		lock (_lock)
		{
			_rawMap[MutableObj<K>.Of(key)] = value;
		}
		return value;
	}

	/// <summary>
	/// 移除缓存
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>移除的值</returns>
	public V Remove(K key)
	{
		lock (_lock)
		{
			var mutableKey = MutableObj<K>.Of(key);
			if (_rawMap.TryGetValue(mutableKey, out var value))
			{
				_rawMap.Remove(mutableKey);
				return value;
			}
			return default;
		}
	}

	/// <summary>
	/// 清空缓存池
	/// </summary>
	public void Clear()
	{
		lock (_lock)
		{
			_rawMap.Clear();
		}
	}

	public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
	{
		return _rawMap.Select(entry => new KeyValuePair<K, V>(entry.Key.Get(), entry.Value)).GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
