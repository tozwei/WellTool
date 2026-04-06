using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// CollUtil集合工具类
/// </summary>
public static class CollUtil
{
	/// <summary>
	/// 是否为空
	/// </summary>
	public static bool IsEmpty<T>(ICollection<T> collection) => collection == null || collection.Count == 0;

	/// <summary>
	/// 是否不为空
	/// </summary>
	public static bool IsNotEmpty<T>(ICollection<T> collection) => !IsEmpty(collection);

	/// <summary>
	/// 获取或默认
	/// </summary>
	public static T GetOrDefault<T>(IList<T> list, int index, T defaultValue = default)
	{
		if (list != null && index >= 0 && index < list.Count)
			return list[index];
		return defaultValue;
	}

	/// <summary>
	/// 创建列表
	/// </summary>
	public static List<T> ListOf<T>(params T[] items)
	{
		var list = new List<T>(items.Length);
		foreach (var item in items)
		{
			list.Add(item);
		}
		return list;
	}

	/// <summary>
	/// 创建哈希集
	/// </summary>
	public static HashSet<T> HashSetOf<T>(params T[] items)
	{
		var set = new HashSet<T>();
		foreach (var item in items)
		{
			set.Add(item);
		}
		return set;
	}

	/// <summary>
	/// 创建数组列表
	/// </summary>
	public static ArrayList<T> ArrayListOf<T>(params T[] items)
	{
		return new ArrayList<T>(items);
	}

	/// <summary>
	/// 创建新的数组列表
	/// </summary>
	public static ArrayList<T> NewArrayList<T>(params T[] items)
	{
		return new ArrayList<T>(items);
	}

	/// <summary>
	/// 对集合进行分区
	/// </summary>
	/// <param name="collection">集合</param>
	/// <param name="size">分区大小</param>
	/// <returns>分区后的列表</returns>
	public static List<List<T>> Partition<T>(IEnumerable<T> collection, int size)
	{
		var result = new List<List<T>>();
		if (collection == null || size <= 0)
		{
			return result;
		}
		var current = new List<T>();
		foreach (var item in collection)
		{
			current.Add(item);
			if (current.Count >= size)
			{
				result.Add(current);
				current = new List<T>();
			}
		}
		if (current.Count > 0)
		{
			result.Add(current);
		}
		return result;
	}
}

/// <summary>
/// 简单ArrayList实现
/// </summary>
public class ArrayList<T> : List<T>
{
	/// <summary>
	/// 构造
	/// </summary>
	public ArrayList()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	public ArrayList(IEnumerable<T> collection) : base(collection)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="capacity">初始容量</param>
	public ArrayList(int capacity) : base(capacity)
	{
	}
}
