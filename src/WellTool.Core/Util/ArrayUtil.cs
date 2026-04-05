using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// ArrayUtil数组工具类
/// </summary>
public static class ArrayUtil
{
	/// <summary>
	/// 是否为空
	/// </summary>
	public static bool IsEmpty(Array array) => array == null || array.Length == 0;

	/// <summary>
	/// 是否不为空
	/// </summary>
	public static bool IsNotEmpty(Array array) => !IsEmpty(array);

	/// <summary>
	/// 长度
	/// </summary>
	public static int Length(Array array) => array?.Length ?? 0;

	/// <summary>
	/// 获取元素
	/// </summary>
	public static T Get<T>(T[] array, int index)
	{
		if (array == null || index < 0 || index >= array.Length)
			return default;
		return array[index];
	}

	/// <summary>
	/// 设置元素
	/// </summary>
	public static void Set<T>(T[] array, int index, T value)
	{
		if (array == null || index < 0 || index >= array.Length)
			return;
		array[index] = value;
	}

	/// <summary>
	/// 转换为列表
	/// </summary>
	public static List<T> ToList<T>(T[] array)
	{
		if (array == null)
			return new List<T>();
		return new List<T>(array);
	}

	/// <summary>
	/// 数组是否包含元素
	/// </summary>
	public static bool Contains<T>(T[] array, T item)
	{
		if (array == null)
			return false;
		foreach (var element in array)
		{
			if (Equals(element, item))
				return true;
		}
		return false;
	}

	/// <summary>
	/// 创建数组
	/// </summary>
	public static T[] NewArray<T>(int length, T defaultValue = default)
	{
		var array = new T[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = defaultValue;
		}
		return array;
	}

	/// <summary>
	/// 创建二维数组
	/// </summary>
	public static T[][] NewTwoDimensionalArray<T>(int rows, int cols, T defaultValue = default)
	{
		var array = new T[rows][];
		for (int i = 0; i < rows; i++)
		{
			array[i] = NewArray(cols, defaultValue);
		}
		return array;
	}

	/// <summary>
	/// 截取数组的子数组
	/// </summary>
	/// <typeparam name="T">数组元素类型</typeparam>
	/// <param name="array">数组</param>
	/// <param name="start">起始索引</param>
	/// <param name="end">结束索引</param>
	/// <param name="step">步长</param>
	/// <returns>子数组</returns>
	public static T[] Sub<T>(T[] array, int start, int end, int step = 1)
	{
		if (array == null)
		{
			return Array.Empty<T>();
		}
		if (start < 0) start = 0;
		if (end > array.Length) end = array.Length;
		if (start >= end || step <= 0)
		{
			return Array.Empty<T>();
		}
		var list = new List<T>();
		for (int i = start; i < end; i += step)
		{
			list.Add(array[i]);
		}
		return list.ToArray();
	}
}
