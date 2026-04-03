using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang;

/// <summary>
/// 加权随机接口
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public interface IWeightRandom<T>
{
	/// <summary>
	/// 添加元素
	/// </summary>
	/// <param name="item">元素</param>
	/// <param name="weight">权重</param>
	void Add(T item, double weight);

	/// <summary>
	/// 移除元素
	/// </summary>
	/// <param name="item">元素</param>
	void Remove(T item);

	/// <summary>
	/// 获取随机元素
	/// </summary>
	/// <returns>随机元素</returns>
	T Next();

	/// <summary>
	/// 获取随机元素
	/// </summary>
	/// <param name="count">数量</param>
	/// <returns>随机元素列表</returns>
	List<T> Next(int count);
}

/// <summary>
/// 加权随机实现
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class WeightRandom<T> : IWeightRandom<T>
{
	private readonly Dictionary<T, double> _weights = new();
	private readonly Random _random = new Random();
	private double _totalWeight;

	/// <summary>
	/// 添加元素
	/// </summary>
	public void Add(T item, double weight)
	{
		if (_weights.ContainsKey(item))
		{
			_totalWeight -= _weights[item];
		}
		_weights[item] = weight;
		_totalWeight += weight;
	}

	/// <summary>
	/// 移除元素
	/// </summary>
	public void Remove(T item)
	{
		if (_weights.TryGetValue(item, out var weight))
		{
			_weights.Remove(item);
			_totalWeight -= weight;
		}
	}

	/// <summary>
	/// 获取随机元素
	/// </summary>
	public T Next()
	{
		if (_weights.Count == 0)
		{
			return default;
		}

		double randomValue = _random.NextDouble() * _totalWeight;
		double cumulativeWeight = 0;

		foreach (var kvp in _weights)
		{
			cumulativeWeight += kvp.Value;
			if (randomValue <= cumulativeWeight)
			{
				return kvp.Key;
			}
		}

		return default;
	}

	/// <summary>
	/// 获取随机元素列表
	/// </summary>
	public List<T> Next(int count)
	{
		var result = new List<T>();
		for (int i = 0; i < count; i++)
		{
			result.Add(Next());
		}
		return result;
	}

	/// <summary>
	/// 获取权重
	/// </summary>
	public double GetWeight(T item)
	{
		return _weights.TryGetValue(item, out var weight) ? weight : 0;
	}

	/// <summary>
	/// 获取元素数量
	/// </summary>
	public int Count => _weights.Count;

	/// <summary>
	/// 清空
	/// </summary>
	public void Clear()
	{
		_weights.Clear();
		_totalWeight = 0;
	}
}
