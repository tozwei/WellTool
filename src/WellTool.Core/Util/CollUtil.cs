using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

	/// <summary>
	/// 分页获取集合元素
	/// </summary>
	public static List<T> Page<T>(IList<T> list, int pageIndex, int pageSize)
	{
		if (list == null || list.Count == 0 || pageIndex < 0 || pageSize <= 0)
		{
			return new List<T>();
		}
		var start = pageIndex * pageSize;
		if (start >= list.Count)
		{
			return new List<T>();
		}
		var end = System.Math.Min(start + pageSize, list.Count);
		var result = new List<T>(end - start);
		for (int i = start; i < end; i++)
		{
			result.Add(list[i]);
		}
		return result;
	}

	/// <summary>
	/// 比较两个集合是否相等
	/// </summary>
	public static bool IsEqual<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		if (ReferenceEquals(collection1, collection2))
		{
			return true;
		}
		if (collection1 == null || collection2 == null)
		{
			return false;
		}
		return collection1.SequenceEqual(collection2);
	}

	/// <summary>
	/// 检查集合1是否是集合2的子集
	/// </summary>
	public static bool IsSub<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		if (collection1 == null)
		{
			return true;
		}
		if (collection2 == null)
		{
			return false;
		}
		var set = new HashSet<T>(collection2);
		return collection1.All(set.Contains);
	}

	/// <summary>
	/// 过滤集合
	/// </summary>
	public static List<T> Filter<T>(IEnumerable<T> collection, Func<T, bool> predicate)
	{
		if (collection == null || predicate == null)
		{
			return new List<T>();
		}
		return collection.Where(predicate).ToList();
	}

	/// <summary>
	/// 映射集合
	/// </summary>
	public static List<TResult> Map<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, TResult> selector)
	{
		if (collection == null || selector == null)
		{
			return new List<TResult>();
		}
		return collection.Select(selector).ToList();
	}

	/// <summary>
	/// 根据字段排序
	/// </summary>
	public static List<T> SortByField<T>(IList<T> list, string fieldName, bool isAsc = true)
	{
		if (list == null || list.Count <= 1 || string.IsNullOrEmpty(fieldName))
		{
			return new List<T>(list);
		}
		var property = typeof(T).GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
		if (property == null)
		{
			return new List<T>(list);
		}
		return list.OrderBy(item => property.GetValue(item)).ToList();
	}

	/// <summary>
	/// 将集合转换为数组
	/// </summary>
	public static T[] ToArray<T>(IEnumerable<T> collection)
	{
		if (collection == null)
		{
			return Array.Empty<T>();
		}
		return collection.ToArray();
	}

	/// <summary>
	/// 将集合转换为数组
	/// </summary>
	public static T[] ToArray<T>(IEnumerable<T> collection, int length)
	{
		var array = ToArray(collection);
		if (array.Length >= length)
		{
			return array;
		}
		var result = new T[length];
		Array.Copy(array, result, array.Length);
		return result;
	}

	/// <summary>
	/// 创建新的数组集合
	/// </summary>
	public static HashSet<T> NewArraySet<T>(params T[] items)
	{
		return new HashSet<T>(items);
	}

	/// <summary>
	/// 移除最后一个元素
	/// </summary>
	public static void RemoveLast<T>(IList<T> list)
	{
		if (list != null && list.Count > 0)
		{
			list.RemoveAt(list.Count - 1);
		}
	}

	/// <summary>
	/// 获取两个集合的交集
	/// </summary>
	public static List<T> GetIntersection<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		if (collection1 == null || collection2 == null)
		{
			return new List<T>();
		}
		var set = new HashSet<T>(collection2);
		return collection1.Where(set.Contains).ToList();
	}

	/// <summary>
	/// 获取两个集合的并集
	/// </summary>
	public static List<T> GetUnion<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		var result = new HashSet<T>();
		if (collection1 != null)
		{
			foreach (var item in collection1)
			{
				result.Add(item);
			}
		}
		if (collection2 != null)
		{
			foreach (var item in collection2)
			{
				result.Add(item);
			}
		}
		return result.ToList();
	}

	/// <summary>
	/// 获取两个集合的差集
	/// </summary>
	public static List<T> GetDisjunction<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		if (collection1 == null && collection2 == null)
		{
			return new List<T>();
		}
		if (collection1 == null)
		{
			return collection2.ToList();
		}
		if (collection2 == null)
		{
			return collection1.ToList();
		}
		var set1 = new HashSet<T>(collection1);
		var set2 = new HashSet<T>(collection2);
		
		// 对称差集：collection1 中有但 collection2 中没有的 + collection2 中有但 collection1 中没有的
		var result = set1.Where(item => !set2.Contains(item)).ToList();
		result.AddRange(set2.Where(item => !set1.Contains(item)));
		return result;
	}

	/// <summary>
	/// 扁平化映射
	/// </summary>
	public static List<TResult> FlatMap<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, IEnumerable<TResult>> selector)
	{
		if (collection == null || selector == null)
		{
			return new List<TResult>();
		}
		var result = new List<TResult>();
		foreach (var item in collection)
		{
			var mapped = selector(item);
			if (mapped != null)
			{
				result.AddRange(mapped);
			}
		}
		return result;
	}

	/// <summary>
	/// 获取两个集合的差集
	/// </summary>
	public static List<T> Disjunction<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
	{
		return GetDisjunction(collection1, collection2);
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
