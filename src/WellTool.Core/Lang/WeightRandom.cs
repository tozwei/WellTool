namespace WellTool.Core.Lang;

/// <summary>
/// 加权随机选择器
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class WeightRandom<T>
{
	private readonly List<WeightedItem> _items = new();
	private double _totalWeight;
	private readonly Random _random = new();

	/// <summary>
	/// 添加带权重的元素
	/// </summary>
	/// <param name="item">元素</param>
	/// <param name="weight">权重</param>
	/// <returns>this</returns>
	public WeightRandom<T> Add(T item, double weight)
	{
		if (weight < 0)
			throw new ArgumentException("Weight must be non-negative", nameof(weight));

		_items.Add(new WeightedItem(item, weight));
		_totalWeight += weight;
		return this;
	}

	/// <summary>
	/// 清除所有元素
	/// </summary>
	/// <returns>this</returns>
	public WeightRandom<T> Clear()
	{
		_items.Clear();
		_totalWeight = 0;
		return this;
	}

	/// <summary>
	/// 获取随机元素
	/// </summary>
	/// <returns>选中的元素</returns>
	public T Next()
	{
		if (_items.Count == 0)
			throw new InvalidOperationException("No items added");

		var randomValue = _random.NextDouble() * _totalWeight;
		double cumulative = 0;

		foreach (var item in _items)
		{
			cumulative += item.Weight;
			if (randomValue <= cumulative)
				return item.Item;
		}

		return _items[^1].Item;
	}

	/// <summary>
	/// 获取随机元素（使用指定随机数生成器）
	/// </summary>
	/// <param name="random">随机数生成器</param>
	/// <returns>选中的元素</returns>
	public T Next(Random random)
	{
		if (_items.Count == 0)
			throw new InvalidOperationException("No items added");

		var randomValue = random.NextDouble() * _totalWeight;
		double cumulative = 0;

		foreach (var item in _items)
		{
			cumulative += item.Weight;
			if (randomValue <= cumulative)
				return item.Item;
		}

		return _items[^1].Item;
	}

	private class WeightedItem
	{
		public T Item { get; }
		public double Weight { get; }

		public WeightedItem(T item, double weight)
		{
			Item = item;
			Weight = weight;
		}
	}

	/// <summary>
	/// 创建加权随机选择器
	/// </summary>
	public static WeightRandom<T> Create() => new();

	/// <summary>
	/// 创建加权随机选择器并添加初始元素
	/// </summary>
	public static WeightRandom<T> Create(params (T item, double weight)[] items)
	{
		var random = new WeightRandom<T>();
		foreach (var (item, weight) in items)
			random.Add(item, weight);
		return random;
	}
}


