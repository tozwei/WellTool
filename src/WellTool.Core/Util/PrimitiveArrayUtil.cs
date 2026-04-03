using System;

namespace WellTool.Core.Util;

/// <summary>
/// 原始数组工具类
/// </summary>
public static class PrimitiveArrayUtil
{
	/// <summary>
	/// 将int数组转换为Integer列表
	/// </summary>
	/// <param name="array">int数组</param>
	/// <returns>Integer列表</returns>
	public static int[] ToIntArray(params int[] array)
	{
		return array;
	}

	/// <summary>
	/// 将long数组转换为Long列表
	/// </summary>
	/// <param name="array">long数组</param>
	/// <returns>Long列表</returns>
	public static long[] ToLongArray(params long[] array)
	{
		return array;
	}

	/// <summary>
	/// 将double数组转换为Double列表
	/// </summary>
	/// <param name="array">double数组</param>
	/// <returns>Double列表</returns>
	public static double[] ToDoubleArray(params double[] array)
	{
		return array;
	}

	/// <summary>
	/// 将float数组转换为Float列表
	/// </summary>
	/// <param name="array">float数组</param>
	/// <returns>Float列表</returns>
	public static float[] ToFloatArray(params float[] array)
	{
		return array;
	}

	/// <summary>
	/// 将short数组转换为Short列表
	/// </summary>
	/// <param name="array">short数组</param>
	/// <returns>Short列表</returns>
	public static short[] ToShortArray(params short[] array)
	{
		return array;
	}

	/// <summary>
	/// 将byte数组转换为Byte列表
	/// </summary>
	/// <param name="array">byte数组</param>
	/// <returns>Byte列表</returns>
	public static byte[] ToByteArray(params byte[] array)
	{
		return array;
	}

	/// <summary>
	/// 将char数组转换为Char列表
	/// </summary>
	/// <param name="array">char数组</param>
	/// <returns>Char列表</returns>
	public static char[] ToCharArray(params char[] array)
	{
		return array;
	}

	/// <summary>
	/// 将boolean数组转换为Boolean列表
	/// </summary>
	/// <param name="array">boolean数组</param>
	/// <returns>Boolean列表</returns>
	public static bool[] ToBooleanArray(params bool[] array)
	{
		return array;
	}

	/// <summary>
	/// 判断数组是否为空
	/// </summary>
	/// <param name="array">数组</param>
	/// <returns>是否为空</returns>
	public static bool IsEmpty(Array array)
	{
		return array == null || array.Length == 0;
	}

	/// <summary>
	/// 获取数组长度
	/// </summary>
	/// <param name="array">数组</param>
	/// <returns>长度</returns>
	public static int Length(Array array)
	{
		return array?.Length ?? 0;
	}
}
