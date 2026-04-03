using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang;

/// <summary>
/// 加权列表随机
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class WeightListRandom<T>
{
	private readonly List<WeightItem<T>> _items = new();
	private readonly Random _random = new Random();

	/// <summary>
	/// 加权项
	/// </summary>
	public class WeightItem
	{
		public T Item { get; set; }
		public double Weight { get; set; }
	}

	/// <summary>
	/// 添加元素
	/// </summary>
	/// <param name="item">元素</param>
	/// <param name="weight">权重</param>
	public void Add(T item, double weight)
	{
		_items.Add(new WeightItem { Item = item, Weight = weight });
	}

	/// <summary>
	/// 获取随机元素
	/// </summary>
	public T Next()
	{
		if (_items.Count == 0)
		{
			return default;
		}

		double totalWeight = _items.Sum(x => x.Weight);
		double randomValue = _random.NextDouble() * totalWeight;
		double cumulativeWeight = 0;

		foreach (var item in _items)
		{
			cumulativeWeight += item.Weight;
			if (randomValue <= cumulativeWeight)
			{
				return item.Item;
			}
		}

		return _items.Last().Item;
	}

	/// <summary>
	/// 获取随机元素列表
	/// </summary>
	/// <param name="count">数量</param>
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
	/// 获取元素数量
	/// </summary>
	public int Count => _items.Count;

	/// <summary>
	/// 清空
	/// </summary>
	public void Clear()
	{
		_items.Clear();
	}
}
