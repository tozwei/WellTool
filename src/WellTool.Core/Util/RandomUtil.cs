using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// RandomUtil随机工具类
/// </summary>
public static class RandomUtil
{
	private static readonly Random _random = new Random();

	/// <summary>
	/// 获取随机数生成器
	/// </summary>
	public static Random GetRandom() => _random;

	/// <summary>
	/// 随机整数
	/// </summary>
	public static int NextInt() => _random.Next();

	/// <summary>
	/// 随机整数（0到max之间）
	/// </summary>
	public static int NextInt(int max) => _random.Next(max);

	/// <summary>
	/// 随机整数（min到max之间）
	/// </summary>
	public static int NextInt(int min, int max) => _random.Next(min, max);

	/// <summary>
	/// 随机long
	/// </summary>
	public static long NextLong()
	{
		var bytes = new byte[8];
		_random.NextBytes(bytes);
		return BitConverter.ToInt64(bytes, 0);
	}

	/// <summary>
	/// 随机double
	/// </summary>
	public static double NextDouble() => _random.NextDouble();

	/// <summary>
	/// 随机bool
	/// </summary>
	public static bool NextBoolean() => _random.Next(2) == 1;

	/// <summary>
	/// 随机字节数组
	/// </summary>
	public static byte[] NextBytes(int length)
	{
		var bytes = new byte[length];
		_random.NextBytes(bytes);
		return bytes;
	}

	/// <summary>
	/// 随机字母数字
	/// </summary>
	public static string RandomString(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var result = new char[length];
		for (int i = 0; i < length; i++)
		{
			result[i] = chars[_random.Next(chars.Length)];
		}
		return new string(result);
	}

	/// <summary>
	/// 随机数字
	/// </summary>
	public static string RandomNumbers(int length)
	{
		const string chars = "0123456789";
		var result = new char[length];
		for (int i = 0; i < length; i++)
		{
			result[i] = chars[_random.Next(chars.Length)];
		}
		return new string(result);
	}

	/// <summary>
	/// 随机字母
	/// </summary>
	public static string RandomLetters(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		var result = new char[length];
		for (int i = 0; i < length; i++)
		{
			result[i] = chars[_random.Next(chars.Length)];
		}
		return new string(result);
	}

	/// <summary>
	/// 随机整数
	/// </summary>
	public static int RandomInt() => NextInt();

	/// <summary>
	/// 随机整数（0到max之间）
	/// </summary>
	public static int RandomInt(int max) => NextInt(max);

	/// <summary>
	/// 随机整数（min到max之间）
	/// </summary>
	public static int RandomInt(int min, int max) => NextInt(min, max);

	/// <summary>
	/// 随机long
	/// </summary>
	public static long RandomLong() => NextLong();

	/// <summary>
	/// 随机long（min到max之间）
	/// </summary>
	public static long RandomLong(long min, long max)
	{
		return min + System.Math.Abs(NextLong()) % (max - min + 1);
	}

	/// <summary>
	/// 随机double
	/// </summary>
	public static double RandomDouble() => NextDouble();

	/// <summary>
	/// 随机double（min到max之间）
	/// </summary>
	public static double RandomDouble(double min, double max)
	{
		return min + NextDouble() * (max - min);
	}

	/// <summary>
	/// 随机字节数组
	/// </summary>
	public static byte[] RandomBytes(int length) => NextBytes(length);

	/// <summary>
	/// 随机获取集合中的元素
	/// </summary>
	public static T RandomEle<T>(IList<T> list)
	{
		if (list == null || list.Count == 0)
		{
			return default;
		}
		return list[_random.Next(list.Count)];
	}

	/// <summary>
	/// 随机获取集合中的多个元素（去重）
	/// </summary>
	public static IList<T> RandomEleSet<T>(IList<T> list, int count)
	{
		if (list == null || list.Count == 0 || count <= 0)
		{
			return new List<T>();
		}
		if (count >= list.Count)
		{
			return new List<T>(list);
		}
		var result = new List<T>();
		var indexes = new HashSet<int>();
		while (indexes.Count < count)
		{
			indexes.Add(_random.Next(list.Count));
		}
		foreach (var index in indexes)
		{
			result.Add(list[index]);
		}
		return result;
	}

	/// <summary>
	/// 生成随机UUID
	/// </summary>
	public static string RandomUUID()
	{
		return Guid.NewGuid().ToString();
	}

	/// <summary>
	/// 获取安全随机数生成器
	/// </summary>
	public static Random SecureRandom()
	{
		return new Random(Guid.NewGuid().GetHashCode());
	}
}
