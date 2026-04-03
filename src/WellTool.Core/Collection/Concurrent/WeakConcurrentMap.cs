namespace WellTool.Core.collection.concurrent;

using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// 弱键并发映射
/// </summary>
public class WeakKeyConcurrentMap<TKey, TValue> where TKey : class
{
	private readonly Dictionary<WeakReference<TKey>, TValue> _map = new();
	private readonly object _lock = new();

	public TValue? Get(TKey key)
	{
		lock (_lock)
		{
			Cleanup();
			foreach (var kvp in _map)
			{
				if (kvp.Key.TryGetTarget(out var target) && target == key)
					return kvp.Value;
			}
			return default;
		}
	}

	public void Put(TKey key, TValue value)
	{
		lock (_lock)
		{
			Cleanup();
			_map[new WeakReference<TKey>(key)] = value;
		}
	}

	public bool TryGet(TKey key, out TValue? value)
	{
		lock (_lock)
		{
			Cleanup();
			foreach (var kvp in _map)
			{
				if (kvp.Key.TryGetTarget(out var target) && target == key)
				{
					value = kvp.Value;
					return true;
				}
			}
			value = default;
			return false;
		}
	}

	public bool Remove(TKey key)
	{
		lock (_lock)
		{
			var keysToRemove = new List<WeakReference<TKey>>();
			foreach (var kvp in _map)
			{
				if (kvp.Key.TryGetTarget(out var target) && target == key)
					keysToRemove.Add(kvp.Key);
			}
			foreach (var keyRef in keysToRemove)
				_map.Remove(keyRef);
			return keysToRemove.Count > 0;
		}
	}

	private void Cleanup()
	{
		var keysToRemove = new List<WeakReference<TKey>>();
		foreach (var kvp in _map)
		{
			if (!kvp.Key.TryGetTarget(out _))
				keysToRemove.Add(kvp.Key);
		}
		foreach (var key in keysToRemove)
			_map.Remove(key);
	}
}

/// <summary>
/// 弱键值并发映射
/// </summary>
public class WeakKeyValueConcurrentMap<TKey, TValue> where TKey : class where TValue : class
{
	private readonly Dictionary<WeakReference<TKey>, WeakReference<TValue>> _map = new();
	private readonly object _lock = new();

	public TValue? Get(TKey key)
	{
		lock (_lock)
		{
			Cleanup();
			foreach (var kvp in _map)
			{
				if (kvp.Key.TryGetTarget(out var target) && target == key)
				{
					if (kvp.Value.TryGetTarget(out var value))
						return value;
				}
			}
			return default;
		}
	}

	public void Put(TKey key, TValue value)
	{
		lock (_lock)
		{
			Cleanup();
			_map[new WeakReference<TKey>(key)] = new WeakReference<TValue>(value);
		}
	}

	private void Cleanup()
	{
		var keysToRemove = new List<WeakReference<TKey>>();
		foreach (var kvp in _map)
		{
			if (!kvp.Key.TryGetTarget(out _) || !kvp.Value.TryGetTarget(out _))
				keysToRemove.Add(kvp.Key);
		}
		foreach (var key in keysToRemove)
			_map.Remove(key);
	}
}
