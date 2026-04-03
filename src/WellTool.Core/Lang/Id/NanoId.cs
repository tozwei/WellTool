using System;
using System.Security.Cryptography;

namespace WellTool.Core.Lang.Id;

/// <summary>
/// NanoId生成器
/// </summary>
public static class NanoId
{
	private const string DEFAULT_ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";
	private static readonly Random _random = new Random();

	/// <summary>
	/// 生成NanoId
	/// </summary>
	/// <param name="size">ID长度</param>
	/// <returns>NanoId</returns>
	public static string Generate(int size = 21)
	{
		return Generate(size, DEFAULT_ALPHABET);
	}

	/// <summary>
	/// 生成NanoId
	/// </summary>
	/// <param name="size">ID长度</param>
	/// <param name="alphabet">字符集</param>
	/// <returns>NanoId</returns>
	public static string Generate(int size, string alphabet)
	{
		if (string.IsNullOrEmpty(alphabet))
		{
			throw new ArgumentException("Alphabet cannot be null or empty");
		}
		if (size <= 0)
		{
			throw new ArgumentException("Size must be greater than 0");
		}

		var alphabetLength = alphabet.Length;
		var result = new char[size];
		var bytes = new byte[size];
		_random.NextBytes(bytes);

		for (int i = 0; i < size; i++)
		{
			result[i] = alphabet[bytes[i] % alphabetLength];
		}

		return new string(result);
	}

	/// <summary>
	/// 生成安全的NanoId
	/// </summary>
	/// <param name="size">ID长度</param>
	/// <returns>NanoId</returns>
	public static string GenerateSecure(int size = 21)
	{
		return GenerateSecure(size, DEFAULT_ALPHABET);
	}

	/// <summary>
	/// 生成安全的NanoId
	/// </summary>
	/// <param name="size">ID长度</param>
	/// <param name="alphabet">字符集</param>
	/// <returns>NanoId</returns>
	public static string GenerateSecure(int size, string alphabet)
	{
		if (string.IsNullOrEmpty(alphabet))
		{
			throw new ArgumentException("Alphabet cannot be null or empty");
		}
		if (size <= 0)
		{
			throw new ArgumentException("Size must be greater than 0");
		}

		var alphabetLength = alphabet.Length;
		var result = new char[size];
		var bytes = new byte[size];
		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(bytes);
		}

		for (int i = 0; i < size; i++)
		{
			result[i] = alphabet[bytes[i] % alphabetLength];
		}

		return new string(result);
	}
}
