namespace WellTool.Core.collection.watch;

using System;
using System.Collections.Generic;

/// <summary>
/// 延迟观察器接口
/// </summary>
public interface IDelayWatcher
{
	/// <summary>
	/// 添加观察项
	/// </summary>
	/// <param name="item">观察项</param>
	/// <param name="delay">延迟时间</param>
	void Add(object item, TimeSpan delay);

	/// <summary>
	/// 移除观察项
	/// </summary>
	/// <param name="item">观察项</param>
	void Remove(object item);

	/// <summary>
	/// 获取待处理的项
	/// </summary>
	/// <returns>项列表</returns>
	IList<object> Get待处理();
}

/// <summary>
/// 延迟观察器实现
/// </summary>
public class DelayWatcher : IDelayWatcher
{
	private readonly Dictionary<object, DateTime> _items = new();

	public void Add(object item, TimeSpan delay)
	{
		_items[item] = DateTime.Now.Add(delay);
	}

	public void Remove(object item)
	{
		_items.Remove(item);
	}

	public IList<object> Get待处理()
	{
		var now = DateTime.Now;
		var result = new List<object>();
		var toRemove = new List<object>();

		foreach (var kvp in _items)
		{
			if (kvp.Value <= now)
			{
				result.Add(kvp.Key);
				toRemove.Add(kvp.Key);
			}
		}

		foreach (var item in toRemove)
		{
			_items.Remove(item);
		}

		return result;
	}
}

/// <summary>
/// 观察链接口
/// </summary>
public interface IWatcherChain
{
	/// <summary>
	/// 处理项
	/// </summary>
	/// <param name="item">项</param>
	void DoProcess(object item);
}

/// <summary>
/// 观察链实现
/// </summary>
public class WatcherChain : IWatcherChain
{
	private readonly IList<IWatcher> _watchers = new List<IWatcher>();

	public void AddWatcher(IWatcher watcher)
	{
		_watchers.Add(watcher);
	}

	public void DoProcess(object item)
	{
		foreach (var watcher in _watchers)
		{
			watcher.OnProcess(item);
		}
	}
}

/// <summary>
/// 观察器接口
/// </summary>
public interface IWatcher
{
	/// <summary>
	/// 处理
	/// </summary>
	/// <param name="item">项</param>
	void OnProcess(object item);
}
