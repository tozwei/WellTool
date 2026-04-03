using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang;

/// <summary>
/// 一致性哈希接口
/// </summary>
/// <typeparam name="T">节点类型</typeparam>
public interface IConsistentHash<T>
{
	/// <summary>
	/// 添加节点
	/// </summary>
	/// <param name="node">节点</param>
	void Add(T node);

	/// <summary>
	/// 移除节点
	/// </summary>
	/// <param name="node">节点</param>
	void Remove(T node);

	/// <summary>
	/// 获取最近的节点
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>节点</returns>
	T Get(object key);
}

/// <summary>
/// 虚拟节点一致性哈希
/// </summary>
/// <typeparam name="T">节点类型</typeparam>
public class VirtualConsistentHash<T> : IConsistentHash<T>
{
	private readonly int _virtualNodes;
	private readonly SortedDictionary<int, T> _ring = new();
	private readonly IEqualityComparer<T> _comparer;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="virtualNodes">虚拟节点数</param>
	public VirtualConsistentHash(int virtualNodes = 150)
	{
		_virtualNodes = virtualNodes;
		_comparer = EqualityComparer<T>.Default;
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="virtualNodes">虚拟节点数</param>
	/// <param name="comparer">比较器</param>
	public VirtualConsistentHash(int virtualNodes, IEqualityComparer<T> comparer)
	{
		_virtualNodes = virtualNodes;
		_comparer = comparer ?? EqualityComparer<T>.Default;
	}

	/// <summary>
	/// 添加节点
	/// </summary>
	public void Add(T node)
	{
		for (int i = 0; i < _virtualNodes; i++)
		{
			int hash = GetHash(node.ToString() + i);
			_ring[hash] = node;
		}
	}

	/// <summary>
	/// 移除节点
	/// </summary>
	public void Remove(T node)
	{
		for (int i = 0; i < _virtualNodes; i++)
		{
			int hash = GetHash(node.ToString() + i);
			_ring.Remove(hash);
		}
	}

	/// <summary>
	/// 获取最近的节点
	/// </summary>
	public T Get(object key)
	{
		if (_ring.Count == 0)
		{
			return default;
		}

		int hash = GetHash(key);
		if (!_ring.ContainsKey(hash))
		{
			int firstKey = -1;
			foreach (var k in _ring.Keys)
			{
				if (k > hash)
				{
					firstKey = k;
					break;
				}
			}
			if (firstKey == -1)
			{
				hash = _ring.Keys.Min();
			}
			else
			{
				hash = firstKey;
			}
		}

		return _ring[hash];
	}

	private int GetHash(object key)
	{
		return Math.Abs(key?.GetHashCode() ?? 0);
	}
}
