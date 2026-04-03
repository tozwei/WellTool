using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang;

/// <summary>
/// 范围接口
/// </summary>
/// <typeparam name="T">范围元素类型</typeparam>
public interface IRange<T>
{
	/// <summary>
	/// 获取起始元素
	/// </summary>
	T Start { get; }

	/// <summary>
	/// 获取结束元素
	/// </summary>
	T End { get; }

	/// <summary>
	/// 是否包含边界
	/// </summary>
	bool Inclusive { get; }

	/// <summary>
	/// 检查元素是否在范围内
	/// </summary>
	/// <param name="element">元素</param>
	/// <returns>是否在范围内</returns>
	bool Contains(T element);

	/// <summary>
	/// 获取范围内的元素
	/// </summary>
	/// <returns>元素枚举</returns>
	IEnumerable<T> Elements();

	/// <summary>
	/// 获取范围大小
	/// </summary>
	/// <returns>元素数量</returns>
	int Size();
}

/// <summary>
/// 范围类
/// </summary>
/// <typeparam name="T">范围元素类型</typeparam>
public class Range<T> : IRange<T> where T : IComparable<T>
{
	/// <summary>
	/// 起始元素
	/// </summary>
	public T Start { get; }

	/// <summary>
	/// 结束元素
	/// </summary>
	public T End { get; }

	/// <summary>
	/// 是否包含边界
	/// </summary>
	public bool Inclusive { get; }

	private readonly Func<T, T, int> _step;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="start">起始元素</param>
	/// <param name="end">结束元素</param>
	/// <param name="step">步进函数</param>
	/// <param name="inclusive">是否包含边界</param>
	public Range(T start, T end, Func<T, T, int> step, bool inclusive = true)
	{
		Start = start;
		End = end;
		_step = step;
		Inclusive = inclusive;
	}

	/// <summary>
	/// 检查元素是否在范围内
	/// </summary>
	public bool Contains(T element)
	{
		int startCompare = element.CompareTo(Start);
		int endCompare = element.CompareTo(End);

		if (Inclusive)
		{
			return startCompare >= 0 && endCompare <= 0;
		}
		return startCompare > 0 && endCompare < 0;
	}

	/// <summary>
	/// 获取范围内的元素
	/// </summary>
	public IEnumerable<T> Elements()
	{
		var current = Start;
		int compare = current.CompareTo(End);

		if (compare == 0)
		{
			yield return current;
			yield break;
		}

		if (compare < 0)
		{
			while (current.CompareTo(End) < 0)
			{
				yield return current;
				current = Next(current);
			}
			if (Inclusive)
			{
				yield return End;
			}
		}
		else
		{
			while (current.CompareTo(End) > 0)
			{
				yield return current;
				current = Previous(current);
			}
			if (Inclusive)
			{
				yield return End;
			}
		}
	}

	/// <summary>
	/// 获取范围大小
	/// </summary>
	public int Size()
	{
		int count = 0;
		foreach (var element in Elements())
		{
			count++;
		}
		return count;
	}

	private T Next(T current)
	{
		if (current is int intVal && typeof(T) == typeof(int))
		{
			return (T)(object)(intVal + 1);
		}
		if (current is long longVal && typeof(T) == typeof(long))
		{
			return (T)(object)(longVal + 1);
		}
		return current;
	}

	private T Previous(T current)
	{
		if (current is int intVal && typeof(T) == typeof(int))
		{
			return (T)(object)(intVal - 1);
		}
		if (current is long longVal && typeof(T) == typeof(long))
		{
			return (T)(object)(longVal - 1);
		}
		return current;
	}
}
