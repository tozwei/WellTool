namespace WellTool.Core.Lang;

/// <summary>
/// 元组接口，用于组合多个元素
/// </summary>
public interface ITuple
{
	/// <summary>
	/// 获取元素数量
	/// </summary>
	int Size { get; }

	/// <summary>
	/// 获取所有元素
	/// </summary>
	object?[] ToArray();
}

/// <summary>
/// 二元组
/// </summary>
/// <typeparam name="T1">第一个元素类型</typeparam>
/// <typeparam name="T2">第二个元素类型</typeparam>
public class Tuple<T1, T2> : ITuple
{
	/// <summary>
	/// 第一个元素
	/// </summary>
	public T1 Item1 { get; }

	/// <summary>
	/// 第二个元素
	/// </summary>
	public T2 Item2 { get; }

	/// <inheritdoc />
	public int Size => 2;

	/// <summary>
	/// 构造
	/// </summary>
	public Tuple(T1 item1, T2 item2)
	{
		Item1 = item1;
		Item2 = item2;
	}

	/// <inheritdoc />
	public object?[] ToArray() => new object?[] { Item1, Item2 };

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is not Tuple<T1, T2> other)
			return false;
		return Equals(Item1, other.Item1) && Equals(Item2, other.Item2);
	}

	/// <inheritdoc />
	public override int GetHashCode() => HashCode.Combine(Item1, Item2);

	/// <inheritdoc />
	public override string? ToString() => $"({Item1}, {Item2})";

	/// <summary>
	/// 解构
	/// </summary>
	public void Deconstruct(out T1 item1, out T2 item2)
	{
		item1 = Item1;
		item2 = Item2;
	}
}

/// <summary>
/// 三元组
/// </summary>
/// <typeparam name="T1">第一个元素类型</typeparam>
/// <typeparam name="T2">第二个元素类型</typeparam>
/// <typeparam name="T3">第三个元素类型</typeparam>
public class Tuple<T1, T2, T3> : ITuple
{
	/// <summary>
	/// 第一个元素
	/// </summary>
	public T1 Item1 { get; }

	/// <summary>
	/// 第二个元素
	/// </summary>
	public T2 Item2 { get; }

	/// <summary>
	/// 第三个元素
	/// </summary>
	public T3 Item3 { get; }

	/// <inheritdoc />
	public int Size => 3;

	/// <summary>
	/// 构造
	/// </summary>
	public Tuple(T1 item1, T2 item2, T3 item3)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
	}

	/// <inheritdoc />
	public object?[] ToArray() => new object?[] { Item1, Item2, Item3 };

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is not Tuple<T1, T2, T3> other)
			return false;
		return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) && Equals(Item3, other.Item3);
	}

	/// <inheritdoc />
	public override int GetHashCode() => HashCode.Combine(Item1, Item2, Item3);

	/// <inheritdoc />
	public override string? ToString() => $"({Item1}, {Item2}, {Item3})";

	/// <summary>
	/// 解构
	/// </summary>
	public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
	{
		item1 = Item1;
		item2 = Item2;
		item3 = Item3;
	}
}

/// <summary>
/// 四元组
/// </summary>
/// <typeparam name="T1">第一个元素类型</typeparam>
/// <typeparam name="T2">第二个元素类型</typeparam>
/// <typeparam name="T3">第三个元素类型</typeparam>
/// <typeparam name="T4">第四个元素类型</typeparam>
public class Tuple<T1, T2, T3, T4> : ITuple
{
	/// <summary>
	/// 第一个元素
	/// </summary>
	public T1 Item1 { get; }

	/// <summary>
	/// 第二个元素
	/// </summary>
	public T2 Item2 { get; }

	/// <summary>
	/// 第三个元素
	/// </summary>
	public T3 Item3 { get; }

	/// <summary>
	/// 第四个元素
	/// </summary>
	public T4 Item4 { get; }

	/// <inheritdoc />
	public int Size => 4;

	/// <summary>
	/// 构造
	/// </summary>
	public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
	}

	/// <inheritdoc />
	public object?[] ToArray() => new object?[] { Item1, Item2, Item3, Item4 };

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is not Tuple<T1, T2, T3, T4> other)
			return false;
		return Equals(Item1, other.Item1) && Equals(Item2, other.Item2) &&
			   Equals(Item3, other.Item3) && Equals(Item4, other.Item4);
	}

	/// <inheritdoc />
	public override int GetHashCode() => HashCode.Combine(Item1, Item2, Item3, Item4);

	/// <inheritdoc />
	public override string? ToString() => $"({Item1}, {Item2}, {Item3}, {Item4})";

	/// <summary>
	/// 解构
	/// </summary>
	public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
	{
		item1 = Item1;
		item2 = Item2;
		item3 = Item3;
		item4 = Item4;
	}
}

/// <summary>
/// 元组静态工厂
/// </summary>
public static class Tuple
{
	/// <summary>
	/// 创建二元组
	/// </summary>
	public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) => new(item1, item2);

	/// <summary>
	/// 创建三元组
	/// </summary>
	public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) => new(item1, item2, item3);

	/// <summary>
	/// 创建四元组
	/// </summary>
	public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) => new(item1, item2, item3, item4);
}
