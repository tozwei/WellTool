using System;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Lang.Hash;

namespace WellTool.Core.Lang;

/// <summary>
/// 一致性Hash算法
/// </summary>
/// <typeparam name="T">节点类型</typeparam>
public class ConsistentHash<T>
{
	/// <summary>
	/// Hash计算对象，用于自定义hash算法
	/// </summary>
	private Hash32<object> _hashFunc;
	/// <summary>
	/// 复制的节点个数
	/// </summary>
	private readonly int _numberOfReplicas;
	/// <summary>
	/// 一致性Hash环
	/// </summary>
	private readonly SortedDictionary<int, T> _circle = new();

	/// <summary>
	/// 构造，使用默认的Hash算法
	/// </summary>
	/// <param name="numberOfReplicas">复制的节点个数，增加每个节点的复制节点有利于负载均衡</param>
	/// <param name="nodes">节点对象</param>
	public ConsistentHash(int numberOfReplicas, IEnumerable<T> nodes)
	{
		_numberOfReplicas = numberOfReplicas;
		_hashFunc = key => HashUtil.FnvHash(key.ToString());
		//初始化节点
		foreach (T node in nodes)
		{
			Add(node);
		}
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="hashFunc">hash算法对象</param>
	/// <param name="numberOfReplicas">复制的节点个数，增加每个节点的复制节点有利于负载均衡</param>
	/// <param name="nodes">节点对象</param>
	public ConsistentHash(Hash32<object> hashFunc, int numberOfReplicas, IEnumerable<T> nodes)
	{
		_numberOfReplicas = numberOfReplicas;
		_hashFunc = hashFunc;
		//初始化节点
		foreach (T node in nodes)
		{
			Add(node);
		}
	}

	/// <summary>
	/// 增加节点
	/// </summary>
	/// <param name="node">节点对象</param>
	public void Add(T node)
	{
		for (int i = 0; i < _numberOfReplicas; i++)
		{
			_circle[_hashFunc(node.ToString() + i)] = node;
		}
	}

	/// <summary>
	/// 移除节点的同时移除相应的虚拟节点
	/// </summary>
	/// <param name="node">节点对象</param>
	public void Remove(T node)
	{
		for (int i = 0; i < _numberOfReplicas; i++)
		{
			_circle.Remove(_hashFunc(node.ToString() + i));
		}
	}

	/// <summary>
	/// 获得一个最近的顺时针节点
	/// </summary>
	/// <param name="key">为给定键取Hash，取得顺时针方向上最近的一个虚拟节点对应的实际节点</param>
	/// <returns>节点对象</returns>
	public T Get(object key)
	{
		if (_circle.Count == 0)
		{
			return default;
		}
		int hash = _hashFunc(key);
		if (!_circle.ContainsKey(hash))
		{
			SortedDictionary<int, T>.KeyCollection keys = _circle.Keys;
			// 返回此映射的部分视图，其键大于等于 hash
			int firstKey = keys.FirstOrDefault(k => k >= hash);
			if (firstKey == 0 && !keys.Contains(hash))
			{
				hash = keys.First();
			}
			else
			{
				hash = firstKey;
			}
		}
		//正好命中
		return _circle[hash];
	}
}
